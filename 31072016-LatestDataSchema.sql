USE [master]
GO
/****** Object:  Database [DB_9BCA69_RedDb]    Script Date: 31.7.2016 г. 19:12:36 ******/
CREATE DATABASE [DB_9BCA69_RedDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RedDataAzure_Data', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MINC\MSSQL\DATA\DB_9BCA69_RedDb_data.mdf' , SIZE = 80832KB , MAXSIZE = 512000KB , FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'RedDataAzure_Log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MINC\MSSQL\DATA\DB_9BCA69_RedDb_log.ldf' , SIZE = 2816KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DB_9BCA69_RedDb] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DB_9BCA69_RedDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DB_9BCA69_RedDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DB_9BCA69_RedDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DB_9BCA69_RedDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DB_9BCA69_RedDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DB_9BCA69_RedDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [DB_9BCA69_RedDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DB_9BCA69_RedDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DB_9BCA69_RedDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DB_9BCA69_RedDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DB_9BCA69_RedDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DB_9BCA69_RedDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DB_9BCA69_RedDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DB_9BCA69_RedDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DB_9BCA69_RedDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DB_9BCA69_RedDb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DB_9BCA69_RedDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DB_9BCA69_RedDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DB_9BCA69_RedDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DB_9BCA69_RedDb] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [DB_9BCA69_RedDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DB_9BCA69_RedDb] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [DB_9BCA69_RedDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DB_9BCA69_RedDb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DB_9BCA69_RedDb] SET  MULTI_USER 
GO
ALTER DATABASE [DB_9BCA69_RedDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DB_9BCA69_RedDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DB_9BCA69_RedDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DB_9BCA69_RedDb] SET TARGET_RECOVERY_TIME = 120 SECONDS 
GO
ALTER DATABASE [DB_9BCA69_RedDb] SET DELAYED_DURABILITY = DISABLED 
GO
USE [DB_9BCA69_RedDb]
GO
/****** Object:  Table [dbo].[AcredetationLevels]    Script Date: 31.7.2016 г. 19:12:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AcredetationLevels](
	[Id] [uniqueidentifier] NOT NULL,
	[Level] [nvarchar](2) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK__Acredeta__3214EC07B5FD218B] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ActionLogProperties]    Script Date: 31.7.2016 г. 19:12:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActionLogProperties](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LogId] [int] NOT NULL,
	[PropertyName] [nvarchar](30) NOT NULL,
	[OldValue] [nvarchar](100) NULL,
	[NewValue] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ActionLogs]    Script Date: 31.7.2016 г. 19:12:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActionLogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[TableName] [nvarchar](25) NOT NULL,
	[FullTableName] [nvarchar](80) NOT NULL,
	[On] [datetime] NOT NULL,
	[ActionTypeId] [int] NOT NULL,
	[TableNameBg] [nvarchar](25) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ActionTypes]    Script Date: 31.7.2016 г. 19:12:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActionTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nchar](20) NOT NULL,
	[TypeNameBg] [nchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ArchivedDiary]    Script Date: 31.7.2016 г. 19:12:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArchivedDiary](
	[Id] [uniqueidentifier] NOT NULL,
	[Number] [int] NOT NULL,
	[AcceptanceDateAndTime] [datetime] NOT NULL,
	[LetterNumber] [nvarchar](40) NOT NULL,
	[LetterDate] [datetime] NOT NULL,
	[Contractor] [nvarchar](150) NOT NULL,
	[Client] [nvarchar](300) NOT NULL,
	[ClientMobile] [nvarchar](30) NULL,
	[Comment] [ntext] NULL,
	[RequestDate] [datetime] NOT NULL,
	[RequestAcceptedBy] [nvarchar](200) NOT NULL,
	[RequestTestingPeriod] [int] NULL,
	[ProtocolIssuedDate] [datetime] NOT NULL,
	[ProtocolTesterMKB] [nvarchar](300) NULL,
	[ProtocolTesterFZH] [nvarchar](300) NULL,
	[ProtocolLabLeader] [nvarchar](300) NOT NULL,
	[Remark] [nvarchar](8) NULL,
 CONSTRAINT [PK_ArchivedDiary] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ArchivedProducts]    Script Date: 31.7.2016 г. 19:12:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArchivedProducts](
	[Id] [uniqueidentifier] NOT NULL,
	[ArchivedDiaryId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](70) NOT NULL,
	[Quantity] [nvarchar](50) NOT NULL,
	[Number] [int] NOT NULL,
 CONSTRAINT [PK_ArchivedProducts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ArchivedProductTests]    Script Date: 31.7.2016 г. 19:12:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArchivedProductTests](
	[Id] [uniqueidentifier] NOT NULL,
	[ArchivedProductId] [uniqueidentifier] NOT NULL,
	[TestName] [nvarchar](150) NOT NULL,
	[TestUnitName] [nvarchar](60) NULL,
	[TestMethods] [nvarchar](150) NULL,
	[TestAcredetationLevel] [nvarchar](2) NOT NULL,
	[TestTemperature] [nvarchar](50) NULL,
	[TestCategory] [nvarchar](120) NOT NULL,
	[TestType] [nvarchar](150) NOT NULL,
	[TestTypeShortName] [nvarchar](4) NOT NULL,
	[MethodValue] [nvarchar](400) NOT NULL,
	[Remark] [nvarchar](400) NULL,
 CONSTRAINT [PK_ArchivedProductTests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ArchivedProtocolRemarks]    Script Date: 31.7.2016 г. 19:12:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArchivedProtocolRemarks](
	[Id] [uniqueidentifier] NOT NULL,
	[ArchivedDiaryId] [uniqueidentifier] NOT NULL,
	[Number] [int] NOT NULL,
	[Remark] [nvarchar](3500) NOT NULL,
	[AcredetationLevel] [nvarchar](2) NOT NULL,
 CONSTRAINT [PK_ArchivedProtocolRemarks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ArchivedProtocolResults]    Script Date: 31.7.2016 г. 19:12:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArchivedProtocolResults](
	[Id] [uniqueidentifier] NOT NULL,
	[ArchivedDiaryId] [uniqueidentifier] NOT NULL,
	[ArchivedProductTestId] [uniqueidentifier] NOT NULL,
	[ResultNumber] [nvarchar](25) NOT NULL,
	[Results] [nvarchar](800) NOT NULL,
 CONSTRAINT [PK_ArchivedProtocolResults] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Clients]    Script Date: 31.7.2016 г. 19:12:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](120) NOT NULL,
	[Mobile] [nvarchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Diary]    Script Date: 31.7.2016 г. 19:12:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Diary](
	[Id] [uniqueidentifier] NOT NULL,
	[Number] [int] NOT NULL,
	[AcceptanceDateAndTime] [datetime] NOT NULL,
	[LetterNumber] [int] NULL,
	[LetterDate] [date] NOT NULL,
	[Contractor] [nvarchar](50) NULL,
	[ClientId] [uniqueidentifier] NOT NULL,
	[Comment] [ntext] NULL,
 CONSTRAINT [PK__Diary__3214EC076B08718B] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ELMAH_Error]    Script Date: 31.7.2016 г. 19:12:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ELMAH_Error](
	[ErrorId] [uniqueidentifier] NOT NULL,
	[Application] [nvarchar](60) NOT NULL,
	[Host] [nvarchar](50) NOT NULL,
	[Type] [nvarchar](100) NOT NULL,
	[Source] [nvarchar](60) NOT NULL,
	[Message] [nvarchar](500) NOT NULL,
	[User] [nvarchar](50) NOT NULL,
	[StatusCode] [int] NOT NULL,
	[TimeUtc] [datetime] NOT NULL,
	[Sequence] [int] IDENTITY(1,1) NOT NULL,
	[AllXml] [ntext] NOT NULL,
 CONSTRAINT [PK_ELMAH_Error] PRIMARY KEY NONCLUSTERED 
(
	[ErrorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Features]    Script Date: 31.7.2016 г. 19:12:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Features](
	[Id] [uniqueidentifier] NOT NULL,
	[DisplayName] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Products]    Script Date: 31.7.2016 г. 19:12:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [uniqueidentifier] NOT NULL,
	[DiaryId] [uniqueidentifier] NOT NULL,
	[Number] [int] NOT NULL,
	[Name] [nvarchar](70) NOT NULL,
	[Quantity] [nvarchar](30) NULL,
 CONSTRAINT [PK__Products__3214EC070351658B] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductTests]    Script Date: 31.7.2016 г. 19:12:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductTests](
	[Id] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[TestId] [uniqueidentifier] NOT NULL,
	[TestMethodId] [uniqueidentifier] NOT NULL,
	[Units] [int] NOT NULL,
	[MethodValue] [nvarchar](400) NULL,
	[Remark] [nvarchar](400) NULL,
 CONSTRAINT [PK_ProductTests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProtocolResults]    Script Date: 31.7.2016 г. 19:12:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProtocolResults](
	[Id] [uniqueidentifier] NOT NULL,
	[ProtocolId] [uniqueidentifier] NOT NULL,
	[ProductTestId] [uniqueidentifier] NOT NULL,
	[ResultNumber] [nchar](25) NOT NULL,
	[Results] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_ProtocolResults] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Protocols]    Script Date: 31.7.2016 г. 19:12:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Protocols](
	[Id] [uniqueidentifier] NOT NULL,
	[RequestId] [uniqueidentifier] NOT NULL,
	[IssuedDate] [datetime] NOT NULL,
	[TesterMKB] [nvarchar](300) NULL,
	[TesterFZH] [nvarchar](300) NULL,
	[LabLeader] [nvarchar](300) NOT NULL,
 CONSTRAINT [PK_Protocol] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProtocolsRemarks]    Script Date: 31.7.2016 г. 19:12:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProtocolsRemarks](
	[Id] [uniqueidentifier] NOT NULL,
	[ProtocolId] [uniqueidentifier] NOT NULL,
	[RemarkId] [uniqueidentifier] NOT NULL,
	[AcredetationLevelId] [uniqueidentifier] NOT NULL,
	[Number] [int] NOT NULL,
 CONSTRAINT [PK_ProtocolsRemarks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Remarks]    Script Date: 31.7.2016 г. 19:12:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Remarks](
	[Id] [uniqueidentifier] NOT NULL,
	[Text] [nvarchar](3500) NOT NULL,
 CONSTRAINT [PK_Remarks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Requests]    Script Date: 31.7.2016 г. 19:12:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Requests](
	[Id] [uniqueidentifier] NOT NULL,
	[DiaryId] [uniqueidentifier] NOT NULL,
	[Date] [datetime] NOT NULL,
	[AcceptedBy] [uniqueidentifier] NULL,
	[IsAccepted] [bit] NOT NULL,
	[TestingPeriod] [int] NULL,
 CONSTRAINT [PK_Requests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Roles]    Script Date: 31.7.2016 г. 19:12:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [uniqueidentifier] NOT NULL,
	[DisplayName] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RolesFeatures]    Script Date: 31.7.2016 г. 19:12:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolesFeatures](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[FeatureId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_RolesFeatures] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TestCategories]    Script Date: 31.7.2016 г. 19:12:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestCategories](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](120) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TestMethods]    Script Date: 31.7.2016 г. 19:12:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestMethods](
	[Id] [uniqueidentifier] NOT NULL,
	[TestId] [uniqueidentifier] NOT NULL,
	[Method] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tests]    Script Date: 31.7.2016 г. 19:12:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tests](
	[Id] [uniqueidentifier] NOT NULL,
	[TestCategoryId] [uniqueidentifier] NOT NULL,
	[TypeId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[AcredetationLevelId] [uniqueidentifier] NOT NULL,
	[Temperature] [nvarchar](50) NULL,
	[UnitName] [nvarchar](60) NULL,
	[MethodValue] [nvarchar](400) NULL,
 CONSTRAINT [PK__Tests__3214EC07452D85B3] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TestTypes]    Script Date: 31.7.2016 г. 19:12:37 ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 31.7.2016 г. 19:12:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Position] [nvarchar](50) NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK__Users__3214EC07756630A5] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_ELMAH_Error_App_Time_Seq]    Script Date: 31.7.2016 г. 19:12:37 ******/
