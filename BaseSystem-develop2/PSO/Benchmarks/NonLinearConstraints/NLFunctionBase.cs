using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PSO.Benchmarks.NonLinearConstraints
{
    public abstract class NLFunctionBase
    {
        public abstract double Function(int[] x);

        public abstract double Function(double[] x);
        /// <summary>
        /// Dimension of the optimization problem
        /// </summary>
        public int Dimension { get; set; }

        /// <summary>
        /// Gets all classes that inherit from <see>
        ///     <cref>FunctionBase</cref>
        /// </see>
        /// </summary>
        public static List<FunctionInfo> GetAvailableFunctions()
        {
            var asmAssembly = Assembly.GetExecutingAssembly();
            var query = from type in asmAssembly.GetTypes()
                        where type.BaseType == typeof(NLFunctionBase)
                        orderby type.Name
                        select new
                        {
                            type.Name,
                            type.FullName
                        };

            var list = query.ToList().Select(info => new FunctionInfo(info.Name, info.FullName)).ToList();
            return list;
        }

        /// <summary>
        /// Creates a function
        /// </summary>
        /// <param name="functionInfo"></param>
        /// <returns></returns>
        public static NLFunctionBase CreateFunction(FunctionInfo functionInfo)
        {
            var fullName = functionInfo.FullName;
            var type = Type.GetType(fullName);
            var o = Activator.CreateInstance(type);
            var instance = o as NLFunctionBase;
            var dimension = SetDimension(type, instance);
            instance.Dimension = dimension;
            return instance as NLFunctionBase;
        }

        private static int SetDimension(Type type, NLFunctionBase instance)
        {
            switch ((BenchmarksNames) Enum.Parse(typeof (BenchmarksNames), instance.GetType().Name, true))
            {
                case BenchmarksNames.G01:
                    return BenchmarksSolution.GetBenchmarkDimension(BenchmarksNames.G01);
                case BenchmarksNames.G02:
                    return BenchmarksSolution.GetBenchmarkDimension(BenchmarksNames.G02);
                case BenchmarksNames.G03:
                    return BenchmarksSolution.GetBenchmarkDimension(BenchmarksNames.G03);
                case BenchmarksNames.G04:
                    return BenchmarksSolution.GetBenchmarkDimension(BenchmarksNames.G04);
                case BenchmarksNames.G05:
                    return BenchmarksSolution.GetBenchmarkDimension(BenchmarksNames.G05);
                case BenchmarksNames.G06:
                    return BenchmarksSolution.GetBenchmarkDimension(BenchmarksNames.G06);
                case BenchmarksNames.G07:
                    return BenchmarksSolution.GetBenchmarkDimension(BenchmarksNames.G07);
                case BenchmarksNames.G08:
                    return BenchmarksSolution.GetBenchmarkDimension(BenchmarksNames.G08);
                case BenchmarksNames.G09:
                    return BenchmarksSolution.GetBenchmarkDimension(BenchmarksNames.G09);
                case BenchmarksNames.G10:
                    return BenchmarksSolution.GetBenchmarkDimension(BenchmarksNames.G10);
                case BenchmarksNames.G11:
                    return BenchmarksSolution.GetBenchmarkDimension(BenchmarksNames.G11);
                case BenchmarksNames.G12:
                    return BenchmarksSolution.GetBenchmarkDimension(BenchmarksNames.G12);
                case BenchmarksNames.G13:
                    return BenchmarksSolution.GetBenchmarkDimension(BenchmarksNames.G13); 
            }
            return 0;
        }
    }
}
