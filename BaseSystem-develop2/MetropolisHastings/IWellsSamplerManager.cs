using System.Collections.Generic;
using System.Threading;
using Octopus.Engine.Geometry.Entities;

namespace Octopus.Engine.Services.RandomGenerator
{
    public interface IWellsSamplerManager
    {
        IList<GeometryWell[]> GetRandomValidWells(int count, CancellationToken token, out int differentSamples, GeometryWell[] initialSample = null);


    }
}