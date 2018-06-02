USE [master]
GO
/****** Object:  Database [PeppermintSample]    Script Date: 6/2/2018 1:38:21 PM ******/
CREATE DATABASE [PeppermintSample]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'NetifySample', FILENAME = N'C:\Databases\NetifySample.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'NetifySample_log', FILENAME = N'C:\Databases\NetifySample.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [PeppermintSample] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PeppermintSample].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PeppermintSample] SET ANSI_NULL_DEFAULT ON 
GO
ALTER DATABASE [PeppermintSample] SET ANSI_NULLS ON 
GO
ALTER DATABASE [PeppermintSample] SET ANSI_PADDING ON 
GO
ALTER DATABASE [PeppermintSample] SET ANSI_WARNINGS ON 
GO
ALTER DATABASE [PeppermintSample] SET ARITHABORT ON 
GO
ALTER DATABASE [PeppermintSample] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PeppermintSample] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PeppermintSample] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PeppermintSample] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PeppermintSample] SET CURSOR_DEFAULT  LOCAL 
GO
ALTER DATABASE [PeppermintSample] SET CONCAT_NULL_YIELDS_NULL ON 
GO
ALTER DATABASE [PeppermintSample] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PeppermintSample] SET QUOTED_IDENTIFIER ON 
GO
ALTER DATABASE [PeppermintSample] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PeppermintSample] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PeppermintSample] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PeppermintSample] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PeppermintSample] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PeppermintSample] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PeppermintSample] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PeppermintSample] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PeppermintSample] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PeppermintSample] SET RECOVERY FULL 
GO
ALTER DATABASE [PeppermintSample] SET  MULTI_USER 
GO
ALTER DATABASE [PeppermintSample] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PeppermintSample] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PeppermintSample] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PeppermintSample] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PeppermintSample] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PeppermintSample] SET QUERY_STORE = OFF
GO
USE [PeppermintSample]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [PeppermintSample]
GO
/****** Object:  Schema [blog]    Script Date: 6/2/2018 1:38:21 PM ******/
CREATE SCHEMA [blog]
GO
/****** Object:  Schema [core]    Script Date: 6/2/2018 1:38:21 PM ******/
CREATE SCHEMA [core]
GO
/****** Object:  Schema [forum]    Script Date: 6/2/2018 1:38:21 PM ******/
CREATE SCHEMA [forum]
GO
/****** Object:  Table [blog].[Posts]    Script Date: 6/2/2018 1:38:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [blog].[Posts](
	[Id] [int] IDENTITY(0,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Title] [varchar](500) NOT NULL,
	[Content] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [core].[Permissions]    Script Date: 6/2/2018 1:38:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [core].[Permissions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Module] [varchar](50) NOT NULL,
	[Group] [varchar](50) NULL,
	[Permission] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [core].[RolePermissions]    Script Date: 6/2/2018 1:38:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [core].[RolePermissions](
	[Id] [int] NOT NULL,
	[PermissionId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[Permit] [bit] NOT NULL,
	[GroupEntityId] [varchar](50) NULL,
 CONSTRAINT [PK_RolePermissions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [core].[Roles]    Script Date: 6/2/2018 1:38:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [core].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [core].[UserGroupPermissions]    Script Date: 6/2/2018 1:38:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [core].[UserGroupPermissions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PermissionId] [int] NOT NULL,
	[UserGroupId] [int] NOT NULL,
	[Permit] [bit] NOT NULL,
	[GroupEntityId] [varchar](50) NULL,
 CONSTRAINT [PK_UserGroupPermissions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [core].[UserGroups]    Script Date: 6/2/2018 1:38:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [core].[UserGroups](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_UserGroups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [core].[Users]    Script Date: 6/2/2018 1:38:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [core].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[Email] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [core].[UserUserGroups]    Script Date: 6/2/2018 1:38:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [core].[UserUserGroups](
	[UserId] [int] NOT NULL,
	[UserGroupId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [forum].[Categories]    Script Date: 6/2/2018 1:38:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [forum].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [forum].[Posts]    Script Date: 6/2/2018 1:38:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [forum].[Posts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Title] [varchar](100) NOT NULL,
	[Content] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [core].[RolePermissions]  WITH CHECK ADD  CONSTRAINT [FK_RolePermissions_Permissions] FOREIGN KEY([PermissionId])
REFERENCES [core].[Permissions] ([Id])
GO
ALTER TABLE [core].[RolePermissions] CHECK CONSTRAINT [FK_RolePermissions_Permissions]
GO
ALTER TABLE [core].[RolePermissions]  WITH CHECK ADD  CONSTRAINT [FK_RolePermissions_Roles] FOREIGN KEY([RoleId])
REFERENCES [core].[Roles] ([Id])
GO
ALTER TABLE [core].[RolePermissions] CHECK CONSTRAINT [FK_RolePermissions_Roles]
GO
ALTER TABLE [core].[UserGroupPermissions]  WITH CHECK ADD  CONSTRAINT [FK_UserGroupPermissions_Permissions] FOREIGN KEY([PermissionId])
REFERENCES [core].[Permissions] ([Id])
GO
ALTER TABLE [core].[UserGroupPermissions] CHECK CONSTRAINT [FK_UserGroupPermissions_Permissions]
GO
ALTER TABLE [core].[UserGroupPermissions]  WITH CHECK ADD  CONSTRAINT [FK_UserGroupPermissions_UserGroups] FOREIGN KEY([UserGroupId])
REFERENCES [core].[UserGroups] ([Id])
GO
ALTER TABLE [core].[UserGroupPermissions] CHECK CONSTRAINT [FK_UserGroupPermissions_UserGroups]
GO
ALTER TABLE [core].[UserUserGroups]  WITH CHECK ADD  CONSTRAINT [FK_UserUserGroups_UserGroups] FOREIGN KEY([UserGroupId])
REFERENCES [core].[UserGroups] ([Id])
GO
ALTER TABLE [core].[UserUserGroups] CHECK CONSTRAINT [FK_UserUserGroups_UserGroups]
GO
ALTER TABLE [core].[UserUserGroups]  WITH CHECK ADD  CONSTRAINT [FK_UserUserGroups_Users] FOREIGN KEY([UserId])
REFERENCES [core].[Users] ([Id])
GO
ALTER TABLE [core].[UserUserGroups] CHECK CONSTRAINT [FK_UserUserGroups_Users]
GO
ALTER TABLE [forum].[Posts]  WITH CHECK ADD  CONSTRAINT [FK_Posts_Categories] FOREIGN KEY([CategoryId])
REFERENCES [forum].[Categories] ([Id])
GO
ALTER TABLE [forum].[Posts] CHECK CONSTRAINT [FK_Posts_Categories]
GO
ALTER TABLE [forum].[Posts]  WITH CHECK ADD  CONSTRAINT [FK_Posts_Users] FOREIGN KEY([UserId])
REFERENCES [core].[Users] ([Id])
GO
ALTER TABLE [forum].[Posts] CHECK CONSTRAINT [FK_Posts_Users]
GO
USE [master]
GO
ALTER DATABASE [PeppermintSample] SET  READ_WRITE 
GO
