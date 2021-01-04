using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PSO.Benchmarks;
using PSO.Benchmarks.NonLinearConstraints;
using PSO.Interfaces;
using PSO.Optimization.Model;
using PSO.Optimization.ParticlesNLC;
using PSO.Optimization.ParticlesNlcDouble;
using PSO.ParticlesNLC;
using PSO.ParticlesNlcDouble;

namespace PSO.Tests.PSO.Benchmarks.Tests
{
    [TestClass]
    public class NonLinearConstraintsTests
    {
        [TestMethod]
        public void TestBenchmarkG01FunctionWithGlobalBest()
        {
            var function = new G01
            {
                Dimension = 13
            };
            var x = new[] {1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 3, 3, 1};
            var cost = function.Function(x);

            var constraints = new NonLinearConstraints(BenchmarksNames.G01);
            PsoConfiguration.PopulationSize = 1;
            var swarm = new OptimizationParticleSwarmNLC(function, false);
            IParticleNLC particle = new OptimizationParticleNLC(swarm, x, x, function, constraints);
            particle.Position = x;
            var isFeasible = constraints.IsFeasible(particle);

            Assert.AreEqual(cost, -15);
            Assert.IsTrue(isFeasible);
        }

        [TestMethod]
        public void TestBenchmarkG01FunctionWithGlobalBest_2()
        {
            var function = new G01
            {
                Dimension = 13
            };
            var x = new[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 24445, 73281, 3, 1 };
            var cost = function.Function(x);

            var constraints = new NonLinearConstraints(BenchmarksNames.G01);
            PsoConfiguration.PopulationSize = 1;
            var swarm = new OptimizationParticleSwarmNLC(function, false);
            IParticleNLC particle = new OptimizationParticleNLC(swarm, x, x, function, constraints);
            particle.Position = x;
            var isFeasible = constraints.IsFeasible(particle);

            Assert.AreEqual(cost, -97735);
            Assert.IsFalse(isFeasible);
        }

        [TestMethod]
        public void TestBenchmarkG01FunctionWithGlobalBest_3()
        {
            var function = new G01
            {
                Dimension = 13
            };
            var x = new[] { 458, 1,-474, 580, 384, 74, -46, 267, 222, 78, 2, 0, -605};
            var cost = function.Function(x);

            var constraints = new NonLinearConstraints(BenchmarksNames.G01);
            PsoConfiguration.PopulationSize = 1;
            var swarm = new OptimizationParticleSwarmNLC(function, false);
            IParticleNLC particle = new OptimizationParticleNLC(swarm, x, x, function, constraints);
            particle.Position = x;
            var isFeasible = constraints.IsFeasible(particle);

            Assert.AreEqual(cost, -3851756);
            Assert.IsFalse(isFeasible);
        }

        [TestMethod]
        public void TestBenchmarkG01FunctionWithGlobalBest_4()
        {
            var function = new G01
            {
                Dimension = 13
            };
            var x = new[] { 1, 1, 1, 1, 2, 1, 1, 1, 0, 3, 2, 1, 1 };
            var cost = function.Function(x);

            var constraints = new NonLinearConstraints(BenchmarksNames.G01);
            PsoConfiguration.PopulationSize = 1;
            var swarm = new OptimizationParticleSwarmNLC(function, false);
            IParticleNLC particle = new OptimizationParticleNLC(swarm, x, x, function, constraints);
            particle.Position = x;
            var isFeasible = constraints.IsFeasible(particle);

            Assert.AreEqual(cost, -12);
            Assert.IsFalse(isFeasible);
        }

        [TestMethod]
        public void TestBenchmarkG01FunctionWithGlobalBest_5()
        {
            var function = new G01
            {
                Dimension = 13
            };
            var x = new[] { 1, 0, 1, 0, 1, 1, 0, 0, 1, 88, 29, 89, 1 };
            var cost = function.Function(x);

            var constraints = new NonLinearConstraints(BenchmarksNames.G01);
            PsoConfiguration.PopulationSize = 1;
            var swarm = new OptimizationParticleSwarmNLC(function, false);
            IParticleNLC particle = new OptimizationParticleNLC(swarm, x, x, function, constraints);
            particle.Position = x;
            var isFeasible = constraints.IsFeasible(particle);

            Assert.AreNotEqual(cost, -12);
            Assert.IsFalse(isFeasible);
        }

