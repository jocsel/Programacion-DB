/*USE [master]*/
GO
/****** Object:  Database [Sistema]    Script Date: 24/02/2018 21:11:55 ******/
CREATE DATABASE [Sistema]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Sistema', FILENAME = 'D:\Solution1\Sistema.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Sistema_log', FILENAME = 'D:\Solution1\Sistema_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Sistema] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Sistema].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Sistema] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Sistema] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Sistema] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Sistema] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Sistema] SET ARITHABORT OFF 
GO
ALTER DATABASE [Sistema] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Sistema] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Sistema] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Sistema] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Sistema] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Sistema] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Sistema] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Sistema] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Sistema] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Sistema] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Sistema] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Sistema] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Sistema] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Sistema] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Sistema] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Sistema] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Sistema] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Sistema] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Sistema] SET  MULTI_USER 
GO
ALTER DATABASE [Sistema] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Sistema] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Sistema] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Sistema] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Sistema] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Sistema]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 24/02/2018 21:11:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Roles](
	[Id_rol] [int] NULL,
	[rol] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Roles_tran]    Script Date: 24/02/2018 21:11:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles_tran](
	[Id_tran] [int] NULL,
	[Id_rol] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Secuencial]    Script Date: 24/02/2018 21:11:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Secuencial](
	[Idsecuencial] [int] NULL,
	[tabla] [nvarchar](50) NULL,
	[secuencial] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tabla]    Script Date: 24/02/2018 21:11:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tabla](
	[Id_tabla] [int] NULL,
	[Descripcion] [nvarchar](254) NULL,
	[Tabla] [nvarchar](64) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Transacciones]    Script Date: 24/02/2018 21:11:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Transacciones](
	[Id_transacciones] [int] NULL,
	[transaccion] [varchar](10) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[usuario]    Script Date: 24/02/2018 21:11:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[usuario](
	[Id_usuario] [int] NULL,
	[Usuario] [varchar](50) NULL,
	[Contraseña] [varchar](8000) NULL,
	[Id_rol] [int] NULL,
	[nombre] [varchar](50) NULL,
	[apellido] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[Sp_rol]    Script Date: 24/02/2018 21:11:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[Sp_rol]
(
@Id_rol int = 0,
@rol varchar(50),
@i_operacion char(1),
@o_msg varchar(254) = null output
)
as
if @i_operacion = 'I'
begin
begin try

if @Id_rol is null
begin 
select @o_msg='EL IDROL ES MANDATORIO'
return 1
end

if isnull(@rol, '') = ''
	 begin
	   select @o_msg = 'EL ROL ES MANDATORIO'
	   RETURN 1
	 end 

	  declare @x int, @y varchar(254)

			exec Sistema..sp_secuencial
				@i_tabla  = 'Roles',
				@i_operacoin = 'S',
				@o_secuencial = @x output,
				@o_msg  = @y output
				select @x as x,@y as y

insert into Roles (Id_rol,rol)
values (@x,@rol)

select @o_msg = 'EL ROL: ' + convert(varchar(10),@x) + ' SE GUARDO'
end try
begin catch
select @o_msg = 'ERROR: ' + ERROR_MESSAGE() + ' EN LA LINEA: ' + CONVERT(VARCHAR(10),ERROR_LINE())
end catch
end

if @i_operacion = 'S'
BEGIN
BEGIN TRY
SELECT Id_rol,rol FROM Roles
END TRY
BEGIN CATCH
select @o_msg = 'ERROR: ' + ERROR_MESSAGE() + ' EN LA LINEA: ' + CONVERT(VARCHAR(10),ERROR_LINE())
END CATCH
END

if @i_operacion = 'U'
BEGIN
BEGIN TRY 
if isnull(@rol, '') = ''
	 begin
	   select @o_msg = 'EL ROL ES MANDATORIO'
	   RETURN 1
	 end 

UPDATE Roles SET rol=@rol WHERE Id_rol=@Id_rol
select @o_msg = 'EL ROL: ' + @rol + ' SE ACTUALIZO'
END TRY
BEGIN CATCH
select @o_msg = 'ERROR: ' + ERROR_MESSAGE() + ' EN LA LINEA: ' + CONVERT(VARCHAR(10),ERROR_LINE())
END CATCH
END



GO
/****** Object:  StoredProcedure [dbo].[SP_ROLES_TRANS]    Script Date: 24/02/2018 21:11:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[SP_ROLES_TRANS]
(
@Id_tran int =0,
@id_rol int = 0,
@i_operacion char(1),
@o_msg varchar(254) = null output
)
as 
if @i_operacion = 'I'
begin
begin try
if @Id_tran is null
begin 
select @o_msg='EL ID-TRAN ES MANDATORIO'
return 1
end
if @Id_rol is null
begin 
select @o_msg='EL IDROL ES MANDATORIO'
return 1
end

 declare @x int, @y varchar(254)

			exec Sistema..sp_secuencial
				@i_tabla  = 'Roles_tran',
				@i_operacoin = 'S',
				@o_secuencial = @x output,
				@o_msg  = @y output
				select @x as x,@y as y


INSERT INTO Roles_tran (Id_tran,Id_rol)
VALUES (@x,@id_rol)

select @o_msg = 'EL ROL-TRAN: ' + convert(varchar(10),@x) + ' SE GUARDO'

end trY

begin catch
select @o_msg = 'ERROR: ' + ERROR_MESSAGE() + ' EN LA LINEA: ' + CONVERT(VARCHAR(10),ERROR_LINE())
end catch
end

if @i_operacion = 'U'
BEGIN 
BEGIN TRY
if @Id_rol is null
begin 
select @o_msg='EL IDROL ES MANDATORIO'
return 1
end

UPDATE Roles_tran SET Id_rol=@id_rol WHERE Id_tran=@Id_tran
select @o_msg = 'EL ROL-TRAN: ' + @Id_tran + ' SE ACTUALIZO'

END TRY
BEGIN CATCH
select @o_msg = 'ERROR: ' + ERROR_MESSAGE() + ' EN LA LINEA: ' + CONVERT(VARCHAR(10),ERROR_LINE())
END CATCH
END

if @i_operacion = 'S'
BEGIN 
BEGIN TRY
SELECT * FROM Roles_tran
END TRY
BEGIN CATCH
select @o_msg = 'ERROR: ' + ERROR_MESSAGE() + ' EN LA LINEA: ' + CONVERT(VARCHAR(10),ERROR_LINE())
END CATCH
END


GO
/****** Object:  StoredProcedure [dbo].[sp_secuencial]    Script Date: 24/02/2018 21:11:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_secuencial]
(
@i_tabla varchar(50) = '',
@i_operacoin varchar(1),
@o_secuencial int = null output,
@o_msg varchar(254) = null output
)
as
declare @w_secuencial int

if @i_operacoin = 'S'
begin
  begin try
    select @w_secuencial = secuencial 
    from Sistema..secuencial 
    where tabla = @i_tabla
	--IF NOT EXISTS(select secuencial from clase..secuencial where tabla = @i_tabla)
	 --BEGIN
	  --SELECT @o_msg = 'TABLA NO EXISTE'
	  --RETURN 1
	--END
	IF @w_secuencial IS NULL
	BEGIN
	  SELECT @o_msg = 'TABLA NO EXISTE'
	  RETURN 1
	END
    select @w_secuencial = @w_secuencial + 1

    update Sistema..secuencial 
    set secuencial = @w_secuencial
    where tabla = @i_tabla

   select @o_secuencial = @w_secuencial
  end try
  begin catch
    select @o_msg = 'ERROR: ' + ERROR_MESSAGE() + ' EN LA LINEA: ' + CONVERT(VARCHAR(10),ERROR_LINE())
  end catch
   

end
if @i_operacoin = 'I'
begin
  begin try
     begin tran
     select @w_secuencial = max(idsecuencial) from Sistema..secuencial  

     select @w_secuencial = isnull(@w_secuencial,0) + 1

     INSERT INTO Sistema..secuencial ([idsecuencial],[tabla],[secuencial])
     values (@w_secuencial,@i_tabla,0)
	 select @o_msg = 'la tabla: ' + @i_tabla + ' se guardo'
	 commit tran
  end try
  begin catch
       rollback tran
      select @o_msg = 'ERROR: ' + ERROR_MESSAGE() + ' EN LA LINEA: ' + CONVERT(VARCHAR(10),ERROR_LINE())
  end catch
  
end


GO
/****** Object:  StoredProcedure [dbo].[sp_tabla]    Script Date: 24/02/2018 21:11:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_tabla]
(
@Id_tabla int =0,
@Descripcion nvarchar(254),
@Tabla nvarchar(64),
@i_operacion varchar(1),
@o_msg varchar(254) = null output
)
as
if @i_operacion = 'I'
begin 
begin try

if isnull(@Descripcion, '') = ''
	 begin
	   select @o_msg = 'LA TABLA ES MANDATORIA'
	   RETURN 1
	 end 
if isnull(@Tabla, '') = ''
	 begin
	   select @o_msg = 'LA TABLA ES MANDATORIA'
	   RETURN 1
	 end 

	 declare @x int, @y varchar(254)

			exec Sistema..sp_secuencial
				@i_tabla  = 'Tabla',
				@i_operacoin = 'I',
				@o_secuencial = @x output,
				@o_msg  = @y output
				select @x as x,@y as y

	 INSERT INTO Tabla (Id_tabla,Descripcion,Tabla)
	 VALUES (@x,@Descripcion,@Tabla)
	 select @o_msg = 'LA TABLA: ' + convert(varchar(10), @x) + ' ' +@TABLA + ' SE GUARDO'

end try
begin catch
select @o_msg = 'ERROR: ' + ERROR_MESSAGE() + ' EN LA LINEA: ' + CONVERT(VARCHAR(10),ERROR_LINE())
end catch
end

if @i_operacion = 'U'
BEGIN 
BEGIN TRY
if isnull(@Descripcion, '') = ''
	 begin
	   select @o_msg = 'LA TABLA ES MANDATORIA'
	   RETURN 1
	 end 
if isnull(@Tabla, '') = ''
	 begin
	   select @o_msg = 'LA TABLA ES MANDATORIA'
	   RETURN 1
	 end 

	 UPDATE Tabla SET Descripcion=@Descripcion, Tabla=@Tabla WHERE Id_tabla=@Id_tabla
	  --select @o_msg = 'LA TABLA: ' + convert(nvarchar(10),@Id_tabla )+ ' ' + @TABLA + ' SE MODIFICO'
END TRY
BEGIN CATCH
select @o_msg = 'ERROR: ' + ERROR_MESSAGE() + ' EN LA LINEA: ' + CONVERT(VARCHAR(10),ERROR_LINE())
END CATCH
END

IF @i_operacion = 'S'
BEGIN 
BEGIN TRY
SELECT * FROM Tabla
END TRY
BEGIN CATCH
select @o_msg = 'ERROR: ' + ERROR_MESSAGE() + ' EN LA LINEA: ' + CONVERT(VARCHAR(10),ERROR_LINE())
END CATCH
END



GO
/****** Object:  StoredProcedure [dbo].[SP_TRANSACCIONES]    Script Date: 24/02/2018 21:11:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[SP_TRANSACCIONES]
(@ID_TRANSACCIONES INT=0,
@TRANSACCIONES VARCHAR(10),
@i_operacion char(1),
@o_msg varchar(254) = null output
)
AS

if @i_operacion = 'I'
BEGIN
BEGIN TRY

if @ID_TRANSACCIONES is null
begin 
select @o_msg='EL ID-TRANSACCION ES MANDATORIO'
return 1
END
if isnull(@TRANSACCIONES, '') = ''
	 begin
	   select @o_msg = 'LA TRANSACCION ES MANDATORIA'
	   RETURN 1
	 end 

	 declare @x int, @y varchar(254)

			exec Sistema..sp_secuencial
				@i_tabla  = 'Transacciones',
				@i_operacoin = 'S',
				@o_secuencial = @x output,
				@o_msg  = @y output
				select @x as x,@y as y



INSERT INTO Transacciones (Id_transacciones,transaccion)
VALUES (@x,@TRANSACCIONES)

select @o_msg = 'LA TRANSACCION: ' + @TRANSACCIONES+ ' SE GUARDO'
END TRY
BEGIN CATCH
select @o_msg = 'ERROR: ' + ERROR_MESSAGE() + ' EN LA LINEA: ' + CONVERT(VARCHAR(10),ERROR_LINE())
END CATCH
END

if @i_operacion = 'U'
BEGIN
BEGIN TRY
if isnull(@TRANSACCIONES, '') = ''
	 begin
	   select @o_msg = 'LA TRANSACCION ES MANDATORIA'
	   RETURN 1
	 end 
	 UPDATE Transacciones SET transaccion=@TRANSACCIONES WHERE Id_transacciones=@ID_TRANSACCIONES
	 select @o_msg = 'LA TRANSACCION: ' + @ID_TRANSACCIONES + ' SE ACTUALIZO'
END TRY
BEGIN CATCH
select @o_msg = 'ERROR: ' + ERROR_MESSAGE() + ' EN LA LINEA: ' + CONVERT(VARCHAR(10),ERROR_LINE())
END CATCH 
END

if @i_operacion = 'S'
BEGIN 
BEGIN TRY
SELECT * FROM Transacciones
END TRY
BEGIN CATCH 
select @o_msg = 'ERROR: ' + ERROR_MESSAGE() + ' EN LA LINEA: ' + CONVERT(VARCHAR(10),ERROR_LINE())
END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[sp_usuario]    Script Date: 24/02/2018 21:11:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_usuario]
(

@Id_usuario int =0,
@Usuario varchar(50) = '',
@Contraseña varchar(1) = '',
@Id_rol int = '',
@nombre varchar(50)= '',
@apellido varchar(50) = '',
@i_operacion varchar(1),
@o_msg varchar(254) = null output
)
as
if @i_operacion = 'I'
begin
begin try

if isnull(@Usuario, '') = ''
	 begin
	   select @o_msg = 'EL USUARIO ES MANDATORIO'
	   RETURN 1
	 end 
if isnull(@Contraseña, '') = ''
	 begin
	   select @o_msg = 'LA CONTRASENA MANDATORIA'
	   RETURN 1
	 end 

if isnull(@Contraseña, '') = ''
	 begin
	   select @o_msg = 'LA CONTRASENA MANDATORIA'
	   RETURN 1
	 end 

if isnull(@nombre, '') = ''
	 begin
	   select @o_msg = 'EL NOMBRE ES MANDATORIO'
	   RETURN 1
	 end 

if isnull(@apellido, '') = ''
	 begin
	   select @o_msg = 'EL APELLIDO ES MANDATORIO'
	   RETURN 1
	 end 

if @Id_rol is null
begin 
select @o_msg='EL ROL ES MANDATORIO'
return 1
end

 declare @x int, @y varchar(254)

			exec Sistema..sp_secuencial
				@i_tabla  = 'usuario',
				@i_operacoin = 'S',
				@o_secuencial = @x output,
				@o_msg  = @y output
				select @x as x,@y as y

insert into Sistema..usuario (Id_usuario,Usuario,Contraseña,Id_rol,nombre,apellido)
values (@x,@Usuario,@Contraseña,@Id_rol,@nombre,@apellido)
select @o_msg = 'EL USUARIO: ' + @nombre + ' ' + @apellido + ' SE GUARDO'
end try
begin catch
select @o_msg = 'ERROR: ' + ERROR_MESSAGE() + ' EN LA LINEA: ' + CONVERT(VARCHAR(10),ERROR_LINE())
end catch

end
if @i_operacion = 'S'
begin
begin try
select Id_usuario,Usuario,Contraseña,Id_rol,nombre,apellido from Sistema..usuario
end try
begin catch
select @o_msg = 'ERROR: ' + ERROR_MESSAGE() + ' EN LA LINEA: ' + CONVERT(VARCHAR(10),ERROR_LINE())
end catch
end


if @i_operacion = 'U'
begin
begin try
if isnull(@Usuario, '') = ''
	 begin
	   select @o_msg = 'EL USUARIO ES MANDATORIO'
	   RETURN 1
	 end 
if isnull(@Contraseña, '') = ''
	 begin
	   select @o_msg = 'LA CONTRASENA MANDATORIA'
	   RETURN 1
	 end 

if isnull(@Contraseña, '') = ''
	 begin
	   select @o_msg = 'LA CONTRASENA MANDATORIA'
	   RETURN 1
	 end 

if isnull(@nombre, '') = ''
	 begin
	   select @o_msg = 'EL NOMBRE ES MANDATORIO'
	   RETURN 1
	 end 

if isnull(@apellido, '') = ''
	 begin
	   select @o_msg = 'EL APELLIDO ES MANDATORIO'
	   RETURN 1
	end
update usuario set Usuario=@Usuario,Contraseña=@contraseña,Id_rol=@Id_rol,nombre=@nombre,apellido=@apellido where Id_usuario=@Id_usuario
select @o_msg = 'EL USUARIO: ' + @nombre + ' ' + @apellido + ' SE ACTUALIZO'
end try
begin catch
select @o_msg = 'ERROR: ' + ERROR_MESSAGE() + ' EN LA LINEA: ' + CONVERT(VARCHAR(10),ERROR_LINE())
end catch
end



GO
USE [master]
GO
ALTER DATABASE [Sistema] SET  READ_WRITE 
GO
