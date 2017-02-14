namespace ExecuteJobRulesUsingWS
{
    partial class DailyExecutionService
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dailyExecutionServiceTimer = new System.Windows.Forms.Timer(this.components);
            this.DSchedulerDailyExecutionLog = new System.Diagnostics.EventLog();
            ((System.ComponentModel.ISupportInitialize)(this.DSchedulerDailyExecutionLog)).BeginInit();
            // 
            // DailyExecutionService
            // 
            this.ServiceName = "Service1";
            ((System.ComponentModel.ISupportInitialize)(this.DSchedulerDailyExecutionLog)).EndInit();

        }

        #endregion

        private System.Windows.Forms.Timer dailyExecutionServiceTimer;
        private System.Diagnostics.EventLog DSchedulerDailyExecutionLog;
    }
}
