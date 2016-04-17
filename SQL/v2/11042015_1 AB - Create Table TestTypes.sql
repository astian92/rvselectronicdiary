USE [DB_9BCA69_RedDb]
GO

/****** Object:  Table [dbo].[TestTypes]    Script Date: 11.4.2016 ã. 21:00:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TestTypes](
	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Type] [nvarchar](150) NOT NULL,
	[ShortName] [nvarchar](4) NOT NULL,
 CONSTRAINT [PK_TestTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[TestTypes] ADD  CONSTRAINT [DF_TestTypes_Id]  DEFAULT (newid()) FOR [Id]
GO


