using System;
using System.Linq;
using System.Linq.Expressions;
using PSO.Interfaces;

namespace PSO.Benchmarks.NonLinearConstraints
{
    public class NonLinearConstraints : IConstraints
    {
        private readonly BenchmarksNames _functionName;

        public NonLinearConstraints(BenchmarksNames functionName)
        {
            _functionName = functionName;
        }

        /// <summary>
        /// Verifica a validade da partícula
        /// </summary>
        /// <param name="particle"></param>
        /// <returns></returns>
        public virtual bool IsFeasible(IParticleNLC particle)
        {
            switch (_functionName)
            {
                case BenchmarksNames.G01:
                    return CheckConstraintsG01(particle) && CheckBoundersG01(particle);
                case BenchmarksNames.G12:
                    return CheckConstraintsG12(particle) && CheckBoundersG12(particle);
                default:
                    return false;
            }

        }

        public bool IsFeasible(IParticle particle)
        {
            switch (_functionName)
            {
                case BenchmarksNames.G02:
                    return CheckConstraintsG02(particle) && CheckBoundersG02(particle);
                case BenchmarksNames.G03:
                    return CheckConstraintsG03(particle) && CheckBoundersG03(particle);
                case BenchmarksNames.G04:
                    return CheckConstraintsG04(particle) && CheckBoundersG04(particle);
                case BenchmarksNames.G05:
                    return CheckConstraintsG05(particle) && CheckBoundersG05(particle);
                case BenchmarksNames.G06:
                    return CheckConstraintsG06(particle) && CheckBoundersG06(particle);
                case BenchmarksNames.G07:
                    return CheckConstraintsG07(particle) && CheckBoundersG07(particle);
                case BenchmarksNames.G08:
                    return CheckConstraintsG08(particle) && CheckBoundersG08(particle);
                case BenchmarksNames.G09:
                    return CheckConstraintsG09(particle) && CheckBoundersG09(particle);
                case BenchmarksNames.G10:
                    return CheckConstraintsG10(particle) && CheckBoundersG10(particle);
                case BenchmarksNames.G11:
                    return CheckConstraintsG11(particle) && CheckBoundersG11(particle);
                case BenchmarksNames.G13:
                    return CheckConstraintsG13(particle) && CheckBoundersG13(particle);
                default:
                    return false;
            }
        }

        #region G01

