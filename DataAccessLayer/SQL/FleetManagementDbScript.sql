USE [master]
GO
/****** Object:  Database [FleetManagementDb]    Script Date: 02-01-22 23:15:51 ******/
CREATE DATABASE [FleetManagementDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FleetManagementDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\FleetManagementDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'FleetManagementDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\FleetManagementDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [FleetManagementDb] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FleetManagementDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FleetManagementDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FleetManagementDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FleetManagementDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FleetManagementDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FleetManagementDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [FleetManagementDb] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [FleetManagementDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FleetManagementDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FleetManagementDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FleetManagementDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FleetManagementDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FleetManagementDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FleetManagementDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FleetManagementDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FleetManagementDb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [FleetManagementDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FleetManagementDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FleetManagementDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FleetManagementDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FleetManagementDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FleetManagementDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FleetManagementDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FleetManagementDb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [FleetManagementDb] SET  MULTI_USER 
GO
ALTER DATABASE [FleetManagementDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FleetManagementDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FleetManagementDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FleetManagementDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FleetManagementDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [FleetManagementDb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [FleetManagementDb] SET QUERY_STORE = OFF
GO
USE [FleetManagementDb]
GO
/****** Object:  Table [dbo].[Bestuurders]    Script Date: 02-01-22 23:15:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bestuurders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Naam] [nvarchar](50) NOT NULL,
	[Voornaam] [nvarchar](50) NOT NULL,
	[Postcode] [int] NULL,
	[Gemeente] [nvarchar](50) NULL,
	[Straat] [nvarchar](100) NULL,
	[Huisnummer] [nvarchar](50) NULL,
	[Geboortedatum] [date] NULL,
	[Rijksregisternummer] [nvarchar](50) NOT NULL,
	[Rijbewijs] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tankkaarten]    Script Date: 02-01-22 23:15:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tankkaarten](
	[Kaartnummer] [int] IDENTITY(1,1) NOT NULL,
	[Geldigheidsdatum] [date] NOT NULL,
	[Pincode] [nvarchar](50) NULL,
	[Brandstof] [nvarchar](50) NULL,
	[BestuurderId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Kaartnummer] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Voertuigen]    Script Date: 02-01-22 23:15:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Voertuigen](
	[Chassisnummer] [nvarchar](50) NOT NULL,
	[Merk] [nvarchar](50) NOT NULL,
	[Model] [nvarchar](50) NOT NULL,
	[Nummerplaat] [nvarchar](15) NOT NULL,
	[Brandstof] [nvarchar](50) NOT NULL,
	[Typewagen] [nvarchar](50) NOT NULL,
	[Kleur] [nvarchar](50) NULL,
	[AantalDeuren] [int] NULL,
	[BestuurderId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Chassisnummer] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Tankkaarten]  WITH CHECK ADD  CONSTRAINT [FK_BestuurderTankkaart] FOREIGN KEY([BestuurderId])
REFERENCES [dbo].[Bestuurders] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Tankkaarten] CHECK CONSTRAINT [FK_BestuurderTankkaart]
GO
ALTER TABLE [dbo].[Voertuigen]  WITH CHECK ADD  CONSTRAINT [FK_BestuurderVoertuig] FOREIGN KEY([BestuurderId])
REFERENCES [dbo].[Bestuurders] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Voertuigen] CHECK CONSTRAINT [FK_BestuurderVoertuig]
GO
USE [master]
GO
ALTER DATABASE [FleetManagementDb] SET  READ_WRITE 
GO
