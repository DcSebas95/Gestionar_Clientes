CREATE DATABASE BD_BODEGA;

use BD_BODEGA;
go; 

/*CREATE TABLE clientes (
    cliente_id INT IDENTITY(1,1) PRIMARY KEY,
    nit INT UNIQUE NOT NULL,  -- Código del cliente
    nombre NVARCHAR(100) NOT NULL,
    apellido NVARCHAR(100) NOT NULL,
    email NVARCHAR(150) UNIQUE NOT NULL,
    telefono NVARCHAR(20),
    direccion NVARCHAR(255)
);*/

SELECT name 
FROM sys.databases;
GO

USE BD_BODEGA;
GO

ALTER ROLE db_owner ADD MEMBER [sa];
GO;




EXEC sp_GetClientes;
GO;

INSERT INTO clientes(nit,nombre,apellido,email,telefono,direccion)
VALUES(
    987654321,
    'sebastian',
    'diaz',
    'sdiaz@gmail.com',
    3198474,
    'cll127c#88b34'
);
go;

ALTER PROCEDURE sp_cliente(
    @cliente_id INT
)
AS
BEGIN
    SELECT 
        cliente_id,
        nit,
        nombre,
        apellido,
        email,
        telefono,
        direccion
    FROM Clientes
    WHERE cliente_id= @cliente_id ;
END;
GO;


EXEC sp_cliente 987654321;
go;

CREATE PROCEDURE sp_CrearClientes(
    @nit INT ,  -- Código del cliente
    @nombre NVARCHAR(100) ,
    @apellido NVARCHAR(100) ,
    @email NVARCHAR(150) ,
    @telefono NVARCHAR(20),
    @direccion NVARCHAR(255)
)

AS
BEGIN
    INSERT INTO clientes(nit,nombre,apellido,email,telefono,direccion)
VALUES(
    @nit,
    @nombre,
    @apellido ,
    @email ,
    @telefono ,
    @direccion 
);
END;

go;

CREATE PROCEDURE sp_EliminarClientes(
    @nit INT   -- Código del cliente
)

AS
BEGIN
    delete from clientes(nit,nombre,apellido,email,telefono,direccion)
WHERE nit = @nit;
END;

SELECT name FROM sys.databases;
GO;

create PROCEDURE sp_EditarClientes(
    @nit INT,  
    @nombre NVARCHAR(100),
    @apellido NVARCHAR(100),
    @email NVARCHAR(150),
    @telefono NVARCHAR(20),
    @direccion NVARCHAR(255))
    as
BEGIN
    SET NOCOUNT ON;

    UPDATE clientes
    SET nit = @nit,
        nombre = @nombre,
        apellido = @apellido,
        email = @email,
        telefono = @telefono,
        direccion = @direccion
    WHERE nit = @nit;
END;
GO

