namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TState"></typeparam>
    public abstract class ReRunnableTask<TInput, TState> : TaskIn<TInput> where TState : ITaskState
    {
        /// <summary>
        ///
        /// </summary>
        protected string Name;

        /// <summary>
        ///
        /// </summary>
        protected TState State;

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        protected ReRunnableTask(string name)
        {
            Name = name;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="argument"></param>
        protected override void Execute(TInput argument)
        {
            State = GetCurrentState(Name);

            ProcessInput(argument, State);

            SaveState(Name, State);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected abstract TState GetCurrentState(string name);

        /// <summary>
        ///
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="state"></param>
        protected abstract TState ProcessInput(TInput argument, TState state);

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <param name="state"></param>
        protected abstract void SaveState(string name, TState state);
    }
}