/*
   Sunday, August 20, 20173:36:32 PM
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
ALTER TABLE dbo.ArchivedProtocolResults
	DROP CONSTRAINT FK_ArchivedProtocolResults_ArchivedDiary
GO
ALTER TABLE dbo.ArchivedProtocolRemarks
	DROP CONSTRAINT FK_ArchivedProtocolRemarks_ArchivedDiary
GO
ALTER TABLE dbo.ArchivedProducts
	DROP CONSTRAINT FK_ArchivedProducts_ArchivedDiary
GO
ALTER TABLE dbo.ArchivedDiary ADD
	AcreditationRegisteredDate datetime NULL,
	AcreditationValidDate datetime NULL
GO
ALTER TABLE dbo.ArchivedDiary SET (LOCK_ESCALATION = TABLE)
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
