Use master;

if DB_ID('Technology') is not null	drop database Technology
Create database Technology
go

Use Technology
go

/* Tablas D */ 
create table Cargo
(
	idCargo		int identity(1,1) not null,
	nomCargo	varchar(50)	not null
)
go

create table Categoria
(
	idCategoria			int identity(1,1) not null,
	nomCategoria		varchar(50) not null
)
go

create table Tipo_Usuario
(
	idTipoUsuario		int identity(1,1) not null,
	nomTipoUsuario		varchar(50) not null
)
go

create table Distrito
(
	idDistrito			int identity(1,1) not null,
	nomDistrito			varchar(50) not null
)
go

/* Tablas C */ 

create table Empleado
(
	idEmpleado		int identity(1,1) not null,
	nomEmpleado		varchar(50) not null,
	apeEmpleado		varchar(50) not null,
	dniEmpleado		char(8) not null,
	tlfEmpleado		varchar(15),
	direcEmpleado	varchar(100) not null,
	idDistrito		int not null,
	idCargo			int not null,
	emailEmpleado	varchar(50),
	passEmpleado	varchar(30),
	idTipoUsuario	int not null
)
go

create table Cliente
(
	idCliente		int identity(1,1) not null,
	nomCliente		varchar(50) not null,
	apeCliente		varchar(50)	not null,
	dniCliente		char(8) not null,
	tlfCliente		varchar(15),
	direcCliente	varchar(100) not null,
	idDistrito		int not null,
	emailCliente	varchar(50) not null,
	passCliente		varchar(30) not null,
	idTipoUsuario	int not null
)
go

create table Producto
(
	idProducto		int identity(1,1) not null,
	desProducto		varchar(100) not null,
	idCategoria		int not null,
	precioProducto	money not null,
	stock_act		int not null,
	stock_min		int not null
)
go

create table Compra_Cabecera
(
	idCompra		int identity(1,1) not null,
	idCliente		int not null,
	fec_ped_comp	datetime not null,
	fec_ent_comp	datetime not null,
	est_comp		int not null
)
go

create table Compra_Detalle
(
	idCompra		int not null,
	idProducto		int not null,
	cantidad		int not null
)
go


-- Restricciones
alter table Cargo
	add constraint PK_CARGO primary key (idCargo)
go

alter table Categoria
	add constraint PK_CATEGORIA primary key (idCategoria)
go

alter table Tipo_Usuario
	add constraint PK_TIPOUSUARIO primary key (idTipoUsuario)
go

alter table Distrito
	add constraint PK_DISTRITO primary key (idDistrito)
go

alter table Cliente
	add constraint PK_CLIENTE primary key (idCliente),
	constraint UNQ_DNI_CLI unique (dniCliente),
	constraint CHK_EMAIL_CLI check (emailCliente like '%@%.%'),
	constraint FK_DIST_CLI foreign key (idDistrito) references Distrito (idDistrito),
	constraint FK_TIPOUSU_CLI foreign key (idTipoUsuario) references Tipo_Usuario (idTipoUsuario),
	constraint DF_TIPOUSU_CLI default 3 for idTipoUsuario
go

alter table Empleado
	add constraint PK_EMPLEADO primary key (idEmpleado),
	constraint UNQ_DNI_EMP unique (dniEmpleado),
	constraint CHK_EMAIL_EMP check (emailEmpleado like '%@%.%'),
	constraint FK_DIST_EMP foreign key (idDistrito) references Distrito (idDistrito),
	constraint FK_CARGO_EMP foreign key (idCargo) references Cargo (idCargo),
	constraint FK_TIPOUSU_EMP foreign key (idTipoUsuario) references Tipo_Usuario (idTipoUsuario)
go

alter table Producto
	add constraint PK_PRODUCTO primary key (idProducto),
	constraint FK_CAT_PROD foreign key (idCategoria) references Categoria (idCategoria),
	constraint UNQ_DES_PROD unique (desProducto)
