/*
   23 април 2016 г.18:43:36
   User: Astian
   Server: 77.78.32.78\MINC
   Database: DB_9BCA69_RedDb
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
ALTER TABLE dbo.Protocols
	DROP CONSTRAINT FK_Protocols_Requests
GO
ALTER TABLE dbo.Requests SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Protocols
	(
	Id uniqueidentifier NOT NULL,
	RequestId uniqueidentifier NOT NULL,
	IssuedDate date NOT NULL,
	TesterMKB nvarchar(300) NULL,
	TesterFZH nvarchar(300) NULL,
	LabLeader nvarchar(300) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Protocols SET (LOCK_ESCALATION = TABLE)
GO
IF EXISTS(SELECT * FROM dbo.Protocols)
	 EXEC('INSERT INTO dbo.Tmp_Protocols (Id, RequestId, IssuedDate, TesterMKB, LabLeader)
		SELECT Id, RequestId, IssuedDate, Tester, LabLeader FROM dbo.Protocols WITH (HOLDLOCK TABLOCKX)')
GO
ALTER TABLE dbo.ProtocolResults
	DROP CONSTRAINT FK_ProtocolResults_Protocols
GO
ALTER TABLE dbo.ProtocolsRemarks
	DROP CONSTRAINT FK_ProtocolsRemarks_Protocols
GO
DROP TABLE dbo.Protocols
GO
EXECUTE sp_rename N'dbo.Tmp_Protocols', N'Protocols', 'OBJECT' 
GO
ALTER TABLE dbo.Protocols ADD CONSTRAINT
	PK_Protocol PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Protocols ADD CONSTRAINT
	FK_Protocols_Requests FOREIGN KEY
	(
	RequestId
	) REFERENCES dbo.Requests
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.ProtocolsRemarks ADD CONSTRAINT
	FK_ProtocolsRemarks_Protocols FOREIGN KEY
	(
	ProtocolId
	) REFERENCES dbo.Protocols
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.ProtocolsRemarks SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.ProtocolResults ADD CONSTRAINT
	FK_ProtocolResults_Protocols FOREIGN KEY
	(
	ProtocolId
	) REFERENCES dbo.Protocols
	(
	Id
	) ON UPDATE  CASCADE 
	 ON DELETE  CASCADE 
	
GO
ALTER TABLE dbo.ProtocolResults SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
