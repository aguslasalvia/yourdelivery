USE ObligatorioP3;
GO

INSERT INTO Users (Name, Lastname, Phone, Birth, Email, Password, Role)
VALUES
    ('Juan', 'P�rez', '+59891234567', '1990-05-15', 'juan.perez@gmail.com', 'password123', 0),
    ('Mar�a', 'G�mez', '+59892345678', '1985-08-20', 'maria.gomez@hotmail.com', 'securepass', 0),
    
    ('Carlos', 'Ram�rez', '+59893456789', '1992-03-10', 'carlos.ramirez@yahoo.com', 'empleado123', 1),
    ('Luc�a', 'Fern�ndez', '+59894567890', '1995-07-25', 'lucia.fernandez@gmail.com', 'empleado456', 1),
    ('Miguel', 'L�pez', '+59895678901', '1988-12-05', 'miguel.lopez@outlook.com', 'empleado789', 1),
    
    ('Sof�a', 'D�az', '+59896789012', '2000-02-18', 'sofia.diaz@gmail.com', 'cliente111', 2),
    ('Andr�s', 'Mart�nez', '+59897890123', '1998-06-30', 'andres.martinez@hotmail.com', 'cliente222', 2),
    ('Valentina', 'Ruiz', '+59898901234', '1993-11-14', 'valentina.ruiz@yahoo.com', 'cliente333', 2),
    ('Diego', 'Torres', '+59899012345', '1987-09-22', 'diego.torres@outlook.com', 'cliente444', 2),
    ('Camila', 'Vega', '+59890123456', '1991-04-08', 'camila.vega@gmail.com', 'cliente555', 2);


SELECT * FROM Users;