        [TestMethod]
        public void TestBenchmarkG01FunctionWithGlobalBest_6()
        {
            var function = new G01
            {
                Dimension = 13
            };
            var x = new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            var cost = function.Function(x);

            var constraints = new NonLinearConstraints(BenchmarksNames.G01);
            PsoConfiguration.PopulationSize = 1;
            var swarm = new OptimizationParticleSwarmNLC(function, false);
            IParticleNLC particle = new OptimizationParticleNLC(swarm, x, x, function, constraints);
            particle.Position = x;
            var isFeasible = constraints.IsFeasible(particle);

            Assert.AreEqual(cost, 0.0);
            Assert.IsTrue(isFeasible);
        }

        [TestMethod]
        public void TestBenchmarkG03FunctionWithGlobalBest()
        {
            var function = new G03
            {
                Dimension = 10
            };
            var xi = 1/Math.Sqrt(10);
            var x = new[] { xi, xi, xi, xi, xi, xi, xi, xi, xi, xi };
            var cost = function.Function(x);

            var constraints = new NonLinearConstraints(BenchmarksNames.G03);
            PsoConfiguration.PopulationSize = 1;
            ParticleSwarmNlcDouble swarm = new OptimizationParticleSwarmNlcDouble(function, true);
            IParticle particle = new OptimizationParticleNlcDouble(swarm, x, x, function, constraints);
            particle.Position = x;
            var isFeasible = constraints.IsFeasible(particle);

            Assert.AreEqual(cost, 1.0, 0.001);
            Assert.IsTrue(isFeasible);
        }

        [TestMethod]
        public void TestBenchmarkG04FunctionWithGlobalBest()
        {
            var function = new G04
            {
                Dimension = 5
            };
            var x = new[] { 78, 33, 29.995256025682, 45, 36.775812905788 };
            var cost = function.Function(x);

            var constraints = new NonLinearConstraints(BenchmarksNames.G04);
            PsoConfiguration.PopulationSize = 1;
            ParticleSwarmNlcDouble swarm = new OptimizationParticleSwarmNlcDouble(function, false);
            IParticle particle = new OptimizationParticleNlcDouble(swarm, x, x, function, constraints);
            particle.Position = x;
            var isFeasible = constraints.IsFeasible(particle);

            Assert.AreEqual(cost, -30665.539, 0.001);
            Assert.IsTrue(isFeasible);
        }

        [TestMethod]
        public void TestBenchmarkG04FunctionWithGlobalBest_2()
        {
            var function = new G04
            {
                Dimension = 5
            };
            var x = new[] { 78.142793348639231, 35.08428341324354, 29.326783477261834, 43.50399603257442, 39.5807101927836 };
            var cost = function.Function(x);

            var constraints = new NonLinearConstraints(BenchmarksNames.G04);
            PsoConfiguration.PopulationSize = 1;
            ParticleSwarmNlcDouble swarm = new OptimizationParticleSwarmNlcDouble(function, false);
            IParticle particle = new OptimizationParticleNlcDouble(swarm, x, x, function, constraints);
            particle.Position = x;
            var isFeasible = constraints.IsFeasible(particle);

            Assert.AreEqual(cost, -30685.123080930018, 0.001);
            Assert.IsFalse(isFeasible);
        }

        [TestMethod]
        public void TestBenchmarkG04FunctionWithGlobalBest_3()
        {
            var function = new G04
            {
                Dimension = 5
            };
            var x = new[] { 78, 33.0000000021055, 29.99490805, 45, 36.7759839 };
            var cost = function.Function(x);

            var constraints = new NonLinearConstraints(BenchmarksNames.G04);
            PsoConfiguration.PopulationSize = 1;
            ParticleSwarmNlcDouble swarm = new OptimizationParticleSwarmNlcDouble(function, false);
            IParticle particle = new OptimizationParticleNlcDouble(swarm, x, x, function, constraints);
            particle.Position = x;
            var isFeasible = constraints.IsFeasible(particle);

            Assert.AreEqual(cost, -30665.63937, 0.001);
            Assert.IsFalse(isFeasible);
        }

        [TestMethod]
        public void TestBenchmarkG04FunctionWithGlobalBest_4()
        {
            var function = new G04
            {
                Dimension = 5
            };
            var x = new[] {78, 33.00209802, 29.99638108, 44.99992155, 36.77299281};
            var cost = function.Function(x);

            var constraints = new NonLinearConstraints(BenchmarksNames.G04);
            PsoConfiguration.PopulationSize = 1;
            ParticleSwarmNlcDouble swarm = new OptimizationParticleSwarmNlcDouble(function, false);
            IParticle particle = new OptimizationParticleNlcDouble(swarm, x, x, function, constraints);
            particle.Position = x;
            var isFeasible = constraints.IsFeasible(particle);

            Assert.AreEqual(cost, -30665.36087, 0.001);
            Assert.IsFalse(isFeasible);
        }

