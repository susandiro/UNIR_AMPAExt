USE [AMPAEXT]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------
--  Borrado de tablas	--
--------------------------

DROP TABLE IF EXISTS [dbo].[TUTOR]
GO
DROP TABLE IF EXISTS [dbo].[ALUMNO_ACTIVIDAD]
GO
DROP TABLE IF EXISTS [dbo].[ALUMNO]
GO
DROP TABLE IF EXISTS [dbo].[CURSO_CLASE]
GO
DROP TABLE IF EXISTS [dbo].[CURSO]
GO
DROP TABLE IF EXISTS [dbo].[CLASE]
GO
DROP TABLE IF EXISTS [dbo].[ACTIVIDAD_HORARIO]
GO
DROP TABLE IF EXISTS [dbo].[MONITOR]
GO
DROP TABLE IF EXISTS [dbo].[ACTIVIDAD_DESCUENTO]
GO
DROP TABLE IF EXISTS [dbo].[ACTIVIDAD]
GO
DROP TABLE IF EXISTS [dbo].[DESCUENTO]
GO
DROP TABLE IF EXISTS [dbo].[USUARIO_EMPRESA]
GO
DROP TABLE IF EXISTS [dbo].[USUARIO_AMPA]
GO
DROP TABLE IF EXISTS [dbo].[EMPRESA_AMPA]
GO
DROP TABLE IF EXISTS [dbo].[EMPRESA]
GO
DROP TABLE IF EXISTS [dbo].[AMPA]
GO
DROP TABLE IF EXISTS [dbo].[TIPO_DOCUMENTO]
GO

--------------------------
--  Creación de tablas	--
--------------------------
--------------------------
--  Tabla AMPA	--
--------------------------
CREATE TABLE [dbo].[AMPA](
	ID_AMPA		  INT IDENTITY(1,1) PRIMARY KEY,
	NOMBRE        VARCHAR(200) 	UNIQUE NOT NULL,
	NIF			  VARCHAR(10)   NOT NULL UNIQUE,
	FECHA		  DATETIME2		DEFAULT GETDATE() NOT NULL
)
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de la AMPA' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AMPA', @level2type=N'COLUMN', @level2name=N'ID_AMPA'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre de la AMPA' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AMPA', @level2type=N'COLUMN', @level2name=N'NOMBRE'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Número de Identificación Fiscal de la AMPA' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AMPA', @level2type=N'COLUMN', @level2name=N'NIF'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de creación de la AMPA' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AMPA', @level2type=N'COLUMN', @level2name=N'FECHA'
GO


----------------------
--  Tabla EMPRESA	-- 
----------------------
CREATE TABLE EMPRESA(
  ID_EMPRESA	INT IDENTITY(1,1) PRIMARY KEY,
  NIF			VARCHAR(10)     NOT NULL UNIQUE,
  NOMBRE        VARCHAR(100)    NOT NULL,
  RAZON_SOCIAL  VARCHAR(150) 	NOT NULL,
  TELEFONO		VARCHAR(15)		NOT NULL,
  PAGINA_WEB	VARCHAR(200),
  FECHA			DATETIME2		DEFAULT GETDATE() NOT NULL,
  FECHA_MOD		DATETIME2,
  USUARIO		VARCHAR (300) NOT NULL
)
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de la empresa' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EMPRESA', @level2type=N'COLUMN', @level2name=N'ID_EMPRESA'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Número de Identificación Fiscal de la empresa' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EMPRESA', @level2type=N'COLUMN', @level2name=N'NIF'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre de la empresa' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EMPRESA', @level2type=N'COLUMN', @level2name=N'NOMBRE'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Razón social de la empresa' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EMPRESA', @level2type=N'COLUMN', @level2name=N'RAZON_SOCIAL'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Teléfono de contacto de la empresa' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EMPRESA', @level2type=N'COLUMN', @level2name=N'TELEFONO'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Página web de la empresa' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EMPRESA', @level2type=N'COLUMN', @level2name=N'PAGINA_WEB'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de creación de la empresa' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EMPRESA', @level2type=N'COLUMN', @level2name=N'FECHA'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha en que la empresa ha sido modificada' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EMPRESA', @level2type=N'COLUMN', @level2name=N'FECHA_MOD'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Usuario que realiza la actualización de la empresa' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EMPRESA', @level2type=N'COLUMN', @level2name=N'USUARIO'
GO

