using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Octopus.Engine.Geometry.Entities;
using Octopus.Engine.Services.Intersections.GeometricAndBusinessRules;
using Octopus.Log;

namespace Octopus.Engine.Services.RandomGenerator
{
    public class WellsSamplerManager : IWellsSamplerManager
    {
        private readonly ISampleSequenceGenerator<GeometryWell[]> _sequenceGenerator;
        private readonly IApplicableRulesManager _applicableRulesManager;
        private readonly int _maximumNumberOfWells;
        private const int MaximumTries = 5000;
        private const int MinimumTries = 50;

        public WellsSamplerManager(ISampleSequenceGenerator<GeometryWell[]> sequenceGenerator, IApplicableRulesManager applicableRulesManager, int maximumNumberOfWells)
        {
            _sequenceGenerator = sequenceGenerator;
            _applicableRulesManager = applicableRulesManager;
            _maximumNumberOfWells = maximumNumberOfWells;
        }

        public IList<GeometryWell[]> GetRandomValidWells(int count, CancellationToken token, out int differentSamples, GeometryWell[] initialSamples = null)
        {
            var output = new List<GeometryWell[]>(count);
            const int maxRuns = 10;
            for (var run = 0; run < maxRuns; run++)
            {
                var tries = 0;
                var nextSampleCounter = 0;
                if (initialSamples == null || _maximumNumberOfWells > initialSamples.Count())
                    _sequenceGenerator.Reset(token);
                else
                    _sequenceGenerator.ResetWithAGivenSample(token, initialSamples);

                for (var index = 0; index < count && tries < MaximumTries; index++)
                {
                    for (; tries < MaximumTries; tries++)
                    {
                        var geometryWells = tries == 0 ? _sequenceGenerator.CurrentSample : _sequenceGenerator.NextSample(token);
                        if (nextSampleCounter-- > 0) continue;
                        if (tries < MinimumTries && output.Count != 0) continue;
                        token.ThrowIfCancellationRequested();

                        // todo: calcular interseções de cada geometry well para poder validar regiões sem poços.

                        if (!_applicableRulesManager.ValidateWellsRules(geometryWells)) continue;
                        output.Add(geometryWells);
                        break;
                    }
                    nextSampleCounter = 1;
                }
                if (output.Count > 0) break;
            }
            differentSamples = output.Count;
            if (output.Count < count)
            {
                if (output.Count == 0)
                    throw new InvalidOperationException(
                        "Não foi possível gerar nenhum indivíduo. " +
                        "É possível que o grid seja pequeno demais para gerar " +
                        "a quantidade solicitada de poços por indivíduo. Tente diminuir a " +
                        "distância mínima entre poços ou aumentar o comprimento máximo do poço.");

                // preenche os indivíduos faltantes
                //LogManager.LogWarning("Foram gerados apenas {0} indivíduos independentes.",
                //    output.Count);

                var firstIndividual = output[0];
                while (output.Count < count)
                {
                    output.Add(firstIndividual); //não precisa clonar pq GeometryWell é imutável.
                }
            }
            return output;
        }

        
    }
}