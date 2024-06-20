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
	MitgliedId int not null,
	Betrag money not null,
	Datum datetime not null, 
	Beschreibung varchar(50) null,
	Constraint FK_ZahlungMitglied
		foreign key (MitgliedId) References Mitglied(Id)
)
go


INSERT INTO Mitglied (Vorname, Nachname, IsActive) 
VALUES
('John', 'Doe', 1),
('Jane', 'Smith', 1),
('Michael', 'Johnson', 1),
('Emily', 'Jones', 1),
('David', 'Brown', 1),
('Emma', 'Davis', 1),
('Daniel', 'Wilson', 1),
('Olivia', 'Moore', 1),
('Matthew', 'Taylor', 1),
('Sophia', 'Anderson', 1);

go
INSERT INTO Zahlung (Betrag, Datum, Beschreibung, MitgliedId) VALUES
(100.50, '2024-01-15', 'Monthly subscription', 1),
(75.00, '2024-02-01', 'Membership fee', 2),
(50.25, '2024-02-15', 'Book purchase', 3),
(200.00, '2024-03-01', 'Annual membership', 4),
(15.75, '2024-03-05', 'Event ticket', 5),
(125.00, '2024-03-20', 'Donation', 6),
(40.00, '2024-04-01', 'Workshop fee', 7),
(60.50, '2024-04-10', 'Monthly subscription', 8),
(80.75, '2024-05-01', 'Membership fee', 9),
(35.00, '2024-05-15', 'Book purchase', 10),
(45.50, '2024-06-01', 'Event ticket', 1),
(95.25, '2024-06-10', 'Monthly subscription', 2),
(110.75, '2023-06-15', 'Workshop fee', 3),
(205.00, '2023-07-01', 'Annual membership', 4),
(60.50, '2023-07-05', 'Book purchase', 5),
(130.00, '2023-07-20', 'Donation', 6),
(42.00, '2023-08-01', 'Monthly subscription', 7),
(65.50, '2023-08-10', 'Event ticket', 8),
(85.75, '2023-09-01', 'Membership fee', 9),
(37.50, '2023-09-15', 'Book purchase', 10),
(105.00, '2023-10-01', 'Monthly subscription', 1),
(75.75, '2023-10-05', 'Event ticket', 2),
(55.25, '2023-10-10', 'Workshop fee', 3),
(210.00, '2023-11-01', 'Annual membership', 4),
(20.50, '2023-11-15', 'Book purchase', 5),
(135.00, '2023-11-20', 'Donation', 6),
(44.00, '2023-12-01', 'Monthly subscription', 7),
(70.50, '2023-12-10', 'Event ticket', 8),
(90.75, '2023-12-15', 'Membership fee', 9),
(40.00, '2023-12-20', 'Book purchase', 10);

