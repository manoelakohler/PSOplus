using System;
using System.Linq;
using System.Threading;
using Octopus.Database.Entities;
using Octopus.Engine.Exceptions;
using Octopus.Engine.Geometry.Entities;
using Octopus.Engine.Services.GridServices.Interfaces;
using Octopus.Engine.Services.Intersections;
using Octopus.Engine.Services.Intersections.GeometricAndBusinessRules;
using Octopus.Engine.Services.Intersections.GeometricAndBusinessRules.ApplicableRules;
using Octopus.Engine.Utils;
using Octopus.Log;
using Octopus.Utils.RandomUtils;

namespace Octopus.Engine.Services.RandomGenerator
{
    public class GeometricWellSampler : ISampler<GeometryWell[]>
    {
        private readonly IGrid _grid;
        private readonly PersistedBlock _depthReferenceBlock;
        private readonly IIntersectionManager _manager;
        private readonly GeometryWell[] _userDefinedWells;

        /// <summary>
        /// Número de poços a serem gerados, além daqueles fornecidos.
        /// </summary>
        private readonly int _numberOfWellsToBuild;

        private readonly bool _lockI;
        private readonly bool _lockJ;
        private readonly bool _lockK;
        private readonly IRandom _random;
        private readonly IApplicableRules _noWellRegionRules;
        private readonly double _porousVolumeThreshold;
        private readonly double _minimumDistanceBetweenWells;
        private readonly double _maximumWellLengh;
        private readonly double _depthWaterOilContact;
        private readonly IApplicableRules _waterOilContactRules;

        private readonly int _maximumAdjacentDeltaI;
        private readonly int _maximumAdjacentDeltaJ;
        private readonly int _maximumAdjacentDeltaK;

        private int TotalLength
        {
            get { return _numberOfWellsToBuild + _userDefinedWells.Length; }
        }

        public GeometricWellSampler(IGrid grid, PersistedBlock depthReferenceBlock, IIntersectionManager manager,
            GeometryWell[] userDefinedWells, int numberOfWells, bool lockI, bool lockJ, bool lockK, IRandom random,
            IApplicableRules noWellRegionRules, double porousVolumeThreshold, double minimumDistanceBetweenWells,
            double maximumWellWellLengh, double depthWaterOilContact, ProducerWellSpecificRules waterOilContactRules = null)
        {
            _grid = grid;
            _depthReferenceBlock = depthReferenceBlock;
            _manager = manager;
            _userDefinedWells = userDefinedWells;
            _numberOfWellsToBuild = numberOfWells;
            _lockI = lockI;
            _lockJ = lockJ;
            _lockK = lockK;
            _random = random;
            _noWellRegionRules = noWellRegionRules;
            _porousVolumeThreshold = porousVolumeThreshold;
            _minimumDistanceBetweenWells = minimumDistanceBetweenWells;
            _maximumWellLengh = maximumWellWellLengh;
            _depthWaterOilContact = depthWaterOilContact;
            _waterOilContactRules = waterOilContactRules;
            _maximumAdjacentDeltaI = GetMaximumAdjacentDelta(_grid.GridNi);
            _maximumAdjacentDeltaJ = GetMaximumAdjacentDelta(_grid.GridNj);
            _maximumAdjacentDeltaK = GetMaximumAdjacentDelta(_grid.GridNk);
        }

