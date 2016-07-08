using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Daisy.Core.Utility
{
    public static class TypeExt
    {
        #region Ctor
         
        private static ConstructorInfo GetConstructor(Type type, params Type[] argumentTypes)
        {
            type.ThrowIfNull("type");
            argumentTypes.ThrowIfNull("argumentTypes");
            
            ConstructorInfo ci = type.GetConstructor(argumentTypes);
            if (ci == null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(type.Name).Append(" has no ctor(");
                for (int i = 0; i < argumentTypes.Length; i++)
                {
                    if (i > 0)
                    {
                        sb.Append(',');
                    }
                    sb.Append(argumentTypes[i].Name);
                }
                sb.Append(')');
                throw new InvalidOperationException(sb.ToString());
            }
            return ci;
        }
        /// <summary>
        /// Obtains a delegate to invoke a parameterless constructor
        /// </summary>
        /// <typeparam name="TResult">The base/interface type to yield as the
        /// new value; often object except for factory pattern implementations</typeparam>
        /// <param name="type">The Type to be created</param>
        /// <returns>A delegate to the constructor if found, else null</returns>
        public static Func<TResult> Ctor<TResult>(this Type type)
        {
            ConstructorInfo ci = GetConstructor(type, Type.EmptyTypes);
            return Expression.Lambda<Func<TResult>>(
                Expression.New(ci)).Compile();
        }
        /// <summary>
        /// Obtains a delegate to invoke a constructor which takes a parameter
        /// </summary>
        /// <typeparam name="TArg1">The type of the constructor parameter</typeparam>
        /// <typeparam name="TResult">The base/interface type to yield as the
        /// new value; often object except for factory pattern implementations</typeparam>
        /// <param name="type">The Type to be created</param>
        /// <returns>A delegate to the constructor if found, else null</returns>
        public static Func<TArg1, TResult>
            Ctor<TArg1, TResult>(this Type type)
        {
            ConstructorInfo ci = GetConstructor(type, typeof(TArg1));
            ParameterExpression
                param1 = Expression.Parameter(typeof(TArg1), "arg1");

            return Expression.Lambda<Func<TArg1, TResult>>(
                Expression.New(ci, param1), param1).Compile();
        }
        /// <summary>
        /// Obtains a delegate to invoke a constructor with multiple parameters
        /// </summary>
        /// <typeparam name="TArg1">The type of the first constructor parameter</typeparam>
        /// <typeparam name="TArg2">The type of the second constructor parameter</typeparam>
        /// <typeparam name="TResult">The base/interface type to yield as the
        /// new value; often object except for factory pattern implementations</typeparam>
        /// <param name="type">The Type to be created</param>
        /// <returns>A delegate to the constructor if found, else null</returns>
        public static Func<TArg1, TArg2, TResult>
            Ctor<TArg1, TArg2, TResult>(this Type type)
        {
            ConstructorInfo ci = GetConstructor(type, typeof(TArg1), typeof(TArg2));
            ParameterExpression
                param1 = Expression.Parameter(typeof(TArg1), "arg1"),
                param2 = Expression.Parameter(typeof(TArg2), "arg2");

            return Expression.Lambda<Func<TArg1, TArg2, TResult>>(
                Expression.New(ci, param1, param2), param1, param2).Compile();
        }
        /// <summary>
        /// Obtains a delegate to invoke a constructor with multiple parameters
        /// </summary>
        /// <typeparam name="TArg1">The type of the first constructor parameter</typeparam>
        /// <typeparam name="TArg2">The type of the second constructor parameter</typeparam>
        /// <typeparam name="TArg3">The type of the third constructor parameter</typeparam>
        /// <typeparam name="TResult">The base/interface type to yield as the
        /// new value; often object except for factory pattern implementations</typeparam>
        /// <param name="type">The Type to be created</param>
        /// <returns>A delegate to the constructor if found, else null</returns>
        public static Func<TArg1, TArg2, TArg3, TResult>
            Ctor<TArg1, TArg2, TArg3, TResult>(this Type type)
        {
            ConstructorInfo ci = GetConstructor(type, typeof(TArg1), typeof(TArg2), typeof(TArg3));
            ParameterExpression
                param1 = Expression.Parameter(typeof(TArg1), "arg1"),
                param2 = Expression.Parameter(typeof(TArg2), "arg2"),
                param3 = Expression.Parameter(typeof(TArg3), "arg3");

            return Expression.Lambda<Func<TArg1, TArg2, TArg3, TResult>>(
                Expression.New(ci, param1, param2, param3),
                    param1, param2, param3).Compile();
        }
        /// <summary>
        /// Obtains a delegate to invoke a constructor with multiple parameters
        /// </summary>
        /// <typeparam name="TArg1">The type of the first constructor parameter</typeparam>
        /// <typeparam name="TArg2">The type of the second constructor parameter</typeparam>
        /// <typeparam name="TArg3">The type of the third constructor parameter</typeparam>
        /// <typeparam name="TArg4">The type of the fourth constructor parameter</typeparam>
        /// <typeparam name="TResult">The base/interface type to yield as the
        /// new value; often object except for factory pattern implementations</typeparam>
        /// <param name="type">The Type to be created</param>
        /// <returns>A delegate to the constructor if found, else null</returns>
        public static Func<TArg1, TArg2, TArg3, TArg4, TResult>
            Ctor<TArg1, TArg2, TArg3, TArg4, TResult>(this Type type)
        {
            ConstructorInfo ci = GetConstructor(type, typeof(TArg1), typeof(TArg2), typeof(TArg3), typeof(TArg4));
            ParameterExpression
                param1 = Expression.Parameter(typeof(TArg1), "arg1"),
                param2 = Expression.Parameter(typeof(TArg2), "arg2"),
                param3 = Expression.Parameter(typeof(TArg3), "arg3"),
                param4 = Expression.Parameter(typeof(TArg4), "arg4");

            return Expression.Lambda<Func<TArg1, TArg2, TArg3, TArg4, TResult>>(
                Expression.New(ci, param1, param2, param3, param4),
                    param1, param2, param3, param4).Compile();
        }
        #endregion

    }

    public static class PropertyCopy<TTarget> where TTarget : class, new()
    {
        /// <summary>
        /// Copies all readable properties from the source to a new instance
        /// of TTarget.
        /// </summary>
        public static TTarget CopyFrom<TSource>(TSource source) where TSource : class
        {
            return PropertyCopier<TSource>.Copy(source);
        }

        /// <summary>
        /// Static class to efficiently store the compiled delegate which can
        /// do the copying. We need a bit of work to ensure that exceptions are
        /// appropriately propagated, as the exception is generated at type initialization
        /// time, but we wish it to be thrown as an ArgumentException.
        /// </summary>
        private static class PropertyCopier<TSource> where TSource : class
        {
            private static readonly Func<TSource, TTarget> copier;
            private static readonly Exception initializationException;

            internal static TTarget Copy(TSource source)
            {
                if (initializationException != null)
                {
                    throw initializationException;
                }
                if (source == null)
                {
                    throw new ArgumentNullException("source");
                }
                return copier(source);
            }

            static PropertyCopier()
            {
                try
                {
                    copier = BuildCopier();
                    initializationException = null;
                }
                catch (Exception e)
                {
                    copier = null;
                    initializationException = e;
                }
            }

            private static Func<TSource, TTarget> BuildCopier()
            {
                ParameterExpression sourceParameter = Expression.Parameter(typeof(TSource), "source");
                var bindings = new List<MemberBinding>();
                foreach (PropertyInfo sourceProperty in typeof(TSource).GetProperties())
                {
                    if (!sourceProperty.CanRead)
                    {
                        continue;
                    }
                    PropertyInfo targetProperty = typeof(TTarget).GetProperty(sourceProperty.Name);
                    if (targetProperty == null)
                    {
                        throw new ArgumentException("Property " + sourceProperty.Name + " is not present and accessible in " + typeof(TTarget).FullName);
                    }
                    if (!targetProperty.CanWrite)
                    {
                        throw new ArgumentException("Property " + sourceProperty.Name + " is not writable in " + typeof(TTarget).FullName);
                    }
                    if (!targetProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType))
                    {
                        throw new ArgumentException("Property " + sourceProperty.Name + " has an incompatible type in " + typeof(TTarget).FullName);
                    }
                    bindings.Add(Expression.Bind(targetProperty, Expression.Property(sourceParameter, sourceProperty)));
                }
                Expression initializer = Expression.MemberInit(Expression.New(typeof(TTarget)), bindings);
                return Expression.Lambda<Func<TSource, TTarget>>(initializer, sourceParameter).Compile();
            }
        }
    }
}