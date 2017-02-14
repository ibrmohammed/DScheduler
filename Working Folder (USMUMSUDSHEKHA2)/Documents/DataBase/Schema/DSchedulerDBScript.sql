
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

/****** Object:  Table TRef ******/
CREATE TABLE TRef(
	RefID int IDENTITY(1,1) NOT NULL,
	RefName varchar(50) NOT NULL,
	RefValue varchar(50) NOT NULL,
	RefDescription varchar(500) NOT NULL , 
	IsActive varchar(1) NOT NULL,
	IsVisible varchar(1) NULL,
	CreatedDateTime datetime NOT NULL,
	CreatedBy varchar(50) NOT NULL,
	UpdatedDateTime datetime NOT NULL,
	UpdatedBy varchar(50) NOT NULL,
CONSTRAINT PK_TRef PRIMARY KEY NONCLUSTERED (RefID)	
	);

/****** Object:  Table TNode ******/
CREATE TABLE TNode(
	NodeID int IDENTITY(1,1) NOT NULL,
	NodeName varchar(25),
	NodePath varchar(200) NOT NULL,
	CreatedDateTime datetime NOT NULL,
	CreatedBy varchar(50) NOT NULL,
	UpdatedDateTime datetime NOT NULL,
	UpdatedBy varchar(50) NOT NULL,
CONSTRAINT PK_TNode PRIMARY KEY NONCLUSTERED (NodeID)		
	);

/****** Object:  Table TLogin ******/
CREATE TABLE TLogin(
	LoginID int IDENTITY(1,1) NOT NULL,
	AccountTypeID int NOT NULL,
	UserName varchar(50) NOT NULL,
	FirstName varchar(50) NULL , 
	LastName varchar(50) NULL,
	Email varchar(50) NOT NULL ,
	[Password] varchar(50) NOT NULL ,
	IsActive varchar(1) NULL,
	Organization varchar(100) NULL,
	CreatedDateTime datetime NOT NULL,
	CreatedBy varchar(50) NOT NULL,
	UpdatedDateTime datetime NOT NULL,
	UpdatedBy varchar(50) NOT NULL,
CONSTRAINT PK_TLogin PRIMARY KEY NONCLUSTERED (LoginID),     
CONSTRAINT FK_TLogin_RefID FOREIGN KEY (AccountTypeID) REFERENCES TRef (RefID)	
	);

/****** Object:  Table TEnvironment ******/
CREATE TABLE TEnvironment(
	EnvironmentID int IDENTITY(1,1) NOT NULL,
	NodeID int NOT NULL,
	EnvironmentName varchar(50) NULL,
	EnvironmentPath varchar(200) NULL , 
	CreatedDateTime datetime NOT NULL,
	CreatedBy varchar(50) NOT NULL,
	UpdatedDateTime datetime NOT NULL,
	UpdatedBy varchar(50) NOT NULL,
CONSTRAINT PK_TEnvironment PRIMARY KEY NONCLUSTERED (EnvironmentID),     
CONSTRAINT FK_TEnvironment_NodeID FOREIGN KEY (NodeID) REFERENCES TNode (NodeID)	
	);

/****** Object:  Table TSession ******/
CREATE TABLE TSession(
	SessionID int IDENTITY(1,1) NOT NULL,
	EnvironmentID int NOT NULL,
	AccountTypeID int NOT NULL,
	SessionName varchar(50) NULL,
	EnvironmentName varchar(50) NULL, 
	CreatedDateTime datetime NOT NULL,
	CreatedBy varchar(50) NOT NULL,
	UpdatedDateTime datetime NOT NULL,
	UpdatedBy varchar(50) NOT NULL,
CONSTRAINT PK_TSession PRIMARY KEY NONCLUSTERED (SessionID),     
CONSTRAINT FK_TSession_EnvironmentID FOREIGN KEY (EnvironmentID) REFERENCES TEnvironment (EnvironmentID),	
CONSTRAINT FK_TSession_AccountTypeID FOREIGN KEY (AccountTypeID) REFERENCES TRef (RefID)
	);