        [TestMethod]
        public void TestBenchmarkG05FunctionWithGlobalBest()
        {
            var function = new G05
            {
                Dimension = 4
            };
            var x = new[] { 679.9453, 1026.067, 0.1188764, -0.3962336 };
            var cost = function.Function(x);

            var constraints = new NonLinearConstraints(BenchmarksNames.G05);
            PsoConfiguration.PopulationSize = 1;
            ParticleSwarmNlcDouble swarm = new OptimizationParticleSwarmNlcDouble(function, false);
            IParticle particle = new OptimizationParticleNlcDouble(swarm, x, x, function, constraints);
            particle.Position = x;
            var isFeasible = constraints.IsFeasible(particle);

            Assert.AreEqual(cost, 5126.4981, 0.001);
            Assert.IsTrue(isFeasible);
        }

        [TestMethod]
        public void TestBenchmarkG05FunctionWithGlobalBest2()
        {
            var function = new G05
            {
                Dimension = 4
            };
            var x = new[] { 679.9121, 1026.013, 0.118887, -0.39621 };
            var cost = function.Function(x);

            var constraints = new NonLinearConstraints(BenchmarksNames.G05);
            PsoConfiguration.PopulationSize = 1;
            ParticleSwarmNlcDouble swarm = new OptimizationParticleSwarmNlcDouble(function, false);
            IParticle particle = new OptimizationParticleNlcDouble(swarm, x, x, function, constraints);
            particle.Position = x;
            var isFeasible = constraints.IsFeasible(particle);

           //Assert.AreEqual(cost, 572.6257, 0.001);
            Assert.IsTrue(isFeasible);
        }

        [TestMethod]
        public void TestBenchmarkG05FunctionWithGlobalBest3()
        {
            var function = new G05
            {
                Dimension = 4
            };
            var x = new[] { 679.9453, 1026.067, 0.118876, -0.39623 };
            var cost = function.Function(x);

            var constraints = new NonLinearConstraints(BenchmarksNames.G05);
            PsoConfiguration.PopulationSize = 1;
            ParticleSwarmNlcDouble swarm = new OptimizationParticleSwarmNlcDouble(function, false);
            IParticle particle = new OptimizationParticleNlcDouble(swarm, x, x, function, constraints);
            particle.Position = x;
            var isFeasible = constraints.IsFeasible(particle);

            //Assert.AreEqual(cost, 572.6257, 0.001);
            Assert.IsTrue(isFeasible);
        }

        [TestMethod]
        public void TestBenchmarkG06FunctionWithGlobalBest()
        {
            var function = new G06
            {
                Dimension = 2
            };
            var x = new[] { 14.095, 0.84296 };
            var cost = function.Function(x);

            var constraints = new NonLinearConstraints(BenchmarksNames.G06);
            PsoConfiguration.PopulationSize = 1;
            var swarm = new OptimizationParticleSwarmNlcDouble(function, false);
            IParticle particle = new OptimizationParticleNlcDouble(swarm, x, x, function, constraints);
            particle.Position = x;
            var isFeasible = constraints.IsFeasible(particle);

            Assert.AreEqual(cost, -6961.81388, 0.001);
            Assert.IsTrue(isFeasible);
        }

        [TestMethod]
        public void TestBenchmarkG06FunctionWithGlobalBest_2()
        {
            var function = new G06
            {
                Dimension = 2
            };
            var x = new[] { 14.0948530438398, 0.842558527579698 };
            var cost = function.Function(x);

            var constraints = new NonLinearConstraints(BenchmarksNames.G06);
            PsoConfiguration.PopulationSize = 1;
            var swarm = new OptimizationParticleSwarmNlcDouble(function, false);
            IParticle particle = new OptimizationParticleNlcDouble(swarm, x, x, function, constraints);
            particle.Position = x;
            var isFeasible = constraints.IsFeasible(particle);

            Assert.AreEqual(cost, -6962.26415813846, 0.01);
            Assert.IsFalse(isFeasible);
        }

