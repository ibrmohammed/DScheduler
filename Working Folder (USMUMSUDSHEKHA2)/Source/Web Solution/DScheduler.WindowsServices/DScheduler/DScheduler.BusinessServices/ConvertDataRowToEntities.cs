

using DScheduler.Framework;
namespace DScheduler.BusinessServices
{
    public class ConvertDataRowToEntities : FilterDataTable<ScheduledSessionDetails, ScheduledSessionsConverter>
    {
        #region Batch Framework Overrides

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void CleanUp()
        {
            base.CleanUp();
        }

        #endregion Batch Framework Overrides
    }
}