/****** Object:  Table TUproc ******/
CREATE TABLE TUproc(
	UprocID int IDENTITY(1,1) NOT NULL,
	JobTypeID int NOT NULL,
	EnvironmentID int NOT NULL,
	AccountTypeID int NOT NULL,
	UprocName varchar(50) NULL,
	EnvironmentName varchar(50) NULL, 
	FolderPath varchar(200) NOT NULL,
	Command varchar(200) NOT NULL,
	CreatedDateTime datetime NOT NULL,
	CreatedBy varchar(50) NOT NULL,
	UpdatedDateTime datetime NOT NULL,
	UpdatedBy varchar(50) NOT NULL,
CONSTRAINT PK_TUproc PRIMARY KEY NONCLUSTERED (UprocID), 
CONSTRAINT FK_TUproc_JobTypeID FOREIGN KEY (JobTypeID) REFERENCES TRef (RefID),    
CONSTRAINT FK_TUproc_EnvironmentID FOREIGN KEY (EnvironmentID) REFERENCES TEnvironment (EnvironmentID),	
CONSTRAINT FK_TUproc_AccountTypeID FOREIGN KEY (AccountTypeID) REFERENCES TRef (RefID)
	);

/****** Object:  Table TRecurrencePattern ******/
CREATE TABLE TRecurrencePattern(
	RecurrencePatternID int IDENTITY(1,1) NOT NULL,
	RecurrenceID int NOT NULL,
	RecPattTypeID int NULL,
	NoOfDays int NULL,
	NoOfMonths int NULL,
	NoOfYears int NULL,
	WeekName varchar(25) NULL, 
	WeekDayName varchar(25) NULL, 
	[MonthName] varchar(25) NULL,
	CreatedDateTime datetime NOT NULL,
	CreatedBy varchar(50) NOT NULL,
	UpdatedDateTime datetime NOT NULL,
	UpdatedBy varchar(50) NOT NULL,
CONSTRAINT PK_TRecurrencePattern PRIMARY KEY NONCLUSTERED (RecurrencePatternID),
CONSTRAINT FK_TRecurrencePattern_RecPattTypeID FOREIGN KEY (RecPattTypeID) REFERENCES TRef (RefID)
	);

/****** Object:  Table TDependencies ******/
CREATE TABLE TDependencies(
	DependentID int IDENTITY(1,1) NOT NULL,
	SessionID int NOT NULL,
	UprocID int NULL,
	DepOnSessionID int NULL,
	DepOnUprocID int NULL,
	SessionName varchar(50) NULL,
	UprocName varchar(50) NULL, 
	DepOnSessionName varchar(50) NULL, 
	DepOnUprocName varchar(50) NULL,
	CreatedDateTime datetime NOT NULL,
	CreatedBy varchar(50) NOT NULL,
	UpdatedDateTime datetime NOT NULL,
	UpdatedBy varchar(50) NOT NULL,
	IsActive int null,
	SequenceNumber int null,
CONSTRAINT PK_TDependencies PRIMARY KEY NONCLUSTERED (DependentID),
CONSTRAINT FK_TDependencies_SessionID FOREIGN KEY (SessionID) REFERENCES TSession (SessionID),
CONSTRAINT FK_TDependencies_UprocID FOREIGN KEY (UprocID) REFERENCES TUproc (UprocID)
	);

/****** Object:  Table TJobRule ******/
CREATE TABLE TJobRule(
	JobRuleID int IDENTITY(1,1) NOT NULL,
	ActivityTypeID int NOT NULL,
	SessionID  int not NULL,
	UprocID int not NULL,
	RecurrenceID int not NULL,
	OutageID int not NULL,
	SessionUprocName varchar(50) NULL,
	JobStartTime Time NULL, 
	Recurrence bit, 
	CreatedDateTime datetime NOT NULL,
	CreatedBy varchar(50) NOT NULL,
	UpdatedDateTime datetime NOT NULL,
	UpdatedBy varchar(50) NOT NULL,
CONSTRAINT PK_TJobRule PRIMARY KEY NONCLUSTERED (JobRuleID),
CONSTRAINT FK_TJobRule_ActivityTypeID FOREIGN KEY (ActivityTypeID) REFERENCES TRef (RefID),
CONSTRAINT FK_TJobRule_RecurrenceID FOREIGN KEY (RecurrenceID) REFERENCES TRecurrencePattern (RecurrencePatternID),
CONSTRAINT FK_TJobRule_SessionID FOREIGN KEY (SessionID) REFERENCES TSession (SessionID),
CONSTRAINT FK_TJobRule_UprocID FOREIGN KEY (UprocID) REFERENCES TUproc (UprocID)
	);

/****** Object:  Table TOutageDetails ******/
CREATE TABLE TOutageDetails(
	OutageDetailID int IDENTITY(1,1) NOT NULL,
	JobRuleID int NOT NULL,
	OutageName varchar(50) NULL,
	OutageDate datetime NULL, 
	OutageStartTime datetime NULL, 
	OutageEndTime datetime NULL,
	CreatedDateTime datetime NOT NULL,
	CreatedBy varchar(50) NOT NULL,
	UpdatedDateTime datetime NOT NULL,
	UpdatedBy varchar(50) NOT NULL,
CONSTRAINT PK_TOutageDetails PRIMARY KEY NONCLUSTERED (OutageDetailID),
CONSTRAINT FK_TOutageDetails_JobRuleID FOREIGN KEY (JobRuleID) REFERENCES TJobRule (JobRuleID)
	);