        private int GetMaximumAdjacentDelta(int gridMaximum)
        {
            return Math.Min(2, gridMaximum % 2 == 0 ? gridMaximum / 2 : (gridMaximum - 1) / 2);
        }

       
        private int SampleCoordinate(int mininumInclusive, int maximumInclusive, int gridMinimum, 
            int gridMaximum)
        {
            /* 
             * A nova amostra deve ser uma conversão entre a série de sorteio para a série espelhada.
             *       
             * xl = | |x_ls|+min     se x_ls < min
             *      | x_ls-[e+(e-1)] se x_ls > max 
             *       
             * e = x_ls-max
             * 
             */

            if (mininumInclusive < gridMinimum && maximumInclusive > gridMaximum)
                throw MetropolisHastingsExceptionGenerator.NewCoordinateUnexistent();
            
            // O _random é uma implementação específica em que o max é inclusive.
            var newCoordinate = _random.NextSuperiorLimitInclusive(mininumInclusive, maximumInclusive); 

            if (newCoordinate < gridMinimum)
                return Math.Abs(newCoordinate) + gridMinimum;
            if (newCoordinate > gridMaximum)
            {
                var e = newCoordinate - gridMaximum;
                return newCoordinate -2*e + 1;
            }

            return newCoordinate;
        }

        private DiscretePoint3D[] SampleRandomBlocks(int minI, int minJ, int minK, int maxI, int maxJ,
            int maxK, int count, CancellationToken token, int? noWellRegionId)
        {
            DiscretePoint3D[] output;
            if (count >= 5)
            {
                output = _grid.BlocksManager.SampleBlocks(count, _porousVolumeThreshold, _random, token, noWellRegionId, _depthReferenceBlock);
            }
            else
            {
                output = new DiscretePoint3D[count];
                for (var index = 0; index < output.Length; index++)
                {
                    var i = SampleCoordinate(minI, maxI, _grid.StartI, _grid.EndI);
                    var j = SampleCoordinate(minJ, maxJ, _grid.StartJ, _grid.EndJ);
                    var k = SampleCoordinate(minK, maxK, _grid.StartK, _grid.EndK);
                    output[index] = new DiscretePoint3D(i, j, k);
                }
            }
            return output;
        }

        private static int GetUpBound(int coordinate, int maxValue)
        {
            return Math.Min(coordinate, maxValue);
        }

        private static int GetLowBound(int coordinate, int minValue)
        {
            return Math.Max(coordinate, minValue);
        }

        /// <summary>
        /// Gera as coordenadas inicial e final de
        /// forma arbitrária (poços inválidos podem ser gerados).
        /// Para garantir que o critério do comprimento máximo é satisfeito,
        /// o método gera sempre poços pontuais.
        /// </summary>
        public GeometryWell[] Sample(CancellationToken token, string studyName, string reservoirName, int reservoirIdOnStudy, int? noWellRegionId)
        {
            var output = GetGeometryWellsInstance();
            var coordinates = SampleRandomBlocks(_grid.StartI, _grid.StartJ,
                _grid.StartK, _grid.EndI, _grid.EndJ, _grid.EndK, _numberOfWellsToBuild*4, token, noWellRegionId);
            if (coordinates.Length < _numberOfWellsToBuild)
            {
                throw new InvalidOperationException(string.Format(
                    "Não há {0} blocos válidos no grid. " +
                    "Um bloco é definido como válido quando não está marcado como pinch-out ou null e, " +
                    "além disso, tem porosidade P tal que P*VolumeDoBloco >= Limiar de volume poroso.", 
                    _numberOfWellsToBuild));
            }
            var countOfWells = 0;
            var coordinatesIndex = 0;
            while (coordinatesIndex < coordinates.Length && countOfWells < _numberOfWellsToBuild)
            {
                token.ThrowIfCancellationRequested();
                var firstPoint = coordinates[coordinatesIndex];
                var secondPoint = firstPoint;
                output[countOfWells] = _manager.BuildGeometryWell(
                    firstPoint, secondPoint, studyName,  reservoirName, reservoirIdOnStudy, false);
                if (IsBlockValid(output[countOfWells]))
                {
                    countOfWells ++; // ok, os blocos são válidos.
                }
                coordinatesIndex = coordinatesIndex + 1;
            }
            if (countOfWells == 0)
            {
                throw new InvalidOperationException(
                    "Não foi possível gerar nenhum poço posicionado em blocos válidos." +
                                                    "Um bloco é definido como válido quando não está marcado como pinch-out ou null e, " +
                                                    "além disso, tem porosidade não-nula.");
            }
            if (countOfWells < _numberOfWellsToBuild)
            {
                // replica o primeiro poço
                token.ThrowIfCancellationRequested();
                var firstWellStart = output[0].Start;
                var firstWellEnd = output[0].End;
                while (countOfWells < _numberOfWellsToBuild)
                {
                    output[countOfWells] = _manager.BuildGeometryWell(firstWellStart, firstWellEnd, studyName, reservoirName, reservoirIdOnStudy, false);
                    countOfWells = countOfWells + 1;
                }
            }
            return output;
        }

