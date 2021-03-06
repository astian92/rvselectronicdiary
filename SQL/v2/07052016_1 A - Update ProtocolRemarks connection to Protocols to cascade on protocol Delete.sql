/*
   07 май 2016 г.16:01:16
   User: borko
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
ALTER TABLE dbo.ProtocolsRemarks
	DROP CONSTRAINT FK_ProtocolsRemarks_Protocols
GO
ALTER TABLE dbo.Protocols SET (LOCK_ESCALATION = TABLE)
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
	) ON UPDATE  CASCADE 
	 ON DELETE  CASCADE 
	
GO
ALTER TABLE dbo.ProtocolsRemarks SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