        private static bool CheckBoundersG01(IParticleNLC particle)
        {
            for (int index = 0; index < 9; index++)
            {
                var particleValue = particle.Position.ElementAt(index);
                if (particleValue < 0 || particleValue > 1)
                    return false;
            }
            for (int index = 9; index < 12; index++)
            {
                var particleValue = particle.Position.ElementAt(index);
                if (particleValue < 0 || particleValue > 100)
                    return false;
            }
            var x = particle.Position.ElementAt(12);
            if (x < 0 || x > 1)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Fronteira para benhcmark G01.
        /// </summary>
        /// <param name="particle"></param>
        /// <returns></returns>
        private static bool CheckConstraintsG01(IParticleNLC particle)
        {
            if (ConstraintsG01Ok(particle))
                return true;
            return false;
        }

        private static bool ConstraintsG01Ok(IParticleNLC particle)
        {
            if (2*particle.Position.ElementAt(0) + 2*particle.Position.ElementAt(1) + particle.Position.ElementAt(9) +
                particle.Position.ElementAt(10) - 10 > 0.001) return false;
            if (2*particle.Position.ElementAt(0) + 2*particle.Position.ElementAt(2) + particle.Position.ElementAt(9) +
                particle.Position.ElementAt(11) - 10 > 0.001) return false;
            if (2*particle.Position.ElementAt(1) + 2*particle.Position.ElementAt(2) + particle.Position.ElementAt(10) +
                particle.Position.ElementAt(11) - 10 > 10) return false;
            if (-8*particle.Position.ElementAt(0) + particle.Position.ElementAt(9) > 0.001) return false;
            if (-8*particle.Position.ElementAt(1) + particle.Position.ElementAt(10) > 0.001) return false;
            if (-8*particle.Position.ElementAt(2) + particle.Position.ElementAt(11) > 0.001) return false;
            if (-2*particle.Position.ElementAt(3) - particle.Position.ElementAt(4) + particle.Position.ElementAt(9) > 0.001)
                return false;
            if (-2*particle.Position.ElementAt(5) - particle.Position.ElementAt(6) + particle.Position.ElementAt(10) > 0.001)
                return false;
            return -2 * particle.Position.ElementAt(7) - particle.Position.ElementAt(8) + particle.Position.ElementAt(11) <= 0.001;
        }

        #endregion

        #region G02

        private static bool CheckBoundersG02(IParticle particle)
        {
            foreach (var particleValue in particle.Position)
            {
                if (particleValue<0 || particleValue > 10)
                {
                    return false;
                }
            }
            return true;
        }

        private static bool CheckConstraintsG02(IParticle particle)
        {
            const double tolerance = 0.01;
            if (0.75 - MathOperations.PowerProduct(1, 20, particle.Position) <= tolerance)
            {
                if (MathOperations.Sum(1, 20, particle.Position) - 7.5 * 20 <= tolerance)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region G03

        private static bool CheckBoundersG03(IParticle particle)
        {
            foreach (var particleValue in particle.Position)
            {
                if (particleValue < 0 || particleValue > 1)
                {
                    return false;
                }
            }
            return true;
        }

        private static bool CheckConstraintsG03(IParticle particle)
        {
            const double tolerance = 0.001;
            if (Math.Abs(MathOperations.SquaredSum(1,10,particle.Position) - 1) < tolerance)
            {
                return true;
            }
            return false;
        }

        #endregion

        #region G04

        private static bool CheckBoundersG04(IParticle particle)
        {
            var particleValue = particle.Position.ElementAt(0);
            if (particleValue < 78 || particleValue >102)
            {
                return false;
            }
            particleValue = particle.Position.ElementAt(1);
            if (particleValue < 33 || particleValue > 45)
            {
                return false;
            }
            for (int index = 2; index < 5; index++)
            {
                particleValue = particle.Position.ElementAt(index);
                if (particleValue < 27 || particleValue > 45)
                {
                    return false;
                }
            }
            return true;
        }

        private static bool CheckConstraintsG04(IParticle particle)
        {
            var particlePosition = particle.Position;
            var fistConstraint = 85.334407 + 0.0056858*particlePosition.ElementAt(1)*particlePosition.ElementAt(4) +
                                 0.0006262*particlePosition.ElementAt(0)*particlePosition.ElementAt(3) -
                                 0.0022053*particlePosition.ElementAt(2)*particlePosition.ElementAt(4) - 92;

            var secondConstraint = -85.334407 - 0.0056858*particlePosition.ElementAt(1)*particlePosition.ElementAt(4) -
                                   0.0006262*particlePosition.ElementAt(0)*particlePosition.ElementAt(3) +
                                   0.0022053*particlePosition.ElementAt(2)*particlePosition.ElementAt(4);

            var thirdConstraint = 80.51249 + 0.0071317 * particlePosition.ElementAt(1) * particlePosition.ElementAt(4) +
                                   0.0029955 * particlePosition.ElementAt(0) * particlePosition.ElementAt(1) +
                                   0.0021813 * Math.Pow(particlePosition.ElementAt(2),2) -110;

            var fourthConstraint = -80.51249 - 0.0071317 * particlePosition.ElementAt(1) * particlePosition.ElementAt(4) -
                                   0.0029955 * particlePosition.ElementAt(0) * particlePosition.ElementAt(1) -
                                   0.0021813 * Math.Pow(particlePosition.ElementAt(2), 2) + 90;

            var fifthConstraint = 9.300961 + 0.0047026 * particlePosition.ElementAt(2) * particlePosition.ElementAt(4) +
                                  0.0012547 * particlePosition.ElementAt(0) * particlePosition.ElementAt(2) +
                                  0.0019085 * particlePosition.ElementAt(2) * particlePosition.ElementAt(3) - 25;

            var sixthConstraint = -9.300961 - 0.0047026 * particlePosition.ElementAt(2) * particlePosition.ElementAt(4) -
                                  0.0012547 * particlePosition.ElementAt(0) * particlePosition.ElementAt(2) -
                                  0.0019085 * particlePosition.ElementAt(2) * particlePosition.ElementAt(3) + 20;

            const double tolerance = 0.000001;
            if (fistConstraint > tolerance) return false;
            if (secondConstraint > tolerance) return false;
            if (thirdConstraint > tolerance) return false;
            if (fourthConstraint > tolerance) return false;
            if (fifthConstraint > tolerance) return false;
            return sixthConstraint <= tolerance;
        }

        #endregion

        #region G05

        private static bool CheckBoundersG05(IParticle particle)
        {
            double particleValue;
            for (var index = 0; index < 2; index++)
            {
                particleValue = particle.Position.ElementAt(index);
                if (particleValue < 0 || particleValue > 1200)
                {
                    return false;
                }
            }

            for (var index = 2; index < 4; index++)
            {
                particleValue = particle.Position.ElementAt(index);
                if (particleValue < -0.55 || particleValue > 0.55)
                {
                    return false;
                }
            }
            
            return true;
        }

        /// <summary>
        /// 3 ultimas restrições não sao nunca seguidas. avaliar forma de inicializacao da populacao
        /// </summary>
        /// <param name="particle"></param>
        /// <returns></returns>
        private static bool CheckConstraintsG05(IParticle particle)
        {
            var particlePosition = particle.Position;
            var fistConstraint = -particlePosition.ElementAt(3) + particlePosition.ElementAt(2) - 0.55;

            var secondConstraint = -particlePosition.ElementAt(2) + particlePosition.ElementAt(3) - 0.55;

            var thirdConstraint = 1000*Math.Sin(-particlePosition.ElementAt(2) - 0.25) +
                                  1000*Math.Sin(-particlePosition.ElementAt(3) - 0.25) + 894.8 -
                                  particlePosition.ElementAt(0);

            var fourthConstraint = 1000*Math.Sin(particlePosition.ElementAt(2) - 0.25) +
                                   1000*Math.Sin(particlePosition.ElementAt(2) - particlePosition.ElementAt(3) - 0.25) +
                                   894.8 - particlePosition.ElementAt(1);

            var fifthConstraint = 1000 * Math.Sin(particlePosition.ElementAt(3) - 0.25) +
                                  1000 * Math.Sin(particlePosition.ElementAt(3) - particlePosition.ElementAt(2) - 0.25) + 1294.8;

            const double tolerance = 0.1;
            if (fistConstraint > tolerance) return false;
            if (secondConstraint > tolerance) return false;
            if (Math.Abs(thirdConstraint) > tolerance) return false;
            if (Math.Abs(fourthConstraint) > tolerance) return false;
            return !(Math.Abs(fifthConstraint) > tolerance);
        }

        #endregion

        #region G06

        private static bool CheckBoundersG06(IParticle particle)
        {
            double particleValue = particle.Position.ElementAt(0);
            if (particleValue < 13 || particleValue > 100)
            {
                return false;
            }
            particleValue = particle.Position.ElementAt(1);
            if (particleValue < 0 || particleValue > 100)
            {
                return false;
            }

            return true;
        }

        private static bool CheckConstraintsG06(IParticle particle)
        {
            var particlePosition = particle.Position;
            var fistConstraint = -Math.Pow((particlePosition.ElementAt(0) - 5), 2) - Math.Pow((particlePosition.ElementAt(1) - 5), 2) + 100;

            var secondConstraint = Math.Pow((particlePosition.ElementAt(0) - 6), 2) + Math.Pow((particlePosition.ElementAt(1) - 5), 2) - 82.81;

            if (fistConstraint > 0.001) return false;
            return secondConstraint <= 0.0005;
        }

        #endregion

        #region G07

        private static bool CheckBoundersG07(IParticle particle)
        {
            foreach (var particlePosition in particle.Position)
            {
                if (particlePosition < -10 || particlePosition > 10)
                {
                    return false;
                }
            }
            return true;
        }

        private static bool CheckConstraintsG07(IParticle particle)
        {
            var particlePosition = particle.Position;
            var firstConstraint = -105 + 4*particlePosition.ElementAt(0) + 5*particlePosition.ElementAt(1) -
                                 3*particlePosition.ElementAt(6) + 9*particlePosition.ElementAt(7);

            var secondConstraint = 10*particlePosition.ElementAt(0) - 8*particlePosition.ElementAt(1) -
                                   17*particlePosition.ElementAt(6) + 2*particlePosition.ElementAt(7);

            var thirdConstraint = -8*particlePosition.ElementAt(0) + 2*particlePosition.ElementAt(1) +
                                  5*particlePosition.ElementAt(8) - 2*particlePosition.ElementAt(9) - 12;

            var fourthConstraint = 3*Math.Pow(particlePosition.ElementAt(0) - 2, 2) +
                                   4*Math.Pow(particlePosition.ElementAt(1) - 3, 2) +
                                   2*Math.Pow(particlePosition.ElementAt(2), 2) - 7*particlePosition.ElementAt(3) - 120;

            var fifthConstraint = 5*Math.Pow(particlePosition.ElementAt(0), 2) +
                                  8*particlePosition.ElementAt(1) +
                                  Math.Pow(particlePosition.ElementAt(2) - 6, 2) - 2*particlePosition.ElementAt(3) - 40;

            var sixthConstraint = Math.Pow(particlePosition.ElementAt(0), 2) +
                                  2*Math.Pow(particlePosition.ElementAt(1) - 2, 2) -
                                  2*particlePosition.ElementAt(0)*particlePosition.ElementAt(1) +
                                  14*particlePosition.ElementAt(4) - 6*particlePosition.ElementAt(5);

            var seventhConstraint = 0.5*Math.Pow((particlePosition.ElementAt(0) - 8), 2) +
                                    2*Math.Pow((particlePosition.ElementAt(1) - 4), 2) +
                                    3*Math.Pow(particlePosition.ElementAt(4), 2) - particlePosition.ElementAt(5) - 30;

            var eigthConstraint = -3*particlePosition.ElementAt(0) + 6*particlePosition.ElementAt(1) +
                                   12*Math.Pow(particlePosition.ElementAt(8) - 8, 2) - 7*particlePosition.ElementAt(9);

            if (firstConstraint > 0.001) return false;
            if (secondConstraint > 0.001) return false;
            if (thirdConstraint > 0.001) return false;
            if (fourthConstraint > 0.001) return false;
            if (fifthConstraint > 0.001) return false;
            if (sixthConstraint > 0.001) return false;
            if (seventhConstraint > 0.001) return false;
            return eigthConstraint <= 0.001;
        }

        #endregion

        #region G08

        private static bool CheckBoundersG08(IParticle particle)
        {
            foreach (var particlePosition in particle.Position)
            {
                if (particlePosition < 0 || particlePosition > 10)
                {
                    return false;
                }
            }
            return true;
        }

        private static bool CheckConstraintsG08(IParticle particle)
        {
            var particlePosition = particle.Position;
            var firstConstraint = Math.Pow(particlePosition.ElementAt(0), 2) - particlePosition.ElementAt(1) + 1;

            var secondConstraint = 1 - particlePosition.ElementAt(0) + Math.Pow((particlePosition.ElementAt(1) - 4), 2);

            if (firstConstraint > 0.001) return false;
            return secondConstraint <= 0.001;
        }

        #endregion

        #region G09

        private static bool CheckBoundersG09(IParticle particle)
        {
            foreach (var particlePosition in particle.Position)
            {
                if (particlePosition < -10 || particlePosition > 10)
                {
                    return false;
                }
            }
            return true;
        }

        private static bool CheckConstraintsG09(IParticle particle)
        {
            var particlePosition = particle.Position;
            var firstConstraint = -127 + 2*Math.Pow(particlePosition.ElementAt(0), 2) +
                                  3*Math.Pow(particlePosition.ElementAt(1), 4) + particlePosition.ElementAt(2) +
                                  4*Math.Pow(particlePosition.ElementAt(3), 2) + 5*particlePosition.ElementAt(4);

            var secondConstraint = -282 + 7*particlePosition.ElementAt(0) + 3*particlePosition.ElementAt(1) +
                                   10*Math.Pow(particlePosition.ElementAt(2), 2) + particlePosition.ElementAt(3) -
                                   particlePosition.ElementAt(4);

            var thirdConstraint = -196 + 23*particlePosition.ElementAt(0) + Math.Pow(particlePosition.ElementAt(1), 2) +
                                  6*Math.Pow(particlePosition.ElementAt(5), 2) - 8*particlePosition.ElementAt(6);

            var fourthConstraint = 4*Math.Pow(particlePosition.ElementAt(0), 2) +
                                   Math.Pow(particlePosition.ElementAt(1), 2) -
                                   3*particlePosition.ElementAt(0)*particlePosition.ElementAt(1) +
                                   2*Math.Pow(particlePosition.ElementAt(2), 2) + 5*particlePosition.ElementAt(5) -
                                   11*particlePosition.ElementAt(6);

            if (firstConstraint > 0.001) return false;
            if (secondConstraint > 0.001) return false;
            if (thirdConstraint > 0.001) return false;
            return fourthConstraint <= 0.001;
        }

        #endregion

        #region G10

        private static bool CheckBoundersG10(IParticle particle)
        {
            var positionValue = particle.Position.ElementAt(0);
            if (positionValue < 100 || positionValue > 10000)
                return false;

            for (var i = 1; i < 3; i++)
            {
                positionValue = particle.Position.ElementAt(i);
                if (positionValue < 1000 || positionValue > 10000)
                    return false;
            }

            for (var i = 3; i < 8; i++)
            {
                positionValue = particle.Position.ElementAt(i);
                if (positionValue < 10 || positionValue > 1000)
                    return false;
            }
            
            return true;
        }

        private static bool CheckConstraintsG10(IParticle particle)
        {
            var particlePosition = particle.Position;
            var firstConstraint = -1 + 0.0025*(particlePosition.ElementAt(3) + particlePosition.ElementAt(5));

            var secondConstraint = -1 +
                                   0.0025*
                                   (particlePosition.ElementAt(4) + particlePosition.ElementAt(6) -
                                    particlePosition.ElementAt(3));

            var thirdConstraint = -1 + 0.01 * (particlePosition.ElementAt(7) - particlePosition.ElementAt(4));

            var fourthConstraint = -particlePosition.ElementAt(0)*particlePosition.ElementAt(5) +
                                   833.33252*particlePosition.ElementAt(3) + 100*particlePosition.ElementAt(0) -
                                   83333.333;

            var fifthConstraint = -particlePosition.ElementAt(1)*particlePosition.ElementAt(6) +
                                  1250*particlePosition.ElementAt(4) + particlePosition.ElementAt(1) *
                                  particlePosition.ElementAt(3) - 1250*particlePosition.ElementAt(3);

            var sixthConstraint = -particlePosition.ElementAt(2)*particlePosition.ElementAt(7) + 1250000 +
                                  particlePosition.ElementAt(2)*particlePosition.ElementAt(4) -
                                  2500*particlePosition.ElementAt(4);

            if (firstConstraint > 0.001) return false;
            if (secondConstraint > 0.001) return false;
            if (thirdConstraint > 0.001) return false;
            if (fourthConstraint > 0.001) return false;
            if (fifthConstraint > 0.001) return false;
            return sixthConstraint <= 0.001;
        }

        #endregion

        #region G11

        private static bool CheckBoundersG11(IParticle particle)
        {
            foreach (var particlePosition in particle.Position)
            {
                if (particlePosition < -1 || particlePosition > 1)
                    return false;
            }
            return true;
        }

        private static bool CheckConstraintsG11(IParticle particle)
        {
            var particlePosition = particle.Position;
            var firstConstraint = particlePosition.ElementAt(1) - Math.Pow(particlePosition.ElementAt(0),2);

            const double tolerance = 0.000001;
            return Math.Abs(firstConstraint) < tolerance;
        }

        #endregion

        #region G12

        private static bool CheckBoundersG12(IParticleNLC particle)
        {
            foreach (var particlePosition in particle.Position)
            {
                if (particlePosition < 0 || particlePosition > 10)
                    return false;
            }
            return true;
        }

        private static bool CheckConstraintsG12(IParticleNLC particle)
        {
            var particlePosition = particle.Position;
            for (var r = 1; r <= 9; r++)
            {
                for (var p = 1; p <= 9; p++)
                {
                    for (var q = 1; q <= 9; q++)
                    {
                        var firstConstraint = Math.Pow(particlePosition.ElementAt(0) - p, 2) +
                                              Math.Pow(particlePosition.ElementAt(0) - q, 2) +
                                              Math.Pow(particlePosition.ElementAt(2) - r, 2) - 0.0625;

                        if (firstConstraint <= 0.001)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        #endregion

        #region G13

        private static bool CheckBoundersG13(IParticle particle)
        {
            for (var i = 0; i < 2; i++)
            {
                var particlePosition = particle.Position.ElementAt(i);
                if (particlePosition < -2.3 || particlePosition > 2.3)
                    return false;
            }

            for (var i = 2; i < 5; i++)
            {
                var particlePosition = particle.Position.ElementAt(i);
                if (particlePosition < -3.2 || particlePosition > 3.2)
                    return false;
            }

            return true;
        }

        private static bool CheckConstraintsG13(IParticle particle)
        {
            var particlePosition = particle.Position;
            var firstConstraint = Math.Pow(particlePosition.ElementAt(0), 2) +
                                  Math.Pow(particlePosition.ElementAt(1), 2) +
                                  Math.Pow(particlePosition.ElementAt(2), 2) +
                                  Math.Pow(particlePosition.ElementAt(3), 2) +
                                  Math.Pow(particlePosition.ElementAt(4), 2) - 10;

            var secondConstraint = particlePosition.ElementAt(1)*particlePosition.ElementAt(2) -
                                   5*particlePosition.ElementAt(3)*particlePosition.ElementAt(4);

            var thirdConstraint = Math.Pow(particlePosition.ElementAt(0), 3) +
                                  Math.Pow(particlePosition.ElementAt(1), 3) + 1;

            const double tolerance = 0.05;
            if (Math.Abs(firstConstraint) > tolerance) return false;
            if (Math.Abs(secondConstraint) > tolerance) return false;
            return !(Math.Abs(thirdConstraint) > tolerance);
        }

        #endregion

    }
}
