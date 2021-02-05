create table MeGusta
(
	Idmegusta bigint not null primary key identity(1,1),
	Idpoema bigint not null foreign key references Poema,
	Ipcliente varchar(20)
);