        /// <summary>
        /// Gera as coordenadas inicial e final iguais à
        /// semente dada, que espera-se que sejam válidos.
        /// </summary>
        public GeometryWell[] Sample(CancellationToken token, string studyName, string reservoirName, int reservoirIdOnStudy, int? noWellRegionId, GeometryWell[] initialSeed )
        {
            LogManager.LogDebugFileOnly("Replicando a semente inicial como semente no GeometricWellSample.");

            var output = GetGeometryWellsInstance();

            for (var geometryWellIndex = 0; geometryWellIndex < _numberOfWellsToBuild; geometryWellIndex++)
            {
                output[geometryWellIndex] = initialSeed[geometryWellIndex];
            }

            return output;
        }

        private bool IsBlockValid(GeometryWell geometryWell)
        {
            return _noWellRegionRules.Validate(geometryWell);
        }

        private GeometryWell[] GetGeometryWellsInstance()
        {
            var output = new GeometryWell[_numberOfWellsToBuild + _userDefinedWells.Length];
            Array.Copy(
                _userDefinedWells, /*source*/
                0, /*source index*/
                output, /*destination*/
                _numberOfWellsToBuild, /*destination start index*/
                _userDefinedWells.Length /*quantidade a copiar*/
                );
            return output;
        }


        public GeometryWell[] Sample(GeometryWell[] sample, CancellationToken token, string studyName, string reservoirName, int reservoirIdOnStudy, int? noWellRegionId)
        {
            if (sample.Length != TotalLength)
                throw new InvalidOperationException("O comprimento do argumento deve ser igual a " + TotalLength);

            // 1. sorteia o índice do poço que será modificado.
            var index = _random.NextSuperiorLimitExclusive(0, _numberOfWellsToBuild);
            var wellStart = sample[index].Start;
            var wellEnd = sample[index].End;

            // 2. sorteia 2 novas posições para o start e o end
            var newStart = SampleRandomBlocks(
                wellStart.I - _maximumAdjacentDeltaI,
                wellStart.J - _maximumAdjacentDeltaJ,
                wellStart.K - _maximumAdjacentDeltaK,
                wellStart.I + _maximumAdjacentDeltaI,
                wellStart.J + _maximumAdjacentDeltaJ,
                wellStart.K + _maximumAdjacentDeltaK,
                1, token, noWellRegionId
                );
            var newEnd = SampleRandomBlocks(
                (!_lockI) ? wellEnd.I - _maximumAdjacentDeltaI : newStart[0].I,
                (!_lockJ) ? wellEnd.J - _maximumAdjacentDeltaJ : newStart[0].J,
                (!_lockK) ? wellEnd.K - _maximumAdjacentDeltaK : newStart[0].K,
                (!_lockI) ? wellEnd.I + _maximumAdjacentDeltaI : newStart[0].I,
                (!_lockJ) ? wellEnd.J + _maximumAdjacentDeltaJ : newStart[0].J,
                (!_lockK) ? wellEnd.K + _maximumAdjacentDeltaK : newStart[0].K,
                1, token, noWellRegionId
                );

            // garante que o poço não está de cabeça para baixo
            /*if (MathUtils.IsFirstPointAboveOrSameLevelOfSecond(newEnd[0], newStart[0], _grid.IsZDirectionDown))
            {
                // inverte
                var temp = newEnd[0];
                newEnd[0] = newStart[0];
                newStart[0] = temp;
            }*/

            // newEnd está sempre abaixo de newStart.
            var newWell = _manager.BuildGeometryWellWithoutIntersections(newStart[0], newEnd[0]
                , studyName, reservoirName, reservoirIdOnStudy); //não calcula interseções!

            // 3. gera um novo array, copiando todos os poços do array "sample", exceto sample[index].
            var output = new GeometryWell[sample.Length];
            for (var i = 0; i < sample.Length; i++)
            {
                if (i != index)
                    output[i] = sample[i];
                else
                    output[i] = newWell;
            }
            return output;
        }

