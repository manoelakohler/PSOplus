using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PSO.Benchmarks.NoConstraints
{
    /// <summary>
    /// R² -> R, x in [-3,3].
    /// </summary>
    public abstract class FunctionBase
    {
        public abstract double Function(double x, double y);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public double Function(double[] x)
        {
            return Function(x[0], x[1]);
        }

        /// <summary>
        /// Gets all classes that inherit from <see>
        ///     <cref>FunctionBase</cref>
        /// </see>
        /// </summary>
        public static List<FunctionInfo> GetAvailableFunctions()
        {
            var asmAssembly = Assembly.GetExecutingAssembly();
            var query = from type in asmAssembly.GetTypes()
                        where type.BaseType == typeof(FunctionBase)
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
        public static FunctionBase CreateFunction(FunctionInfo functionInfo)
        {
            var fullName = functionInfo.FullName;
            var type = Type.GetType(fullName);
            var o = Activator.CreateInstance(type);
            return o as FunctionBase;
        }
    }
}