/****** Object:  Table TRecurrenceRange******/
CREATE TABLE TRecurrenceRange(
	RecurrenceRangeID int IDENTITY(1,1) NOT NULL,
	RecRangeTypeID int NOT NULL,
	StartDateTime datetime NULL,
	EndDateTime datetime NULL,
	OccurenceCount int NULL,
	CreatedDateTime datetime NOT NULL,
	CreatedBy varchar(50) NOT NULL,
	UpdatedDateTime datetime NOT NULL,
	UpdatedBy varchar(50) NOT NULL,
CONSTRAINT PK_TRecurrenceRange PRIMARY KEY NONCLUSTERED (RecurrenceRangeID),
CONSTRAINT FK_TRecurrenceRange_RecRangeTypeID FOREIGN KEY (RecRangeTypeID) REFERENCES TRef (RefID)
	);

/****** Object:  Table TJobMonitor ******/
CREATE TABLE TJobMonitor(
	JobID int IDENTITY(1,1) NOT NULL,
	SessionID  int not NULL,
	UprocID int not NULL,
	EnvironmentID int not NULL,
	NodeID int not NULL,
	SessionUprocName varchar(50) NULL,
	ParentJobName varchar(50) NULL,
	EnvironmentName varchar(50) NULL,
	NodeName varchar(50) NULL,
	ScheduleDateTime datetime,
	StartedDateTime datetime,
	CompletedDateTime datetime,
	CreatedDateTime datetime NOT NULL,
	CreatedBy varchar(50) NOT NULL,
	UpdatedDateTime datetime NOT NULL,
	UpdatedBy varchar(50) NOT NULL,
CONSTRAINT PK_TJobMonitor PRIMARY KEY NONCLUSTERED (JobID),
CONSTRAINT FK_TJobMonitor_EnvironmentID FOREIGN KEY (EnvironmentID) REFERENCES TEnvironment (EnvironmentID),
CONSTRAINT FK_TJobMonitor_NodeID FOREIGN KEY (NodeID) REFERENCES TNode (NodeID),
CONSTRAINT FK_TJobMonitor_SessionID FOREIGN KEY (SessionID) REFERENCES TSession (SessionID),
CONSTRAINT FK_TJobMonitor_UprocID FOREIGN KEY (UprocID) REFERENCES TUproc (UprocID)
	);

/****** Object:  Table TJobAction******/
CREATE TABLE TJobAction(
	JobActionID int IDENTITY(1,1) NOT NULL,
	JobID  int not NULL,
	JobActionTypeID int not NULL,
	CreatedDateTime datetime NOT NULL,
	CreatedBy varchar(50) NOT NULL,
	UpdatedDateTime datetime NOT NULL,
	UpdatedBy varchar(50) NOT NULL,
CONSTRAINT PK_TJobAction PRIMARY KEY NONCLUSTERED (JobActionID),
CONSTRAINT FK_TJobAction_JobID FOREIGN KEY (JobID) REFERENCES TJobMonitor (JobID),
CONSTRAINT FK_TJobAction_JobActionTypeID FOREIGN KEY (JobActionTypeID) REFERENCES TRef (RefID)
	);

/****** Object:  Table TTemplate******/
CREATE TABLE TTemplate(
	TemplateID int IDENTITY(1,1) NOT NULL,
	TemplateName  varchar(50) not NULL,
	CreatedDateTime datetime NOT NULL,
	CreatedBy varchar(50) NOT NULL,
	UpdatedDateTime datetime NOT NULL,
	UpdatedBy varchar(50) NOT NULL,
CONSTRAINT PK_TTemplate PRIMARY KEY NONCLUSTERED (TemplateID)
	);

/****** Object:  Table TReports******/
CREATE TABLE TReports(
	ReportID int IDENTITY(1,1) NOT NULL,
	TemplateID  int not NULL,
	TemplateName  varchar(50) not NULL,
	CreatedDateTime datetime NOT NULL,
	CreatedBy varchar(50) NOT NULL,
	UpdatedDateTime datetime NOT NULL,
	UpdatedBy varchar(50) NOT NULL,
CONSTRAINT PK_TReports PRIMARY KEY NONCLUSTERED (ReportID),
CONSTRAINT FK_TReports_TemplateID FOREIGN KEY (TemplateID) REFERENCES TTemplate (TemplateID)
	);

GO

