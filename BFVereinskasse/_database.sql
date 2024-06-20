use master 
go

ALTER DATABASE BFVereinskasse SET SINGLE_USER WITH ROLLBACK IMMEDIATE 
GO
Drop DATABASE if exists BFVereinskasse
GO
Create Database BFVereinskasse
go

use BFVereinskasse
go

Create Table Mitglied(
	Id int Primary Key Identity not null, 
	Vorname varchar(255) not null,
	Nachname varchar(255) not null,
	IsActive bit not null,
	Bild varchar(255) null
)
Create Table Zahlung(
	Id int Primary Key Identity not null, 
	MemberId int not null,
	Betrag money not null,
	Datum datetime not null, 
	Beschreibung varchar(50) null,
	Constraint FK_ZahlungMitglied
		foreign key (MemberId) References Mitglied(Id)
)
go

insert into Mitglied (Vorname, Nachname, IsActive) 
VALUES (N'Heasd', N'Oida', 1)
go
