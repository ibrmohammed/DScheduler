CREATE PROCEDURE [DScheduler.uspInsertDataIntoJobMonitor]
(
	@NodeID INT,
	@EnvironmentID INT,
	@SessionID INT,
	@UProcId INT,
	@UProcStatus VARCHAR(50),
	@StartedDateTime DATETIME,
	@CreatedBy VARCHAR(50),
	@UpdatedBy VARCHAR(50),
	@UpdatedDateTime DATETIME,
	@CreatedDateTime DATETIME
)
AS
BEGIN
BEGIN TRY

SET XACT_ABORT ON
SET NOCOUNT ON;
	INSERT INTO TJobMonitor2
			   (
			       NodeID,EnvironmentID,SessionID,UprocID,UProcStatus,StartedDateTime,CreatedBy,UpdatedBy,UpdatedDateTime,CreatedDateTime
			   )
	VALUES
				(
				 @NodeID,@EnvironmentID,@SessionID,@UprocID,@UProcStatus,@StartedDateTime,@CreatedBy,@UpdatedBy,@UpdatedDateTime,@CreatedDateTime
				)

END TRY
BEGIN CATCH
			DECLARE @error_seq INT
			DECLARE @ErrorNumber INT
			DECLARE @ErrorSeverity INT
			DECLARE @ErrorState INT
			DECLARE @ErrorProcedure NVARCHAR(126)
			DECLARE @ErrorLine INT
			DECLARE @ErrorMessage NVARCHAR(4000)
			SELECT
				@ErrorNumber    = ERROR_NUMBER(),
				@ErrorSeverity  = ERROR_SEVERITY(),
				@ErrorState     = ERROR_STATE(),
				@ErrorProcedure = ERROR_PROCEDURE(),
				@ErrorLine      = ERROR_LINE(),
				@ErrorMessage   = ERROR_MESSAGE();

			RAISERROR( 'Error #: %d in %s . Message:%s', @ErrorSeverity, @ErrorState, @error_seq, @ErrorProcedure,  @ErrorMessage )

END CATCH
END