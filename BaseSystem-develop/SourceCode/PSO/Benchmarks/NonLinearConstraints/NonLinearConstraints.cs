using System;
using System.Linq;
using PSO.Interfaces;

namespace PSO.Benchmarks.NonLinearConstraints
{
    public class NonLinearConstraints : IConstraints
    {
        public BenchmarksNames FunctionName { get; private set; }
        public NonLinearConstraints(BenchmarksNames functionName)
        {
            FunctionName = functionName;
        }

        /// <summary>
        /// Verifica a validade da partícula
        /// </summary>
        /// <param name="particle"></param>
        /// <returns></returns>
        public virtual bool IsFeasible(IParticleNLC particle)
        {
            switch (FunctionName)
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
            switch (FunctionName)
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
                case BenchmarksNames.AckleyFunction30:
                    return CheckBoundersAckley30(particle);
                case BenchmarksNames.RosenbrockFunctionMinus9To11:
                    return CheckBoundersRosenbrockMinus9To11(particle);
                case BenchmarksNames.SphereFunction600:
                    return CheckBoundersSphere600(particle);
                case BenchmarksNames.Rastrigin5Dot12:
                    return CheckBoundersRastrigin5Dot12(particle);
                case BenchmarksNames.GriewankFunction30:
                    return CheckBoundersGriewank30(particle);

                case BenchmarksNames.G14:
                    return CheckConstraintsG14(particle) && CheckBoundersG14(particle);
                case BenchmarksNames.G15:
                    return CheckConstraintsG15(particle) && CheckBoundersG15(particle);
                case BenchmarksNames.G16:
                    return CheckConstraintsG16(particle) && CheckBoundersG16(particle);
                case BenchmarksNames.G17:
                    return CheckConstraintsG17(particle) && CheckBoundersG17(particle);
                case BenchmarksNames.G18:
                    return CheckConstraintsG18(particle) && CheckBoundersG18(particle);
                case BenchmarksNames.G19:
                    return CheckConstraintsG19(particle) && CheckBoundersG19(particle);
                case BenchmarksNames.G20:
                    return CheckConstraintsG20(particle) && CheckBoundersG20(particle);
                case BenchmarksNames.G21:
                    return CheckConstraintsG21(particle) && CheckBoundersG21(particle);
                case BenchmarksNames.G22:
                    return CheckConstraintsG22(particle) && CheckBoundersG22(particle);
                case BenchmarksNames.G23:
                    return CheckConstraintsG23(particle) && CheckBoundersG23(particle);
                case BenchmarksNames.G24:
                    return CheckConstraintsG24(particle) && CheckBoundersG24(particle);

                case BenchmarksNames.HoleFunction:
                case BenchmarksNames.PeaksFunction:
                case BenchmarksNames.ManyPeaksFunction:
                case BenchmarksNames.StepFunction:
                case BenchmarksNames.RosenbockFunction:
                case BenchmarksNames.SincFunction:
                case BenchmarksNames.BumpsFunction:
                    return true;

                default:
                    return false;
            }
        }