        public double GetLikelihood(GeometryWell[] sample , CancellationToken token)
        {
            var distanceLikelihood = ComputeDistanceLikelihood(sample);
            token.ThrowIfCancellationRequested();
            var lengthLikelihood = ComputeWellLengthLikelihood(sample);
            token.ThrowIfCancellationRequested();
            var blocksLikelihood = ComputeBlocksLikelihood(sample);
            token.ThrowIfCancellationRequested();
            var waterOilContactLikelihood = ComputeWaterOilContactLikelihood(sample);
            token.ThrowIfCancellationRequested();

            var finalLikelihoon = distanceLikelihood * lengthLikelihood * blocksLikelihood * waterOilContactLikelihood;
            return finalLikelihoon;
        }

        /// <summary>
        /// Calcula a parte da Verossimilhança que verifica se o poço está abaixo da linha 
        /// de DWOC. Caso esteja abaixo da linha de DWOC a amostra é considerada ruim e 
        /// é dado um valor de verossimilhança para ela ela próxima a 0. Caso esteja acima,
        /// esse valor tende a 1.
        /// </summary>
        private double ComputeWaterOilContactLikelihood(GeometryWell[] sample)
        {
            var index = 1.0;

            if (_waterOilContactRules == null)
                return index;

            for (var i = 0; i < _numberOfWellsToBuild; i++)
            {
                var geometryWell = sample[i];
                var waterOilContactRule = _waterOilContactRules.WellRules.First() as WaterOilContactRule;
                var wellLastCompletionDepth = waterOilContactRule.GetWellDepth(geometryWell.Body.Start.Z, geometryWell.Body.End.Z);

                var x = wellLastCompletionDepth - _depthWaterOilContact;
                var f = (-Math.Atan(x) + (Math.PI / 2)) / (-Math.Atan(-_depthWaterOilContact) + (Math.PI / 2));
                index *= f;
            }
            return index;
        }

        private double ComputeBlocksLikelihood(GeometryWell[] sample)
        {
            var index = 1.0;
            const double reductionFactor = 1.0e-4;
            for (var i = 0; i < _numberOfWellsToBuild; i++)
            {
                var geometryWell = sample[i];

                var firstBlock = geometryWell.StartBlock;
                if (IsBlockNull(firstBlock)) index *= reductionFactor;

                var lastBlock = geometryWell.EndBlock;
                if (IsBlockNull(lastBlock)) index *= reductionFactor;
            }
            return index;
        }

        private bool IsBlockNull(Block block)
        {
            return block.IsNull || IsBlockNullByPorousThreshold(block);
        }

        private bool IsBlockNullByPorousThreshold(Block block)
        {
            return block.PorosityVolume < _porousVolumeThreshold;
        }

