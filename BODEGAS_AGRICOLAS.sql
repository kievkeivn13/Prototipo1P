#drop database BODEGAS_AGRICOLAS;
create database BODEGAS_AGRICOLAS;
use BODEGAS_AGRICOLAS;

create table TIPO_USUARIO(
id_tipo_usuario varchar(15) primary key not null,
nombre_tipo_usuario varchar(30) not null

)ENGINE=InnoDB DEFAULT CHARSET =utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

insert into tipo_usuario values ("1", "Admin");

create table USUARIOS(
id_usuario varchar(15) primary key not null,
id_tipo_usuario varchar(15) not null,
nombre_usuario varchar(30) not null,
passwd_usuario varchar(15) not null,
estado_usuario varchar(1) not null,
foreign key (id_tipo_usuario) references TIPO_USUARIO(id_tipo_usuario)
)ENGINE=InnoDB DEFAULT CHARSET =utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

insert into usuarios values ("1", "1", "admin", "12345", "1");

create table PUESTO(
id_puesto varchar(15) primary key not null,
nombre_puesto varchar(15) not null,
status_puesto varchar(1)
)ENGINE=InnoDB DEFAULT CHARSET =utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

insert into puesto values ("1" , "Vendedor", "1");
insert into puesto values ("2" , "Cobrador", "1");

create table CLIENTE(
id_cliente varchar(15) not null,
nit_cliente varchar(10) not null,
nombre_cliente varchar(30) not null,
apellido_cliente varchar(30) not null,
telefono_cliente varchar(15) not null,
direccion_cliente varchar(50) not null,
email_cliente varchar(50) not null,
status_cliente varchar(1) not null,
primary key (id_cliente, nit_cliente)
)ENGINE=InnoDB DEFAULT CHARSET =utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


insert into cliente values ('1', '2089698-7', 'Mario', 'Gonzales', '5963565', 'Ciudad', 'mariog@gmail.com', '1');
insert into cliente values ('2', '49567890-3', 'Daniela', 'Toledo', '45678900', 'Ciudad', 'dtoledo@gmail.com', '1');

create table EMPLEADO(
id_empleado varchar(15) primary key not null,
cui_empleado varchar(30) not null,
nit_empleado varchar(10) not null,
nombre_empleado varchar(30) not null,
apellido_empleado varchar(30) not null,
genero_empleado varchar(1) not null,
edad_empleado int not null,
telefono_empleado varchar(15) not null,
direccion_empleado varchar(50) not null,
email_empleado varchar(50) not null,
status_empleado varchar(1) not null,
id_puesto varchar(15) not null,
foreign key (id_puesto) references PUESTO(id_puesto)
)ENGINE=InnoDB DEFAULT CHARSET =utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

insert into empleado VALUES ('1', '3003888450101', '2017258-3', 'David', 'Romero', 'M', 21, '48125651', 'Ciudad', 'davidr21@gmail.com', '1', '1');

create table TIPO_INVENTARIO(
id_tipo_inventario varchar(15) primary key not null,
nombre_tipo_inventario varchar(30) not null

)ENGINE=InnoDB DEFAULT CHARSET =utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

create table BODEGA(
id_bodega varchar(15) primary key not null,
nombre_bodega varchar(30) not null,
status_bodega varchar(15) not null

)ENGINE=InnoDB DEFAULT CHARSET =utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
create table PROVEEDOR(
id_proveedor varchar(15) not null,
nit_proveedor varchar(10) not null,
nombre_proveedor varchar(30) not null,
telefono_proveedor varchar(15) not null,
direccion_proveedor varchar(50) not null,
email_proveedor varchar(50) not null,
status_proveedor varchar(1) not null,
primary key (id_proveedor, nit_proveedor)
)ENGINE=InnoDB DEFAULT CHARSET =utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
create table INVENTARIO(
id_inventario varchar(15) primary key not null,
nombre_inventario varchar(30) not null,
id_tipo_inventario varchar(15) not null,
id_proveedor varchar(15) not null,
id_bodega varchar(15) not null,
status_inventario varchar(2) not null,

foreign key (id_tipo_inventario) references TIPO_INVENTARIO(id_tipo_inventario),
foreign key (id_bodega) references BODEGA(id_bodega),
foreign key (id_proveedor) references BODEGA(id_proveedor)
)ENGINE=InnoDB DEFAULT CHARSET =utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

