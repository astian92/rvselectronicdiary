/*
   Saturday, July 29, 20172:05:03 PM
   User: gems_rvs_user
   Server: 91.215.216.112\MSSQL2014,2014
   Database: gems_dev_rvs
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_ArchivedDiary
	(
	Id uniqueidentifier NOT NULL,
	Number int NOT NULL,
	AcceptanceDateAndTime datetime NOT NULL,
	LetterNumber nvarchar(40) NOT NULL,
	LetterDate datetime NOT NULL,
	Contractor nvarchar(150) NOT NULL,
	Client nvarchar(300) NOT NULL,
	ClientMobile nvarchar(30) NULL,
	Comment ntext NULL,
	RequestDate datetime NOT NULL,
	RequestAcceptedBy nvarchar(200) NOT NULL,
	RequestTestingPeriod int NULL,
	ProtocolIssuedDate datetime NOT NULL,
	ProtocolTester nvarchar(600) NULL,
	ProtocolLabLeader nvarchar(300) NOT NULL,
	Remark nvarchar(8) NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_ArchivedDiary SET (LOCK_ESCALATION = TABLE)
GO
IF EXISTS(SELECT * FROM dbo.ArchivedDiary)
	 EXEC('INSERT INTO dbo.Tmp_ArchivedDiary (Id, Number, AcceptanceDateAndTime, LetterNumber, LetterDate, Contractor, Client, ClientMobile, Comment, RequestDate, RequestAcceptedBy, RequestTestingPeriod, ProtocolIssuedDate, ProtocolTester, ProtocolLabLeader, Remark)
		SELECT Id, Number, AcceptanceDateAndTime, LetterNumber, LetterDate, Contractor, Client, ClientMobile, Comment, RequestDate, RequestAcceptedBy, RequestTestingPeriod, ProtocolIssuedDate, ProtocolTesterMKB, ProtocolLabLeader, Remark FROM dbo.ArchivedDiary WITH (HOLDLOCK TABLOCKX)')
GO
ALTER TABLE dbo.ArchivedProducts
	DROP CONSTRAINT FK_ArchivedProducts_ArchivedDiary
GO
ALTER TABLE dbo.ArchivedProtocolRemarks
	DROP CONSTRAINT FK_ArchivedProtocolRemarks_ArchivedDiary
GO
ALTER TABLE dbo.ArchivedProtocolResults
	DROP CONSTRAINT FK_ArchivedProtocolResults_ArchivedDiary
GO
DROP TABLE dbo.ArchivedDiary
GO
EXECUTE sp_rename N'dbo.Tmp_ArchivedDiary', N'ArchivedDiary', 'OBJECT' 
GO
ALTER TABLE dbo.ArchivedDiary ADD CONSTRAINT
	PK_ArchivedDiary PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.ArchivedProtocolResults WITH NOCHECK ADD CONSTRAINT
	FK_ArchivedProtocolResults_ArchivedDiary FOREIGN KEY
	(
	ArchivedDiaryId
	) REFERENCES dbo.ArchivedDiary
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.ArchivedProtocolResults SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.ArchivedProtocolRemarks WITH NOCHECK ADD CONSTRAINT
	FK_ArchivedProtocolRemarks_ArchivedDiary FOREIGN KEY
	(
	ArchivedDiaryId
	) REFERENCES dbo.ArchivedDiary
	(
	Id
	) ON UPDATE  CASCADE 
	 ON DELETE  CASCADE 
	
GO
ALTER TABLE dbo.ArchivedProtocolRemarks SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.ArchivedProducts WITH NOCHECK ADD CONSTRAINT
	FK_ArchivedProducts_ArchivedDiary FOREIGN KEY
	(
	ArchivedDiaryId
	) REFERENCES dbo.ArchivedDiary
	(
	Id
	) ON UPDATE  CASCADE 
	 ON DELETE  CASCADE 
	
GO
ALTER TABLE dbo.ArchivedProducts SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
