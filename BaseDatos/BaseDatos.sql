use master
go
create database PoetaMuerto
go
use PoetaMuerto
go
create table Poema
(
	Idpoema bigint not null identity(1,1),
	Titulo varchar(500) not null,
	Verso varchar(max) not null,
	Imagen varchar(max) null,
	Bhabilitado varchar(1) not null,
	primary key(Idpoema) 
)
go
create table MeGusta
(
	Idmegusta bigint not null identity(1,1),
	Idpoema bigint not null,
	Ipcliente varchar(10) not null,
	primary key(Idmegusta),
	foreign key(Idpoema) references Poema
)
go
create procedure ListarPoemas
as
begin
	select * from Poema where Bhabilitado='A';
end
go
create procedure GuardarMeGusta
@Idpoema bigint,
@Ipcliente varchar(10)
as
begin
	insert into MeGusta(Idpoema, Ipcliente) values(@Idpoema,@Ipcliente)
end
go
create procedure ActualizarPoema
@Idpoema bigint,
@Titulo varchar(500),
@Verso varchar(max),
@Imagen varchar(max)
as
begin
	update Poema set Titulo=@Titulo, Verso=@Verso, Imagen=@Imagen where Idpoema=@Idpoema;
end
go

create proc FiltrarPoemas
@Titulo varchar(500)
as
begin
select * from Poema where Titulo like('%'+@Titulo+'%')
end