        [TestMethod]
        public void TestBenchmarkG06FunctionWithGlobalBest_3()
        {
            var function = new G06
            {
                Dimension = 2
            };
            var x = new[] { 14.0942507554563, 0.841442119653501 };
            var cost = function.Function(x);

            var constraints = new NonLinearConstraints(BenchmarksNames.G06);
            PsoConfiguration.PopulationSize = 1;
            var swarm = new OptimizationParticleSwarmNlcDouble(function, false);
            IParticle particle = new OptimizationParticleNlcDouble(swarm, x, x, function, constraints);
            particle.Position = x;
            var isFeasible = constraints.IsFeasible(particle);

            Assert.AreEqual(cost, -6962.26415813846, 0.01);
            Assert.IsFalse(isFeasible);
        }

        [TestMethod]
        public void TestBenchmarkG07FunctionWithGlobalBest()
        {
            var function = new G07
            {
                Dimension = 10
            };
            var x = new[] { 2.171996, 2.363683, 8.773926, 5.095984, 0.9906548, 1.430574, 1.321644, 9.828726, 8.280092, 8.375927 };
            var cost = function.Function(x);

            var constraints = new NonLinearConstraints(BenchmarksNames.G07);
            PsoConfiguration.PopulationSize = 1;
            ParticleSwarmNlcDouble swarm = new OptimizationParticleSwarmNlcDouble(function, false);
            IParticle particle = new OptimizationParticleNlcDouble(swarm, x, x, function, constraints);
            particle.Position = x;
            var isFeasible = constraints.IsFeasible(particle);

            Assert.AreEqual(cost, 24.3062091, 0.001);
            Assert.IsTrue(isFeasible);
        }
        
        [TestMethod]
        public void TestBenchmarkG08FunctionWithGlobalBest()
        {
            var function = new G08
            {
                Dimension = 2
            };
            var x = new[] { 1.2279713, 4.2453733 };
            var cost = function.Function(x);

            var constraints = new NonLinearConstraints(BenchmarksNames.G08);
            PsoConfiguration.PopulationSize = 1;
            ParticleSwarmNlcDouble swarm = new OptimizationParticleSwarmNlcDouble(function, true);
            IParticle particle = new OptimizationParticleNlcDouble(swarm, x, x, function, constraints);
            particle.Position = x;
            var isFeasible = constraints.IsFeasible(particle);

            Assert.AreEqual(cost, 0.095825, 0.001);
            Assert.IsTrue(isFeasible);
        }

        [TestMethod]
        public void TestBenchmarkG09FunctionWithGlobalBest()
        {
            var function = new G09
            {
                Dimension = 7
            };
            var x = new[] { 2.330499, 1.951372, -0.4775414, 4.365726, -0.6244870, 1.038131, 1.594227 };
            var cost = function.Function(x);

            var constraints = new NonLinearConstraints(BenchmarksNames.G09);
            PsoConfiguration.PopulationSize = 1;
            ParticleSwarmNlcDouble swarm = new OptimizationParticleSwarmNlcDouble(function, true);
            IParticle particle = new OptimizationParticleNlcDouble(swarm, x, x, function, constraints);
            particle.Position = x;
            var isFeasible = constraints.IsFeasible(particle);

            Assert.AreEqual(cost, 680.6300573, 0.001);
            Assert.IsTrue(isFeasible);
        }

        [TestMethod]
        public void TestBenchmarkG10FunctionWithGlobalBest()
        {
            var function = new G10
            {
                Dimension = 8
            };
            var x = new[] { 579.3167, 1359.943, 5110.071, 182.0174, 295.5985, 217.9799, 286.4162, 395.5979 };
            var cost = function.Function(x);

            var constraints = new NonLinearConstraints(BenchmarksNames.G10);
            PsoConfiguration.PopulationSize = 1;
            ParticleSwarmNlcDouble swarm = new OptimizationParticleSwarmNlcDouble(function, false);
            IParticle particle = new OptimizationParticleNlcDouble(swarm, x, x, function, constraints);
            particle.Position = x;
            var isFeasible = constraints.IsFeasible(particle);

            Assert.AreEqual(cost, 7049.3307, 0.001);
            Assert.IsTrue(isFeasible);
        }

