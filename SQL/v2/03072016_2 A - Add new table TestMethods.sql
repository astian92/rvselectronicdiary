/*
   03 юли 2016 г.15:56:17
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
CREATE TABLE dbo.TestMethods
	(
	Id uniqueidentifier NOT NULL,
	TestId uniqueidentifier NOT NULL,
	Method nvarchar(150) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.TestMethods ADD CONSTRAINT
	PK_Table_1 PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.TestMethods SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