----------------------------------
--  Tabla EMPRESA_AMPA	-- 
----------------------------------
CREATE TABLE EMPRESA_AMPA(
  ID_AMPA					INT NOT NULL  FOREIGN KEY REFERENCES AMPA (ID_AMPA),
  ID_EMPRESA				INT NOT NULL  FOREIGN KEY REFERENCES EMPRESA (ID_EMPRESA),
  ACTIVO					VARCHAR(1)    DEFAULT 'S' NOT NULL  CHECK (ACTIVO IN ('S','N')),
  FECHA						DATETIME2	  DEFAULT GETDATE() NOT NULL,
  FECHA_MOD					DATETIME2,
  MOTIVO_BAJA				VARCHAR(500),
  USUARIO					VARCHAR (300) NOT NULL,
  OBSERVACIONES				VARCHAR(500),
  PRIMARY KEY (ID_AMPA, ID_EMPRESA)
)
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de la AMPA a la que pertenece la empresa' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EMPRESA_AMPA', @level2type=N'COLUMN', @level2name=N'ID_AMPA'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de la empresa' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EMPRESA_AMPA', @level2type=N'COLUMN', @level2name=N'ID_EMPRESA'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Indicativo de si la empresa se encuentra activa para el tipo de usuario (Valores: S/N)' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EMPRESA_AMPA', @level2type=N'COLUMN', @level2name=N'ACTIVO'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de creación de la empresa' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EMPRESA_AMPA', @level2type=N'COLUMN', @level2name=N'FECHA'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha en que la empresa ha sido modificada' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EMPRESA_AMPA', @level2type=N'COLUMN', @level2name=N'FECHA_MOD'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Motivo por el que la empresa ha sido dada de baja' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EMPRESA_AMPA', @level2type=N'COLUMN', @level2name=N'MOTIVO_BAJA'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Usuario que realiza la actualización de la empresa' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EMPRESA_AMPA', @level2type=N'COLUMN', @level2name=N'USUARIO'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Observaciones a incorporar sobre la empresa' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EMPRESA_AMPA', @level2type=N'COLUMN', @level2name=N'OBSERVACIONES'
GO

------------------------------
--  Tabla TIPO_DOCUMENTO	-- 
------------------------------
CREATE TABLE TIPO_DOCUMENTO(
	ID_TIPO_DOCUMENTO   INT IDENTITY(1,1) PRIMARY KEY,
	NOMBRE				VARCHAR(10) NOT NULL,
	DESCRIPCION			VARCHAR(100) NOT NULL,
)
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del tipo de documento' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'TIPO_DOCUMENTO', @level2type=N'COLUMN', @level2name=N'ID_TIPO_DOCUMENTO'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del tipo de documento' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'TIPO_DOCUMENTO', @level2type=N'COLUMN', @level2name=N'NOMBRE'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripción del tipo de documento' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'TIPO_DOCUMENTO', @level2type=N'COLUMN', @level2name=N'DESCRIPCION'
GO