        [TestMethod]
        public void TestBenchmarkG11FunctionWithGlobalBest()
        {
            var function = new G11
            {
                Dimension = 2
            };
            var x1 = 1.0/Math.Sqrt(2);
            var x2 = 1.0/2;
            var x = new[] { x1 , x2};
            var cost = function.Function(x);

            var constraints = new NonLinearConstraints(BenchmarksNames.G11);
            PsoConfiguration.PopulationSize = 1;
            ParticleSwarmNlcDouble swarm = new OptimizationParticleSwarmNlcDouble(function, false);
            IParticle particle = new OptimizationParticleNlcDouble(swarm, x, x, function, constraints);
            particle.Position = x;
            var isFeasible = constraints.IsFeasible(particle);

            Assert.AreEqual(cost, 0.75, 0.001);
            Assert.IsTrue(isFeasible);

            //segunda possivel posição
            x = new[] { -x1, x2 };
            cost = function.Function(x);
            particle.Position = x;
            isFeasible = constraints.IsFeasible(particle);

            Assert.AreEqual(cost, 0.75, 0.001);
            Assert.IsTrue(isFeasible);
        }

        [TestMethod]
        public void TestBenchmarkG11FunctionWithGlobalBest_2()
        {
            var function = new G11
            {
                Dimension = 2
            };
            var x1 = 0.697732221;
            var x2 = 0.487749565;
            var x = new[] { x1, x2 };
            var cost = function.Function(x);

            var constraints = new NonLinearConstraints(BenchmarksNames.G11);
            PsoConfiguration.PopulationSize = 1; 
            ParticleSwarmNlcDouble swarm = new OptimizationParticleSwarmNlcDouble(function, false);
            IParticle particle = new OptimizationParticleNlcDouble(swarm, x, x, function, constraints);
            particle.Position = x;
            var isFeasible = constraints.IsFeasible(particle);

            Assert.AreEqual(cost, 0.74923076, 0.001);
            Assert.IsFalse(isFeasible);
        }

        [TestMethod]
        public void TestBenchmarkG12FunctionWithGlobalBest()
        {
            var function = new G12
            {
                Dimension = 3
            };
            var x = new[] { 5, 5, 5 };
            var cost = function.Function(x);

            var constraints = new NonLinearConstraints(BenchmarksNames.G12);
            PsoConfiguration.PopulationSize = 1;
            ParticleSwarmNLC swarm = new OptimizationParticleSwarmNLC(function, true);
            IParticleNLC particle = new OptimizationParticleNLC(swarm, x, x, function, constraints);
            particle.Position = x;
            var isFeasible = constraints.IsFeasible(particle);

            Assert.AreEqual(cost, 1);
            Assert.IsTrue(isFeasible);
        }

       [TestMethod]
        public void TestBenchmarkG13FunctionWithGlobalBest()
       {
           var function = new G13 {Dimension = 5};
            var x = new[] { -1.717143, 1.595709, 1.827247, -0.7636413, -0.763645 };
            var cost = function.Function(x);

            var constraints = new NonLinearConstraints(BenchmarksNames.G13);
            PsoConfiguration.PopulationSize = 1;
            ParticleSwarmNlcDouble swarm = new OptimizationParticleSwarmNlcDouble(function, false);
            IParticle particle = new OptimizationParticleNlcDouble(swarm, x, x, function, constraints);
            particle.Position = x;
            var isFeasible = constraints.IsFeasible(particle);

            Assert.AreEqual(cost, 0.0539498, 0.001);
            Assert.IsTrue(isFeasible);
        }

        [TestMethod]
        public void TestBenchmarkG13FunctionWithGlobalBest_2()
        {
            var function = new G13 { Dimension = 5 };
            var x = new[] { -1.724734247, 1.616877525, -1.81040892, -0.707040138, 0.856289346 };
            var cost = function.Function(x);

            var constraints = new NonLinearConstraints(BenchmarksNames.G13);
            PsoConfiguration.PopulationSize = 1;
            ParticleSwarmNlcDouble swarm = new OptimizationParticleSwarmNlcDouble(function, false);
            IParticle particle = new OptimizationParticleNlcDouble(swarm, x, x, function, constraints);
            particle.Position = x;
            var isFeasible = constraints.IsFeasible(particle);

            Assert.AreEqual(cost, 0.047046725, 0.001);
            Assert.IsFalse(isFeasible);
        }


        [TestMethod]
        public void TestBenchmarkSphere600()
        {
            var function = new SphereFunction600();
            var x = new double[] { 0,0,0,0,0,0,0,0,0,0 };
            var cost = function.Function(x);

            Assert.AreEqual(cost, 0, 0.001);
        }

        [TestMethod]
        public void TestBenchmarkAckley()
        {
            var function = new AckleyFunction30();
            var x = new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            var cost = function.Function(x);

            Assert.AreEqual(cost, 0, 0.001);
        }

