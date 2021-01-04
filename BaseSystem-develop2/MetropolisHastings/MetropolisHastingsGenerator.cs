using System;
using System.Collections.Generic;
using System.Threading;
using Octopus.Engine.Geometry.Entities;
using Octopus.Log;
using Ude.Core;

namespace Octopus.Engine.Services.RandomGenerator
{
    /// <summary>
    /// Gerador de sequências aleatórias para distribuições de probabilidade muldidimensionais
    /// baseado no algoritmo de Metropolis-Hastings
    /// (http://en.wikipedia.org/wiki/Metropolis%E2%80%93Hastings_algorithm#Step-by-step_instructions)
    /// </summary>
    public class MetropolisHastingsGenerator<T>:ISampleSequenceGenerator<T>
    {
        private readonly ISampler<T> _sampler;
        private readonly string _studyName;
        private readonly string _reservoirName;
        private readonly int _reservoirIdOnStudy;
        private T _currentSample;
        private double _currentLikelihood;
        private readonly Random _random = new Random();
        private readonly int? _noWellRegionId;

        public MetropolisHastingsGenerator(ISampler<T> sampler, CancellationToken token, string studyName, string reservoirName, int reservoirIdOnStudy, int? noWellRegionId)
        {
            _sampler = sampler;
            _studyName = studyName;
            _reservoirName = reservoirName;
            _reservoirIdOnStudy = reservoirIdOnStudy;
            _noWellRegionId = noWellRegionId;
            Reset(token);
        }

        public void Reset(CancellationToken token)
        {
            _currentSample = _sampler.Sample(token, _studyName, _reservoirName, _reservoirIdOnStudy, _noWellRegionId);
            _currentLikelihood = _sampler.GetLikelihood(_currentSample, token);
        }

        public void ResetWithAGivenSample(CancellationToken token, T sample)
        {
            _currentSample = _sampler.Sample(token, _studyName, _reservoirName, _reservoirIdOnStudy, _noWellRegionId, sample);
            _currentLikelihood = _sampler.GetLikelihood(_currentSample, token);
        }
        
        public T NextSample(CancellationToken token)
        {
            var candidateSample = _sampler.Sample(_currentSample, token, _studyName, _reservoirName, _reservoirIdOnStudy, _noWellRegionId);
            var candidateLikelihood = _sampler.GetLikelihood(candidateSample, token);

            var a1 = candidateLikelihood/_currentLikelihood;
            var a2 = _sampler.GetLikelihood(_currentSample, candidateSample, token)/
                     _sampler.GetLikelihood(candidateSample, _currentSample, token);

            if(Math.Abs(a2 - 1) > 1e-6)
                LogManager.LogDebugFileOnly(Messages.ConditionalProbabilityDifferentFromOneLogMessage);

            var a = a1*a2;
            bool change;
            if (a >= 1)
            {
                change = true;
            }
            else
            {
                if (double.IsNaN(a1) || double.IsPositiveInfinity(a1) || 
                    double.IsPositiveInfinity(a2))
                {
                    change = true; // a amostra atual tem likelihood 0! Logo, deve trocar.
                }
                else if (double.IsNaN(a))
                {
                    change = false;
                }
                else
                {
                    // sorteia se há troca, com probabilidade de trocar igual a "a".
                    var t = _random.NextDouble(); /* 0 <= t <= 1 */
                    change = t <= a;
                }
            }
            if (change)
            {
                // substitui a amostra anterior pela nova amostra.
                _currentSample = candidateSample;
                _currentLikelihood = candidateLikelihood;
            }
            return _currentSample;
        }

        public T CurrentSample { get { return _currentSample; } }
    }
}