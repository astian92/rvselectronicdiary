/*
   03 юли 2016 г.15:52:29
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
ALTER TABLE dbo.Tests
	DROP COLUMN TestMethods
GO
ALTER TABLE dbo.Tests SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
