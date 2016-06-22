#region

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;

#endregion

namespace Ryan.Core
{
    /// <summary>
    ///     Engine
    /// </summary>
    public abstract class WorkEngine
    {
        /// <summary>
        ///     Run startup tasks
        /// </summary>
        protected virtual void RunStartupTasks()
        {
            var typeFinder = ServiceLocator.Current.GetInstance<ITypeFinder>();
            var startUpTaskTypes = typeFinder.FindClassesOfType<IStartupTask>();
            var startUpTasks =
                startUpTaskTypes.Select(startUpTaskType => (IStartupTask) Activator.CreateInstance(startUpTaskType))
                    .ToList();

            startUpTasks = startUpTasks.AsQueryable().OrderBy(st => st.Order).ToList();
            foreach (var startUpTask in startUpTasks)
                startUpTask.Execute();
        }

        /// <summary>
        ///     Runs the aysnc startup tasks.
        /// </summary>
        protected virtual void RunAysncStartupTasks()
        {
            var startUpTasks = ResolveAll<IAsyncStartupTask>().ToList();
            Parallel.ForEach(startUpTasks, d => d.Execute());
        }


        /// <summary>
        ///     Register dependencies
        /// </summary
        public abstract void RegisterDependencies();

        #region Methods

        /// <summary>
        ///     初始化
        /// </summary>
        public void Initialize()
        {
            RegisterDependencies();

            RunStartupTasks();
            RunAysncStartupTasks();
        }


        /// <summary>
        ///     Resolve dependencies
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns></returns>
        private T[] ResolveAll<T>()
        {
            return ServiceLocator.Current.GetAllInstances<T>().ToArray();
        }

        #endregion
    }
}