----------------------
--  Tabla USUARIO_AMPA	-- 
----------------------
CREATE TABLE USUARIO_AMPA(
	ID_USUARIO          INT IDENTITY(1,1) PRIMARY KEY,
	ID_AMPA				INT NOT NULL FOREIGN KEY REFERENCES AMPA (ID_AMPA),
	ID_TIPO_DOCUMENTO   INT NOT NULL FOREIGN KEY REFERENCES TIPO_DOCUMENTO (ID_TIPO_DOCUMENTO),
	NUMERO_DOCUMENTO    VARCHAR(50) NOT NULL UNIQUE,
	NOMBRE              VARCHAR(100) NOT NULL,
	APELLIDO1           VARCHAR(100) NOT NULL,
	APELLIDO2           VARCHAR(100),
	EMAIL               VARCHAR(150) NOT NULL,
	TELEFONO			VARCHAR(20),
	LOGIN               VARCHAR(50) NOT NULL UNIQUE,
	PASSWORD            VARCHAR(200) NOT NULL,
	OBSERVACIONES       VARCHAR(250),
	FECHA				DATETIME2	  DEFAULT GETDATE() NOT NULL,
	FECHA_MOD			DATETIME2,
	USUARIO				VARCHAR (300) NOT NULL,
	UNIQUE (ID_USUARIO, ID_AMPA)
)
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del usuario' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'USUARIO_AMPA', @level2type=N'COLUMN', @level2name=N'ID_USUARIO'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de la AMPA a la que se asocia el usuario' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'USUARIO_AMPA', @level2type=N'COLUMN', @level2name=N'ID_AMPA'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del tipo de documento de identificación del usuario' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'USUARIO_AMPA', @level2type=N'COLUMN', @level2name=N'ID_TIPO_DOCUMENTO'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Número de documento de identificación del usuario' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'USUARIO_AMPA', @level2type=N'COLUMN', @level2name=N'NUMERO_DOCUMENTO'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del usuario' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'USUARIO_AMPA', @level2type=N'COLUMN', @level2name=N'NOMBRE'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primer apellido del usuario' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'USUARIO_AMPA', @level2type=N'COLUMN', @level2name=N'APELLIDO1'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Segundo apellido del usuario' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'USUARIO_AMPA', @level2type=N'COLUMN', @level2name=N'APELLIDO2'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Correo electrónico de contacto del usuario' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'USUARIO_AMPA', @level2type=N'COLUMN', @level2name=N'EMAIL'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Teléfono de contacto del usuario' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'USUARIO_AMPA', @level2type=N'COLUMN', @level2name=N'TELEFONO'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Login de acceso a la aplicación del usuario' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'USUARIO_AMPA', @level2type=N'COLUMN', @level2name=N'LOGIN'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Contraseña de acceso a la aplicación del usuario' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'USUARIO_AMPA', @level2type=N'COLUMN', @level2name=N'PASSWORD'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Observaciones del usuario' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'USUARIO_AMPA', @level2type=N'COLUMN', @level2name=N'OBSERVACIONES'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de creación del usuario' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'USUARIO_AMPA', @level2type=N'COLUMN', @level2name=N'FECHA'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha en que el usuario ha sido modificado' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'USUARIO_AMPA', @level2type=N'COLUMN', @level2name=N'FECHA_MOD'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Usuario que realiza la actualización del usuario' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'USUARIO_AMPA', @level2type=N'COLUMN', @level2name=N'USUARIO'
GO


------------------------------
--  Tabla USUARIO_EMPRESA	-- 
------------------------------
CREATE TABLE USUARIO_EMPRESA(
	ID_USUARIO_EMP      INT IDENTITY(1,1) PRIMARY KEY,
	ID_EMPRESA			INT NOT NULL FOREIGN KEY REFERENCES EMPRESA (ID_EMPRESA),
	ID_TIPO_DOCUMENTO   INT NOT NULL FOREIGN KEY REFERENCES TIPO_DOCUMENTO (ID_TIPO_DOCUMENTO),
	NUMERO_DOCUMENTO    VARCHAR(50) NOT NULL UNIQUE,
	NOMBRE              VARCHAR(100) NOT NULL,
	APELLIDO1           VARCHAR(100) NOT NULL,
	APELLIDO2           VARCHAR(100),
	EMAIL               VARCHAR(150) NOT NULL,
	TELEFONO			VARCHAR(20),
	LOGIN               VARCHAR(50) NOT NULL UNIQUE,
	PASSWORD            VARCHAR(200) NOT NULL,
	OBSERVACIONES       VARCHAR(250),
	FECHA				DATETIME2	  DEFAULT GETDATE() NOT NULL,
	FECHA_MOD			DATETIME2,
	USUARIO				VARCHAR (300) NOT NULL
)
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del usuario' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'USUARIO_EMPRESA', @level2type=N'COLUMN', @level2name=N'ID_USUARIO_EMP'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de la empresa a la que se asocia el usuario' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'USUARIO_EMPRESA', @level2type=N'COLUMN', @level2name=N'ID_EMPRESA'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del tipo de documento de identificación del usuario' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'USUARIO_EMPRESA', @level2type=N'COLUMN', @level2name=N'ID_TIPO_DOCUMENTO'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Número de documento de identificación del usuario' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'USUARIO_EMPRESA', @level2type=N'COLUMN', @level2name=N'NUMERO_DOCUMENTO'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del usuario' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'USUARIO_EMPRESA', @level2type=N'COLUMN', @level2name=N'NOMBRE'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primer apellido del usuario' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'USUARIO_EMPRESA', @level2type=N'COLUMN', @level2name=N'APELLIDO1'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Segundo apellido del usuario' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'USUARIO_EMPRESA', @level2type=N'COLUMN', @level2name=N'APELLIDO2'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Correo electrónico de contacto del usuario' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'USUARIO_EMPRESA', @level2type=N'COLUMN', @level2name=N'EMAIL'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Teléfono de contacto del usuario' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'USUARIO_EMPRESA', @level2type=N'COLUMN', @level2name=N'TELEFONO'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Login de acceso a la aplicación del usuario' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'USUARIO_EMPRESA', @level2type=N'COLUMN', @level2name=N'LOGIN'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Contraseña de acceso a la aplicación del usuario' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'USUARIO_EMPRESA', @level2type=N'COLUMN', @level2name=N'PASSWORD'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Observaciones del usuario' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'USUARIO_EMPRESA', @level2type=N'COLUMN', @level2name=N'OBSERVACIONES'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de creación del usuario' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'USUARIO_EMPRESA', @level2type=N'COLUMN', @level2name=N'FECHA'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha en que el usuario ha sido modificado' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'USUARIO_EMPRESA', @level2type=N'COLUMN', @level2name=N'FECHA_MOD'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Usuario que realiza la actualización del registro' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'USUARIO_EMPRESA', @level2type=N'COLUMN', @level2name=N'USUARIO'
GO

