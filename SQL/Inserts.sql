USE obligatorioP3;
GO

INSERT INTO Users
	(Name, Lastname, Phone, Birth, Email, Password, Role, Gender, State, CreatedByID, UpdatedByID, LastUpdated)
VALUES
	('Juan', 'Perez', '+59891234567', '1990-05-15', 'juan.perez@gmail.com', 'password123', 0, 0, 0, 1, 1, '2025-01-01 00:00:00'),
	('Maria', 'Gomez', '+59892345678', '1985-08-20', 'gerente@gmail.com', 'Gerente2025', 0, 0, 0, 1, 1, '2025-01-01 00:00:00'),

	('Carlos', 'Ramirez', '+59893456789', '1992-03-10', 'carlos.ramirez@yahoo.com', 'empleado123', 1, 1, 0, 1, 1, '2025-01-01 00:00:00'),
	('Lucia', 'Fernandez', '+59894567890', '1995-07-25', 'lucia.fernandez@gmail.com', 'empleado456', 1, 0, 0, 1, 2, '2025-01-01 00:00:00'),
	('Miguel', 'Lopez', '+59895678901', '1988-12-05', 'miguel.lopez@outlook.com', 'empleado789', 1, 1, 0, 1, 1, '2025-01-01 00:00:00'),
	('Bruno', 'Castro', '+59892223344', '1989-07-21', 'bruno.castro@hotmail.com', 'bruno456', 1, 1, 0, 1, 2, '2025-01-01 00:00:00'),
	('Daniel', 'Sosa', '+59894445566', '1992-09-17', 'daniel.sosa@outlook.com', 'daniel321', 1, 1, 0, 1, 1, '2025-01-01 00:00:00'),
	('Hugo', 'Ramos', '+59898889900', '1988-12-02', 'hugo.ramos@outlook.com', 'hugo222', 1, 1, 0, 1, 2, '2025-01-01 00:00:00'),

	('Sofia', 'Diaz', '+59896789012', '2000-02-18', 'sofia.diaz@gmail.com', 'cliente111', 2, 0, 0, 1, 1, '2025-01-01 00:00:00'),
	('Andres', 'Martinez', '+59897890123', '1998-06-30', 'andres.martinez@hotmail.com', 'cliente222', 2, 1, 0, 1, 2, '2025-01-01 00:00:00'),
	('Valentina', 'Ruiz', '+59898901234', '1993-11-14', 'valentina.ruiz@yahoo.com', 'cliente333', 2, 0, 0, 1, 1, '2025-01-01 00:00:00'),
	('Diego', 'Torres', '+59899012345', '1987-09-22', 'diego.torres@outlook.com', 'cliente444', 2, 1, 0, 1, 2, '2025-01-01 00:00:00'),
	('Camila', 'Vega', '+59890123456', '1991-04-08', 'camila.vega@gmail.com', 'cliente555', 2, 0, 0, 1, 1, '2025-01-01 00:00:00'),
	('Ana', 'Silva', '+59891112233', '1994-03-12', 'ana.silva@gmail.com', 'cliente666', 2, 0, 0, 1, 2, '2025-01-01 00:00:00'),
	('Carla', 'Mendez', '+59893334455', '1997-01-30', 'carla.mendez@yahoo.com', 'cliente777', 2, 0, 0, 1, 1, '2025-01-01 00:00:00'),
	('Elena', 'Pereira', '+59895556677', '1996-05-25', 'elena.pereira@gmail.com', 'cliente888', 2, 0, 0, 1, 2, '2025-01-01 00:00:00'),
	('Felipe', 'Morales', '+59896667788', '1991-11-11', 'felipe.morales@hotmail.com', 'cliente999', 2, 1, 0, 1, 1, '2025-01-01 00:00:00'),
	('Gabriela', 'Suarez', '+59897778899', '1993-08-19', 'gabriela.suarez@yahoo.com', 'cliente100', 2, 0, 0, 1, 2, '2025-01-01 00:00:00'),
	('Irene', 'Alonso', '+59899990011', '1995-04-14', 'irene.alonso@gmail.com', 'cliente111', 2, 0, 0, 1, 1, '2025-01-01 00:00:00'),
	('Joaquin', 'Vazquez', '+59890001122', '1990-10-28', 'joaquin.vazquez@hotmail.com', 'cliente122', 2, 1, 0, 1, 2, '2025-01-01 00:00:00');

INSERT INTO Agencies
	(Name, Address, Latitude, Longitude)
