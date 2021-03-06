/*
   Wednesday, July 26, 20178:19:24 PM
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
ALTER TABLE dbo.TestTypes ADD
	SortOrder int NOT NULL CONSTRAINT DF_TestTypes_SortOrder DEFAULT 0
GO
ALTER TABLE dbo.TestTypes SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