------------------------------
--  Tabla ACTIVIDAD	-- 
------------------------------
CREATE TABLE ACTIVIDAD(
	ID_ACTIVIDAD        INT IDENTITY(1,1) PRIMARY KEY,
	ID_EMPRESA		    INT NOT NULL FOREIGN KEY REFERENCES EMPRESA (ID_EMPRESA),
	ID_AMPA			    INT NOT NULL FOREIGN KEY REFERENCES AMPA (ID_AMPA),
	NOMBRE			    VARCHAR(250) NOT NULL,
	DESCRIPCION         VARCHAR(300) NOT NULL,
	ACTIVO				VARCHAR(1) NOT NULL DEFAULT 'S',
	OBSERVACIONES       VARCHAR(250),
	FECHA				DATETIME2	  DEFAULT GETDATE() NOT NULL,
	FECHA_MOD			DATETIME2,
	USUARIO				VARCHAR (300) NOT NULL
)
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de la actividad' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ACTIVIDAD', @level2type=N'COLUMN', @level2name=N'ID_ACTIVIDAD'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de la empresa' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ACTIVIDAD', @level2type=N'COLUMN', @level2name=N'ID_EMPRESA'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de La AMPA' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ACTIVIDAD', @level2type=N'COLUMN', @level2name=N'ID_AMPA'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre de la actividad' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ACTIVIDAD', @level2type=N'COLUMN', @level2name=N'NOMBRE'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripción de la actividad' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ACTIVIDAD', @level2type=N'COLUMN', @level2name=N'DESCRIPCION'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'si la actividad se encuentra activa en el sistema S/N' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ACTIVIDAD', @level2type=N'COLUMN', @level2name=N'ACTIVO'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Observaciones a la actividad' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ACTIVIDAD', @level2type=N'COLUMN', @level2name=N'OBSERVACIONES'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de creación del registro' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ACTIVIDAD', @level2type=N'COLUMN', @level2name=N'FECHA'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha en que el registro ha sido modificado' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ACTIVIDAD', @level2type=N'COLUMN', @level2name=N'FECHA_MOD'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Usuario que realiza la actualización del registro' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ACTIVIDAD', @level2type=N'COLUMN', @level2name=N'USUARIO'
GO

