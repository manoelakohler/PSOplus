using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octopus.Engine.Geometry.Entities;
using Octopus.Engine.Services.GridServices.Interfaces;
using Octopus.Engine.Utils;

namespace Octopus.Engine.Services.RandomGenerator
{
    public class WellCoordinateProbability
    {
        private readonly double _i;
        private readonly double _j;
        private readonly double _k;

        public WellCoordinateProbability(IGrid grid, DiscretePoint3D discretePointOfSample, DiscretePoint3D discretePointOfGivenSample, CoordinateVariation coordinateVariation)
        {
            var maximumAdjacentDeltaI = GetMaximumAdjacentDelta(grid.GridNi);
            var maximumAdjacentDeltaJ = GetMaximumAdjacentDelta(grid.GridNj);
            var maximumAdjacentDeltaK = GetMaximumAdjacentDelta(grid.GridNk);

            _i = GetCoordinateNumberOfProbability(discretePointOfSample.I, discretePointOfGivenSample.I, coordinateVariation.MinPoint.I, coordinateVariation.MaxPoint.I, maximumAdjacentDeltaI);
            _j = GetCoordinateNumberOfProbability(discretePointOfSample.J, discretePointOfGivenSample.J, coordinateVariation.MinPoint.J, coordinateVariation.MaxPoint.J, maximumAdjacentDeltaJ);
            _k = GetCoordinateNumberOfProbability(discretePointOfSample.K, discretePointOfGivenSample.K, coordinateVariation.MinPoint.K, coordinateVariation.MaxPoint.K, maximumAdjacentDeltaK);
        }

        private int GetMaximumAdjacentDelta(int gridMaximum)
        {
            return Math.Min(2, gridMaximum % 2 == 0 ? gridMaximum / 2 : (gridMaximum - 1) / 2);
        }

        public Double I
        {
            get { return _i; }
        }
        public Double J
        {
            get { return _j; }
        }
        public Double K
        {
            get { return _k; }
        }

        private static double GetCoordinateNumberOfProbability(int coordinateValueOfnewSample, int coordinateValueOfGivenSample, int min, int max, int maximumAdjacentDelta)
        {
            if (coordinateValueOfGivenSample - maximumAdjacentDelta < min) return CoordinateOutOfRangeOnLeft(coordinateValueOfnewSample, coordinateValueOfGivenSample, min, maximumAdjacentDelta);
            if (coordinateValueOfGivenSample + maximumAdjacentDelta > max) return CoordinateOutOfRangeOnRight(coordinateValueOfnewSample, coordinateValueOfGivenSample, max, maximumAdjacentDelta);
            return CoordinateInsideOfRange(coordinateValueOfnewSample, coordinateValueOfGivenSample, maximumAdjacentDelta);
        }

        private static double CoordinateOutOfRangeOnLeft(int sample, int givenSample, int min, int maximumAdjacentDelta)
        {
            var auxiliar = maximumAdjacentDelta - givenSample + min;
            if (sample > auxiliar && sample <= givenSample + maximumAdjacentDelta) return 1 / (2.0 * maximumAdjacentDelta + 1.0);
            if (sample >= min && sample <= auxiliar) return 2 / (2.0 * maximumAdjacentDelta + 1.0);
            return 0;
        }

        private static double CoordinateOutOfRangeOnRight(int sample, int givenSample, int max, int maximumAdjacentDelta)
        {
            var auxiliar = 2 * max - maximumAdjacentDelta - givenSample + 1;
            if (sample >= givenSample - maximumAdjacentDelta && sample < auxiliar) return 1 / (2.0 * maximumAdjacentDelta + 1.0);
            if (sample >= auxiliar && sample <= max) return 2 / (2.0 * maximumAdjacentDelta + 1.0);
            return 0;
        }

        private static double CoordinateInsideOfRange(int sample, int givenSample, int maximumAdjacentDelta)
        {
            if ((sample >= givenSample - maximumAdjacentDelta && sample <= givenSample + maximumAdjacentDelta)) return 1 / (2.0 * maximumAdjacentDelta + 1.0);
            return 0;
        }
    }
}
