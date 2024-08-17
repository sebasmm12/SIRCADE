USE [master]
GO
/****** Object:  Database [BD_SIRCADE]    Script Date: 8/17/2024 6:29:42 PM ******/
CREATE DATABASE [BD_SIRCADE]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BD_SIRCADE', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\BD_SIRCADE.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BD_SIRCADE_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\BD_SIRCADE_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [BD_SIRCADE] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BD_SIRCADE].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BD_SIRCADE] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BD_SIRCADE] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BD_SIRCADE] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BD_SIRCADE] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BD_SIRCADE] SET ARITHABORT OFF 
GO
ALTER DATABASE [BD_SIRCADE] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [BD_SIRCADE] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BD_SIRCADE] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BD_SIRCADE] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BD_SIRCADE] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BD_SIRCADE] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BD_SIRCADE] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BD_SIRCADE] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BD_SIRCADE] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BD_SIRCADE] SET  ENABLE_BROKER 
GO
ALTER DATABASE [BD_SIRCADE] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BD_SIRCADE] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BD_SIRCADE] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BD_SIRCADE] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BD_SIRCADE] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BD_SIRCADE] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BD_SIRCADE] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BD_SIRCADE] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BD_SIRCADE] SET  MULTI_USER 
GO
ALTER DATABASE [BD_SIRCADE] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BD_SIRCADE] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BD_SIRCADE] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BD_SIRCADE] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BD_SIRCADE] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BD_SIRCADE] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [BD_SIRCADE] SET QUERY_STORE = OFF
GO
USE [BD_SIRCADE]
GO
/****** Object:  Table [dbo].[CanchasDeportivas]    Script Date: 8/17/2024 6:29:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CanchasDeportivas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Tipo] [int] NOT NULL,
	[Nombre] [varchar](100) NULL,
 CONSTRAINT [PK_CanchasDeportivas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContrasenasUsuario]    Script Date: 8/17/2024 6:29:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContrasenasUsuario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NULL,
	[Contrasena] [varchar](400) NULL,
 CONSTRAINT [PK_ContrasenasUsuario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetalleUsuario]    Script Date: 8/17/2024 6:29:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetalleUsuario](
	[Id] [int] NOT NULL,
	[Grado] [varchar](100) NULL,
	[ApellidoPaterno] [varchar](100) NULL,
	[ApellidoMaterno] [varchar](100) NULL,
	[Nombres] [varchar](500) NULL,
	[Unidad] [int] NULL,
	[FechaNacimiento] [date] NULL,
	[Direccion] [varchar](500) NULL,
	[Telefono] [varchar](100) NULL,
	[Celular] [varchar](100) NULL,
	[Correo] [varchar](100) NULL,
	[Afiliado] [bit] NULL,
	[Situacion] [int] NULL,
	[DNI] [varchar](8) NULL,
	[EstadoCivil] [int] NULL,
	[Observacion] [varchar](500) NULL,
 CONSTRAINT [PK_Socios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HorariosCancha]    Script Date: 8/17/2024 6:29:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HorariosCancha](
	[Id] [int] NOT NULL,
	[IdCancha] [int] NULL,
	[FechaInicio] [datetime] NULL,
	[FechaFin] [datetime] NULL,
	[Estado] [int] NULL,
	[IdUsuario] [int] NULL,
	[FechaRegistro] [datetime] NULL,
	[IdUsuarioModificador] [int] NULL,
	[FechaActualizacion] [datetime] NULL,
 CONSTRAINT [PK_HorariosCancha] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notificaciones]    Script Date: 8/17/2024 6:29:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notificaciones](
	[Id] [int] NOT NULL,
	[Tipo] [int] NULL,
	[Plantilla] [varchar](max) NULL,
	[TipoEnvio] [int] NULL,
 CONSTRAINT [PK_Notificaciones] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NotificacionesUsuario]    Script Date: 8/17/2024 6:29:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NotificacionesUsuario](
	[Id] [int] NOT NULL,
	[IdNotificacion] [int] NOT NULL,
	[IdUsuarioEmisor] [int] NOT NULL,
	[IdUsuarioReceptor] [int] NOT NULL,
	[Mensaje] [varchar](max) NULL,
	[FechaEnvio] [datetime] NULL,
	[Estado] [int] NULL,
 CONSTRAINT [PK_NotificacionesUsuario_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permisos]    Script Date: 8/17/2024 6:29:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permisos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NULL,
	[Descripcion] [varchar](100) NULL,
	[Tipo] [int] NULL,
	[Icono] [varchar](100) NULL,
 CONSTRAINT [PK_Permisos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PermisosRol]    Script Date: 8/17/2024 6:29:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PermisosRol](
	[IdRol] [int] NOT NULL,
	[IdPermiso] [int] NOT NULL,
 CONSTRAINT [PK_PermisosRol] PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC,
	[IdPermiso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reservas]    Script Date: 8/17/2024 6:29:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NULL,
	[IdHorario] [int] NULL,
	[Estado] [int] NULL,
 CONSTRAINT [PK_Reservas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 8/17/2024 6:29:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NULL,
	[Activo] [bit] NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TiposCanchaDeportiva]    Script Date: 8/17/2024 6:29:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TiposCanchaDeportiva](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NULL,
 CONSTRAINT [PK_TiposCanchaDeportiva] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Unidades]    Script Date: 8/17/2024 6:29:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Unidades](
	[Id] [int] NOT NULL,
	[Nombre] [varchar](500) NULL,
	[Ubicacion] [varchar](500) NULL,
	[SIGLAS] [varchar](100) NULL,
 CONSTRAINT [PK_Unidades] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 8/17/2024 6:29:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdRol] [int] NOT NULL,
	[NSA] [varchar](400) NULL,
	[Contrasena] [varchar](400) NULL,
	[Salt] [varchar](400) NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[CanchasDeportivas] ON 

INSERT [dbo].[CanchasDeportivas] ([Id], [Tipo], [Nombre]) VALUES (1, 1, N'Cancha de futbol 1')
INSERT [dbo].[CanchasDeportivas] ([Id], [Tipo], [Nombre]) VALUES (2, 1, N'Cancha de futbol 2')
INSERT [dbo].[CanchasDeportivas] ([Id], [Tipo], [Nombre]) VALUES (3, 2, N'Cancha de tenis 1')
INSERT [dbo].[CanchasDeportivas] ([Id], [Tipo], [Nombre]) VALUES (4, 2, N'Cancha de tenis 2')
INSERT [dbo].[CanchasDeportivas] ([Id], [Tipo], [Nombre]) VALUES (5, 2, N'Cancha de tenis 3')
INSERT [dbo].[CanchasDeportivas] ([Id], [Tipo], [Nombre]) VALUES (6, 3, N'Cancha de basket 1')
INSERT [dbo].[CanchasDeportivas] ([Id], [Tipo], [Nombre]) VALUES (8, 3, N'Cancha de basket 2')
INSERT [dbo].[CanchasDeportivas] ([Id], [Tipo], [Nombre]) VALUES (9, 4, N'Cancha de voley 1')
INSERT [dbo].[CanchasDeportivas] ([Id], [Tipo], [Nombre]) VALUES (10, 4, N'Cancha de voley 2')
INSERT [dbo].[CanchasDeportivas] ([Id], [Tipo], [Nombre]) VALUES (11, 4, N'Cancha de voley 3')
INSERT [dbo].[CanchasDeportivas] ([Id], [Tipo], [Nombre]) VALUES (12, 5, N'Cancha de fulbito 1')
INSERT [dbo].[CanchasDeportivas] ([Id], [Tipo], [Nombre]) VALUES (14, 5, N'Cancha de fulbito 2')
INSERT [dbo].[CanchasDeportivas] ([Id], [Tipo], [Nombre]) VALUES (16, 5, N'Cancha de fulbito 3')
INSERT [dbo].[CanchasDeportivas] ([Id], [Tipo], [Nombre]) VALUES (17, 5, N'Cancha de fulbito 4')
SET IDENTITY_INSERT [dbo].[CanchasDeportivas] OFF
GO
INSERT [dbo].[DetalleUsuario] ([Id], [Grado], [ApellidoPaterno], [ApellidoMaterno], [Nombres], [Unidad], [FechaNacimiento], [Direccion], [Telefono], [Celular], [Correo], [Afiliado], [Situacion], [DNI], [EstadoCivil], [Observacion]) VALUES (2, N'Coronel', N'ACUÑA', N'REYNOSO', N'FERNANDO', 1, CAST(N'1976-11-19' AS Date), N'Esquina Jirón Rodríguez de Mendoza N° 299 c', N'2679143', N'988479159', N'smirandamanrique@gmail.com', 1, 0, N'09763421', 0, N'')
GO
SET IDENTITY_INSERT [dbo].[Permisos] ON 

INSERT [dbo].[Permisos] ([Id], [Nombre], [Descripcion], [Tipo], [Icono]) VALUES (1, N'Roles', NULL, 0, NULL)
INSERT [dbo].[Permisos] ([Id], [Nombre], [Descripcion], [Tipo], [Icono]) VALUES (2, N'Socios', NULL, 0, NULL)
INSERT [dbo].[Permisos] ([Id], [Nombre], [Descripcion], [Tipo], [Icono]) VALUES (3, N'Personal', NULL, 0, NULL)
SET IDENTITY_INSERT [dbo].[Permisos] OFF
GO
INSERT [dbo].[PermisosRol] ([IdRol], [IdPermiso]) VALUES (1, 1)
INSERT [dbo].[PermisosRol] ([IdRol], [IdPermiso]) VALUES (1, 2)
INSERT [dbo].[PermisosRol] ([IdRol], [IdPermiso]) VALUES (1, 3)
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [Nombre], [Activo]) VALUES (1, N'Administrador', 1)
INSERT [dbo].[Roles] ([Id], [Nombre], [Activo]) VALUES (2, N'Socio', 1)
INSERT [dbo].[Roles] ([Id], [Nombre], [Activo]) VALUES (3, N'Recepcionista', 1)
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[TiposCanchaDeportiva] ON 

INSERT [dbo].[TiposCanchaDeportiva] ([Id], [Nombre]) VALUES (1, N'Futbol')
INSERT [dbo].[TiposCanchaDeportiva] ([Id], [Nombre]) VALUES (2, N'Tenis')
INSERT [dbo].[TiposCanchaDeportiva] ([Id], [Nombre]) VALUES (3, N'Basket')
INSERT [dbo].[TiposCanchaDeportiva] ([Id], [Nombre]) VALUES (4, N'Voley')
INSERT [dbo].[TiposCanchaDeportiva] ([Id], [Nombre]) VALUES (5, N'Fulbito')
SET IDENTITY_INSERT [dbo].[TiposCanchaDeportiva] OFF
GO
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (1, N'DIGLO', NULL, N'DIGLO')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (2, N'FOSEP', NULL, N'FOSEP')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (3, N'AGARG', NULL, N'AGARG')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (4, N'AGCOR', NULL, N'AGCOR')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (5, N'AGECU', NULL, N'AGECU')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (6, N'AGESP', NULL, N'AGESP')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (7, N'AGITA', NULL, N'AGITA')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (8, N'AGSRA', NULL, N'AGSRA')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (9, N'ALAR2', NULL, N'ALAR2')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (10, N'ALAR4', NULL, N'ALAR4')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (11, N'ALAR6', NULL, N'ALAR6')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (12, N'CAM', NULL, N'CAM')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (13, N'CAMEP', NULL, N'CAMEP')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (14, N'CASED', NULL, N'CASED')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (15, N'CAVRAE', NULL, N'CAVRAE')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (16, N'CCFA', NULL, N'CCFA')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (17, N'CEVRAE', NULL, N'CEVRAE')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (18, N'COMOP', NULL, N'COMOP')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (19, N'CONIDA', NULL, N'CONIDA')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (20, N'COPCE', NULL, N'COPCE')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (21, N'DEFOR', NULL, N'DEFOR')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (22, N'DIGED', NULL, N'DIGED')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (23, N'DIDEP', NULL, N'DIDEP')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (24, N'DIAPE', NULL, N'DIAPE')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (25, N'DIGPE', NULL, N'DIGPE')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (26, N'DIPAC', NULL, N'DIPAC')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (27, N'DIREC', NULL, N'DIREC')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (28, N'DISAN', NULL, N'DISAN')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (29, N'DITEL', NULL, N'DITEL')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (30, N'DIVAN', NULL, N'DIVAN')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (31, N'EMGRA', NULL, N'EMGRA')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (32, N'ESFAP', NULL, N'ESFAP')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (33, N'EAFAP', NULL, N'EAFAP')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (34, N'EOFAP', NULL, N'EOFAP')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (35, N'ESOFA', NULL, N'ESOFA')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (36, N'ESMAR', NULL, N'ESMAR')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (37, N'ESMON', NULL, N'ESMON')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (38, N'FOVIM', NULL, N'FOVIM')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (39, N'HOSPI', NULL, N'HOSPI')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (40, N'GRU31', NULL, N'GRU31')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (41, N'GRU42', NULL, N'GRU42')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (42, N'GRUP3', NULL, N'GRUP3')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (43, N'GRU51', NULL, N'GRU51')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (44, N'HORES', NULL, N'HORES')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (45, N'HOREO', NULL, N'HOREO')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (46, N'INSPE', NULL, N'INSPE')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (47, N'MIDEF', NULL, N'MIDEF')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (48, N'SEDIN', NULL, N'SEDIN')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (49, N'ODEMI', NULL, N'ODEMI')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (50, N'PEREXT', NULL, N'PEREXT')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (51, N'SECIN', NULL, N'SECIN')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (52, N'SECRE', NULL, N'SECRE')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (53, N'TSMP', NULL, N'TSMP')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (54, N'SEMFAP', NULL, N'SEMFAP')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (55, N'SEMSAC', NULL, N'SEMSAC')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (56, N'SESAN', NULL, N'SESAN')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (57, N'SINFA', NULL, N'SINFA')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (58, N'SEMAG', NULL, N'SEMAG')
INSERT [dbo].[Unidades] ([Id], [Nombre], [Ubicacion], [SIGLAS]) VALUES (59, N'SEINT', NULL, N'SEINT')
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 

INSERT [dbo].[Usuarios] ([Id], [IdRol], [NSA], [Contrasena], [Salt]) VALUES (2, 2, N'96529', N'b+86fZTsT5gWduW3fvrvXztqI2/NRoQ0uqMY0fdNwQM=', N'evRxu2TWOJk+jWl8RODZDg==')
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
ALTER TABLE [dbo].[CanchasDeportivas]  WITH CHECK ADD  CONSTRAINT [FK_CanchasDeportivas_TiposCanchaDeportiva] FOREIGN KEY([Tipo])
REFERENCES [dbo].[TiposCanchaDeportiva] ([Id])
GO
ALTER TABLE [dbo].[CanchasDeportivas] CHECK CONSTRAINT [FK_CanchasDeportivas_TiposCanchaDeportiva]
GO
ALTER TABLE [dbo].[ContrasenasUsuario]  WITH CHECK ADD  CONSTRAINT [FK_ContrasenasUsuario_Usuarios] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[ContrasenasUsuario] CHECK CONSTRAINT [FK_ContrasenasUsuario_Usuarios]
GO
ALTER TABLE [dbo].[DetalleUsuario]  WITH CHECK ADD  CONSTRAINT [FK_DetalleUsuario_Usuarios] FOREIGN KEY([Id])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[DetalleUsuario] CHECK CONSTRAINT [FK_DetalleUsuario_Usuarios]
GO
ALTER TABLE [dbo].[DetalleUsuario]  WITH CHECK ADD  CONSTRAINT [FK_Socios_Unidades] FOREIGN KEY([Unidad])
REFERENCES [dbo].[Unidades] ([Id])
GO
ALTER TABLE [dbo].[DetalleUsuario] CHECK CONSTRAINT [FK_Socios_Unidades]
GO
ALTER TABLE [dbo].[HorariosCancha]  WITH CHECK ADD  CONSTRAINT [FK_HorariosCancha_CanchasDeportivas] FOREIGN KEY([IdCancha])
REFERENCES [dbo].[CanchasDeportivas] ([Id])
GO
ALTER TABLE [dbo].[HorariosCancha] CHECK CONSTRAINT [FK_HorariosCancha_CanchasDeportivas]
GO
ALTER TABLE [dbo].[HorariosCancha]  WITH CHECK ADD  CONSTRAINT [FK_HorariosCancha_Usuarios] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[HorariosCancha] CHECK CONSTRAINT [FK_HorariosCancha_Usuarios]
GO
ALTER TABLE [dbo].[HorariosCancha]  WITH CHECK ADD  CONSTRAINT [FK_HorariosCancha_Usuarios1] FOREIGN KEY([IdUsuarioModificador])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[HorariosCancha] CHECK CONSTRAINT [FK_HorariosCancha_Usuarios1]
GO
ALTER TABLE [dbo].[NotificacionesUsuario]  WITH CHECK ADD  CONSTRAINT [FK_NotificacionesUsuario_Notificaciones] FOREIGN KEY([IdNotificacion])
REFERENCES [dbo].[Notificaciones] ([Id])
GO
ALTER TABLE [dbo].[NotificacionesUsuario] CHECK CONSTRAINT [FK_NotificacionesUsuario_Notificaciones]
GO
ALTER TABLE [dbo].[NotificacionesUsuario]  WITH CHECK ADD  CONSTRAINT [FK_NotificacionesUsuario_Usuarios] FOREIGN KEY([IdUsuarioEmisor])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[NotificacionesUsuario] CHECK CONSTRAINT [FK_NotificacionesUsuario_Usuarios]
GO
ALTER TABLE [dbo].[NotificacionesUsuario]  WITH CHECK ADD  CONSTRAINT [FK_NotificacionesUsuario_Usuarios1] FOREIGN KEY([IdUsuarioReceptor])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[NotificacionesUsuario] CHECK CONSTRAINT [FK_NotificacionesUsuario_Usuarios1]
GO
ALTER TABLE [dbo].[PermisosRol]  WITH CHECK ADD  CONSTRAINT [FK_PermisosRol_Permisos] FOREIGN KEY([IdPermiso])
REFERENCES [dbo].[Permisos] ([Id])
GO
ALTER TABLE [dbo].[PermisosRol] CHECK CONSTRAINT [FK_PermisosRol_Permisos]
GO
ALTER TABLE [dbo].[PermisosRol]  WITH CHECK ADD  CONSTRAINT [FK_PermisosRol_Roles] FOREIGN KEY([IdRol])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[PermisosRol] CHECK CONSTRAINT [FK_PermisosRol_Roles]
GO
ALTER TABLE [dbo].[Reservas]  WITH CHECK ADD  CONSTRAINT [FK_Reservas_HorariosCancha] FOREIGN KEY([IdHorario])
REFERENCES [dbo].[HorariosCancha] ([Id])
GO
ALTER TABLE [dbo].[Reservas] CHECK CONSTRAINT [FK_Reservas_HorariosCancha]
GO
ALTER TABLE [dbo].[Reservas]  WITH CHECK ADD  CONSTRAINT [FK_Reservas_Usuarios] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Reservas] CHECK CONSTRAINT [FK_Reservas_Usuarios]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_Roles] FOREIGN KEY([IdRol])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [FK_Usuarios_Roles]
GO
USE [master]
GO
ALTER DATABASE [BD_SIRCADE] SET  READ_WRITE 
GO