------------------------------
--  Tabla ACTIVIDAD_HORARIO	-- 
------------------------------
CREATE TABLE ACTIVIDAD_HORARIO(
	ID_ACT_HORARIO      INT IDENTITY(1,1) PRIMARY KEY,
	ID_ACTIVIDAD		INT NOT NULL FOREIGN KEY REFERENCES ACTIVIDAD (ID_ACTIVIDAD),
	ID_MONITOR			INT NULL FOREIGN KEY REFERENCES MONITOR (ID_MONITOR),
	DIAS			    VARCHAR(10) NOT NULL,
	HORA_INI			VARCHAR(50) NOT NULL,
	HORA_FIN			VARCHAR(50) NOT NULL,
	CUOTA				DECIMAL  	NOT NULL,
	FECHA				DATETIME2	DEFAULT GETDATE() NOT NULL,
	FECHA_MOD			DATETIME2,
	USUARIO				VARCHAR (300) NOT NULL
)
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de la tabla' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ACTIVIDAD_HORARIO', @level2type=N'COLUMN', @level2name=N'ID_ACT_HORARIO'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de la actividad' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ACTIVIDAD_HORARIO', @level2type=N'COLUMN', @level2name=N'ID_ACTIVIDAD'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del monitor que imparte la actividad' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ACTIVIDAD_HORARIO', @level2type=N'COLUMN', @level2name=N'ID_MONITOR'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Días en los que se imparte la actividad. Secuencia de iniciales separadas por coma (L,M,X,J,V)' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ACTIVIDAD_HORARIO', @level2type=N'COLUMN', @level2name=N'DIAS'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Hora de inicio en la que se imparte la actividad' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ACTIVIDAD_HORARIO', @level2type=N'COLUMN', @level2name=N'HORA_INI'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Hora de fin en la que se imparte la actividad' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ACTIVIDAD_HORARIO', @level2type=N'COLUMN', @level2name=N'HORA_FIN'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cuota asociada a la actividad en el horario' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ACTIVIDAD_HORARIO', @level2type=
N'COLUMN', @level2name=N'CUOTA'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de creación del registro' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ACTIVIDAD_HORARIO', @level2type=N'COLUMN', @level2name=N'FECHA'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha en que el registro ha sido modificado' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ACTIVIDAD_HORARIO', @level2type=N'COLUMN', @level2name=N'FECHA_MOD'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Usuario que realiza la actualización del registro' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ACTIVIDAD_HORARIO', @level2type=N'COLUMN', @level2name=N'USUARIO'
GO

------------------------------
--  Tabla DESCUENTO      	-- 
------------------------------
CREATE TABLE DESCUENTO(
	ID_DESCUENTO    INT IDENTITY(1,1) PRIMARY KEY,
	NOMBRE			VARCHAR (300) NOT NULL
)
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de la tabla' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DESCUENTO', @level2type=N'COLUMN', @level2name=N'ID_DESCUENTO'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del descuento' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DESCUENTO', @level2type=N'COLUMN', @level2name=N'NOMBRE'
GO

----------------------------------
--  Tabla ACTIVIDAD_DESCUENTO	-- 
----------------------------------
CREATE TABLE ACTIVIDAD_DESCUENTO(
	ID_ACT_DESCUENTO    INT IDENTITY(1,1) PRIMARY KEY,
	ID_ACTIVIDAD		INT NOT NULL FOREIGN KEY REFERENCES ACTIVIDAD (ID_ACTIVIDAD),
	ID_DESCUENTO		INT NOT NULL FOREIGN KEY REFERENCES DESCUENTO (ID_DESCUENTO),
	VALOR			    INT NOT NULL,
	FECHA				DATETIME2	  DEFAULT GETDATE() NOT NULL,
	FECHA_MOD			DATETIME2,
	USUARIO				VARCHAR (300) NOT NULL,
	UNIQUE (ID_ACTIVIDAD, ID_DESCUENTO)
)
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de la tabla' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ACTIVIDAD_DESCUENTO', @level2type=N'COLUMN', @level2name=N'ID_ACT_DESCUENTO'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de la actividad' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ACTIVIDAD_DESCUENTO', @level2type=N'COLUMN', @level2name=N'ID_ACTIVIDAD'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Valor del descuento' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ACTIVIDAD_DESCUENTO', @level2type=N'COLUMN', @level2name=N'VALOR'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de creación del registro' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ACTIVIDAD_DESCUENTO', @level2type=N'COLUMN', @level2name=N'FECHA'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha en que el registro ha sido modificado' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ACTIVIDAD_DESCUENTO', @level2type=N'COLUMN', @level2name=N'FECHA_MOD'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Usuario que realiza la actualización del registro' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ACTIVIDAD_DESCUENTO', @level2type=N'COLUMN', @level2name=N'USUARIO'
GO

