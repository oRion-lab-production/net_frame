USE [master]
GO
/****** Object:  Database [vcs_db]    Script Date: 12-05-May-2023 12:55:30 PM ******/
CREATE DATABASE [vcs_db]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'vcs_db', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\vcs_db.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'vcs_db_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\vcs_db_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [vcs_db] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [vcs_db].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [vcs_db] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [vcs_db] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [vcs_db] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [vcs_db] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [vcs_db] SET ARITHABORT OFF 
GO
ALTER DATABASE [vcs_db] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [vcs_db] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [vcs_db] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [vcs_db] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [vcs_db] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [vcs_db] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [vcs_db] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [vcs_db] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [vcs_db] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [vcs_db] SET  DISABLE_BROKER 
GO
ALTER DATABASE [vcs_db] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [vcs_db] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [vcs_db] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [vcs_db] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [vcs_db] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [vcs_db] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [vcs_db] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [vcs_db] SET RECOVERY FULL 
GO
ALTER DATABASE [vcs_db] SET  MULTI_USER 
GO
ALTER DATABASE [vcs_db] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [vcs_db] SET DB_CHAINING OFF 
GO
ALTER DATABASE [vcs_db] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [vcs_db] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [vcs_db] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [vcs_db] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'vcs_db', N'ON'
GO
ALTER DATABASE [vcs_db] SET QUERY_STORE = ON
GO
ALTER DATABASE [vcs_db] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [vcs_db]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 12-05-May-2023 12:55:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Price] [decimal](19, 5) NOT NULL,
	[Quantity] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[CreatedDt] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](100) NULL,
	[ModifiedDt] [datetime] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 12-05-May-2023 12:55:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[CreatedDt] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](100) NULL,
	[ModifiedDt] [datetime] NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 12-05-May-2023 12:55:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](250) NOT NULL,
	[Password] [nvarchar](250) NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[CreatedDt] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](100) NULL,
	[ModifiedDt] [datetime] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VersionInfo]    Script Date: 12-05-May-2023 12:55:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VersionInfo](
	[Version] [bigint] NOT NULL,
	[AppliedOn] [datetime] NULL,
	[Description] [nvarchar](1024) NULL
) ON [PRIMARY]
GO
/****** Object:  Index [UC_Version]    Script Date: 12-05-May-2023 12:55:31 PM ******/
CREATE UNIQUE CLUSTERED INDEX [UC_Version] ON [dbo].[VersionInfo]
(
	[Version] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_Quantity]  DEFAULT ((0)) FOR [Quantity]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Role_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
USE [master]
GO
ALTER DATABASE [vcs_db] SET  READ_WRITE 
GO

INSERT INTO vcs_db.dbo.VersionInfo (Version,AppliedOn,Description) VALUES
	 (20230507130900,'2023-05-12 04:14:00.0','DB_20230507_130900_Initial'),
	 (20230507133500,'2023-05-12 04:14:25.0','DB_20230507_133500_Seeder'),
	 (20230507174800,'2023-05-12 04:14:00.0','DB_20230507_174800_Initial');
	 
INSERT INTO vcs_db.dbo.[Role] (Id,Name,IsActive,CreatedBy,CreatedDt,ModifiedBy,ModifiedDt) VALUES
	 ('79DA5D46-A845-4A54-BA8F-88FA49BF5D73','Admin',1,'system','2023-05-12 12:14:25.0',NULL,NULL),
	 ('439D0F3C-3673-466A-B347-A8F0760DA562','User',1,'system','2023-05-12 12:14:25.0',NULL,NULL);
