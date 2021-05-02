

/*  Procedimientos Almacenados de Cargo  */
create or alter proc usp_adm_cargo_listar
as
	begin
		select 
				e.idCargo,
				e.nomCargo
			from Cargo e
	end
go

create or alter proc usp_adm_cargo_registrar
(
	@nombre varchar(50)
)
as
	begin
		insert into Cargo values
		(@nombre)
	end
go

create or alter proc usp_adm_cargo_actualizar
(
	@codigo int,
	@nombre varchar(50)
)
as
	begin
		update Cargo 
		set nomCargo = @nombre
		where idCargo = @codigo
	end
go

create or alter proc usp_adm_cargo_eliminar
(
	@codigo int
)
as
	begin
		delete from Cargo
		where idCargo = @codigo
	end
go





/*  Procedimientos Almacenados de Categoria  */


create or alter proc usp_adm_categoria_listar
as
	begin
		select 
				c.idCategoria,
				c.nomCategoria
			from Categoria c
	end
go

create or alter proc usp_adm_categoria_registrar
(
	@nombre varchar(50)
)
as
	begin
		insert into Categoria values
		(@nombre)
	end
go

create or alter proc usp_adm_categoria_actualizar
(
	@codigo int,
	@nombre varchar(50)
)
as
	begin
		update Categoria 
		set nomCategoria = @nombre
		where idCategoria = @codigo
	end
go

create or alter proc usp_adm_categoria_eliminar
(
	@codigo int
)
as
	begin
		delete from Categoria
		where idCategoria = @codigo
	end
go


/*  Procedimientos Almacenados de Cliente  */

create or alter proc usp_adm_cliente_listar
as
	begin
		select 
				cli.idCliente, cli.nomCliente, cli.apeCliente, cli.dniCliente, cli.tlfCliente, cli.direcCliente,
				d.nomDistrito, cli.emailCliente, cli.passCliente
			from Cliente cli inner join Distrito d
			on cli.idDistrito = d.idDistrito
	end
go


create or alter proc usp_adm_cliente_actualizar
(
	@codigo int,
	@nombre varchar(50),
	@apellido varchar(50),
	@dni char(8),
	@telefono varchar(15),
	@direccion varchar(80),
	@distrito	int,
	@correo varchar(50),
	@password varchar(15)

)
as
	begin
		update Cliente 
		set nomCliente = @nombre, apeCliente = @apellido, dniCliente = @dni, tlfCliente = @telefono, direcCliente = @direccion,
			idDistrito = @distrito, emailCliente = @correo , passCliente = @password
		where idCliente = @codigo
	end
go

create or alter proc usp_adm_cliente_eliminar
(
	@codigo int
)
as
	begin
		delete from Cliente
		where idCliente = @codigo
	end
go








/*  Procedimientos Almacenados de Distrito  */


create or alter proc usp_adm_distrito_listar
as
	begin
		select 
				d.idDistrito,
				d.nomDistrito
			from Distrito d
	end
go

create or alter proc usp_adm_distrito_registrar
(
	@nombre varchar(50)
)
as
	begin
		insert into Distrito values
		(@nombre)
	end
go

create or alter proc usp_adm_distrito_actualizar
(
	@codigo int,
	@nombre varchar(50)
)
as
	begin
		update Distrito 
		set nomDistrito = @nombre
		where idDistrito = @codigo
	end
go

create or alter proc usp_adm_distrito_eliminar
(
	@codigo int
)
as
	begin
		delete from Distrito
		where idDistrito = @codigo
	end
go




/*  Procedimientos Almacenados de Empleado  */

create or alter proc usp_adm_listar_empleados
as
	begin
		select
				e.idEmpleado, e.nomEmpleado, e.apeEmpleado, e.dniEmpleado, e.tlfEmpleado, e.direcEmpleado,
				d.nomDistrito, c.nomCargo, e.emailEmpleado, e.passEmpleado, tp.nomTipoUsuario
				from Empleado e inner join Cargo c
				on e.idCargo = c.idCargo inner join Distrito d
				on e.idDistrito = d.idDistrito inner join Tipo_Usuario tp
				on e.idTipoUsuario = tp.idTipoUsuario
	end