go

alter table Compra_Cabecera
	add constraint PK_COMPRACABE primary key (idCompra),
	constraint FK_CLI_COMPRA foreign key (idCliente) references Cliente (idCliente),
	constraint DF_FECPED default getdate() for fec_ped_comp
go

alter table Compra_Detalle
	add constraint PK_COMPRADETA primary key (idCompra, idProducto),
	constraint FK_COMPCABE_DETA foreign key (idCompra) references Compra_Cabecera (idCompra),
	constraint FK_PRODUCTO_DETA foreign key (idProducto) references Producto (idProducto),
	constraint DF_CANTIDAD default 1 for cantidad
go




/*
---- Aquí empieza el registro de datos en las tablas
*/
insert into Cargo values ('Gerente General')
insert into Cargo values ('Supervisor')
insert into Cargo values ('Vendedor')
insert into Cargo values ('Cajero')
insert into Cargo values ('Reponedor')
go

insert into Categoria values ('Memorias')
insert into Categoria values ('Celulares')
insert into Categoria values ('Laptops')
insert into Categoria values ('Audífonos')
insert into Categoria values ('Televisores')
go

insert into Distrito values ('Cercado de Lima')
insert into Distrito values ('Ate')
insert into Distrito values ('Barranco')
insert into Distrito values ('Breña')
insert into Distrito values ('Comas')
insert into Distrito values ('Chorrillos')
insert into Distrito values ('El Agustino')
insert into Distrito values ('Jesús María')
insert into Distrito values ('La Molina')
insert into Distrito values ('La Victoria')
insert into Distrito values ('Lince')
insert into Distrito values ('Magdalena del Mar')
insert into Distrito values ('Miraflores')
insert into Distrito values ('Puente Piedra')
insert into Distrito values ('Rimac')
insert into Distrito values ('San Isidro')
insert into Distrito values ('Independencia')
insert into Distrito values ('San Martin de Porres')
insert into Distrito values ('San Miguel')
insert into Distrito values ('Santiago de Surco')
insert into Distrito values ('Los Olivos')
go

insert into Tipo_Usuario values ('Administrador')
insert into Tipo_Usuario values ('Empleado')
insert into Tipo_Usuario values ('Cliente')
go

insert into Empleado values ('Marco', 'De la cruz Henriquez', 12345678, 987654321, 'Av. universitaria 123', 5, 1, 'marcodelacruz@gmail.com', '1234', 1)
go

/*
insert into Cliente values ('Pedro', 'Camargo', 52147839, 987123456, 'Av. por ahí 145', 1, 'pedrocamargo@gmail.com', '1234', default)
go
*/

insert into Producto values ('Memoria Micro SD Transcend de 8GB clase 10', 1, 22.50, 50, 5)
insert into Producto values ('iPhone X 128GB' , 2 ,4000  , 30 , 10)
insert into Producto values ('SAMSUNG J6', 2, 700, 100, 20)
insert into Producto values ('HUAWEI P20 64GB, 4GB RAM, 16MP', 2, 2300, 30, 10)
insert into Producto values ('HUAWEI Y5 2018 16GB, 1GB RAM , 8MP', 2, 500, 30, 5)
insert into Producto values ('Laptop HP WIN10 core I7 3.2 8Gb ram , 1TB HDD', 3, 3600.00, 150, 50)
insert into Producto values ('Laptop Dell WIN10 core I5 2.5ghz, 4GB ram, 500GB H', 3, 2500.00, 30, 5)
insert into Producto values ('Laptop Lenovo WIN10 core I5 2.7ghz, 8GB ram,500GB ', 3, 2700.00, 45, 15)
insert into Producto values ('Laptop Advance WIN10 core I7 3.1ghz, 4GB ram, 500G', 3, 2800.00, 55, 10)
go



/*
---- Aquí acaba el registro de datos en las tablas
*/




