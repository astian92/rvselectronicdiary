/*
   03 юли 2016 г.16:24:00
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
ALTER TABLE dbo.ProductTests
	DROP CONSTRAINT FK_ProductTests_Products
GO
ALTER TABLE dbo.Products SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.ProductTests
	DROP CONSTRAINT FK_ProductTests_Tests
GO
ALTER TABLE dbo.Tests SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_ProductTests
	(
	Id uniqueidentifier NOT NULL,
	ProductId uniqueidentifier NOT NULL,
	TestId uniqueidentifier NOT NULL,
	TestMethodId uniqueidentifier NOT NULL,
	Units int NOT NULL,
	MethodValue nvarchar(400) NULL,
	Remark nvarchar(400) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_ProductTests SET (LOCK_ESCALATION = TABLE)
GO
IF EXISTS(SELECT * FROM dbo.ProductTests)
	 EXEC('INSERT INTO dbo.Tmp_ProductTests (Id, ProductId, TestId, Units, MethodValue, Remark)
		SELECT Id, ProductId, TestId, Units, MethodValue, Remark FROM dbo.ProductTests WITH (HOLDLOCK TABLOCKX)')
GO
ALTER TABLE dbo.ProtocolResults
	DROP CONSTRAINT FK_ProtocolResults_Products
GO
DROP TABLE dbo.ProductTests
GO
EXECUTE sp_rename N'dbo.Tmp_ProductTests', N'ProductTests', 'OBJECT' 
GO
ALTER TABLE dbo.ProductTests ADD CONSTRAINT
	PK_ProductTests PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.ProductTests ADD CONSTRAINT
	FK_ProductTests_Tests FOREIGN KEY
	(
	TestId
	) REFERENCES dbo.Tests
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.ProductTests ADD CONSTRAINT
	FK_ProductTests_Products FOREIGN KEY
	(
	ProductId
	) REFERENCES dbo.Products
	(
	Id
	) ON UPDATE  CASCADE 
	 ON DELETE  CASCADE 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.ProtocolResults ADD CONSTRAINT
	FK_ProtocolResults_Products FOREIGN KEY
	(
	ProductTestId
	) REFERENCES dbo.ProductTests
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.ProtocolResults SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
