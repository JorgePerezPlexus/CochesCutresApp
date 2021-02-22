create database CochesCutres;

create table TipoVehiculos
(
TipoID int identity primary key,
Tipo varchar(50)
);

create table Vehiculos
(
VehiculoId int identity primary key,
Marca varchar(50) not null,
Modelo varchar(50),
Puertas int,
Color varchar(50),
TipoID int,
MesesGarantia int,
Stock bit,
Foto varchar(100),
Foreign key (TipoID) references TipoVehiculos(TipoID)
);

create table Clientes
(
ClienteId int identity primary key,
NIF varchar(9),
Nombre varchar(20) not null,
Apellidos varchar(50),
Telefono varchar(9),
Direccion varchar(200),
Email varchar(100)
);

create table Empleados
(
EmpleadoId int identity primary key,
NIF varchar(9),
Nombre varchar(20) not null,
Apellidos varchar(50),
Telefono varchar(9),
Direccion varchar(200),
Email varchar(100)
);


create table TipoCompraVenta
(
TipoId int identity primary key,
TipoCompraVenta varchar(10)
);

create table Compras
(
CompraId int identity primary key,
Fecha Date,
VehiculoID int,
TipoID int,
EmpleadoId int,
ClienteId int,
Precio float,
Foreign key (VehiculoID) references Vehiculos(VehiculoID),
Foreign key (TipoID) references TipoCompraventa(TipoID),
Foreign key (EmpleadoId) references Empleados(EmpleadoId),
Foreign key (ClienteId) references Clientes(ClienteId)
);

drop table Compras;

insert into TipoCompraVenta values ('Compra');
insert into TipoCompraVenta values ('Venta');

insert into TipoVehiculos values ('Utilitario');
insert into TipoVehiculos values ('Coupe');
insert into TipoVehiculos values ('Familiar');
insert into TipoVehiculos values ('SUV');
insert into TipoVehiculos values ('Industrial');

insert into Clientes values ('79331337j','Jorge','Perez','677364917','Cee', 'jpgseva@hotmail.com');
insert into Empleados values ('75554555j','Miguel','Perez','677355852','Coruña', 'mp@hotmail.com');

insert into Vehiculos values ('Seat','Ibiza',5,'blanco',1,12,1,'ibiza.jpg');

insert into Compras values ('12/02/2020',2, 1,1,3,2500);

create trigger tr_borrarVehiculo
on Compras
after insert 
as
declare @id int
begin
set @id = (select VehiculoId from Inserted where TipoId = 1)
delete from Vehiculos where VehiculoId=@Id
end;