VALUES
	('Agencia Central', 'Av. 18 de Julio 1234, Montevideo', -34.905, -56.191),
	('Sucursal Pocitos', 'Ellauri 456, Montevideo', -34.917, -56.153),
	('Oficina Ciudad Vieja', 'Piedras 789, Montevideo', -34.906, -56.203),
	('Agencia Punta Carretas', 'José Ellauri 350, Montevideo', -34.927, -56.156),
	('Sucursal Tres Cruces', 'Bv. Artigas 1825, Montevideo', -34.894, -56.168),
	('Sucursal Cordón', 'Av. Rivera 2000, Montevideo', -34.901, -56.170),
	('Oficina Parque Rodó', 'Bvar. España 123, Montevideo', -34.917, -56.164),
	('Agencia Prado', 'Av. Agraciada 3500, Montevideo', -34.866, -56.203),
	('Sucursal Buceo', 'Av. Italia 4567, Montevideo', -34.887, -56.110),
	('Oficina Malvín', 'Av. Legrand 789, Montevideo', -34.894, -56.112);

INSERT INTO Shippings
	(Weight, EmployeeID, ClientID, State, Discriminator, PickupId, Address, Send, Arrival)
VALUES
	(20, 3, 6, 0, 'Common', 4, NULL, NULL, NULL),
	(15, 3, 7, 0, 'Common', 2, NULL, NULL, NULL),
	(8, 4, 8, 1, 'Urgent', NULL, 'Av. Italia 1234, Montevideo', NULL, NULL),
	(25, 5, 9, 0, 'Common', 3, NULL, NULL, NULL),
	(12, 3, 10, 1, 'Urgent', NULL, 'Bvar. España 456, Montevideo', NULL, NULL),
	(30, 4, 6, 0, 'Common', 5, NULL, NULL, NULL),
	(18, 5, 7, 1, 'Urgent', NULL, 'Piedras 789, Montevideo', NULL, NULL),
	(22, 3, 8, 0, 'Common', 7, NULL, NULL, NULL),
	(10, 4, 9, 1, 'Urgent', NULL, 'Av. Rivera 2000, Montevideo', NULL, NULL),
	(28, 5, 10, 0, 'Common', 9, NULL, NULL, NULL),
	(12, 11, 16, 0, 'Common', 1, NULL, NULL, NULL),
	(9, 12, 17, 1, 'Urgent', NULL, 'Av. Italia 2500, Montevideo', NULL, NULL),
	(14, 13, 18, 0, 'Common', 3, NULL, NULL, NULL),
	(7, 14, 19, 1, 'Urgent', NULL, 'Colonia 1200, Montevideo', NULL, NULL),
	(20, 15, 20, 0, 'Common', 5, NULL, NULL, NULL),
	(11, 16, 11, 1, 'Urgent', NULL, 'Av. Millán 4000, Montevideo', NULL, NULL),
	(16, 17, 12, 0, 'Common', 6, NULL, NULL, NULL),
	(8, 18, 13, 1, 'Urgent', NULL, 'Av. Sayago 3000, Montevideo', NULL, NULL),
	(13, 19, 14, 0, 'Common', 8, NULL, NULL, NULL),
	(10, 20, 15, 1, 'Urgent', NULL, 'Camino Maldonado 7000, Montevideo', NULL, NULL);

INSERT INTO Commentaries
	(Text, Date, UserId, ShippingId)
VALUES
	('Entrega rápida y sin problemas.', '2025-05-18 10:15:00', 6, 1),
	('Muy conforme con el servicio.', '2025-05-18 11:20:00', 7, 2),
	('El paquete llegó en buen estado.', '2025-05-18 12:30:00', 8, 3),
	('Hubo un pequeño retraso.', '2025-05-18 13:45:00', 9, 4),
	('Excelente atención al cliente.', '2025-05-18 14:10:00', 10, 5),
	('Recomiendo esta agencia.', '2025-05-18 15:00:00', 6, 6),
	('El envío fue más rápido de lo esperado.', '2025-05-18 16:25:00', 7, 7),
	('El repartidor fue muy amable.', '2025-05-18 17:40:00', 8, 8),
	('El paquete llegó antes de lo previsto.', '2025-05-18 18:55:00', 9, 9),
	('Todo perfecto, muchas gracias.', '2025-05-18 19:30:00', 10, 10);

SELECT *
FROM Users;
SELECT *
FROM Agencies;
SELECT *
FROM Shippings;
SELECT *
FROM Commentaries;