        public ConstraintViolationAndVariables GetConstraintViolation(double[] position)
        {
            switch (FunctionName)
            {

                case BenchmarksNames.G02:
                    return GetConstraintViolationG02(position);
                case BenchmarksNames.G03:
                    return GetConstraintViolationG03(position);
                case BenchmarksNames.G04:
                    return GetConstraintViolationG04(position);
                case BenchmarksNames.G05:
                    return GetConstraintViolationG05(position);
                case BenchmarksNames.G06:
                    return GetConstraintViolationG06(position);
                case BenchmarksNames.G07:
                    return GetConstraintViolationG07(position);
                case BenchmarksNames.G08:
                    return GetConstraintViolationG08(position);
                case BenchmarksNames.G09:
                    return GetConstraintViolationG09(position);
                case BenchmarksNames.G10:
                    return GetConstraintViolationG10(position);
                case BenchmarksNames.G11:
                    return GetConstraintViolationG11(position);

                case BenchmarksNames.G13:
                    return GetConstraintViolationG13(position);

                case BenchmarksNames.G14:
                    return GetConstraintViolationG14(position);
                case BenchmarksNames.G15:
                    return GetConstraintViolationG15(position);
                case BenchmarksNames.G16:
                    return GetConstraintViolationG16(position);
                case BenchmarksNames.G17:
                    return GetConstraintViolationG17(position);
                case BenchmarksNames.G18:
                    return GetConstraintViolationG18(position);
                case BenchmarksNames.G19:
                    return GetConstraintViolationG19(position);
                case BenchmarksNames.G20:
                    return GetConstraintViolationG20(position);
                case BenchmarksNames.G21:
                    return GetConstraintViolationG21(position);
                case BenchmarksNames.G22:
                    return GetConstraintViolationG22(position);
                case BenchmarksNames.G23:
                    return GetConstraintViolationG23(position);
                case BenchmarksNames.G24:
                    return GetConstraintViolationG24(position);
                    
                //case BenchmarksNames.AckleyFunction30:
                //    return GetConstraintViolationAckley30(position);
                //case BenchmarksNames.RosenbrockFunctionMinus9To11:
                //    return GetConstraintViolationRosenbrockMinus9To11(position);
                //case BenchmarksNames.SphereFunction600:
                //    return GetConstraintViolationSphere600(position);
                //case BenchmarksNames.Rastrigin5Dot12:
                //    return GetConstraintViolationRastrigin5Dot12(position);
                //case BenchmarksNames.GriewankFunction30:
                //    return GetConstraintViolationGriewank30(position);
            }
            return null;
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
            const double tolerance = 0.1;
            if (0.75 - MathOperations.PowerProduct(1, 20, particle.Position) <= tolerance)
            {
                if (MathOperations.Sum(1, 20, particle.Position) - 7.5 * 20 <= tolerance)
                {
                    return true;
                }
            }
            return false;
        }
        public ConstraintViolationAndVariables GetConstraintViolationG02(double[] position)
        {
            throw new NotImplementedException();
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

        public ConstraintViolationAndVariables GetConstraintViolationG03(double[] position)
        {
            throw new NotImplementedException();
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
        public ConstraintViolationAndVariables GetConstraintViolationG04(double[] position)
        {
            throw new NotImplementedException();
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

        public ConstraintViolationAndVariables GetConstraintViolationG05(double[] x)
        {
            var cv = 0;
            var violationCost = new double[5];
            var violationVariables = new int[5, 3]; //todo

            for (var index = 0; index < 2; index++)
            {
                if (x[index] < 0 || x[index] > 1200)
                {
                    cv++;
                }
            }
            for (var index = 2; index < 4; index++)
            {
                if (x[index] < -0.55 || x[index] > 0.55)
                {
                    cv++;
                }
            }

            var g1 = -x[3] + x.ElementAt(2) - 0.55;
            var g2 = -x.ElementAt(2) + x.ElementAt(3) - 0.55;
            
            if (g1 > 0)
            {
                cv++;
                violationCost[0] = Math.Abs(g1);
                violationVariables[0, 0] = 3;
                violationVariables[0, 1] = 2;
                violationVariables[0, 2] = -1;
            }
            if (g2 > 0)
            {
                cv++;
                violationCost[1] = Math.Abs(g2);
                violationVariables[1, 0] = 2;
                violationVariables[1, 1] = 3;
                violationVariables[1, 2] = -1;
            }

            var h1 = 1000*Math.Sin(-x.ElementAt(2) - 0.25) + 1000*Math.Sin(-x.ElementAt(3) - 0.25) + 894.8 -
                     x.ElementAt(0);

            var h2 = 1000*Math.Sin(x.ElementAt(2) - 0.25) + 1000*Math.Sin(x.ElementAt(2) - x.ElementAt(3) - 0.25) +
                     894.8 - x.ElementAt(1);

            var h3 = 1000*Math.Sin(x.ElementAt(3) - 0.25) + 1000*Math.Sin(x.ElementAt(3) - x.ElementAt(2) - 0.25) +
                     1294.8;

            const double tolerance = 0.01; //todo

            if (Math.Abs(h1) > tolerance)
            {
                cv++;
                violationCost[2] = Math.Abs(h1);
                violationVariables[2, 0] = 2;
                violationVariables[2, 1] = 3;
                violationVariables[2, 2] = 0;
            }
            if (Math.Abs(h2) > tolerance)
            {
                cv++;
                violationCost[3] = Math.Abs(h2);
                violationVariables[3, 0] = 2;
                violationVariables[3, 1] = 3;
                violationVariables[3, 2] = 1;
            }
            if (Math.Abs(h3) > tolerance)
            {
                cv++;
                violationCost[4] = Math.Abs(h3);
                violationVariables[4, 0] = 3;
                violationVariables[4, 1] = 2;
                violationVariables[4, 2] = -1;
            }

            return new ConstraintViolationAndVariables(violationVariables, cv, violationCost);
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

        public ConstraintViolationAndVariables GetConstraintViolationG06(double[] position)
        {
            throw new NotImplementedException();
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

        public ConstraintViolationAndVariables GetConstraintViolationG07(double[] position)
        {
            throw new NotImplementedException();
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
        public ConstraintViolationAndVariables GetConstraintViolationG08(double[] position)
        {
            throw new NotImplementedException();
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

        public ConstraintViolationAndVariables GetConstraintViolationG09(double[] position)
        {
            throw new NotImplementedException();
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

        public ConstraintViolationAndVariables GetConstraintViolationG10(double[] position)
        {
            throw new NotImplementedException();
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

        public ConstraintViolationAndVariables GetConstraintViolationG11(double[] position)
        {
            throw new NotImplementedException();
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
        public ConstraintViolationAndVariables GetConstraintViolationG13(double[] position)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region G14

        private static bool CheckBoundersG14(IParticle particle)
        {
            for (var i = 0; i < 10; i++)
            {
                var particlePosition = particle.Position.ElementAt(i);
                if (particlePosition <= 0 || particlePosition > 10)
                    return false;
            }
            return true;
        }

        private static bool CheckConstraintsG14(IParticle particle)
        {
            var particlePosition = particle.Position;
            var firstConstraint = particlePosition[0] + 2*particlePosition[1] + 2*particlePosition[2] +
                                  particlePosition[5] + particlePosition[9] - 2;

            var secondConstraint = particlePosition[3] + 2*particlePosition[4] + particlePosition[5] +
                                   particlePosition[6] - 1;

            var thirdConstraint = particlePosition[2] + particlePosition[6] + particlePosition[7] +
                                  2*particlePosition[8] + particlePosition[9] - 1;

            const double tolerance = 0.5;
            if (Math.Abs(firstConstraint) > tolerance) return false;
            if (Math.Abs(secondConstraint) > tolerance) return false;
            return !(Math.Abs(thirdConstraint) > tolerance);
        }

        public ConstraintViolationAndVariables GetConstraintViolationG14(double[] x)
        {
            var cv = 0;
            var violationCost = new double[3];
            var violationVariables = new int[3, 10]; //todo

            for (var i = 0; i < 10; i++)
            {
                if (x[i] <= 0 || x[i] > 10)
                    { cv++; }
            }

            var h1 = x[0] + 2 * x[1] + 2 * x[2] + x[5] + x[9] - 2;
            var h2 = x[3] + 2 * x[4] + x[5] + x[6] - 1;
            var h3 = x[2] + x[6] + x[7] + 2 * x[8] + x[9] - 1;

            const double tolerance = 0.5;

            if (Math.Abs(h1) > tolerance)
            {
                cv++;
                violationCost[0] = Math.Abs(h1);
                violationVariables[0, 0] = 0;
                violationVariables[0, 1] = 1;
                violationVariables[0, 2] = 2;
                violationVariables[0, 3] = 5;
                violationVariables[0, 4] = 9;
            }
            if (Math.Abs(h2) > tolerance)
            {
                cv++;
                violationCost[1] = Math.Abs(h2);
                violationVariables[1, 0] = 3;
                violationVariables[1, 1] = 4;
                violationVariables[1, 2] = 5;
                violationVariables[1, 3] = 6;
                violationVariables[1, 4] = -1;
            }
            if (Math.Abs(h3) > tolerance)
            {
                cv++;
                violationCost[2] = Math.Abs(h3);
                violationVariables[2, 0] = 2;
                violationVariables[2, 1] = 6;
                violationVariables[2, 2] = 7;
                violationVariables[2, 3] = 8;
                violationVariables[2, 4] = 9;
            }

            return new ConstraintViolationAndVariables(violationVariables, cv, violationCost);
        }
        #endregion

        #region G15

        private static bool CheckBoundersG15(IParticle particle)
        {
            for (var i = 0; i < 3; i++)
            {
                var particlePosition = particle.Position.ElementAt(i);
                if (particlePosition < 0 || particlePosition > 10)
                    return false;
            }

            return true;
        }

        private static bool CheckConstraintsG15(IParticle particle)
        {
            var particlePosition = particle.Position;
            var firstConstraint = Math.Pow(particlePosition[0], 2) + Math.Pow(particlePosition[1], 2) +
                                  Math.Pow(particlePosition[2], 2) - 25;

            var secondConstraint = 8*particlePosition[0] + 14*particlePosition[1] + 7*particlePosition[2] - 56;

            const double tolerance = 0.05;
            if (Math.Abs(firstConstraint) > tolerance) return false;
            return !(Math.Abs(secondConstraint) > tolerance);
        }

        public ConstraintViolationAndVariables GetConstraintViolationG15(double[] x)
        {
            var cv = 0;
            var violationCost = new double[2];
            var violationVariables = new int[2, 3]; //todo

            for (var i = 0; i < 3; i++)
            {

                if (x[i] < 0 || x[i] > 10)
                { cv++; }
            }

            var h1 = Math.Pow(x[0], 2) + Math.Pow(x[1], 2) + Math.Pow(x[2], 2) - 25;
            var h2 = 8 * x[0] + 14 * x[1] + 7 * x[2] - 56;

            const double tolerance = 0.05;

            if (Math.Abs(h1) > tolerance)
            {
                cv++;
                violationCost[0] = Math.Abs(h1);
                violationVariables[0, 0] = 0;
                violationVariables[0, 1] = 1;
                violationVariables[0, 2] = 2;
            }
            if (Math.Abs(h2) > tolerance)
            {
                cv++;
                violationCost[1] = Math.Abs(h2);
                violationVariables[1, 0] = 0;
                violationVariables[1, 1] = 1;
                violationVariables[1, 2] = 2;
            }

            return new ConstraintViolationAndVariables(violationVariables, cv, violationCost);
        }

        #endregion

        #region G16

        private static bool CheckBoundersG16(IParticle particle)
        {
            if (particle.Position[0] < 704.4148 || particle.Position[0] > 906.3855)
                return false;

            if (particle.Position[1] < 68.6 || particle.Position[1] > 288.88)
                return false;

            if (particle.Position[2] < 0 || particle.Position[2] > 134.75)
                return false;

            if (particle.Position[3] < 193 || particle.Position[3] > 287.0966)
                return false;

            return !(particle.Position[4] < 25) && !(particle.Position[4] > 84.1988);
        }

        private static bool CheckConstraintsG16(IParticle particle)
        {
            var x = particle.Position;
            var y1 = x.ElementAt(1) + x.ElementAt(2) + 41.6;
            var c1 = 0.024*x.ElementAt(3) - 4.62;
            var y2 = 12.5/c1 + 12;
            var c2 = 0.0003535*Math.Pow(x.ElementAt(0), 2) + 0.5311*x.ElementAt(0) + 0.08705*y2*x.ElementAt(0);
            var c3 = 0.052*x.ElementAt(0) + 78 + 0.002377*y2*x.ElementAt(0);
            var y3 = c2/c3;
            var y4 = 19*y3;
            var c4 = 0.04782*(x.ElementAt(0) - y3) + (0.1956*Math.Pow(x.ElementAt(0) - y3, 2))/x.ElementAt(1) +
                     0.6376*y4 + 1.594*y3;
            var c5 = 100*x.ElementAt(1);
            var c6 = x.ElementAt(0) - y3 - y4;
            var c7 = 0.95 - (c4/c5);
            var y5 = c6*c7;
            var y6 = x.ElementAt(0) - y5 - y4 - y3;
            var c8 = (y5 + y4)*0.995;
            var y7 = c8/y1;
            var y8 = c8/3798;
            var c9 = y7 - (0.0663*y7/y8) - 0.3153;
            var y9 = 96.82/c9 + 0.321*y1;
            var y10 = 1.29*y5 + 1.258*y4 + 2.29*y3 + 1.71*y6;
            var y11 = 1.71*x.ElementAt(0) - 0.452*y4 + 0.58*y3;
            var c10 = 12.3/752.3;
            var c11 = 1.75*y2*0.995*x.ElementAt(0);
            var c12 = 0.995*y10 + 1998;
            var y12 = c10*x.ElementAt(0) + c11/c12;
            var y13 = c12 - 1.75*y2;
            var y14 = 3623 + 64.4*x.ElementAt(1) + 58.4*x.ElementAt(2) + 146312/(y9 + x.ElementAt(4));
            var c13 = 0.995*y10 + 60.8*x.ElementAt(1) + 48*x.ElementAt(3) - 0.1121*y14 - 5095;
            var y15 = y13/c13;
            var y16 = 148000 - 331000*y15 + 40*y13 - 61*y15*y13;
            var c14 = 2324*y10 - 28740000*y2;
            var y17 = 14130000 - 1328*y10 - 531*y11 + c14/c12;
            var c15 = y13/y15 - y13/0.52;
            var c16 = 1.104 - 0.72*y15;
            var c17 = y9 + x.ElementAt(4);

            var g1 = 0.28/0.72*y5 - y4;
            var g2 = x[2] - 1.5*x[1];
            var g3 = 3496*(y2/c12) - 21;
            var g4 = 110.6 + y1 - 62212/c17;
            var g5 = 213.1 - y1;
            var g6 = y1 - 405.23;
            var g7 = 17.505 - y2;
            var g8 = y2 - 1053.6667;
            var g9 = 11.275 - y3;
            var g10 = y3 - 35.03;
            var g11 = 214.228 - y4;
            var g12 = y4 - 665.585;
            var g13 = 7.458 - y5;
            var g14 = y5 - 584.463;
            var g15 = 0.961 - y6;
            var g16 = y6 - 265.916;
            var g17 = 1.612 - y7;
            var g18 = y7 - 7.046;
            var g19 = 0.146 - y8;
            var g20 = y8 - 0.222;
            var g21 = 107.99 - y9;
            var g22 = y9 - 273.366;
            var g23 = 922.693 - y10;
            var g24 = y10 - 1286.105;
            var g25 = 926.832 - y11;
            var g26 = y11 - 1444.046;
            var g27 = 18.766 - y12;
            var g28 = y12 - 537.141;
            var g29 = 1072.163 - y13;
            var g30 = y13 - 3247.039;
            var g31 = 8961.448 - y14;
            var g32 = y14 - 26844.086;
            var g33 = 0.063 - y15;
            var g34 = y15 - 0.386;
            var g35 = 71084.33 - y16;
            var g36 = -140000 + y16;
            var g37 = 2802713 - y17;
            var g38 = y17 - 12146108;

            if (g1 > 0 || g2 > 0 || g3 > 0 || g4 > 0 || g5 > 0 || g6 > 0 || g7 > 0 || g8 > 0 || g9 > 0 || g10 > 0 ||
                g11 > 0 || g12 > 0 || g13 > 0 || g14 > 0 || g15 > 0 || g16 > 0 || g17 > 0 || g18 > 0 || g19 > 0 ||
                g20 > 0 || g21 > 0 || g22 > 0 || g23 > 0 || g24 > 0 || g25 > 0 || g26 > 0 || g27 > 0 || g28 > 0 ||
                g29 > 0 || g30 > 0 || g31 > 0 || g32 > 0 || g33 > 0 || g34 > 0 || g35 > 0 || g36 > 0 || g37 > 0 ||
                g38 > 0) return false;
            return true;
        }

        public ConstraintViolationAndVariables GetConstraintViolationG16(double[] x)
        {
            throw new NotImplementedException();


            //var cv = 0;
            //var violationCost = new double[38];
            //var violationVariables = new int[38, 5]; //todo

            //if (x[0] < 704.4148 || x[0] > 906.3855)
            //{
            //    cv++;
            //}

            //if (x[1] < 68.6 || x[1] > 288.88)
            //{
            //    cv++;
            //}

            //if (x[2] < 0 || x[2] > 134.75)
            //{
            //    cv++;
            //}

            //if (x[3] < 193 || x[3] > 287.0966)
            //{
            //    cv++;
            //}

            //if (x[4] < 25) ||
            //(x[4] > 84.1988)
            //{
            //    cv++;
            //}

            //var y1 = x.ElementAt(1) + x.ElementAt(2) + 41.6;
            //var c1 = 0.024*x.ElementAt(3) - 4.62;
            //var y2 = 12.5/c1 + 12;
            //var c2 = 0.0003535*Math.Pow(x.ElementAt(0), 2) + 0.5311*x.ElementAt(0) + 0.08705*y2*x.ElementAt(0);
            //var c3 = 0.052*x.ElementAt(0) + 78 + 0.002377*y2*x.ElementAt(0);
            //var y3 = c2/c3;
            //var y4 = 19*y3;
            //var c4 = 0.04782*(x.ElementAt(0) - y3) + (0.1956*Math.Pow(x.ElementAt(0) - y3, 2))/x.ElementAt(1) +
            //         0.6376*y4 + 1.594*y3;
            //var c5 = 100*x.ElementAt(1);
            //var c6 = x.ElementAt(0) - y3 - y4;
            //var c7 = 0.95 - (c4/c5);
            //var y5 = c6*c7;
            //var y6 = x.ElementAt(0) - y5 - y4 - y3;
            //var c8 = (y5 + y4)*0.995;
            //var y7 = c8/y1;
            //var y8 = c8/3798;
            //var c9 = y7 - (0.0663*y7/y8) - 0.3153;
            //var y9 = 96.82/c9 + 0.321*y1;
            //var y10 = 1.29*y5 + 1.258*y4 + 2.29*y3 + 1.71*y6;
            //var y11 = 1.71*x.ElementAt(0) - 0.452*y4 + 0.58*y3;
            //var c10 = 12.3/752.3;
            //var c11 = 1.75*y2*0.995*x.ElementAt(0);
            //var c12 = 0.995*y10 + 1998;
            //var y12 = c10*x.ElementAt(0) + c11/c12;
            //var y13 = c12 - 1.75*y2;
            //var y14 = 3623 + 64.4*x.ElementAt(1) + 58.4*x.ElementAt(2) + 146312/(y9 + x.ElementAt(4));
            //var c13 = 0.995*y10 + 60.8*x.ElementAt(1) + 48*x.ElementAt(3) - 0.1121*y14 - 5095;
            //var y15 = y13/c13;
            //var y16 = 148000 - 331000*y15 + 40*y13 - 61*y15*y13;
            //var c14 = 2324*y10 - 28740000*y2;
            //var y17 = 14130000 - 1328*y10 - 531*y11 + c14/c12;
            //var c15 = y13/y15 - y13/0.52;
            //var c16 = 1.104 - 0.72*y15;
            //var c17 = y9 + x.ElementAt(4);

            //var g1 = 0.28/0.72*y5 - y4;
            //var g2 = x[2] - 1.5*x[1];
            //var g3 = 3496*(y2/c12) - 21;
            //var g4 = 110.6 + y1 - 62212/c17;
            //var g5 = 213.1 - y1;
            //var g6 = y1 - 405.23;
            //var g7 = 17.505 - y2;
            //var g8 = y2 - 1053.6667;
            //var g9 = 11.275 - y3;
            //var g10 = y3 - 35.03;
            //var g11 = 214.228 - y4;
            //var g12 = y4 - 665.585;
            //var g13 = 7.458 - y5;
            //var g14 = y5 - 584.463;
            //var g15 = 0.961 - y6;
            //var g16 = y6 - 265.916;
            //var g17 = 1.612 - y7;
            //var g18 = y7 - 7.046;
            //var g19 = 0.146 - y8;
            //var g20 = y8 - 0.222;
            //var g21 = 107.99 - y9;
            //var g22 = y9 - 273.366;
            //var g23 = 922.693 - y10;
            //var g24 = y10 - 1286.105;
            //var g25 = 926.832 - y11;
            //var g26 = y11 - 1444.046;
            //var g27 = 18.766 - y12;
            //var g28 = y12 - 537.141;
            //var g29 = 1072.163 - y13;
            //var g30 = y13 - 3247.039;
            //var g31 = 8961.448 - y14;
            //var g32 = y14 - 26844.086;
            //var g33 = 0.063 - y15;
            //var g34 = y15 - 0.386;
            //var g35 = 71084.33 - y16;
            //var g36 = -140000 + y16;
            //var g37 = 2802713 - y17;
            //var g38 = y17 - 12146108;

            //if (g1 > 0)
            //{
            //    cv++;
            //    violationCost[0] = Math.Abs(g1);
            //    violationVariables[0, 0] = 0;
            //    violationVariables[0, 1] = 3;
            //    violationVariables[0, 2] = 1;
            //    violationVariables[0, 3] = -1;
            //}

            //if (g2 > 0)
            //{
            //    cv++;
            //    violationCost[1] = Math.Abs(g2);
            //    violationVariables[1, 0] = 2;
            //    violationVariables[1, 1] = 1;
            //    violationVariables[1, 2] = -1;
            //    violationVariables[1, 3] = -1;
            //}

            //return new ConstraintViolationAndVariables(violationVariables, cv, violationCost);

        }

        #endregion

        #region G17

        private static bool CheckBoundersG17(IParticle particle)
        {
            if (particle.Position[0] < 0 || particle.Position[0] > 400)
                return false;

            if (particle.Position[1] < 0 || particle.Position[1] > 1000)
                return false;

            if (particle.Position[2] < 340 || particle.Position[2] > 420)
                return false;

            if (particle.Position[3] < 340 || particle.Position[3] > 420)
                return false;

            if (particle.Position[4] < -1000 || particle.Position[4] > 1000)
                return false;

            if (particle.Position[5] < 0 || particle.Position[5] > 0.5236)
                return false;

            return true;
        }

        private static bool CheckConstraintsG17(IParticle particle)
        {
            var x = particle.Position;
            var firstConstraint = -x[0] + 300 - x[2]*x[3]/131.078*Math.Cos(1.48477 - x[5]) +
                                  0.90798*Math.Pow(x[2], 2)/131.078*Math.Cos(1.47588);

            var secondConstraint = -x[1] - x[2]*x[3]/131.078*Math.Cos(1.48477 + x[5]) +
                                   0.90798*Math.Pow(x[3], 2)/131.078*Math.Cos(1.47588);

            var thirdConstraint = -x[4] - x[2] * x[3] / 131.078 * Math.Sin(1.48477 + x[5]) +
                                   0.90798 * Math.Pow(x[3], 2) / 131.078 * Math.Sin(1.47588);

            var fourthConstraint = 200 - x[2] * x[3] / 131.078 * Math.Sin(1.48477 - x[5]) +
                                   0.90798 * Math.Pow(x[2], 2) / 131.078 * Math.Sin(1.47588);

            const double tolerance = 1;
            if (Math.Abs(firstConstraint) > tolerance) return false;
            if (Math.Abs(secondConstraint) > tolerance) return false;
            if (Math.Abs(thirdConstraint) > tolerance) return false;
            return !(Math.Abs(fourthConstraint) > tolerance);
        }

        public ConstraintViolationAndVariables GetConstraintViolationG17(double[] x)
        {
            var cv = 0;
            var violationCost = new double[4];
            var violationVariables = new int[4,4];

            if (x[0] < 0 || x[0] > 400)
                cv++;

            if (x[1] < 0 || x[1] > 1000)
                cv++;

            if (x[2] < 340 || x[2] > 420)
                cv++;

            if (x[3] < 340 || x[3] > 420)
                cv++;

            if (x[4] < -1000 || x[4] > 1000)
                cv++;

            if (x[5] < 0 || x[5] > 0.5236)
                cv++;

            var firstConstraint = -x[0] + 300 - x[2] * x[3] / 131.078 * Math.Cos(1.48477 - x[5]) +
                                  0.90798 * Math.Pow(x[2], 2) / 131.078 * Math.Cos(1.47588);

            var secondConstraint = -x[1] - x[2] * x[3] / 131.078 * Math.Cos(1.48477 + x[5]) +
                                   0.90798 * Math.Pow(x[3], 2) / 131.078 * Math.Cos(1.47588);

            var thirdConstraint = -x[4] - x[2] * x[3] / 131.078 * Math.Sin(1.48477 + x[5]) +
                                   0.90798 * Math.Pow(x[3], 2) / 131.078 * Math.Sin(1.47588);

            var fourthConstraint = 200 - x[2] * x[3] / 131.078 * Math.Sin(1.48477 - x[5]) +
                                   0.90798 * Math.Pow(x[2], 2) / 131.078 * Math.Sin(1.47588);

            const double tolerance = 1;
            if (Math.Abs(firstConstraint) > tolerance)
            {
                cv++;
                violationCost[0] = Math.Abs(firstConstraint);
                violationVariables[0, 0] = 0;
                violationVariables[0, 1] = 2;
                violationVariables[0, 2] = 3;
                violationVariables[0, 3] = 5;
            }
            if (Math.Abs(secondConstraint) > tolerance)
            {
                cv++;
                violationCost[1] = Math.Abs(secondConstraint);
                violationVariables[1, 0] = 1;
                violationVariables[1, 1] = 2;
                violationVariables[1, 2] = 3;
                violationVariables[1, 3] = 5;
            }
            if (Math.Abs(thirdConstraint) > tolerance)
            {
                cv++;
                violationCost[2] = Math.Abs(thirdConstraint);
                violationVariables[2, 0] = 4;
                violationVariables[2, 1] = 2;
                violationVariables[2, 2] = 3;
                violationVariables[2, 3] = 5;
            }
            if (Math.Abs(fourthConstraint) > tolerance)
            {
                cv++;
                violationCost[3] = Math.Abs(fourthConstraint);
                violationVariables[3, 0] = 2;
                violationVariables[3, 1] = 3;
                violationVariables[3, 2] = 5;
                violationVariables[3, 3] = -1; //não tem mais
            }
           
            return new ConstraintViolationAndVariables(violationVariables, cv, violationCost);
        }

        #endregion

        #region G18

        private static bool CheckBoundersG18(IParticle particle)
        {
            for (var i = 0; i < 8; i++)
            {
                var particlePosition = particle.Position.ElementAt(i);
                if (particlePosition < -10 || particlePosition > 10)
                    return false;
            }

            var x9 = particle.Position.ElementAt(8);
            if (x9 < 0 || x9 > 20)
                return false;

            return true;
        }

        private static bool CheckConstraintsG18(IParticle particle)
        {
            var x = particle.Position;

            var g1 = Math.Pow(x[2], 2) + Math.Pow(x[3], 2) - 1;
            var g2 = Math.Pow(x[8], 2) -1;
            var g3 = Math.Pow(x[4], 2) + Math.Pow(x[5], 2) -1;
            var g4 = Math.Pow(x[0], 2) + Math.Pow(x[1] - x[8], 2) - 1;
            var g5 = Math.Pow(x[0] - x[4], 2) + Math.Pow(x[1] - x[5], 2) - 1;
            var g6 = Math.Pow(x[0] - x[6], 2) + Math.Pow(x[1] - x[7], 2) - 1;
            var g7 = Math.Pow(x[2] - x[4], 2) + Math.Pow(x[3] - x[5], 2) - 1;
            var g8 = Math.Pow(x[2] - x[6], 2) + Math.Pow(x[3] - x[7], 2) - 1;
            var g9 = Math.Pow(x[6], 2) + Math.Pow(x[7] - x[8], 2) - 1;
            var g10 = x[1]*x[2] - x[0]*x[3];
            var g11 = -x[2]*x[8];
            var g12 = x[4]*x[8];
            var g13 = x[5] * x[6] - x[4] * x[7];

            if (g1 > 0 || g2 > 0 || g3 > 0 || g4 > 0 || g5 > 0 || g6 > 0 || g7 > 0 || g8 > 0 || g9 > 0 || g10 > 0 ||
                g11 > 0 || g12 > 0 || g13 > 0) return false;
            return true;
        }

        public ConstraintViolationAndVariables GetConstraintViolationG18(double[] x)
        {
            var cv = 0;
            var violationCost = new double[13];
            var violationVariables = new int[13, 4];

            for (var i = 0; i < 8; i++)
            {
                if (x[i] < -10 || x[i] > 10)
                    cv++;
            }

            if (x[8] < 0 || x[8] > 20)
                cv++;

            var g1 = Math.Pow(x[2], 2) + Math.Pow(x[3], 2) - 1;
            var g2 = Math.Pow(x[8], 2) - 1;
            var g3 = Math.Pow(x[4], 2) + Math.Pow(x[5], 2) - 1;
            var g4 = Math.Pow(x[0], 2) + Math.Pow(x[1] - x[8], 2) - 1;
            var g5 = Math.Pow(x[0] - x[4], 2) + Math.Pow(x[1] - x[5], 2) - 1;
            var g6 = Math.Pow(x[0] - x[6], 2) + Math.Pow(x[1] - x[7], 2) - 1;
            var g7 = Math.Pow(x[2] - x[4], 2) + Math.Pow(x[3] - x[5], 2) - 1;
            var g8 = Math.Pow(x[2] - x[6], 2) + Math.Pow(x[3] - x[7], 2) - 1;
            var g9 = Math.Pow(x[6], 2) + Math.Pow(x[7] - x[8], 2) - 1;
            var g10 = x[1] * x[2] - x[0] * x[3];
            var g11 = -x[2] * x[8];
            var g12 = x[4] * x[8];
            var g13 = x[5] * x[6] - x[4] * x[7];

            if (g1 > 0)
            {
                cv++;
                violationCost[0] = Math.Abs(g1);
                violationVariables[0, 0] = 2;
                violationVariables[0, 1] = 3;
                violationVariables[0, 2] = -1;//não tem mais
                violationVariables[0, 3] = -1;//não tem mais
            }
            if(g2 > 0)
            {
                cv++;
                violationCost[1] = Math.Abs(g2);
                violationVariables[1, 0] = 8;
                violationVariables[1, 1] = -1;
                violationVariables[1, 2] = -1;
                violationVariables[1, 3] = -1;
            }
            if(g3 > 0)
            {
                cv++;
                violationCost[2] = Math.Abs(g3);
                violationVariables[2, 0] = 4;
                violationVariables[2, 1] = 5;
                violationVariables[2, 2] = -1;
                violationVariables[2, 3] = -1;
            }
            if(g4 > 0)
            {
                cv++;
                violationCost[3] = Math.Abs(g4);
                violationVariables[3, 0] = 0;
                violationVariables[3, 1] = 1;
                violationVariables[3, 2] = 8;
                violationVariables[3, 3] = -1; 
            }

            if(g5 > 0)
            {
                cv++;
                violationCost[4] = Math.Abs(g5);
                violationVariables[4, 0] = 0;
                violationVariables[4, 1] = 4;
                violationVariables[4, 2] = 1;
                violationVariables[4, 3] = 5;
            }

            if(g6 > 0)
            {
                cv++;
                violationCost[5] = Math.Abs(g6);
                violationVariables[5, 0] = 0;
                violationVariables[5, 1] = 6;
                violationVariables[5, 2] = 1;
                violationVariables[5, 3] = 7;
            }
            if(g7 > 0)
            {
                cv++;
                violationCost[6] = Math.Abs(g7);
                violationVariables[6, 0] = 2;
                violationVariables[6, 1] = 4;
                violationVariables[6, 2] = 3;
                violationVariables[6, 3] = 5;
            }
            if(g8 > 0)
            {
                cv++;
                violationCost[7] = Math.Abs(g8);
                violationVariables[7, 0] = 2;
                violationVariables[7, 1] = 6;
                violationVariables[7, 2] = 3;
                violationVariables[7, 3] = 7;
            }
            if(g9 > 0)
            {
                cv++;
                violationCost[8] = Math.Abs(g9);
                violationVariables[8, 0] = 6;
                violationVariables[8, 1] = 7;
                violationVariables[8, 2] = 8;
                violationVariables[8, 3] = -1;
            }

            if(g10 > 0)
            {
                cv++;
                violationCost[9] = Math.Abs(g10);
                violationVariables[9, 0] = 1;
                violationVariables[9, 1] = 2;
                violationVariables[9, 2] = 0;
                violationVariables[9, 3] = 3;
            }
            if(g11 > 0)
            {
                cv++;
                violationCost[10] = Math.Abs(g11);
                violationVariables[10, 0] = 2;
                violationVariables[10, 1] = 8;
                violationVariables[10, 2] = -1;
                violationVariables[10, 3] = -1;
            }
            if(g12 > 0)
            {
                cv++;
                violationCost[11] = Math.Abs(g12);
                violationVariables[11, 0] = 4;
                violationVariables[11, 1] = 8;
                violationVariables[11, 2] = -1;
                violationVariables[11, 3] = -1;
            }
            if(g13 > 0)
            {
                cv++;
                violationCost[12] = Math.Abs(g13);
                violationVariables[12, 0] = 5;
                violationVariables[12, 1] = 6;
                violationVariables[12, 2] = 4;
                violationVariables[12, 3] = 7;
            }

            return new ConstraintViolationAndVariables(violationVariables, cv, violationCost);
        }
        
        #endregion

        #region G19

        private static bool CheckBoundersG19(IParticle particle)
        {
            for (var i = 0; i < 15; i++)
            {
                var particlePosition = particle.Position.ElementAt(i);
                if (particlePosition < 0 || particlePosition > 10)
                    return false;
            }

            return true;
        }

        private static bool CheckConstraintsG19(IParticle particle)
        {
            var x = particle.Position;
            var b = new[] {-40, -2, -0.25, -4, -4, -1, -40, -60, 5, 1};
            var e = new int[] {-15, -27, -36, -18, -12};
            var c = new int[,]
            {
                {30, -20, -10, 32, -10}, {-20, 39, -6, -31, 32}, {-10, -6, 10, -6, -10}, {32, -31, -6, 39, -20},
                {-10, 32, -10, -20, 30}
            };
            var d = new int[] {4, 8, 10, 6, 2};
            var a = new double[,]
            {
                {-16, 2, 0, 1, 0}, {0, -2, 0, 0.4, 2}, {-3.5, 0, 2, 0, 0}, {0, -2, 0, -4, -1}, {0, -9, -2, 1, -2.8},
                {2, 0, -4, 0, 0}, {-1, -1, -1, -1, -1}, {-1, -2, -3, -2, -1}, {1, 2, 3, 4, 5}, {1, 1, 1, 1, 1}
            };

            var g1 = -2*FirstSum(x, 0, c) - 3*d[0]*Math.Pow(x[10], 2) - e[0] + SecondSum(x, 0, a);
            var g2 = -2*FirstSum(x, 1, c) - 3*d[1]*Math.Pow(x[11], 2) - e[1] + SecondSum(x, 1, a);
            var g3 = -2*FirstSum(x, 2, c) - 3*d[2]*Math.Pow(x[12], 2) - e[2] + SecondSum(x, 2, a);
            var g4 = -2*FirstSum(x, 3, c) - 3*d[3]*Math.Pow(x[13], 2) - e[3] + SecondSum(x, 3, a);
            var g5 = -2*FirstSum(x, 4, c) - 3*d[4]*Math.Pow(x[14], 2) - e[4] + SecondSum(x, 4, a);

            if (g1 > 1e-10 || g2 > 0 || g3 > 0 || g4 > 1e-10 || g5 > 0) return false;
            return true;
        }

        private static double FirstSum(double[] x, int j, int[,] c)
        {
            var accumulator = 0.0;
            for (var i = 0; i < 5; i++)
            {
                accumulator += c[i, j] * x[10 + i];
            }
            return accumulator;
        }
        private static double SecondSum(double[] x, int j, double[,] a)
        {
            var accumulator = 0.0;
            for (var i = 0; i < 10; i++)
            {
                accumulator += a[i, j] * x[i];
            }
            return accumulator;
        }
        public ConstraintViolationAndVariables GetConstraintViolationG19(double[] x)
        {
            var cv = 0;
            var violationCost = new double[5];
            var violationVariables = new int[5, 15];

            for (var i = 0; i < 15; i++)
            {
                if (x[i] < 0 || x[i] > 10)
                    cv++;
            }

            var e = new[] { -15, -27, -36, -18, -12 };
            var c = new[,]
            {
                {30, -20, -10, 32, -10}, {-20, 39, -6, -31, 32}, {-10, -6, 10, -6, -10}, {32, -31, -6, 39, -20},
                {-10, 32, -10, -20, 30}
            };
            var d = new[] { 4, 8, 10, 6, 2 };
            var a = new[,]
            {
                {-16, 2, 0, 1, 0}, {0, -2, 0, 0.4, 2}, {-3.5, 0, 2, 0, 0}, {0, -2, 0, -4, -1}, {0, -9, -2, 1, -2.8},
                {2, 0, -4, 0, 0}, {-1, -1, -1, -1, -1}, {-1, -2, -3, -2, -1}, {1, 2, 3, 4, 5}, {1, 1, 1, 1, 1}
            };

            var g1 = -2 * FirstSum(x, 0, c) - 3 * d[0] * Math.Pow(x[10], 2) - e[0] + SecondSum(x, 0, a);
            var g2 = -2 * FirstSum(x, 1, c) - 3 * d[1] * Math.Pow(x[11], 2) - e[1] + SecondSum(x, 1, a);
            var g3 = -2 * FirstSum(x, 2, c) - 3 * d[2] * Math.Pow(x[12], 2) - e[2] + SecondSum(x, 2, a);
            var g4 = -2 * FirstSum(x, 3, c) - 3 * d[3] * Math.Pow(x[13], 2) - e[3] + SecondSum(x, 3, a);
            var g5 = -2 * FirstSum(x, 4, c) - 3 * d[4] * Math.Pow(x[14], 2) - e[4] + SecondSum(x, 4, a);

            if (g1 > 1e-10)
            {
                cv++;
                violationCost[0] = Math.Abs(g1);
                GetViolationVariablesG19(violationVariables, 0);
            }

            if (g2 > 1e-10)
            {
                cv++;
                violationCost[1] = Math.Abs(g2);
                GetViolationVariablesG19(violationVariables, 1);
            }
            if (g3 > 1e-10)
            {
                cv++;
                violationCost[2] = Math.Abs(g3);
                GetViolationVariablesG19(violationVariables, 2);
            }
            if (g4 > 1e-10)
            {
                cv++;
                violationCost[3] = Math.Abs(g4);
                GetViolationVariablesG19(violationVariables, 3);
            }
            if (g5 > 1e-10)
            {
                cv++;
                violationCost[4] = Math.Abs(g5);
                GetViolationVariablesG19(violationVariables, 4);
            }

            return new ConstraintViolationAndVariables(violationVariables, cv, violationCost);
        }

        private static void GetViolationVariablesG19(int[,] violationVariables, int constraintIndex)
        {
            violationVariables[constraintIndex, 0] = 10;
            violationVariables[constraintIndex, 1] = 11;
            violationVariables[constraintIndex, 2] = 12;
            violationVariables[constraintIndex, 3] = 13;
            violationVariables[constraintIndex, 4] = 14;
            violationVariables[constraintIndex, 5] = 0;
            violationVariables[constraintIndex, 6] = 1;
            violationVariables[constraintIndex, 7] = 2;
            violationVariables[constraintIndex, 8] = 3;
            violationVariables[constraintIndex, 9] = 4;
            violationVariables[constraintIndex, 10] = 5;
            violationVariables[constraintIndex, 11] = 6;
            violationVariables[constraintIndex, 12] = 7;
            violationVariables[constraintIndex, 13] = 8;
            violationVariables[constraintIndex, 14] = 9;
        }

        #endregion

        #region G20

        private static bool CheckBoundersG20(IParticle particle)
        {
            for (var i = 0; i < 24; i++)
            {
                var particlePosition = particle.Position.ElementAt(i);
                if (particlePosition < 0 || particlePosition > 1)
                    return false;
            }

            return true;
        }

        private static bool CheckConstraintsG20(IParticle particle)
        {
            var k = 0.7302*530*(14.7/40);
            var a = new[]
            {
                0.0693, 0.0577, 0.05, 0.2, 0.26, 0.55, 0.06, 0.1, 0.12, 0.18, 0.1, 0.09, 0.0693, 0.0577, 0.05, 0.2, 0.26,
                0.55, 0.06, 0.1, 0.12, 0.18, 0.1, 0.09
            };
            var b = new[]
            {
                44.094, 58.12, 58.12, 137.4, 120.9, 170.9, 62.501, 84.94, 133.425, 82.507, 46.07, 60.097, 44.094, 58.12,
                58.12, 137.4, 120.9, 170.9, 62.501, 84.94, 133.425, 82.507, 46.07, 60.097
            };
            var c = new[]
            {
                123.7, 31.7, 45.7, 14.7, 84.7, 27.7, 49.7, 7.1, 2.1, 17.7, 0.85, 0.64
            };
            var d = new[]
            {
                31.244, 36.12, 34.784, 92.7, 82.7, 91.6, 56.708, 82.7, 80.8, 64.517, 49.4, 49.1
            };
            var e = new[]
            {
                0.1, 0.3, 0.4, 0.3, 0.6, 0.3
            };

            var x = particle.Position;

            var g1 = (x[0] + x[12]) / DenominatorSum(x, 0, e); //todo
            var g2 = (x[1] + x[13]) / DenominatorSum(x, 1, e);
            var g3 = (x[2] + x[14]) / DenominatorSum(x, 2, e);

            var g4 = (x[6] + x[18]) / DenominatorSum(x, 3, e);
            var g5 = (x[7] + x[19]) / DenominatorSum(x, 4, e);
            var g6 = (x[8] + x[20]) / DenominatorSum(x, 5, e);

//            if (g1 > 0.2 || g2 > 1e-10 || g3 > 1e-10 || g4 > 1e-10 || g5 > 1e-10 || g6 > 1e-10) return false;
            if (g1 > 0.2 || g2 > 0.0001 || g3 > 0.0001 || g4 > 0.0001 || g5 > 0.0001 || g6 > 0.0001) return false;

            const double tolerance = 0.0001;
            for (var i = 0; i < 12; i++)
            {
                var h = x[12 + i]/(b[i + 12]*SecondDenominatorSum(x, b, 12, 24)) -
                        c[i]*x[i]/(40*b[i]*SecondDenominatorSum(x, b, 0, 12));
                
                if (Math.Abs(h) > tolerance) return false;
            }

            var accum = 0.0;
            for (int i = 0; i < 24; i++)
            {
                accum += x[i];
            }
            var h13 = accum - 1;

            var h14 = ThirdDenominatorSum(x, d, 0, 12) + k*ThirdDenominatorSum(x, b, 12, 24) - 1.671;
            
            if (Math.Abs(h13) > tolerance) return false;
            if (Math.Abs(h14) > tolerance) return false; //todo 0.2
            return true;
        }

        private static double ThirdDenominatorSum(double[] x, double[] denominator, int lowerBound, int upperbound)
        {
            var accum = 0.0;
            for (int i = lowerBound; i < upperbound; i++)
            {
                accum += x[i] / denominator[i];
            }
            return accum;
        }

        private static double SecondDenominatorSum(double[] x, double[] b, int lowerBound, int upperbound)
        {
            var accum = 0.0;
            for (int j = lowerBound; j < upperbound; j++)
            {
                accum += x[j]/b[j];
            }
            return accum;
        }

        private static double DenominatorSum(double[] x, int i, double[] e)
        {
            var accumulator = 0.0;
            for (var j = 0; j < 24; j++)
            {
                accumulator += x[j] ;
            }
            return accumulator + e[i];
        }

        public ConstraintViolationAndVariables GetConstraintViolationG20(double[] x)
        {
            var cv = 0;
            var violationCost = new double[20];
            var violationVariables = new int[20, 24]; //todo

            for (var i = 0; i < 24; i++)
            {
                if (x[i] < 0 || x[i] > 10)
                    cv++;
            }

            var k = 0.7302 * 530 * (14.7 / 40);
            var b = new[]
            {
                44.094, 58.12, 58.12, 137.4, 120.9, 170.9, 62.501, 84.94, 133.425, 82.507, 46.07, 60.097, 44.094, 58.12,
                58.12, 137.4, 120.9, 170.9, 62.501, 84.94, 133.425, 82.507, 46.07, 60.097
            };
            var c = new[]
            {
                123.7, 31.7, 45.7, 14.7, 84.7, 27.7, 49.7, 7.1, 2.1, 17.7, 0.85, 0.64
            };
            var d = new[]
            {
                31.244, 36.12, 34.784, 92.7, 82.7, 91.6, 56.708, 82.7, 80.8, 64.517, 49.4, 49.1
            };
            var e = new[]
            {
                0.1, 0.3, 0.4, 0.3, 0.6, 0.3
            };
            
            var g1 = (x[0] + x[12]) / DenominatorSum(x, 0, e); //todo
            var g2 = (x[1] + x[13]) / DenominatorSum(x, 1, e);
            var g3 = (x[2] + x[14]) / DenominatorSum(x, 2, e);

            var g4 = (x[6] + x[18]) / DenominatorSum(x, 3, e);
            var g5 = (x[7] + x[19]) / DenominatorSum(x, 4, e);
            var g6 = (x[8] + x[20]) / DenominatorSum(x, 5, e);

            if (g1 > 0.2)
            {
                cv++;
                violationCost[0] = Math.Abs(g1);
                GetViolationVariableG20(violationVariables,0);
            }

            if (g2 > 0.0001)
            {
                cv++;
                violationCost[1] = Math.Abs(g2);
                GetViolationVariableG20(violationVariables, 1);
            }
            if (g3 > 0.0001)
            {
                cv++;
                violationCost[2] = Math.Abs(g3);
                GetViolationVariableG20(violationVariables, 2);
            }
            if (g4 > 0.0001)
            {
                cv++;
                violationCost[3] = Math.Abs(g4);
                GetViolationVariableG20(violationVariables, 3);
            }
            if (g5 > 0.0001)
            {
                cv++;
                violationCost[4] = Math.Abs(g5);
                GetViolationVariableG20(violationVariables, 4);
            }
            if (g6 > 0.0001)
            {
                cv++;
                violationCost[5] = Math.Abs(g6);
                GetViolationVariableG20(violationVariables, 5);
            }

            const double tolerance = 0.0001;
            for (var i = 0; i < 12; i++)
            {
                var h = x[12 + i] / (b[i + 12] * SecondDenominatorSum(x, b, 12, 24)) -
                        c[i] * x[i] / (40 * b[i] * SecondDenominatorSum(x, b, 0, 12));

                if (Math.Abs(h) > tolerance)
                {
                    cv++;
                    violationCost[6+i] = Math.Abs(h);
                    GetViolationVariableG20(violationVariables, 6+i);
                }
            }

            var accum = 0.0;
            for (int i = 0; i < 24; i++)
            {
                accum += x[i];
            }
            var h13 = accum - 1;

            var h14 = ThirdDenominatorSum(x, d, 0, 12) + k * ThirdDenominatorSum(x, b, 12, 24) - 1.671;

            if (Math.Abs(h13) > tolerance)
            {
                cv++;
                violationCost[18] = Math.Abs(h13);
                GetViolationVariableG20(violationVariables, 18);
            }
            if (Math.Abs(h14) > tolerance) //todo: 0.2
            {
                cv++;
                violationCost[19] = Math.Abs(h14);
                GetViolationVariableG20(violationVariables, 19);
            }

            return new ConstraintViolationAndVariables(violationVariables, cv, violationCost);
        }

        private static void GetViolationVariableG20(int[,] violationVariables, int constraintIndex)
        {
            violationVariables[constraintIndex, 0] = 0;
            violationVariables[constraintIndex, 1] = 1;
            violationVariables[constraintIndex, 2] = 2;
            violationVariables[constraintIndex, 3] = 3;
            violationVariables[constraintIndex, 4] = 4;
            violationVariables[constraintIndex, 5] = 5;
            violationVariables[constraintIndex, 6] = 6;
            violationVariables[constraintIndex, 7] = 7;
            violationVariables[constraintIndex, 8] = 8;
            violationVariables[constraintIndex, 9] = 9;
            violationVariables[constraintIndex, 10] = 10;
            violationVariables[constraintIndex, 11] = 11;
            violationVariables[constraintIndex, 12] = 12;
            violationVariables[constraintIndex, 13] = 13;
            violationVariables[constraintIndex, 14] = 14;
            violationVariables[constraintIndex, 15] = 15;
            violationVariables[constraintIndex, 16] = 16;
            violationVariables[constraintIndex, 17] = 17;
            violationVariables[constraintIndex, 18] = 18;
            violationVariables[constraintIndex, 19] = 19;
            violationVariables[constraintIndex, 20] = 20;
            violationVariables[constraintIndex, 21] = 21;
            violationVariables[constraintIndex, 22] = 22;
            violationVariables[constraintIndex, 23] = 23;
        }

        #endregion

        #region G21

        private static bool CheckBoundersG21(IParticle particle)
        {
            var x = particle.Position;

            if (x[0] < 0 || x[0] > 1000) return false;
            if (x[1] < 0 || x[1] > 40) return false;
            if (x[2] < 0 || x[2] > 40) return false;
            if (x[3] < 100 || x[3] > 300) return false;
            if (x[4] < 6.3 || x[4] > 6.7) return false;
            if (x[5] < 5.9 || x[5] > 6.4) return false;
            if (x[6] < 4.5 || x[6] > 6.25) return false;

            return true;
        }

        private static bool CheckConstraintsG21(IParticle particle)
        {
            var x = particle.Position;

            var g1 = -x[0] + 35*Math.Pow(x[1], 0.6) + 35*Math.Pow(x[2], 0.6);
            if (g1 > 0) return false;

            var h1 = -300*x[2] + 7500*x[4] - 7500*x[5] - 25*x[3]*x[4] + 25*x[3]*x[5] + x[2]*x[3];
            var h2 = 100*x[1] + 155.365*x[3] + 2500*x[6] - x[1]*x[3] - 25*x[3]*x[6] - 15536.5;
            var h3 = -x[4] + Math.Log(-x[3] + 900);
            var h4 = -x[5] + Math.Log(x[3] + 300);
            var h5 = -x[6] + Math.Log(-2*x[3] + 700);

            const double tolerance = 0.05;
            if (Math.Abs(h1) > tolerance) return false;
            if (Math.Abs(h2) > tolerance) return false;
            if (Math.Abs(h3) > tolerance) return false;
            if (Math.Abs(h4) > tolerance) return false;
            return !(Math.Abs(h5) > tolerance);
        }

        public ConstraintViolationAndVariables GetConstraintViolationG21(double[] x)
        {
            var cv = 0;
            var violationCost = new double[6];
            var violationVariables = new int[6, 4]; //todo

            if (x[0] < 0 || x[0] > 1000) {cv++;}
            if (x[1] < 0 || x[1] > 40) { cv++; }
            if (x[2] < 0 || x[2] > 40) { cv++; }
            if (x[3] < 100 || x[3] > 300) { cv++; }
            if (x[4] < 6.3 || x[4] > 6.7) { cv++; }
            if (x[5] < 5.9 || x[5] > 6.4) { cv++; }
            if (x[6] < 4.5 || x[6] > 6.25) { cv++; }

            var g1 = -x[0] + 35 * Math.Pow(x[1], 0.6) + 35 * Math.Pow(x[2], 0.6);
            
            if (g1 > 0)
            {
                cv++;
                violationCost[0] = Math.Abs(g1);
                violationVariables[0, 0] = 0;
                violationVariables[0, 1] = 1;
                violationVariables[0, 2] = 2;
                violationVariables[0, 3] = -1;
            }

            var h1 = -300 * x[2] + 7500 * x[4] - 7500 * x[5] - 25 * x[3] * x[4] + 25 * x[3] * x[5] + x[2] * x[3];
            var h2 = 100 * x[1] + 155.365 * x[3] + 2500 * x[6] - x[1] * x[3] - 25 * x[3] * x[6] - 15536.5;
            var h3 = -x[4] + Math.Log(-x[3] + 900);
            var h4 = -x[5] + Math.Log(x[3] + 300);
            var h5 = -x[6] + Math.Log(-2 * x[3] + 700);

            const double tolerance = 0.05;
            
            if (Math.Abs(h1) > tolerance)
            {
                cv++;
                violationCost[1] = Math.Abs(h1);
                violationVariables[1, 0] = 2;
                violationVariables[1, 1] = 4;
                violationVariables[1, 2] = 5;
                violationVariables[1, 3] = 3;
            }
            if (Math.Abs(h2) > tolerance)
            {
                cv++;
                violationCost[2] = Math.Abs(h2);
                violationVariables[2, 0] = 1;
                violationVariables[2, 1] = 3;
                violationVariables[2, 2] = 6;
                violationVariables[2, 3] = -1;
            }
            if (Math.Abs(h3) > tolerance)
            {
                cv++;
                violationCost[3] = Math.Abs(h3);
                violationVariables[3, 0] = 4;
                violationVariables[3, 1] = 3;
                violationVariables[3, 2] = -1;
                violationVariables[3, 3] = -1;
            }
            if (Math.Abs(h4) > tolerance)
            {
                cv++;
                violationCost[4] = Math.Abs(h4);
                violationVariables[4, 0] = 5;
                violationVariables[4, 1] = 3;
                violationVariables[4, 2] = -1;
                violationVariables[4, 3] = -1;
            }
            if (Math.Abs(h5) > tolerance)
            {
                cv++;
                violationCost[5] = Math.Abs(h5);
                violationVariables[5, 0] = 6;
                violationVariables[5, 1] = 3;
                violationVariables[5, 2] = -1;
                violationVariables[5, 3] = -1;
            }

            return new ConstraintViolationAndVariables(violationVariables, cv, violationCost);
        }

        #endregion

        #region G22

        //private static bool CheckBoundersG22(IParticle particle)
        //{
        //    var x = particle.Position;
        //    if (x[0] < 0 || x[0] > 20000) return false;

        //    for (var i = 1; i < 4; i++)
        //    {
        //        if (x[i] < 0 || x[i] > 10e6)
        //            return false;
        //    }
        //    for (var i = 4; i < 7; i++)
        //    {
        //        if (x[i] < 0 || x[i] > 4 * 10e7)
        //            return false;
        //    }
        //    if (x[7] < 100 || x[7] > 299.99) return false;
        //    if (x[8] < 100 || x[8] > 399.99) return false;
        //    if (x[9] < 100.01 || x[9] > 300) return false;
        //    if (x[10] < 100 || x[10] > 400) return false;
        //    if (x[11] < 100 || x[11] > 600) return false;

        //    for (var i = 12; i < 15; i++)
        //    {
        //        if (x[i] < 0 || x[i] > 500)
        //            return false;
        //    }

        //    if (x[15] < 0.01 || x[15] > 300) return false;
        //    if (x[16] < 0.01 || x[16] > 400) return false;

        //    for (var i = 17; i < 22; i++)
        //    {
        //        if (x[i] < -4.7 || x[i] > 6.25)
        //            return false;
        //    }

        //    return true;
        //}

        private static bool CheckBoundersG22(IParticle particle)
        {
            var x = particle.Position;
            if (x[0] < 200 || x[0] > 300) return false;

            for (var i = 1; i < 3; i++)
            {
                if (x[i] < 100 || x[i] > 250)
                    return false;
            }
            for (var i = 3; i < 4; i++)
            {
                if (x[i] < 6000 || x[i] > 7000)
                    return false;
            }
           
            for (var i = 4; i < 7; i++)
            {
                if (x[i] < 10e5 || x[i] > 4 * 10e7)
                    return false;
            }
            if (x[7] < 100 || x[7] > 200) return false;
            if (x[8] < 100 || x[8] > 200) return false;
            if (x[9] < 200 || x[9] > 300) return false;
            if (x[10] < 300 || x[10] > 400) return false;
            if (x[11] < 300 || x[11] > 400) return false;

            for (var i = 12; i < 15; i++)
            {
                if (x[i] < 100 || x[i] > 250)
                    return false;
            }

            if (x[15] < 200 || x[15] > 300) return false;
            if (x[16] < 100 || x[16] > 200) return false;

            for (var i = 17; i < 22; i++)
            {
                if (x[i] < 5 || x[i] > 6)
                    return false;
            }

            return true;
        }
        
        private static bool CheckConstraintsG22(IParticle particle)
        {
            var x = particle.Position;

            var g1 = -x[0] + Math.Pow(x[1], 0.6) + Math.Pow(x[2], 0.6) + Math.Pow(x[3], 0.6);
            if (g1 > 1) return false;

            var h1 = x[4] - 100000 * x[7] + 1e7;
            var h2 = x[5] + 100000 * x[7] - 100000 * x[8];
            var h3 = x[6] + 100000 * x[8] - 5e7;
            var h4 = x[4] + 100000 * x[9] - 3.3e7;
            var h5 = x[5] + 100000 * x[10] - 4.4e7;
            var h6 = x[6] + 100000 * x[11] - 6.6e7;
            var h7 = x[4] - 120 * x[1] * x[12];
            var h8 = x[5] - 80 * x[2] * x[13];
            var h9 = x[6] - 40 * x[3] * x[14];
            var h10 = x[7] - x[10] + x[15];
            var h11 = x[8] - x[11] + x[16];
            var h12 = -x[17] + Math.Log(x[9] - 100);
            var h13 = -x[18] + Math.Log(-x[7] + 300);
            var h14 = -x[19] + Math.Log(x[15]);
            var h15 = -x[20] + Math.Log(-x[8] + 400);
            var h16 = -x[21] + Math.Log(x[16]);
            var h17 = -x[7] - x[9] + x[12] * x[17] - x[12] * x[18] + 400;
            var h18 = x[7] - x[8] - x[10] + x[13] * x[19] - x[13] * x[20] + 400;
            var h19 = x[8] - x[11] - 4.60517 * x[14] + x[14] * x[21] + 100;

            const double tolerance = 1;
            if (Math.Abs(h1) > tolerance) return false;
            if (Math.Abs(h2) > tolerance) return false;
            if (Math.Abs(h3) > tolerance) return false;
            if (Math.Abs(h4) > tolerance) return false;
            if (Math.Abs(h5) > tolerance) return false;
            if (Math.Abs(h6) > tolerance) return false;
            if (Math.Abs(h7) > tolerance) return false;
            if (Math.Abs(h8) > tolerance) return false;
            if (Math.Abs(h9) > tolerance) return false;
            if (Math.Abs(h10) > tolerance) return false;
            if (Math.Abs(h11) > tolerance) return false;
            if (Math.Abs(h12) > tolerance) return false;
            if (Math.Abs(h13) > tolerance) return false;
            if (Math.Abs(h14) > tolerance) return false;
            if (Math.Abs(h15) > tolerance) return false;
            if (Math.Abs(h16) > tolerance) return false;
            if (Math.Abs(h17) > tolerance) return false;
            if (Math.Abs(h18) > tolerance) return false;
            if (Math.Abs(h19) > tolerance) return false;
            return !(Math.Abs(h3) > tolerance);
        }

        public ConstraintViolationAndVariables GetConstraintViolationG22(double[] x)
        {
            var cv = 0;
            var violationCost = new double[20];
            var violationVariables = new int[20, 6]; //todo

            if (x[0] < 0 || x[0] > 20000) { cv++; }

            for (var i = 1; i < 4; i++)
            {
                if (x[i] < 0 || x[i] > 10e6)
                { cv++; }
            }
            for (var i = 4; i < 7; i++)
            {
                if (x[i] < 0 || x[i] > 4 * 10e7)
                { cv++; }
            }
            if (x[7] < 100 || x[7] > 299.99) { cv++; }
            if (x[8] < 100 || x[8] > 399.99) { cv++; }
            if (x[9] < 100.01 || x[9] > 300) { cv++; }
            if (x[10] < 100 || x[10] > 400) { cv++; }
            if (x[11] < 100 || x[11] > 600) { cv++; }

            for (var i = 12; i < 15; i++)
            {
                if (x[i] < 0 || x[i] > 500)
                { cv++; }
            }

            if (x[15] < 0.01 || x[15] > 300) { cv++; }
            if (x[16] < 0.01 || x[16] > 400) { cv++; }

            for (var i = 17; i < 22; i++)
            {
                if (x[i] < -4.7 || x[i] > 6.25)
                { cv++; }
            }

            var g1 = -x[0] + Math.Pow(x[1], 0.6) + Math.Pow(x[2], 0.6) + Math.Pow(x[3], 0.6);

            if (g1 > 1)
            {
                cv++;
                violationCost[0] = Math.Abs(g1);
                violationVariables[0, 0] = 0;
                violationVariables[0, 1] = 1;
                violationVariables[0, 2] = 2;
                violationVariables[0, 3] = 3;
                violationVariables[0, 4] = -1;
                violationVariables[0, 5] = -1;
            }

            var h1 = x[4] - 100000 * x[7] + 1e7;
            var h2 = x[5] + 100000 * x[7] - 100000 * x[8];
            var h3 = x[6] + 100000 * x[8] - 5e7;
            var h4 = x[4] + 100000 * x[9] - 3.3e7;
            var h5 = x[5] + 100000 * x[10] - 4.4e7;
            var h6 = x[6] + 100000 * x[11] - 6.6e7;
            var h7 = x[4] - 120 * x[1] * x[12];
            var h8 = x[5] - 80 * x[2] * x[13];
            var h9 = x[6] - 40 * x[3] * x[14];
            var h10 = x[7] - x[10] + x[15];
            var h11 = x[8] - x[11] + x[16];
            var h12 = -x[17] + Math.Log(x[9] - 100);
            var h13 = -x[18] + Math.Log(-x[7] + 300);
            var h14 = -x[19] + Math.Log(x[15]);
            var h15 = -x[20] + Math.Log(-x[8] + 400);
            var h16 = -x[21] + Math.Log(x[16]);
            var h17 = -x[7] - x[9] + x[12] * x[17] - x[12] * x[18] + 400;
            var h18 = x[7] - x[8] - x[10] + x[13] * x[19] - x[13] * x[20] + 400;
            var h19 = x[8] - x[11] - 4.60517 * x[14] + x[14] * x[21] + 100;

            const double tolerance = 1;

            if (Math.Abs(h1) > tolerance)
            {
                cv++;
                violationCost[1] = Math.Abs(h1);
                violationVariables[1, 0] = 4;
                violationVariables[1, 1] = 7;
                violationVariables[1, 2] = -1;
                violationVariables[1, 3] = -1;
                violationVariables[1, 4] = -1;
                violationVariables[1, 5] = -1;
            }
            if (Math.Abs(h2) > tolerance)
            {
                cv++;
                violationCost[2] = Math.Abs(h2);
                violationVariables[2, 0] = 5;
                violationVariables[2, 1] = 7;
                violationVariables[2, 2] = 8;
                violationVariables[2, 3] = -1;
                violationVariables[2, 4] = -1;
                violationVariables[2, 5] = -1;
            }
            if (Math.Abs(h3) > tolerance)
            {
                cv++;
                violationCost[3] = Math.Abs(h3);
                violationVariables[3, 0] = 6;
                violationVariables[3, 1] = 8;
                violationVariables[3, 2] = -1;
                violationVariables[3, 3] = -1;
                violationVariables[3, 4] = -1;
                violationVariables[3, 5] = -1;
            }
            if (Math.Abs(h4) > tolerance)
            {
                cv++;
                violationCost[4] = Math.Abs(h4);
                violationVariables[4, 0] = 4;
                violationVariables[4, 1] = 9;
                violationVariables[4, 2] = -1;
                violationVariables[4, 3] = -1;
                violationVariables[4, 4] = -1;
                violationVariables[4, 5] = -1;
            }
            if (Math.Abs(h5) > tolerance)
            {
                cv++;
                violationCost[5] = Math.Abs(h5);
                violationVariables[5, 0] = 5;
                violationVariables[5, 1] = 10;
                violationVariables[5, 2] = -1;
                violationVariables[5, 3] = -1;
                violationVariables[5, 4] = -1;
                violationVariables[5, 5] = -1;
            }
            if (Math.Abs(h6) > tolerance)
            {
                cv++;
                violationCost[6] = Math.Abs(h6);
                violationVariables[6, 0] = 6;
                violationVariables[6, 1] = 11;
                violationVariables[6, 2] = -1;
                violationVariables[6, 3] = -1;
                violationVariables[6, 4] = -1;
                violationVariables[6, 5] = -1;
            }
            if (Math.Abs(h7) > tolerance)
            {
                cv++;
                violationCost[7] = Math.Abs(h7);
                violationVariables[7, 0] = 4;
                violationVariables[7, 1] = 1;
                violationVariables[7, 2] = 12;
                violationVariables[7, 3] = -1;
                violationVariables[7, 4] = -1;
                violationVariables[7, 5] = -1;
            }
            if (Math.Abs(h8) > tolerance)
            {
                cv++;
                violationCost[8] = Math.Abs(h8);
                violationVariables[8, 0] = 5;
                violationVariables[8, 1] = 2;
                violationVariables[8, 2] = 13;
                violationVariables[8, 3] = -1;
                violationVariables[8, 4] = -1;
                violationVariables[8, 5] = -1;
            }
            if (Math.Abs(h9) > tolerance)
            {
                cv++;
                violationCost[9] = Math.Abs(h9);
                violationVariables[9, 0] = 6;
                violationVariables[9, 1] = 3;
                violationVariables[9, 2] = 14;
                violationVariables[9, 3] = -1;
                violationVariables[9, 4] = -1;
                violationVariables[9, 5] = -1;
            }
            if (Math.Abs(h10) > tolerance)
            {
                cv++;
                violationCost[10] = Math.Abs(h10);
                violationVariables[10, 0] = 7;
                violationVariables[10, 1] = 10;
                violationVariables[10, 2] = 15;
                violationVariables[10, 3] = -1;
                violationVariables[10, 4] = -1;
                violationVariables[10, 5] = -1;
            }
            if (Math.Abs(h11) > tolerance)
            {
                cv++;
                violationCost[11] = Math.Abs(h11);
                violationVariables[11, 0] = 8;
                violationVariables[11, 1] = 11;
                violationVariables[11, 2] = 16;
                violationVariables[11, 3] = -1;
                violationVariables[11, 4] = -1;
                violationVariables[11, 5] = -1;
            }
            if (Math.Abs(h12) > tolerance)
            {
                cv++;
                violationCost[12] = Math.Abs(h12);
                violationVariables[12, 0] = 17;
                violationVariables[12, 1] = 9;
                violationVariables[12, 2] = -1;
                violationVariables[12, 3] = -1;
                violationVariables[12, 4] = -1;
                violationVariables[12, 5] = -1;
            }
            if (Math.Abs(h13) > tolerance)
            {
                cv++;
                violationCost[13] = Math.Abs(h13);
                violationVariables[13, 0] = 18;
                violationVariables[13, 1] = 7;
                violationVariables[13, 2] = -1;
                violationVariables[13, 3] = -1;
                violationVariables[13, 4] = -1;
                violationVariables[13, 5] = -1;
            }
            if (Math.Abs(h14) > tolerance)
            {
                cv++;
                violationCost[14] = Math.Abs(h14);
                violationVariables[14, 0] = 19;
                violationVariables[14, 1] = 15;
                violationVariables[14, 2] = -1;
                violationVariables[14, 3] = -1;
                violationVariables[14, 4] = -1;
                violationVariables[14, 5] = -1;
            }
            if (Math.Abs(h15) > tolerance)
            {
                cv++;
                violationCost[15] = Math.Abs(h15);
                violationVariables[15, 0] = 20;
                violationVariables[15, 1] = 8;
                violationVariables[15, 2] = -1;
                violationVariables[15, 3] = -1;
                violationVariables[15, 4] = -1;
                violationVariables[15, 5] = -1;
            }
            if (Math.Abs(h16) > tolerance)
            {
                cv++;
                violationCost[16] = Math.Abs(h16);
                violationVariables[16, 0] = 21;
                violationVariables[16, 1] = 16;
                violationVariables[16, 2] = -1;
                violationVariables[16, 3] = -1;
                violationVariables[16, 4] = -1;
                violationVariables[16, 5] = -1;
            }
            if (Math.Abs(h17) > tolerance)
            {
                cv++;
                violationCost[17] = Math.Abs(h17);
                violationVariables[17, 0] = 7;
                violationVariables[17, 1] = 9;
                violationVariables[17, 2] = 12;
                violationVariables[17, 3] = 17;
                violationVariables[17, 4] = 18;
                violationVariables[17, 5] = -1;
            }
            if (Math.Abs(h18) > tolerance)
            {
                cv++;
                violationCost[18] = Math.Abs(h18);
                violationVariables[18, 0] = 7;
                violationVariables[18, 1] = 8;
                violationVariables[18, 2] = 10;
                violationVariables[18, 3] = 13;
                violationVariables[18, 4] = 19;
                violationVariables[18, 5] = 20;
            }
            if (Math.Abs(h19) > tolerance)
            {
                cv++;
                violationCost[19] = Math.Abs(h19);
                violationVariables[19, 0] = 8;
                violationVariables[19, 1] = 11;
                violationVariables[19, 2] = 14;
                violationVariables[19, 3] = 21;
                violationVariables[19, 4] = -1;
                violationVariables[19, 5] = -1;
            }

            return new ConstraintViolationAndVariables(violationVariables, cv, violationCost);
        }

        #endregion

        #region G23

        private static bool CheckBoundersG23(IParticle particle)
        {
            var x = particle.Position;
            for (var i = 0; i < 2; i++)
            {
                if (x[i] < 0 || x[i] > 100)
                    return false;
            }

            if (x[2] < 0 || x[2] > 100) return false;
            if (x[3] < 0 || x[3] > 100) return false;
            if (x[4] < 0 || x[4] > 100) return false;
            if (x[5] < 0 || x[5] > 300) return false;
            if (x[6] < 0 || x[6] > 100) return false;
            if (x[7] < 0 || x[7] > 200) return false;
            if (x[8] < 0.01 || x[8] > 0.03) return false;

            return true;
        }

        private static bool CheckConstraintsG23(IParticle particle)
        {
            var x = particle.Position;

            var g1 = x[8]*x[2] + 0.02*x[5] - 0.025*x[4];
            if (g1 > 0) return false;

            var g2 = x[8]*x[3] + 0.02*x[6] - 0.015*x[7];
            if (g2 > 0) return false;

            var h1 = x[0] + x[1] - x[2] - x[3];
            var h2 = 0.03*x[0] + 0.01*x[1] - x[8]*(x[2] + x[3]);
            var h3 = x[2] + x[5] - x[4];
            var h4 = x[3] + x[6] - x[7];

            const double tolerance = 0.05;
            if (Math.Abs(h1) > tolerance) return false;
            if (Math.Abs(h2) > tolerance) return false;
            if (Math.Abs(h3) > tolerance) return false;
            return !(Math.Abs(h4) > tolerance);
        }

        public ConstraintViolationAndVariables GetConstraintViolationG23(double[] x)
        {
            var cv = 0;
            var violationCost = new double[6];
            var violationVariables = new int[6, 5]; //todo

            for (var i = 0; i < 2; i++)
            {
                if (x[i] < 0 || x[i] > 100)
                { cv++; }
            }

            if (x[2] < 0 || x[2] > 100) { cv++; }
            if (x[3] < 0 || x[3] > 100) { cv++; }
            if (x[4] < 0 || x[4] > 100) { cv++; }
            if (x[5] < 0 || x[5] > 300) { cv++; }
            if (x[6] < 0 || x[6] > 100) { cv++; }
            if (x[7] < 0 || x[7] > 200) { cv++; }
            if (x[8] < 0.01 || x[8] > 0.03) { cv++; }

            var g1 = x[8] * x[2] + 0.02 * x[5] - 0.025 * x[4];
            var g2 = x[8] * x[3] + 0.02 * x[6] - 0.015 * x[7];

            if (g1 > 0)
            {
                cv++;
                violationCost[0] = Math.Abs(g1);
                violationVariables[0, 0] = 8;
                violationVariables[0, 1] = 2;
                violationVariables[0, 2] = 5;
                violationVariables[0, 3] = 4;
                violationVariables[0, 4] = -1;
            }
            if (g2 > 0)
            {
                cv++;
                violationCost[2] = Math.Abs(g2);
                violationVariables[1, 0] = 8;
                violationVariables[1, 1] = 3;
                violationVariables[1, 2] = 6;
                violationVariables[1, 3] = 7;
                violationVariables[1, 4] = -1;
            }

            var h1 = x[0] + x[1] - x[2] - x[3];
            var h2 = 0.03 * x[0] + 0.01 * x[1] - x[8] * (x[2] + x[3]);
            var h3 = x[2] + x[5] - x[4];
            var h4 = x[3] + x[6] - x[7];

            const double tolerance = 0.05;

            if (Math.Abs(h1) > tolerance)
            {
                cv++;
                violationCost[2] = Math.Abs(h1);
                violationVariables[2, 0] = 0;
                violationVariables[2, 1] = 1;
                violationVariables[2, 2] = 2;
                violationVariables[2, 3] = 3;
                violationVariables[2, 4] = -1;
            }
            if (Math.Abs(h2) > tolerance)
            {
                cv++;
                violationCost[3] = Math.Abs(h2);
                violationVariables[3, 0] = 0;
                violationVariables[3, 1] = 1;
                violationVariables[3, 2] = 8;
                violationVariables[3, 3] = 2;
                violationVariables[3, 4] = 3;
            }
            if (Math.Abs(h3) > tolerance)
            {
                cv++;
                violationCost[4] = Math.Abs(h3);
                violationVariables[4, 0] = 2;
                violationVariables[4, 1] = 5;
                violationVariables[4, 2] = 4;
                violationVariables[4, 3] = -1;
                violationVariables[4, 4] = -1;
            }
            if (Math.Abs(h4) > tolerance)
            {
                cv++;
                violationCost[5] = Math.Abs(h4);
                violationVariables[5, 0] = 3;
                violationVariables[5, 1] = 6;
                violationVariables[5, 2] = 7;
                violationVariables[5, 3] = -1;
                violationVariables[5, 4] = -1;
            }

            return new ConstraintViolationAndVariables(violationVariables, cv, violationCost);
        }

        #endregion

        #region G24

        private static bool CheckBoundersG24(IParticle particle)
        {
            var x = particle.Position;
            if (x[0] < 0 || x[0] > 3) return false;
            if (x[1] < 0 || x[1] > 4) return false;

            return true;
        }

        private static bool CheckConstraintsG24(IParticle particle)
        {
            var x = particle.Position;

            var g1 = -2 * Math.Pow(x[0], 4) + 8 * Math.Pow(x[0], 3) - 8 * Math.Pow(x[0], 2) + x[1] - 2;
            if (g1 > 0) return false;

            var g2 = -4*Math.Pow(x[0], 4) + 32*Math.Pow(x[0], 3) - 88*Math.Pow(x[0], 2) + 96*x[0] + x[1] - 36;
            if (g2 > 1e-12) return false; //todo: verificar se não é melhor fazer isso em todos

            return true;
        }

        public ConstraintViolationAndVariables GetConstraintViolationG24(double[] x)
        {
            var cv = 0;
            var violationCost = new double[2];
            var violationVariables = new int[2, 2]; //todo

            if (x[0] < 0 || x[0] > 3) { cv++; }
            if (x[1] < 0 || x[1] > 4) { cv++; }

            var g1 = -2 * Math.Pow(x[0], 4) + 8 * Math.Pow(x[0], 3) - 8 * Math.Pow(x[0], 2) + x[1] - 2;
            var g2 = -4 * Math.Pow(x[0], 4) + 32 * Math.Pow(x[0], 3) - 88 * Math.Pow(x[0], 2) + 96 * x[0] + x[1] - 36;
            
            if (g1 > 0)
            {
                cv++;
                violationCost[0] = Math.Abs(g1);
                violationVariables[0, 0] = 0;
                violationVariables[0, 1] = 1;
            }
            if (g2 > 1e-12)
            {
                cv++;
                violationCost[1] = Math.Abs(g2);
                violationVariables[1, 0] = 0;
                violationVariables[1, 1] = 1;
            }
            
            return new ConstraintViolationAndVariables(violationVariables, cv, violationCost);
        }


        #endregion
        
        #region Harold
        private bool CheckBoundersGriewank30(IParticle particle)
        {
            for (var index = 0; index < 10; index++)
            {
                var particleValue = particle.Position.ElementAt(index);
                if (particleValue < -30 || particleValue > 30)
                    return false;
            }
            return true;
        }

        private bool CheckBoundersRastrigin5Dot12(IParticle particle)
        {
            for (var index = 0; index < 10; index++)
            {
                var particleValue = particle.Position.ElementAt(index);
                if (particleValue < -5.12 || particleValue > 5.12)
                    return false;
            }
            return true;
        }

        private bool CheckBoundersSphere600(IParticle particle)
        {
            for (var index = 0; index < 10; index++)
            {
                var particleValue = particle.Position.ElementAt(index);
                if (particleValue < -600 || particleValue > 600)
                    return false;
            }
            return true;
        }

        private bool CheckBoundersRosenbrockMinus9To11(IParticle particle)
        {
            for (var index = 0; index < 10; index++)
            {
                var particleValue = particle.Position.ElementAt(index);
                if (particleValue < -9 || particleValue > 11)
                    return false;
            }
            return true;
        }

        private static bool CheckBoundersAckley30(IParticle particle)
        {
            for (var index = 0; index < 10; index++)
            {
                var particleValue = particle.Position.ElementAt(index);
                if (particleValue < -30 || particleValue > 30)
                    return false;
            }
            return true;
        }
        #endregion

    }
}