        private double ComputeWellLengthLikelihood(GeometryWell[] sample)
        {
            /*
             * usa uma distribuição que valoriza comprimentos próximos do máximo.
             * f(l) = 1 / (1 + (l/Lmax-1)^2), se l<=Lmax.
             * f(l) = exp(-k*(l/lmax - 1)), se l>Lmax.
             * 
             * A função f tem a propriedade de ser integrável e quadrado-integrável,
             * além de ser contínua. Valores especiais:
             * 
             * a) f(0) = 0.5 (poço pontual)
             * b) f(1) = 1.0 (valor máximo - poço no comprimento máximo)
             * c) f(+inf) = 0.0 (quanto maior o poço acima do comprimento máximo, pior).
             */
            var index = 1.0;
            const double k = 10.0;
            for (var i = 0; i < _numberOfWellsToBuild /*não conta os poços somente leitura*/; i++)
            {
                var geometryWell = sample[i];
                var length = geometryWell.Length;
                var lMax = _maximumWellLengh;
                double f;
                var factor = length/lMax - 1;
                if (length <= lMax)
                {
                    f = 1/(1 + Math.Pow(factor,2));
                }
                else
                {
                    f = Math.Exp(-factor*k);
                }
                index *= f;
            }
            return index;
        }

        private bool IsReadOnlyWell(int wellIndex)
        {
            return wellIndex >= _numberOfWellsToBuild;
        }

        private double ComputeDistanceLikelihood(GeometryWell[] sample)
        {
            /*
             * deseja-se uma função de distribuição que valorize muito pouco abaixo de dmin e seja integrável.
             * x = d/dmin
             * f(d) = (A*tanh(k1*(x-1))+1)/u, se x<=5
             * f(d) = (exp(-x+(log(u)+5)))/u, se x>5
             * 
             * f(5) = 1.
             *
             * a = (u-1)/tanh(4*k1)
             * 
             * o máximo ocorre em x=5 e seu valor é 1.
             */
            const double u = 1.9;
            const double k1 = 10.0;
            var a = (u - 1)/Math.Tanh(4*k1);

            var index = 1.0;
            for (var firstIndex = 0; firstIndex < sample.Length; firstIndex++)
            {
                var firstWell = sample[firstIndex];
                for (var secondIndex = firstIndex + 1; secondIndex < sample.Length; secondIndex++)
                {
                    if (IsReadOnlyWell(firstIndex) && IsReadOnlyWell(secondIndex))
                        continue; /*comparação entre 2 poços fixos*/
                    var secondWell = sample[secondIndex];
                    var distance = LineSegment.GetMinimumDistance(firstWell.Body, secondWell.Body);

                    double f;
                    var x = distance/_minimumDistanceBetweenWells;
                    if (x <= 5)
                    {
                        f = (a * Math.Tanh(k1 * (x - 1)) + 1);
                    }
                    else
                    {
                        f = Math.Exp(-x + Math.Log(u) + 5);
                    }

                    f = f/u;

                    index *= f;
                }
            }
            return index;
        }

        public double GetLikelihood(GeometryWell[] sample, GeometryWell[] givenSample, CancellationToken token)
        {
            var totalLikelihood = 0.0;
            var wellIndicatorProbability = 1.0 / givenSample.Length;
            for(var i = 0; i< givenSample.Length;i++)
                totalLikelihood += GetWellConditionalProbability(sample[i], givenSample[i]) * wellIndicatorProbability;
            return totalLikelihood;
        }


        /// <summary>
        /// Calcula a probabilidade (ou verossimilhança) de gerar qualquer outro poço a
        /// partir deste (distribuição uniforme).
        /// </summary>
        private double GetWellConditionalProbability(GeometryWell sample, GeometryWell givenSample)
        {
            var startPointVariation = new CoordinateVariation(_grid, givenSample.Start);
            var endPointVariation = new CoordinateVariation(_grid, givenSample.End);

            var initialCoordinateProbability = new WellCoordinateProbability(_grid, sample.Start, givenSample.Start, startPointVariation);

            var finalCoordinateProbability = new WellCoordinateProbability(_grid, sample.End, givenSample.End, endPointVariation);

            var totalPossibilities = initialCoordinateProbability.I * initialCoordinateProbability.J * initialCoordinateProbability.K * finalCoordinateProbability.I * finalCoordinateProbability.J * finalCoordinateProbability.K;
            return totalPossibilities;
        }
    }
}