------------------------------
--  Tabla MONITOR	        -- 
------------------------------
CREATE TABLE MONITOR(
	ID_MONITOR      	INT IDENTITY(1,1) PRIMARY KEY,
	ID_EMPRESA		    INT NOT NULL FOREIGN KEY REFERENCES EMPRESA (ID_EMPRESA),
	ID_TIPO_DOCUMENTO   INT NOT NULL FOREIGN KEY REFERENCES TIPO_DOCUMENTO (ID_TIPO_DOCUMENTO),
	NUMERO_DOCUMENTO    VARCHAR(50) NOT NULL UNIQUE,
	NOMBRE              VARCHAR(100) NOT NULL,
	APELLIDO1           VARCHAR(100) NOT NULL,
	APELLIDO2           VARCHAR(100),
	EMAIL               VARCHAR(150) NOT NULL,
	TELEFONO			VARCHAR(20),
	LOGIN               VARCHAR(50) NOT NULL UNIQUE,
	PASSWORD            VARCHAR(200) NOT NULL,
	FECHA				DATETIME2	  DEFAULT GETDATE() NOT NULL,
	FECHA_MOD			DATETIME2,
	USUARIO				VARCHAR (300) NOT NULL
)
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del monitor' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MONITOR', @level2type=N'COLUMN', @level2name=N'ID_MONITOR'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de la empresa a la que pertenece el monitor' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MONITOR', @level2type=N'COLUMN', @level2name=N'ID_EMPRESA'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del tipo de documento de identificación del monitor' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MONITOR', @level2type=N'COLUMN', @level2name=N'ID_TIPO_DOCUMENTO'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Número de documento de identificación del monitor' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MONITOR', @level2type=N'COLUMN', @level2name=N'NUMERO_DOCUMENTO'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del monitor' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MONITOR', @level2type=N'COLUMN', @level2name=N'NOMBRE'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primer apellido del monitor' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MONITOR', @level2type=N'COLUMN', @level2name=N'APELLIDO1'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Segundo apellido del monitor' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MONITOR', @level2type=N'COLUMN', @level2name=N'APELLIDO2'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Correo electrónico de contacto del monitor' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MONITOR', @level2type=N'COLUMN', @level2name=N'EMAIL'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Teléfono de contacto del monitor' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MONITOR', @level2type=N'COLUMN', @level2name=N'TELEFONO'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Login de acceso a la aplicación del monitor' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MONITOR', @level2type=N'COLUMN', @level2name=N'LOGIN'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Contraseña de acceso a la aplicación del monitor' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MONITOR', @level2type=N'COLUMN', @level2name=N'PASSWORD'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de creación del monitor' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MONITOR', @level2type=N'COLUMN', @level2name=N'FECHA'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha en que el monitor ha sido modificado' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MONITOR', @level2type=N'COLUMN', @level2name=N'FECHA_MOD'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Usuario que realiza la actualización del registro' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MONITOR', @level2type=N'COLUMN', @level2name=N'USUARIO'
GO

------------------------------
--  Tabla CURSO      	-- 
------------------------------
CREATE TABLE CURSO(
	ID_CURSO    INT IDENTITY(1,1) PRIMARY KEY,
	NOMBRE		VARCHAR (20) NOT NULL
)
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de la tabla' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CURSO', @level2type=N'COLUMN', @level2name=N'ID_CURSO'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del curso' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CURSO', @level2type=N'COLUMN', @level2name=N'NOMBRE'
GO

--------------------------
--  Tabla CLASE      	-- 
--------------------------
CREATE TABLE CLASE(
	ID_CLASE    INT IDENTITY(1,1) PRIMARY KEY,
	NOMBRE		VARCHAR (3) NOT NULL
)
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de la tabla' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CLASE', @level2type=N'COLUMN', @level2name=N'ID_CLASE'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre de la clase' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CLASE', @level2type=N'COLUMN', @level2name=N'NOMBRE'
GO

--------------------------
--  Tabla CURSO_CLASE  	-- 
--------------------------
CREATE TABLE CURSO_CLASE(
	ID_CURSO_CLASE  INT IDENTITY(1,1) PRIMARY KEY,
	ID_CURSO		INT NOT NULL FOREIGN KEY REFERENCES CURSO (ID_CURSO),
	ID_CLASE		INT NOT NULL FOREIGN KEY REFERENCES CLASE (ID_CLASE),
	ID_AMPA			INT NOT NULL FOREIGN KEY REFERENCES AMPA  (ID_AMPA)
)
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de la tabla' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CURSO_CLASE', @level2type=N'COLUMN', @level2name=N'ID_CURSO_CLASE'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del curso' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CURSO_CLASE', @level2type=N'COLUMN', @level2name=N'ID_CURSO'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de la clase' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CURSO_CLASE', @level2type=N'COLUMN', @level2name=N'ID_CLASE'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de la AMPA' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CURSO_CLASE', @level2type=N'COLUMN', @level2name=N'ID_AMPA'
GO

