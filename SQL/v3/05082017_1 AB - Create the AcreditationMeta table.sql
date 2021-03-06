/*
   Saturday, August 5, 20172:29:29 PM
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
CREATE TABLE dbo.AcreditationMeta
	(
	Id int NOT NULL IDENTITY (1, 1),
	Registered datetime NOT NULL,
	ValidTo datetime NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.AcreditationMeta ADD CONSTRAINT
	PK_AcreditationMeta_1 PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.AcreditationMeta SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
