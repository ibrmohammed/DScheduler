namespace DScheduler.Framework
{
    ///  <summary>
    ///
    ///  </summary>
    ///  <typeparam name="TItem"></typeparam>
    /// <typeparam name="TState"></typeparam>
    public abstract class ReRunBySegmentTask<TItem, TState> : ReRunnableTask<TItem[], TState> where TState : IBatchTaskState
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        protected ReRunBySegmentTask(string name)
            : base(name)
        {
        }

        /// <summary>
        ///
        /// </summary>
        public abstract int SegmentSize
        {
            get;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="state"></param>
        protected override TState ProcessInput(TItem[] argument, TState state)
        {
            var batchPassed = false;

            for (var iItem = State.CurrentPosition; iItem < argument.Length; iItem += SegmentSize)
            {
                try
                {
                    batchPassed = false;

                    State = SegmentStarted(State);

                    for (var i = 0; i < SegmentSize; i++)
                    {
                        State = ProcessItem(argument[iItem + i], State);
                    }

                    batchPassed = true;
                }
                finally
                {
                    if (batchPassed)
                    {
                        State.CurrentPosition += SegmentSize;
                        SaveSegment(Name, State);
                        SaveState(Name, State);
                    }
                }
            }

            return State;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="item"></param>
        /// <param name="state"></param>
        protected abstract TState ProcessItem(TItem item, TState state);

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        protected abstract TState SaveSegment(string name, TState state);

        /// <summary>
        ///
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected abstract TState SegmentStarted(TState state);
    }
}