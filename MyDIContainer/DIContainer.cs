using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyDIContainer
{
    public static class IoC
    {
        //warning - static class not tested
        static readonly IDictionary<Type, Type> types = new Dictionary<Type, Type>();
        /// <summary>
        /// add type to container, generic
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        public static void Register<TContract, TImplementation>()
        {
            types[typeof(TContract)] = typeof(TImplementation);
        }

        /// <summary>
        /// add type to container, generic
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        public static void Register(Type tContract, Type tImplementation)
        {
            types[tContract] = tImplementation;
        }

        /// <summary>
        /// extract type from container
        /// </summary>
        /// <typeparam name="T">Extract type</typeparam>
        /// <returns></returns>
        public static T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        /// <summary>
        /// extract type from container
        /// </summary>
        /// <typeparam name="T">Extract type</typeparam>
        /// <returns></returns>
        public static object Resolve(Type contract)
        {
            Type implementation = types[contract];
            ConstructorInfo constructor = implementation.GetConstructors()[0];
            ParameterInfo[] constructorParameters = constructor.GetParameters();
            if (constructorParameters.Length == 0)
            {
                return Activator.CreateInstance(implementation);
            }

            List<object> parameters = new List<object>(constructorParameters.Length);
            foreach (ParameterInfo parameterInfo in constructorParameters)
            {
                parameters.Add(Resolve(parameterInfo.ParameterType));
            }

            return constructor.Invoke(parameters.ToArray());
        }
    }
}