/*
	--------- Aquí comienza los procedimientos Almacenados
*/

-- Procedimientos para Login
create or alter proc usp_logincliente
(
@email varchar(80),
@pass varchar(30)
)
as
	begin
		select 
				idCliente 
				from Cliente
				where emailCliente = @email and passCliente = @pass
	end
go


--Procedimientos para Producto
create or alter proc usp_listar_productos
as
	begin
		select p.idProducto, p.desProducto, p.precioProducto, c.idCategoria, c.nomCategoria, p.stock_act
		from Producto p join Categoria c
			on p.idCategoria = c.idCategoria
	end
go


create or alter proc usp_detalle_producto(@cod int)
as
	begin
		select p.idProducto, p.desProducto, p.precioProducto, c.idCategoria, c.nomCategoria, p.stock_act
		from Producto p join Categoria c
			on p.idCategoria = c.idCategoria
		where p.idProducto = @cod
	end
go

create or alter proc usp_transaccion_compra_cabecera
(
	@id_comp int,
	@cod_cli int,
	@fec_ped_comp datetime,
	@fec_ent_comp datetime,
	@estado int,
	@tipo int
)
as
	if @tipo = 0
		begin
			insert into Compra_Cabecera values (@cod_cli, @fec_ped_comp, @fec_ent_comp, 0)
		end
	else
		begin
			update Compra_Cabecera
			set est_comp = @estado
			where idCompra = @id_comp
		end
go


create or alter proc usp_insertar_compra_detalle
	@id_comp int,
	@cod_prod int,
	@cantidad int
as
	begin
		insert into Compra_Detalle values (@id_comp, @cod_prod, @cantidad)
		update Producto
		set stock_act = stock_act - 1
		where idProducto = @cod_prod
	end
go

create or alter proc usp_transaccion_compra_cabecera
	@id_comp int,
	@cod_cli int,
	@fec_ped_comp datetime,
	@fec_ent_comp datetime,
	@est_comp int,
	@tipo int
as
	if @tipo = 0
		begin
			insert into Compra_Cabecera values (@cod_cli, @fec_ped_comp, @fec_ent_comp, 0)
		end
	else
		begin
			update Compra_Cabecera
			set  est_comp = @est_comp
			where idCompra = @id_comp
		end
go


create proc usp_detalle_compra
(
@idcomp int
)
as
	begin
		select cd.idCompra, cd.idProducto, p.desProducto, p.precioProducto
		from Compra_Detalle cd join Producto p
			on cd.idProducto = p.idProducto
			where idCompra = @idcomp
	end
go



-- Procedimientos para mantenimiento de Cliente
create or alter proc usp_registrar_cliente
(
	@nomcli varchar(50),
	@apecli varchar(50),
	@dnicli char(8),
	@tlfcli varchar(15),
	@dircli varchar(80),
	@iddist	int,
	@emailcli	varchar(80),
	@passcli	varchar(15)
)
as
	begin
		insert into Cliente values
		(@nomcli, @apecli, @dnicli, @tlfcli, @dircli, @iddist, @emailcli, @passcli, 3)
	end
go


create or alter proc usp_editar_cliente
(
	@codcli int,
	@nomcli varchar(50),
	@apecli varchar(50),
	@iddist	int,
	@dircli varchar(80),
	@tlfcli varchar(15),
	@passcli	varchar(15)
)
as
	begin
		update Cliente
		set nomCliente = @nomcli, apeCliente = @apecli, idDistrito = @iddist, direcCliente = @dircli, tlfCliente = @tlfcli, passCliente = @passcli
		where idCliente = @codcli
	end
go

/*
	------ Aquí acaba los procedimientos almacenados
*/

select * from Cargo;
select * from Categoria;
select * from Distrito;
select * from Tipo_Usuario;
select * from Empleado;
select * from Cliente;
select * from Producto;
select * from Compra_Cabecera;
select * from Compra_Detalle;