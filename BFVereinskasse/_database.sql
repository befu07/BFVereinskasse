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

Create Table Vereinsmitglied(
	Id int Identity not null, 
	Vorname varchar(255),
	Nachname varchar(255),
	IsActive bit,
	Bild image
)
Create Table Zahlungen(
	Id int Identity not null, 
	VereinsmitgliedId int not null,
	Betrag money not null,
	Datum datetime not null, 
	Beschreibung varchar(50),
)