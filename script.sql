USE [master]
GO
/****** Crear base de datos PreguntadOrt con TDE ******/
CREATE DATABASE [PreguntadOrt]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PreguntadOrt', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\PreguntadOrt.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PreguntadOrt_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\PreguntadOrt_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [PreguntadOrt] SET COMPATIBILITY_LEVEL = 140
GO

USE [PreguntadOrt]
GO

/****** Crear tabla Users ******/
CREATE TABLE [dbo].[Users](
    [UsersId] [int] IDENTITY(1,1) NOT NULL,
      NOT NULL,
      NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([UsersId] ASC)
) ON [PRIMARY]
GO

INSERT INTO [dbo].[Users] (Username, PasswordHash)
VALUES ('admin', HASHBYTES('adminmvc', 'adminmvc'))
GO

/****** Crear tablas del juego ******/
CREATE TABLE [dbo].[Categoria](
    [CategoriaId] [int] IDENTITY(1,1) NOT NULL,
      NOT NULL,
 CONSTRAINT [PK_Categorias] PRIMARY KEY CLUSTERED ([CategoriaId] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Dificultad](
    [DificultadId] [int] IDENTITY(1,1) NOT NULL,
      NOT NULL,
 CONSTRAINT [PK_Dificultades] PRIMARY KEY CLUSTERED ([DificultadId] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Pregunta](
    [PreguntaId] [int] IDENTITY(1,1) NOT NULL,
      NOT NULL,
    [CategoriaId] [int] NOT NULL,
    [DificultadId] [int] NOT NULL,
 CONSTRAINT [PK_Pregunta] PRIMARY KEY CLUSTERED ([PreguntaId] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Respuesta](
    [RespuestaId] [int] IDENTITY(1,1) NOT NULL,
    [PreguntaId] [int] NOT NULL,
      NOT NULL,
    [EsCorrecta] [bit] NOT NULL,
 CONSTRAINT [PK_Respuesta] PRIMARY KEY CLUSTERED ([RespuestaId] ASC)
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Pregunta]  WITH CHECK ADD  CONSTRAINT [FK_Preguntas_Categorias] FOREIGN KEY([CategoriaId])
REFERENCES [dbo].[Categoria] ([CategoriaId])
GO

ALTER TABLE [dbo].[Pregunta]  WITH CHECK ADD  CONSTRAINT [FK_Preguntas_Dificultades] FOREIGN KEY([DificultadId])
REFERENCES [dbo].[Dificultade] ([DificultadId])
GO

ALTER TABLE [dbo].[Respuesta]  WITH CHECK ADD  CONSTRAINT [FK_Respuestas_Preguntas] FOREIGN KEY([PreguntaId])
REFERENCES [dbo].[Pregunta] ([PreguntaId])
GO

/****** Habilitar cifrado transparente (TDE) ******/
USE master
GO

-- Crear certificado para TDE
CREATE CERTIFICATE TDECert WITH SUBJECT = 'Certificado TDE PreguntadOrt'
GO

-- Crear clave de cifrado de base de datos
USE PreguntadOrt
GO
CREATE DATABASE ENCRYPTION KEY
WITH ALGORITHM = AES_256
ENCRYPTION BY SERVER CERTIFICATE TDECert
GO

-- Activar TDE
ALTER DATABASE PreguntadOrt
SET ENCRYPTION ON
GO

ALTER DATABASE [PreguntadOrt] SET  READ_WRITE
GO
