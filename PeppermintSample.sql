USE [master]
GO
/****** Object:  Database [PeppermintSample]    Script Date: 6/14/2018 9:22:22 PM ******/
CREATE DATABASE [PeppermintSample]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PeppermintSample', FILENAME = N'C:\Databases\PeppermintSample.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'PeppermintSample_log', FILENAME = N'C:\Databases\PeppermintSample_log.ldf' , SIZE = 1280KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [PeppermintSample] SET COMPATIBILITY_LEVEL = 120
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
/****** Object:  Schema [blog]    Script Date: 6/14/2018 9:22:22 PM ******/
CREATE SCHEMA [blog]
GO
/****** Object:  Schema [core]    Script Date: 6/14/2018 9:22:22 PM ******/
CREATE SCHEMA [core]
GO
/****** Object:  Schema [forum]    Script Date: 6/14/2018 9:22:22 PM ******/
CREATE SCHEMA [forum]
GO
/****** Object:  Table [blog].[Categories]    Script Date: 6/14/2018 9:22:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [blog].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Slug] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [blog].[Posts]    Script Date: 6/14/2018 9:22:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [blog].[Posts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Title] [varchar](500) NOT NULL,
	[Slug] [varchar](700) NOT NULL,
	[Content] [varchar](max) NULL,
	[Created] [datetime] NOT NULL,
	[Updated] [datetime] NULL,
 CONSTRAINT [PK__tmp_ms_x__3214EC074866290F] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [blog].[PostTags]    Script Date: 6/14/2018 9:22:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [blog].[PostTags](
	[PostId] [int] NOT NULL,
	[Tag] [varchar](100) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [core].[GroupPermissions]    Script Date: 6/14/2018 9:22:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [core].[GroupPermissions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PermissionId] [int] NOT NULL,
	[GroupId] [int] NOT NULL,
	[Permit] [bit] NOT NULL,
	[GroupEntityId] [varchar](50) NULL,
 CONSTRAINT [PK_UserGroupPermissions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [core].[Groups]    Script Date: 6/14/2018 9:22:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [core].[Groups](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_UserGroups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [core].[Permissions]    Script Date: 6/14/2018 9:22:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [core].[Permissions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Module] [varchar](50) NOT NULL,
	[Group] [varchar](50) NULL,
	[Name] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [core].[RolePermissions]    Script Date: 6/14/2018 9:22:22 PM ******/
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
/****** Object:  Table [core].[Roles]    Script Date: 6/14/2018 9:22:22 PM ******/
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
/****** Object:  Table [core].[UserGroups]    Script Date: 6/14/2018 9:22:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [core].[UserGroups](
	[UserId] [int] NOT NULL,
	[GroupId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [core].[Users]    Script Date: 6/14/2018 9:22:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [core].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Email] [varchar](100) NOT NULL,
	[TwitterHandle] [varchar](50) NULL,
	[Excerpt] [varchar](500) NULL,
 CONSTRAINT [PK__Users__3214EC0765B6345A] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [forum].[Categories]    Script Date: 6/14/2018 9:22:22 PM ******/
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
/****** Object:  Table [forum].[Posts]    Script Date: 6/14/2018 9:22:22 PM ******/
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
ALTER TABLE [blog].[Posts]  WITH CHECK ADD  CONSTRAINT [FK_Posts_Categories] FOREIGN KEY([CategoryId])
REFERENCES [blog].[Categories] ([Id])
GO
ALTER TABLE [blog].[Posts] CHECK CONSTRAINT [FK_Posts_Categories]
GO
ALTER TABLE [blog].[PostTags]  WITH CHECK ADD  CONSTRAINT [FK_PostTags_Posts] FOREIGN KEY([PostId])
REFERENCES [blog].[Posts] ([Id])
GO
ALTER TABLE [blog].[PostTags] CHECK CONSTRAINT [FK_PostTags_Posts]
GO
ALTER TABLE [core].[GroupPermissions]  WITH CHECK ADD  CONSTRAINT [FK_UserGroupPermissions_Permissions] FOREIGN KEY([PermissionId])
REFERENCES [core].[Permissions] ([Id])
GO
ALTER TABLE [core].[GroupPermissions] CHECK CONSTRAINT [FK_UserGroupPermissions_Permissions]
GO
ALTER TABLE [core].[GroupPermissions]  WITH CHECK ADD  CONSTRAINT [FK_UserGroupPermissions_UserGroups] FOREIGN KEY([GroupId])
REFERENCES [core].[Groups] ([Id])
GO
ALTER TABLE [core].[GroupPermissions] CHECK CONSTRAINT [FK_UserGroupPermissions_UserGroups]
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
ALTER TABLE [core].[UserGroups]  WITH CHECK ADD  CONSTRAINT [FK_UserUserGroups_UserGroups] FOREIGN KEY([GroupId])
REFERENCES [core].[Groups] ([Id])
GO
ALTER TABLE [core].[UserGroups] CHECK CONSTRAINT [FK_UserUserGroups_UserGroups]
GO
ALTER TABLE [core].[UserGroups]  WITH CHECK ADD  CONSTRAINT [FK_UserUserGroups_Users] FOREIGN KEY([UserId])
REFERENCES [core].[Users] ([Id])
GO
ALTER TABLE [core].[UserGroups] CHECK CONSTRAINT [FK_UserUserGroups_Users]
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
