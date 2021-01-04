using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Octopus.Engine.Geometry.Entities;

namespace Octopus.Engine.Services.RandomGenerator
{
    public interface ISampleSequenceGenerator<T> 
    {
        void Reset(CancellationToken token);
        void ResetWithAGivenSample(CancellationToken token, T sample);
        T NextSample(CancellationToken token);
        T CurrentSample { get; }
    }
}