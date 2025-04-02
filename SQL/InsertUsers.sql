
USE ObligatorioP3;
GO

-- Agregar la columna Gender

-- Insertar los datos con UTF-8 y la columna Gender
INSERT INTO Users (Name, Lastname, Phone, Birth, Email, Password, Role, Gender)
VALUES
    ('Juan', 'Perez', '+59891234567', '1990-05-15', 'juan.perez@gmail.com', 'password123', 0, 1),
    ('Maria', 'Gomez', '+59892345678', '1985-08-20', 'maria.gomez@hotmail.com', 'securepass', 0, 0),
    
    ('Carlos', 'Ramirez', '+59893456789', '1992-03-10', 'carlos.ramirez@yahoo.com', 'empleado123', 1, 1),
    ('Lucia', 'Fernandez', '+59894567890', '1995-07-25', 'lucia.fernandez@gmail.com', 'empleado456', 1, 0),
    ('Miguel', 'Lopez', '+59895678901', '1988-12-05', 'miguel.lopez@outlook.com', 'empleado789', 1, 1),
    
    ('Sofia', 'Diaz', '+59896789012', '2000-02-18', 'sofia.diaz@gmail.com', 'cliente111', 2, 0),
    ('Andres', 'Martinez', '+59897890123', '1998-06-30', 'andres.martinez@hotmail.com', 'cliente222', 2, 1),
    ('Valentina', 'Ruiz', '+59898901234', '1993-11-14', 'valentina.ruiz@yahoo.com', 'cliente333', 2, 0),
    ('Diego', 'Torres', '+59899012345', '1987-09-22', 'diego.torres@outlook.com', 'cliente444', 2, 1),
    ('Camila', 'Vega', '+59890123456', '1991-04-08', 'camila.vega@gmail.com', 'cliente555', 2, 0);

-- Seleccionar los datos de la tabla Users
SELECT * FROM Users;