------------------------------
--  Tabla ALUMNO	        -- 
------------------------------
CREATE TABLE ALUMNO(
	ID_ALUMNO       	INT IDENTITY(1,1) PRIMARY KEY,
	ID_TUTOR			INT NOT NULL FOREIGN KEY REFERENCES TUTOR (ID_TUTOR),
	NOMBRE              VARCHAR(100) NOT NULL,
	APELLIDO1           VARCHAR(100) NOT NULL,
	APELLIDO2           VARCHAR(100),
	FECHA_NACIMIENTO    DATETIME NOT NULL,
	ID_CURSO_CLASE		INT NOT NULL FOREIGN KEY REFERENCES CURSO_CLASE (ID_CURSO_CLASE),
	FECHA				DATETIME	  DEFAULT GETDATE() NOT NULL,
	FECHA_MOD			DATETIME,
	USUARIO				VARCHAR (300) NOT NULL
)
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del alumno' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ALUMNO', @level2type=N'COLUMN', @level2name=N'ID_ALUMNO'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del tutor' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ALUMNO', @level2type=N'COLUMN', @level2name=N'ID_TUTOR'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del alumno' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ALUMNO', @level2type=N'COLUMN', @level2name=N'NOMBRE'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primer apellido del alumno' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ALUMNO', @level2type=N'COLUMN', @level2name=N'APELLIDO1'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Segundo apellido del alumno' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ALUMNO', @level2type=N'COLUMN', @level2name=N'APELLIDO2'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de nacimiento del alumno' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ALUMNO', @level2type=N'COLUMN', @level2name=N'FECHA_NACIMIENTO'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del curso y letra al que pertenece' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ALUMNO', @level2type=N'COLUMN', @level2name=N'ID_CURSO_CLASE'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de creación del alumno' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ALUMNO', @level2type=N'COLUMN', @level2name=N'FECHA'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha en que el alumno ha sido modificado' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ALUMNO', @level2type=N'COLUMN', @level2name=N'FECHA_MOD'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Usuario que realiza la actualización del registro' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ALUMNO', @level2type=N'COLUMN', @level2name=N'USUARIO'
GO