go

create or alter proc usp_adm_registrar_empleado
(
	@nombre varchar(50),
	@apellido varchar(50),
	@dni char(8),
	@telefono varchar(15),
	@direccion varchar(100),
	@coddis int,
	@codcargo int,
	@email varchar(50),
	@pass varchar(30),
	@codtipusu int
)
as
	begin
		insert into Empleado values
		(@nombre, @apellido, @dni, @telefono, @direccion, @coddis, @codcargo, @email, @pass,@codtipusu)
	end
go



create or alter proc usp_adm_actualizar_empleado
(
	@codigo int,
	@nombre varchar(50),
	@apellido varchar(50),
	@dni char(8),
	@telefono varchar(15),
	@direccion varchar(100),
	@iddistrito int,
	@idcargo int,
	@email varchar(50),
	@pass varchar(50),
	@idtipusu int
)
as
	begin
		update Empleado
		set 
			nomEmpleado = @nombre, apeEmpleado = @apellido, dniEmpleado = @dni,
			tlfEmpleado = @telefono, direcEmpleado = @direccion, idDistrito = @iddistrito,
			idCargo = @idcargo, emailEmpleado = @email, passEmpleado = @pass, idTipoUsuario = @idtipusu

		where idEmpleado = @codigo
	end
go

create or alter proc usp_adm_eliminar_empleado
(
	@codigoemp int
)
as
	begin
		delete from Empleado
		 where idEmpleado = @codigoemp
	end
go



/*  Procedimientos Almacenados de Productos  */


create or alter proc usp_adm_producto_listar
as
	begin
		select
				p.idProducto,
				p.desProducto,
				p.precioProducto,
				p.stock_act,
				p.stock_min,
				c.nomCategoria
				from Producto p inner join Categoria c
				on p.idCategoria = c.idCategoria
	end
go




create or alter proc usp_adm_producto_registrar
(
	@descripcion varchar(100),
	@precio money,
	@stockact int,
	@stockmin int,
	@idcateg int
)
as
	begin
		insert into Producto values
		(@descripcion, @idcateg, @precio, @stockact, @stockmin)
	end
go

create or alter proc usp_adm_producto_actualizar
(
	@codigo int,
	@descripcion varchar(100),
	@precio money,
	@stockact int,
	@stockmin int,
	@idcateg int
)
as
	begin
		update Producto 
		set desProducto = @descripcion,
			idCategoria = @idcateg,
			precioProducto = @precio,
			stock_act = @stockact,
			stock_min = @stockmin
		where idProducto = @codigo
	end
go

create or alter proc usp_adm_producto_eliminar
(
	@codigo int
)
as
	begin
		delete from Producto
		where idProducto = @codigo
	end
go




/*  Procedimientos Almacenados de Tipo de Usuario  */


create or alter proc usp_adm_tipousuario_listar
as
	begin
		select 
				t.idTipoUsuario,
				t.nomTipoUsuario
			from Tipo_Usuario t
	end
go




create or alter proc usp_adm_tipousuario_registrar
(
	@nombre varchar(50)
)
as
	begin
		insert into Tipo_Usuario values
		(@nombre)
	end
go

create or alter proc usp_adm_tipousuario_actualizar
(
	@codigo int,
	@nombre varchar(50)
)
as
	begin
		update Tipo_Usuario 
		set nomTipoUsuario = @nombre
		where idTipoUsuario = @codigo
	end
go

create or alter proc usp_adm_tipousuario_eliminar
(
	@codigo int
)
as
	begin
		delete from Tipo_Usuario
		where idTipoUsuario = @codigo
	end
go

/*  Procedimientos Almacenados de Compra*/


create or alter proc usp_adm_compra_lista
as
	begin
		select

				cc.idCompra,
				c.nomCliente, c.apeCliente,
				cc.fec_ped_comp, cc.fec_ent_comp, p.desProducto,
				cc.est_comp
				from Compra_Cabecera cc inner join Cliente c
				on cc.idCliente = c.idCliente inner join Compra_Detalle cd
				on cd.idCompra = cc.idCompra inner join Producto p
				on cd.idProducto = p.idProducto
	end
go
