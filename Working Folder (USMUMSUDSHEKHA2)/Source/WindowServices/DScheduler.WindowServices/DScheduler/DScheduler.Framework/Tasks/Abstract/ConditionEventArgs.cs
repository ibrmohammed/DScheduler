
namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public class ConditionEventArgs<T> : EventArgs<ConditionInfo<T>>
    {
        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="item"></param>
        public ConditionEventArgs(ConditionInfo<T> item)
            : base(item)
        {
        }
    }
}