--------------------------
--  Tabla TUTOR	        -- 
--------------------------
CREATE TABLE TUTOR(
	ID_TUTOR        	   INT IDENTITY(1,1) PRIMARY KEY,
	ID_AMPA				   INT NOT NULL FOREIGN KEY REFERENCES AMPA (ID_AMPA),
	T1_ID_TIPO_DOCUMENTO   INT NOT NULL FOREIGN KEY REFERENCES TIPO_DOCUMENTO (ID_TIPO_DOCUMENTO),
	T1_NUMERO_DOCUMENTO    VARCHAR(50) NOT NULL,
	T1_NOMBRE              VARCHAR(100) NOT NULL,
	T1_APELLIDO1           VARCHAR(100) NOT NULL,
	T1_APELLIDO2           VARCHAR(100),
	T1_EMAIL               VARCHAR(150) NOT NULL,
	T1_TELEFONO			   VARCHAR(20),
	T2_ID_TIPO_DOCUMENTO   INT NULL FOREIGN KEY REFERENCES TIPO_DOCUMENTO (ID_TIPO_DOCUMENTO),
	T2_NUMERO_DOCUMENTO    VARCHAR(50),
	T2_NOMBRE              VARCHAR(100),
	T2_APELLIDO1           VARCHAR(100),
	T2_APELLIDO2           VARCHAR(100),
	T2_EMAIL               VARCHAR(150),
	T2_TELEFONO			   VARCHAR(20),
	INDIVIDUAL			   VARCHAR(1) DEFAULT 'N',
	FECHA				   DATETIME2  DEFAULT GETDATE() NOT NULL,
	FECHA_MOD			   DATETIME2,
	USUARIO				   VARCHAR (300) NOT NULL,
	UNIQUE(T1_NUMERO_DOCUMENTO,ID_AMPA)
)
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del tutor' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'TUTOR', @level2type=N'COLUMN', @level2name=N'ID_TUTOR'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del AMPA al que pertenece el tutor' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'TUTOR', @level2type=N'COLUMN', @level2name=N'ID_AMPA'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del tipo de documento de identificación del tutor1' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'TUTOR', @level2type=N'COLUMN', @level2name=N'T1_ID_TIPO_DOCUMENTO'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Número de documento de identificación del tutor1' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'TUTOR', @level2type=N'COLUMN', @level2name=N'T1_NUMERO_DOCUMENTO'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del tutor1' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'TUTOR', @level2type=N'COLUMN', @level2name=N'T1_NOMBRE'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primer apellido del tutor1' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'TUTOR', @level2type=N'COLUMN', @level2name=N'T1_APELLIDO1'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Segundo apellido del tutor1' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'TUTOR', @level2type=N'COLUMN', @level2name=N'T1_APELLIDO2'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Correo electrónico de contacto del tutor1' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'TUTOR', @level2type=N'COLUMN', @level2name=N'T1_EMAIL'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Teléfono de contacto del tutor1' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'TUTOR', @level2type=N'COLUMN', @level2name=N'T1_TELEFONO'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del tipo de documento de identificación del tutor2' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'TUTOR', @level2type=N'COLUMN', @level2name=N'T2_ID_TIPO_DOCUMENTO'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Número de documento de identificación del tutor2' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'TUTOR', @level2type=N'COLUMN', @level2name=N'T2_NUMERO_DOCUMENTO'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del tutor2' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'TUTOR', @level2type=N'COLUMN', @level2name=N'T2_NOMBRE'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primer apellido del tutor2' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'TUTOR', @level2type=N'COLUMN', @level2name=N'T2_APELLIDO1'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Segundo apellido del tutor2' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'TUTOR', @level2type=N'COLUMN', @level2name=N'T2_APELLIDO2'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Correo electrónico de contacto del tutor2' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'TUTOR', @level2type=N'COLUMN', @level2name=N'T2_EMAIL'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Teléfono de contacto del tutor2' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'TUTOR', @level2type=N'COLUMN', @level2name=N'T2_TELEFONO'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Indicador de si responden de forma individual S/N' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'TUTOR', @level2type=N'COLUMN', @level2name=N'INDIVIDUAL'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de creación del tutor' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'TUTOR', @level2type=N'COLUMN', @level2name=N'FECHA'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha en que el tutor ha sido modificado' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'TUTOR', @level2type=N'COLUMN', @level2name=N'FECHA_MOD'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Usuario que realiza la actualización del registro' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'TUTOR', @level2type=N'COLUMN', @level2name=N'USUARIO'
GO


------------------------------
--  Tabla ALUMNO_ACTIVIDAD	-- 
------------------------------
CREATE TABLE ALUMNO_ACTIVIDAD(
    ID_ALUM_ACT	    INT IDENTITY(1,1) PRIMARY KEY,
	ID_ALUMNO		INT NOT NULL FOREIGN KEY REFERENCES ALUMNO (ID_ALUMNO),
	ID_ACT_HORARIO	INT NOT NULL FOREIGN KEY REFERENCES ACTIVIDAD_HORARIO (ID_ACT_HORARIO),
	FECHA			DATETIME	  DEFAULT GETDATE() NOT NULL,
	FECHA_MOD		DATETIME,
	USUARIO			VARCHAR (300) NOT NULL
)
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del registro' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ALUMNO_ACTIVIDAD', @level2type=N'COLUMN', @level2name=N'ID_ALUM_ACT'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del alumno' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ALUMNO_ACTIVIDAD', @level2type=N'COLUMN', @level2name=N'ID_ALUMNO'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador de la actividad y horario' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ALUMNO_ACTIVIDAD', @level2type=N'COLUMN', @level2name=N'ID_ACT_HORARIO'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de creación del alumno' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ALUMNO_ACTIVIDAD', @level2type=N'COLUMN', @level2name=N'FECHA'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha en que el alumno ha sido modificado' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ALUMNO_ACTIVIDAD', @level2type=N'COLUMN', @level2name=N'FECHA_MOD'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Usuario que realiza la actualización del registro' , @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ALUMNO_ACTIVIDAD', @level2type=N'COLUMN', @level2name=N'USUARIO'
GO