create table PRODUCTO(
id_producto varchar(15) primary key not null,
nombre_producto varchar(30) not null,
id_inventario varchar(15) not null,
precio_producto varchar(15) not null,
status_producto varchar(2) not null,
stock_producto int not null,

foreign key (id_inventario) references INVENTARIO(id_inventario)
)ENGINE=InnoDB DEFAULT CHARSET =utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

create table ENCABEZADO_FACTURA(
id_encabezado_factura varchar(15) primary key not null,
id_cliente varchar(15) not null,
id_empleado varchar(15) not null,
fecha_factura date not null,
hora_factura time not null,

foreign key (id_cliente) references CLIENTE(id_cliente),
foreign key (id_empleado) references EMPLEADO(id_empleado)
)ENGINE=InnoDB DEFAULT CHARSET =utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

create table DETALLE_FACTURA(
id_detalle_factura varchar(15) primary key not null,
id_encabezado_factura varchar(15) not null,
id_producto varchar(15) not null,
total_factura float not null,

foreign key (id_encabezado_factura) references ENCABEZADO_FACTURA(id_encabezado_factura),
foreign key (id_producto) references PRODUCTO(id_producto)
)ENGINE=InnoDB DEFAULT CHARSET =utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

create table ENCABEZADO_COTIZACION(
id_encabezado_cotizacion varchar(15) primary key not null,
id_cliente varchar(15) not null,
id_empleado varchar(15) not null,
fecha_cotizacion date not null,
hora_cotizacion time not null,

foreign key (id_cliente) references CLIENTE(id_cliente),
foreign key (id_empleado) references EMPLEADO(id_empleado)
)ENGINE=InnoDB DEFAULT CHARSET =utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

#eliminar carrito y poner solo detalle cotizacion

create table DETALLE_COTIZACION(
id_detalle_cotizacion varchar(15) primary key not null,
id_encabezado_cotizacion varchar(15) not null,
id_producto varchar(15) null,
total_cotizacion float not null,

foreign key (id_encabezado_cotizacion) references ENCABEZADO_COTIZACION(id_encabezado_cotizacion),
foreign key (id_producto) references PRODUCTO(id_producto)
)ENGINE=InnoDB DEFAULT CHARSET =utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

create table CAJA(
id_caja varchar(15) primary key not null,
nombre_caja varchar(30) not null,
saldo_caja float not null,
id_empleado varchar(30) not null,

foreign key (id_empleado) references EMPLEADO(id_empleado)

)ENGINE=InnoDB DEFAULT CHARSET =utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

create table FORMAS_PAGO(
id_forma_pago varchar(15) primary key not null,
nombre_forma_pago varchar(30) not null,
status_forma_pago varchar(1) not null
)ENGINE=InnoDB DEFAULT CHARSET =utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

create table CREDITO(
id_credito varchar(15) primary key not null,
id_cliente varchar(15) not null,
id_forma_pago varchar(15) not null,
id_factura varchar(15) not null,
plazo_credito int not null,

foreign key (id_cliente) references CLIENTE(id_cliente),
foreign key (id_forma_pago) references FORMAS_PAGO(id_forma_pago),
foreign key (id_factura) references ENCABEZADO_FACTURA(id_encabezado_factura)
)ENGINE=InnoDB DEFAULT CHARSET =utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

create table COMISIONES(
id_comision varchar(15) primary key not null,
id_empleado varchar(15) not null,
porcentaje int not null,

foreign key (id_empleado) references EMPLEADO(id_empleado)
)ENGINE=InnoDB DEFAULT CHARSET =utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