CREATE NONCLUSTERED INDEX [IX_ELMAH_Error_App_Time_Seq] ON [dbo].[ELMAH_Error]
(
	[Application] ASC,
	[TimeUtc] DESC,
	[Sequence] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ELMAH_Error] ADD  CONSTRAINT [DF_ELMAH_Error_ErrorId]  DEFAULT (newid()) FOR [ErrorId]
GO
ALTER TABLE [dbo].[Requests] ADD  CONSTRAINT [DF_Requests_IsAccepted]  DEFAULT ((0)) FOR [IsAccepted]
GO
ALTER TABLE [dbo].[TestTypes] ADD  CONSTRAINT [DF_TestTypes_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[ActionLogProperties]  WITH CHECK ADD  CONSTRAINT [FK_ActionLogProperties_ToTable] FOREIGN KEY([LogId])
REFERENCES [dbo].[ActionLogs] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ActionLogProperties] CHECK CONSTRAINT [FK_ActionLogProperties_ToTable]
GO
ALTER TABLE [dbo].[ActionLogs]  WITH CHECK ADD  CONSTRAINT [FK_ActionLogs_ActionTypes] FOREIGN KEY([ActionTypeId])
REFERENCES [dbo].[ActionTypes] ([Id])
GO
ALTER TABLE [dbo].[ActionLogs] CHECK CONSTRAINT [FK_ActionLogs_ActionTypes]
GO
ALTER TABLE [dbo].[ActionLogs]  WITH CHECK ADD  CONSTRAINT [FK_ActionLogs_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ActionLogs] CHECK CONSTRAINT [FK_ActionLogs_Users]
GO
ALTER TABLE [dbo].[ArchivedProducts]  WITH CHECK ADD  CONSTRAINT [FK_ArchivedProducts_ArchivedDiary] FOREIGN KEY([ArchivedDiaryId])
REFERENCES [dbo].[ArchivedDiary] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ArchivedProducts] CHECK CONSTRAINT [FK_ArchivedProducts_ArchivedDiary]
GO
ALTER TABLE [dbo].[ArchivedProductTests]  WITH CHECK ADD  CONSTRAINT [FK_ArchivedProductTests_ArchivedProducts] FOREIGN KEY([ArchivedProductId])
REFERENCES [dbo].[ArchivedProducts] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ArchivedProductTests] CHECK CONSTRAINT [FK_ArchivedProductTests_ArchivedProducts]
GO
ALTER TABLE [dbo].[ArchivedProtocolRemarks]  WITH CHECK ADD  CONSTRAINT [FK_ArchivedProtocolRemarks_ArchivedDiary] FOREIGN KEY([ArchivedDiaryId])
REFERENCES [dbo].[ArchivedDiary] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ArchivedProtocolRemarks] CHECK CONSTRAINT [FK_ArchivedProtocolRemarks_ArchivedDiary]
GO
ALTER TABLE [dbo].[ArchivedProtocolResults]  WITH CHECK ADD  CONSTRAINT [FK_ArchivedProtocolResults_ArchivedDiary] FOREIGN KEY([ArchivedDiaryId])
REFERENCES [dbo].[ArchivedDiary] ([Id])
GO
ALTER TABLE [dbo].[ArchivedProtocolResults] CHECK CONSTRAINT [FK_ArchivedProtocolResults_ArchivedDiary]
GO
ALTER TABLE [dbo].[ArchivedProtocolResults]  WITH CHECK ADD  CONSTRAINT [FK_ArchivedProtocolResults_ArchivedProductTests] FOREIGN KEY([ArchivedProductTestId])
REFERENCES [dbo].[ArchivedProductTests] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ArchivedProtocolResults] CHECK CONSTRAINT [FK_ArchivedProtocolResults_ArchivedProductTests]
GO
ALTER TABLE [dbo].[Diary]  WITH CHECK ADD  CONSTRAINT [FK_Diary_Clients] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Clients] ([Id])
GO
ALTER TABLE [dbo].[Diary] CHECK CONSTRAINT [FK_Diary_Clients]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Diary] FOREIGN KEY([DiaryId])
REFERENCES [dbo].[Diary] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Diary]
GO
ALTER TABLE [dbo].[ProductTests]  WITH CHECK ADD  CONSTRAINT [FK_ProductTests_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[ProductTests] CHECK CONSTRAINT [FK_ProductTests_Products]
GO
ALTER TABLE [dbo].[ProductTests]  WITH CHECK ADD  CONSTRAINT [FK_ProductTests_TestMethods] FOREIGN KEY([TestMethodId])
REFERENCES [dbo].[TestMethods] ([Id])
GO
ALTER TABLE [dbo].[ProductTests] CHECK CONSTRAINT [FK_ProductTests_TestMethods]
GO
ALTER TABLE [dbo].[ProductTests]  WITH CHECK ADD  CONSTRAINT [FK_ProductTests_Tests] FOREIGN KEY([TestId])
REFERENCES [dbo].[Tests] ([Id])
GO
ALTER TABLE [dbo].[ProductTests] CHECK CONSTRAINT [FK_ProductTests_Tests]
GO
ALTER TABLE [dbo].[ProtocolResults]  WITH CHECK ADD  CONSTRAINT [FK_ProtocolResults_ProductTests] FOREIGN KEY([ProductTestId])
REFERENCES [dbo].[ProductTests] ([Id])
GO
ALTER TABLE [dbo].[ProtocolResults] CHECK CONSTRAINT [FK_ProtocolResults_ProductTests]
GO
ALTER TABLE [dbo].[ProtocolResults]  WITH CHECK ADD  CONSTRAINT [FK_ProtocolResults_Protocols] FOREIGN KEY([ProtocolId])
REFERENCES [dbo].[Protocols] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProtocolResults] CHECK CONSTRAINT [FK_ProtocolResults_Protocols]
GO
ALTER TABLE [dbo].[Protocols]  WITH CHECK ADD  CONSTRAINT [FK_Protocols_Requests] FOREIGN KEY([RequestId])
REFERENCES [dbo].[Requests] ([Id])
GO
ALTER TABLE [dbo].[Protocols] CHECK CONSTRAINT [FK_Protocols_Requests]
GO
ALTER TABLE [dbo].[ProtocolsRemarks]  WITH CHECK ADD  CONSTRAINT [FK_ProtocolsRemarks_AcredetationLevels] FOREIGN KEY([AcredetationLevelId])
REFERENCES [dbo].[AcredetationLevels] ([Id])
GO
ALTER TABLE [dbo].[ProtocolsRemarks] CHECK CONSTRAINT [FK_ProtocolsRemarks_AcredetationLevels]
GO
ALTER TABLE [dbo].[ProtocolsRemarks]  WITH CHECK ADD  CONSTRAINT [FK_ProtocolsRemarks_Protocols] FOREIGN KEY([ProtocolId])
REFERENCES [dbo].[Protocols] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProtocolsRemarks] CHECK CONSTRAINT [FK_ProtocolsRemarks_Protocols]
GO
ALTER TABLE [dbo].[ProtocolsRemarks]  WITH CHECK ADD  CONSTRAINT [FK_ProtocolsRemarks_Remarks] FOREIGN KEY([RemarkId])
REFERENCES [dbo].[Remarks] ([Id])
GO
ALTER TABLE [dbo].[ProtocolsRemarks] CHECK CONSTRAINT [FK_ProtocolsRemarks_Remarks]
GO
ALTER TABLE [dbo].[Requests]  WITH CHECK ADD  CONSTRAINT [FK_Requests_Diary] FOREIGN KEY([DiaryId])
REFERENCES [dbo].[Diary] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Requests] CHECK CONSTRAINT [FK_Requests_Diary]
GO
ALTER TABLE [dbo].[Requests]  WITH CHECK ADD  CONSTRAINT [FK_Requests_Users] FOREIGN KEY([AcceptedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Requests] CHECK CONSTRAINT [FK_Requests_Users]
GO
ALTER TABLE [dbo].[RolesFeatures]  WITH CHECK ADD  CONSTRAINT [FK_RolesFeatures_Features] FOREIGN KEY([FeatureId])
REFERENCES [dbo].[Features] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RolesFeatures] CHECK CONSTRAINT [FK_RolesFeatures_Features]
GO
ALTER TABLE [dbo].[RolesFeatures]  WITH CHECK ADD  CONSTRAINT [FK_RolesFeatures_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RolesFeatures] CHECK CONSTRAINT [FK_RolesFeatures_Roles]
GO
ALTER TABLE [dbo].[TestMethods]  WITH CHECK ADD  CONSTRAINT [FK_TestMethods_Tests] FOREIGN KEY([TestId])
REFERENCES [dbo].[Tests] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TestMethods] CHECK CONSTRAINT [FK_TestMethods_Tests]
GO
ALTER TABLE [dbo].[Tests]  WITH CHECK ADD  CONSTRAINT [FK_Tests_AcredetationLevels] FOREIGN KEY([AcredetationLevelId])
REFERENCES [dbo].[AcredetationLevels] ([Id])
GO
ALTER TABLE [dbo].[Tests] CHECK CONSTRAINT [FK_Tests_AcredetationLevels]
GO
ALTER TABLE [dbo].[Tests]  WITH CHECK ADD  CONSTRAINT [FK_Tests_TestCategories] FOREIGN KEY([TestCategoryId])
REFERENCES [dbo].[TestCategories] ([Id])
GO
ALTER TABLE [dbo].[Tests] CHECK CONSTRAINT [FK_Tests_TestCategories]
GO
ALTER TABLE [dbo].[Tests]  WITH CHECK ADD  CONSTRAINT [FK_Tests_TestTypes] FOREIGN KEY([TypeId])
REFERENCES [dbo].[TestTypes] ([Id])
GO
ALTER TABLE [dbo].[Tests] CHECK CONSTRAINT [FK_Tests_TestTypes]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO
/****** Object:  StoredProcedure [dbo].[ELMAH_GetErrorsXml]    Script Date: 31.7.2016 г. 19:12:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ELMAH_GetErrorsXml]
(
    @Application NVARCHAR(60),
    @PageIndex INT = 0,
    @PageSize INT = 15,
    @TotalCount INT OUTPUT
)
AS 

    SET NOCOUNT ON

    DECLARE @FirstTimeUTC DATETIME
    DECLARE @FirstSequence INT
    DECLARE @StartRow INT
    DECLARE @StartRowIndex INT

    SELECT 
        @TotalCount = COUNT(1) 
    FROM 
        [ELMAH_Error]
    WHERE 
        [Application] = @Application

    -- Get the ID of the first error for the requested page

    SET @StartRowIndex = @PageIndex * @PageSize + 1

    IF @StartRowIndex <= @TotalCount
    BEGIN

        SET ROWCOUNT @StartRowIndex

        SELECT  
            @FirstTimeUTC = [TimeUtc],
            @FirstSequence = [Sequence]
        FROM 
            [ELMAH_Error]
        WHERE   
            [Application] = @Application
        ORDER BY 
            [TimeUtc] DESC, 
            [Sequence] DESC

    END
    ELSE
    BEGIN

        SET @PageSize = 0

    END

    -- Now set the row count to the requested page size and get
    -- all records below it for the pertaining application.

    SET ROWCOUNT @PageSize

    SELECT 
        errorId     = [ErrorId], 
        application = [Application],
        host        = [Host], 
        type        = [Type],
        source      = [Source],
        message     = [Message],
        [user]      = [User],
        statusCode  = [StatusCode], 
        time        = CONVERT(VARCHAR(50), [TimeUtc], 126) + 'Z'
    FROM 
        [ELMAH_Error] error
    WHERE
        [Application] = @Application
    AND
        [TimeUtc] <= @FirstTimeUTC
    AND 
        [Sequence] <= @FirstSequence
    ORDER BY
        [TimeUtc] DESC, 
        [Sequence] DESC
    FOR
        XML AUTO


GO
/****** Object:  StoredProcedure [dbo].[ELMAH_GetErrorXml]    Script Date: 31.7.2016 г. 19:12:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ELMAH_GetErrorXml]
(
    @Application NVARCHAR(60),
    @ErrorId UNIQUEIDENTIFIER
)
AS

    SET NOCOUNT ON

    SELECT 
        [AllXml]
    FROM 
        [ELMAH_Error]
    WHERE
        [ErrorId] = @ErrorId
    AND
        [Application] = @Application


GO
/****** Object:  StoredProcedure [dbo].[ELMAH_LogError]    Script Date: 31.7.2016 г. 19:12:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ELMAH_LogError]
(
    @ErrorId UNIQUEIDENTIFIER,
    @Application NVARCHAR(60),
    @Host NVARCHAR(30),
    @Type NVARCHAR(100),
    @Source NVARCHAR(60),
    @Message NVARCHAR(500),
    @User NVARCHAR(50),
    @AllXml NTEXT,
    @StatusCode INT,
    @TimeUtc DATETIME
)
AS

    SET NOCOUNT ON

    INSERT
    INTO
        [ELMAH_Error]
        (
            [ErrorId],
            [Application],
            [Host],
            [Type],
            [Source],
            [Message],
            [User],
            [AllXml],
            [StatusCode],
            [TimeUtc]
        )
    VALUES
        (
            @ErrorId,
            @Application,
            @Host,
            @Type,
            @Source,
            @Message,
            @User,
            @AllXml,
            @StatusCode,
            @TimeUtc
        )


GO
USE [master]
GO
ALTER DATABASE [DB_9BCA69_RedDb] SET  READ_WRITE 
GO
