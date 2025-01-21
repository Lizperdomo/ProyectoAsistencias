create database registroasistencia;

use registroasistencia;

CREATE TABLE usuarios (
    IdUsuario INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    Nombre NVARCHAR(40) NOT NULL,
    ApellidoP NVARCHAR(40) NOT NULL, 
    ApellidoM NVARCHAR(40) NOT NULL, 
    Correo NVARCHAR(100) NOT NULL, 
    Telefono NVARCHAR(10) NOT NULL
);

CREATE TABLE asistencias (
    IdAsistencia INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    IdUsuario INT NOT NULL,
    HoraEntrada TIME NOT NULL,
    HoraSalida TIME NOT NULL,
    Fecha DATE NOT NULL,
    FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario)
);


INSERT INTO usuarios (nombre, apellidoP, apellidoM, correo, telefono) VALUES
('Juan', 'P�rez', 'Gonz�lez', 'juan.perez@gmail.com', '5512345678'),
('Mar�a', 'L�pez', 'Mart�nez', 'maria.lopez@gmail.com', '5523456789'),
('Carlos', 'Hern�ndez', 'Ram�rez', 'carlos.hernandez@gmail.com', '5534567890'),
('Ana', 'S�nchez', 'Torres', 'ana.sanchez@gmail.com', '5545678901'),
('Luis', 'Garc�a', 'Flores', 'luis.garcia@gmail.com', '5556789012'),
('Sof�a', 'Mart�nez', 'G�mez', 'sofia.martinez@gmail.com', '5567890123'),
('Miguel', 'Ram�rez', 'Luna', 'miguel.ramirez@gmail.com', '5578901234'),
('Paula', 'Torres', 'Vargas', 'paula.torres@gmail.com', '5589012345'),
('David', 'Vargas', 'Morales', 'david.vargas@gmail.com', '5590123456'),
('Laura', 'Flores', 'P�rez', 'laura.flores@gmail.com', '5512345678');


INSERT INTO asistencias (idUsuario, horaEntrada, horaSalida, fecha) VALUES
(1, '08:30:00', '17:30:00', '2025-01-01'),
(2, '09:00:00', '18:00:00', '2025-01-01'),
(3, '07:45:00', '16:45:00', '2025-01-01'),
(4, '08:00:00', '17:00:00', '2025-01-02'),
(5, '08:15:00', '17:15:00', '2025-01-02'),
(6, '08:30:00', '17:30:00', '2025-01-03'),
(7, '09:00:00', '18:00:00', '2025-01-03'),
(8, '07:45:00', '16:45:00', '2025-01-04'),
(9, '08:00:00', '17:00:00', '2025-01-04'),
(10, '08:15:00', '17:15:00', '2025-01-05');