        [TestMethod]
        public void TestBenchmarkRastrigin()
        {
            var function = new Rastrigin5Dot12();
            var x = new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            var cost = function.Function(x);

            Assert.AreEqual(cost, 0, 0.001);
        }

        [TestMethod]
        public void TestBenchmarkRosenbrock()
        {
            var function = new RosenbrockFunctionMinus9To11();
            var x = new double[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            var cost = function.Function(x);

            Assert.AreEqual(cost, 0, 0.001);
        }

        [TestMethod]
        public void TestBenchmarkGriewank()
        {
            var function = new GriewankFunction30();
            var x = new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            var cost = function.Function(x);

            Assert.AreEqual(cost, 0, 0.001);
        }

        [TestMethod]
        public void TestBenchmarkG14()
        {
            var function = new G14 { Dimension = 10 };
            var x = new[]
            {
                0.0406684113216282, 0.147721240492452, 0.783205732104114, 0.00141433931889084, 0.485293636780388,
                0.000693183051556082, 0.0274052040687766, 0.0179509660214818, 0.0373268186859717, 0.0968844604336845
            };
            var cost = function.Function(x);

            var constraints = new NonLinearConstraints(BenchmarksNames.G14);
            PsoConfiguration.PopulationSize = 1;
            ParticleSwarmNlcDouble swarm = new OptimizationParticleSwarmNlcDouble(function, false);
            IParticle particle = new OptimizationParticleNlcDouble(swarm, x, x, function, constraints);
            particle.Position = x;
            var isFeasible = constraints.IsFeasible(particle);

            Assert.AreEqual(cost, -47.7648884594915, 0.001);
            Assert.IsTrue(isFeasible);
        }

        [TestMethod]
        public void TestBenchmarkG15()
        {
            var function = new G15 { Dimension = 3 };
            var x = new[]
            {
                3.51212812611795133,0.216987510429556135, 3.55217854929179921
            };
            var cost = function.Function(x);

            var constraints = new NonLinearConstraints(BenchmarksNames.G15);
            PsoConfiguration.PopulationSize = 1;
            ParticleSwarmNlcDouble swarm = new OptimizationParticleSwarmNlcDouble(function, false);
            IParticle particle = new OptimizationParticleNlcDouble(swarm, x, x, function, constraints);
            particle.Position = x;
            var isFeasible = constraints.IsFeasible(particle);

            Assert.AreEqual(cost, 961.715022289961, 0.001);
            Assert.IsTrue(isFeasible);
        }

        [TestMethod]
        public void TestBenchmarkG16()
        {
            var function = new G16 { Dimension = 5 };
            var x = new[]
            {
                705.174537070090537, 68.5999999999999943, 102.899999999999991, 282.324931593660324, 37.5841164258054832
            };
            var cost = function.Function(x);

            var constraints = new NonLinearConstraints(BenchmarksNames.G16);
            PsoConfiguration.PopulationSize = 1;
            ParticleSwarmNlcDouble swarm = new OptimizationParticleSwarmNlcDouble(function, false);
            IParticle particle = new OptimizationParticleNlcDouble(swarm, x, x, function, constraints);
            particle.Position = x;
            var isFeasible = constraints.IsFeasible(particle);

            Assert.AreEqual(cost, -1.90515525853479, 0.001);
            Assert.IsTrue(isFeasible);
        }

        [TestMethod]
        public void TestBenchmarkG17()
        {
            var function = new G17 { Dimension = 6 };
            var x = new[]
            {
                201.784467214523659, 99.9999999999999005, 383.071034852773266, 420, -10.9076584514292652, 0.0731482312084287128
            };
            var cost = function.Function(x);

            var constraints = new NonLinearConstraints(BenchmarksNames.G17);
            PsoConfiguration.PopulationSize = 1;
            ParticleSwarmNlcDouble swarm = new OptimizationParticleSwarmNlcDouble(function, false);
            IParticle particle = new OptimizationParticleNlcDouble(swarm, x, x, function, constraints);
            particle.Position = x;
            var isFeasible = constraints.IsFeasible(particle);

            Assert.AreEqual(cost, 8853.53967480648, 0.01);
            Assert.IsTrue(isFeasible);
        }

        [TestMethod]
        public void TestBenchmarkG18()
        {
            var function = new G18 { Dimension = 9 };
            var x = new[]
            {
                -0.657776192427943163, -0.153418773482438542, 0.323413871675240938, -0.946257611651304398,
                -0.657776194376798906, -0.753213434632691414, 0.323413874123576972, -0.346462947962331735,
                0.59979466285217542
            };
            var cost = function.Function(x);

            var constraints = new NonLinearConstraints(BenchmarksNames.G18);
            PsoConfiguration.PopulationSize = 1;
            ParticleSwarmNlcDouble swarm = new OptimizationParticleSwarmNlcDouble(function, false);
            IParticle particle = new OptimizationParticleNlcDouble(swarm, x, x, function, constraints);
            particle.Position = x;
            var isFeasible = constraints.IsFeasible(particle);

            Assert.AreEqual(cost, -0.866025403784439, 0.001);
            Assert.IsTrue(isFeasible);
        }

        [TestMethod]
        public void TestBenchmarkG19()
        {
            var function = new G19 { Dimension = 15 };
            var x = new[]
            {
                1.66991341326291344e-17, 3.95378229282456509e-16, 3.94599045143233784, 1.06036597479721211e-16,
                3.2831773458454161, 9.99999999999999822, 1.12829414671605333e-17, 1.2026194599794709e-17,
                2.50706276000769697e-15, 2.24624122987970677e-15, 0.370764847417013987, 0.278456024942955571,
                0.523838487672241171, 0.388620152510322781, 0.298156764974678579
            };
            var cost = function.Function(x);

            var constraints = new NonLinearConstraints(BenchmarksNames.G19);
            PsoConfiguration.PopulationSize = 1;
            ParticleSwarmNlcDouble swarm = new OptimizationParticleSwarmNlcDouble(function, false);
            IParticle particle = new OptimizationParticleNlcDouble(swarm, x, x, function, constraints);
            particle.Position = x;
            var isFeasible = constraints.IsFeasible(particle);

            Assert.AreEqual(cost, 32.6555929502463, 0.001);
            Assert.IsTrue(isFeasible);
        }

        [TestMethod]
        public void TestBenchmarkG20()
        {
            var function = new G20 { Dimension = 24 };
            var x = new[]
            {
                1.28582343498528086e-18, 4.83460302526130664e-34, 0, 0, 6.30459929660781851e-18, 7.57192526201145068e-34,
                5.03350698372840437e-34, 9.28268079616618064e-34, 0, 1.76723384525547359e-17, 3.55686101822965701e-34,
                2.99413850083471346e-34, 0.158143376337580827, 2.29601774161699833e-19, 1.06106938611042947e-18,
                1.31968344319506391e-18, 0.530902525044209539, 0, 2.89148310257773535e-18, 3.34892126180666159e-18, 0,
                0.310999974151577319, 5.41244666317833561e-05, 4.84993165246959553e-16
            };
            var cost = function.Function(x);

            var constraints = new NonLinearConstraints(BenchmarksNames.G20);
            PsoConfiguration.PopulationSize = 1;
            ParticleSwarmNlcDouble swarm = new OptimizationParticleSwarmNlcDouble(function, false);
            IParticle particle = new OptimizationParticleNlcDouble(swarm, x, x, function, constraints);
            particle.Position = x;
            var isFeasible = constraints.IsFeasible(particle);

            Assert.AreEqual(cost, 0.2049794002, 0.001);
            Assert.IsTrue(isFeasible);
        }

        [TestMethod]
        public void TestBenchmarkG20_2()
        {
            var function = new G20 { Dimension = 24 };
            var x = new[]
            {
                0.0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0, 0, 0,
                1, 0, 0
            };
            var cost = function.Function(x);

            var constraints = new NonLinearConstraints(BenchmarksNames.G20);
            PsoConfiguration.PopulationSize = 1;
            ParticleSwarmNlcDouble swarm = new OptimizationParticleSwarmNlcDouble(function, false);
            IParticle particle = new OptimizationParticleNlcDouble(swarm, x, x, function, constraints);
            particle.Position = x;
            var isFeasible = constraints.IsFeasible(particle);

            Assert.AreEqual(cost, 0.2049794002, 0.001);
            Assert.IsTrue(isFeasible);
        }

        [TestMethod]
        public void TestBenchmarkG21()
        {
            var function = new G21 { Dimension = 7 };
            var x = new[]
            {
                193.724510070034967, 5.56944131553368433e-27, 17.3191887294084914, 100.047897801386839,
                6.68445185362377892, 5.99168428444264833, 6.21451648886070451
            };
            var cost = function.Function(x);

            var constraints = new NonLinearConstraints(BenchmarksNames.G21);
            PsoConfiguration.PopulationSize = 1;
            ParticleSwarmNlcDouble swarm = new OptimizationParticleSwarmNlcDouble(function, false);
            IParticle particle = new OptimizationParticleNlcDouble(swarm, x, x, function, constraints);
            particle.Position = x;
            var isFeasible = constraints.IsFeasible(particle);

            Assert.AreEqual(cost, 193.724510070035, 0.001);
            Assert.IsTrue(isFeasible);
        }

        [TestMethod]
        public void TestBenchmarkG22()
        {
            var function = new G22 { Dimension = 22 };
            var x = new[]
            {
                236.430975504001054, 135.82847151732463, 204.818152544824585, 6446.54654059436416, 3007540.83940215595,
                4074188.65771341929, 32918270.5028952882, 130.075408394314167, 170.817294970528621, 299.924591605478554,
                399.258113423595205, 330.817294971142758, 184.51831230897065, 248.64670239647424, 127.658546694545862,
                269.182627528746707, 160.000016724090955, 5.29788288102680571, 5.13529735903945728, 5.59531526444068827,
                5.43444479314453499, 5.07517453535834395
            };
            var cost = function.Function(x);

            var constraints = new NonLinearConstraints(BenchmarksNames.G22);
            PsoConfiguration.PopulationSize = 1;
            ParticleSwarmNlcDouble swarm = new OptimizationParticleSwarmNlcDouble(function, false);
            IParticle particle = new OptimizationParticleNlcDouble(swarm, x, x, function, constraints);
            particle.Position = x;
            var isFeasible = constraints.IsFeasible(particle);

            Assert.AreEqual(cost, 236.430975504001, 0.001);
            Assert.IsTrue(isFeasible);
        }

        [TestMethod]
        public void TestBenchmarkG23() //todo: 8 variáveis. deveria ser 9 // CONSERTADO
        {
            var function = new G23{ Dimension = 9 };
            var x = new[]
            {
                0.00510000000000259465, 99.9947000000000514, 9.01920162996045897e-18, 99.9999000000000535,
                0.000100000000027086086, 2.75700683389584542e-14, 99.9999999999999574, 200 , 0.01
            };
            var cost = function.Function(x);

            var constraints = new NonLinearConstraints(BenchmarksNames.G23);
            PsoConfiguration.PopulationSize = 1;
            ParticleSwarmNlcDouble swarm = new OptimizationParticleSwarmNlcDouble(function, false);
            IParticle particle = new OptimizationParticleNlcDouble(swarm, x, x, function, constraints);
            particle.Position = x;
            var isFeasible = constraints.IsFeasible(particle);

            Assert.AreEqual(cost, -400.055099999999584, 0.001);
            Assert.IsTrue(isFeasible);
        }

        [TestMethod]
        public void TestBenchmarkG23_2()
        {
            var function = new G23 { Dimension = 9 };
            var x = new[]
            {
                3.65723380827049E-31, 
                99.95,
                0,
                100,
                0.0323864774792452,
                1.42220082411506E-22,
                100,
                200,
                0.01
            };
            var cost = function.Function(x);

            var constraints = new NonLinearConstraints(BenchmarksNames.G23);
            PsoConfiguration.PopulationSize = 1;
            ParticleSwarmNlcDouble swarm = new OptimizationParticleSwarmNlcDouble(function, false);
            IParticle particle = new OptimizationParticleNlcDouble(swarm, x, x, function, constraints);
            particle.Position = x;
            var isFeasible = constraints.IsFeasible(particle);

            Assert.AreEqual(cost, -400.055099999999584, 0.001);
            Assert.IsTrue(isFeasible);
        }

        [TestMethod]
        public void TestBenchmarkG24()
        {
            var function = new G24 { Dimension = 2 };
            var x = new[]
            {
                2.32952019747762, 3.17849307411774
            };
            var cost = function.Function(x);

            var constraints = new NonLinearConstraints(BenchmarksNames.G24);
            PsoConfiguration.PopulationSize = 1;
            ParticleSwarmNlcDouble swarm = new OptimizationParticleSwarmNlcDouble(function, false);
            IParticle particle = new OptimizationParticleNlcDouble(swarm, x, x, function, constraints);
            particle.Position = x;
            var isFeasible = constraints.IsFeasible(particle);

            Assert.AreEqual(cost, -5.50801327159536, 0.001);
            Assert.IsTrue(isFeasible);
        }
    }
}
