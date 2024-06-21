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


INSERT INTO Mitglied (Bild,Vorname, Nachname, IsActive) 
VALUES
('playerholderm.png','John', 'Doe', 1),
('playerholderf.png','Jane', 'Smith', 1),
('playerholderm.png','Michael', 'Johnson', 1),
('playerholderf.png','Emily', 'Jones', 1),
('playerholderm.png','David', 'Brown', 1),
('playerholderf.png','Emma', 'Davis', 1),
('playerholderm.png','Daniel', 'Wilson', 1),
('playerholderf.png','Olivia', 'Moore', 1),
('playerholderm.png','Matthew', 'Taylor', 1),
('playerholderf.png','Sophia', 'Anderson', 1);

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
(110.75, '2024-06-15', 'Workshop fee', 3),
(205.00, '2024-07-01', 'Annual membership', 4),
(60.50, '2024-07-05', 'Book purchase', 5),
(130.00, '2024-07-20', 'Donation', 6),
(42.00, '2024-08-01', 'Monthly subscription', 7),
(65.50, '2024-08-10', 'Event ticket', 8),
(85.75, '2024-09-01', 'Membership fee', 9),
(37.50, '2024-09-15', 'Book purchase', 10),
(105.00, '2024-10-01', 'Monthly subscription', 1),
(75.75, '2024-10-05', 'Event ticket', 2),
(55.25, '2024-10-10', 'Workshop fee', 3),
(210.00, '2024-11-01', 'Annual membership', 4),
(20.50, '2024-11-15', 'Book purchase', 5),
(135.00, '2024-11-20', 'Donation', 6),
(44.00, '2024-12-01', 'Monthly subscription', 7),
(70.50, '2024-12-10', 'Event ticket', 8),
(90.75, '2024-12-15', 'Membership fee', 9),
(40.00, '2024-12-20', 'Book purchase', 10),
(-30.00, '2024-01-10', 'Refund', 1),
(-45.25, '2024-02-20', 'Refund', 2),
(100.00, '2024-03-10', 'Monthly subscription', 3),
(75.50, '2024-04-15', 'Membership fee', 4),
(200.00, '2024-05-05', 'Annual membership', 5),
(-15.75, '2024-06-15', 'Refund', 6),
(120.00, '2024-07-20', 'Donation', 7),
(50.50, '2024-08-01', 'Workshop fee', 8),
(60.75, '2024-09-10', 'Event ticket', 9),
(85.00, '2024-10-05', 'Book purchase', 10),
(-50.00, '2024-11-20', 'Refund', 1),
(110.50, '2024-12-01', 'Monthly subscription', 2),
(100.75, '2024-12-10', 'Membership fee', 3),
(95.00, '2024-12-15', 'Annual membership', 4),
(-25.75, '2024-12-20', 'Refund', 5),
(-130.00, '2024-12-25', 'Donation', 6),
(42.50, '2024-12-30', 'Workshop fee', 7),
(65.75, '2024-12-31', 'Event ticket', 8),
(50.00, '2023-01-15', 'Monthly subscription', 1),
(60.25, '2023-02-01', 'Membership fee', 2),
(70.50, '2023-02-15', 'Equipment purchase', 3),
(80.75, '2023-03-01', 'Annual membership', 4),
(90.00, '2023-03-05', 'Event ticket', 5),
(100.25, '2023-03-20', 'Charity donation', 6),
(110.50, '2023-04-01', 'Workshop fee', 7),
(120.75, '2023-04-10', 'Monthly subscription', 8),
(130.00, '2023-05-01', 'Membership fee', 9),
(140.25, '2023-05-15', 'Book purchase', 10),
(150.50, '2023-06-01', 'Event ticket', 1),
(160.75, '2023-06-10', 'Monthly subscription', 2),
(170.00, '2023-06-15', 'Workshop fee', 3),
(180.25, '2023-07-01', 'Annual membership', 4),
(190.50, '2023-07-05', 'Equipment purchase', 5),
(200.75, '2023-07-20', 'Charity donation', 6),
(210.00, '2023-08-01', 'Monthly subscription', 7),
(220.25, '2023-08-10', 'Event ticket', 8),
(230.50, '2023-09-01', 'Membership fee', 9),
(240.75, '2023-09-15', 'Book purchase', 10),
(250.00, '2023-10-01', 'Monthly subscription', 1),
(260.25, '2023-10-05', 'Event ticket', 2),
(270.50, '2023-10-10', 'Workshop fee', 3),
(280.75, '2023-11-01', 'Annual membership', 4),
(290.00, '2023-11-15', 'Book purchase', 5),
(-300.25, '2023-11-20', 'Charity donation', 6),
(310.50, '2023-12-01', 'Monthly subscription', 7),
(320.75, '2023-12-10', 'Event ticket', 8),
(330.00, '2023-12-15', 'Membership fee', 9),
(340.25, '2023-12-20', 'Book purchase', 10),
(-50.00, '2023-01-10', 'Refund', 1),
(-60.25, '2023-02-20', 'Refund', 2),
(70.00, '2023-03-10', 'Monthly subscription', 3),
(80.50, '2023-04-15', 'Membership fee', 4),
(90.75, '2023-05-05', 'Annual membership', 5),
(-100.00, '2023-06-15', 'Refund', 6),
(110.25, '2023-07-20', 'Charity donation', 7),
(120.50, '2023-08-01', 'Workshop fee', 8),
(130.75, '2023-09-10', 'Event ticket', 9),
(140.00, '2023-10-05', 'Book purchase', 10),
(-150.25, '2023-11-20', 'Refund', 1),
(160.50, '2023-12-01', 'Monthly subscription', 2),
(170.75, '2023-12-10', 'Membership fee', 3),
(180.00, '2023-12-15', 'Annual membership', 4),
(-190.25, '2023-12-20', 'Refund', 5),
(200.50, '2023-12-25', 'Charity donation', 6),
(210.75, '2023-12-30', 'Workshop fee', 7),
(220.00, '2023-12-31', 'Event ticket', 8);

update Zahlung set Datum = DATEADD(year,-1,Datum) where Datum > GETDATE();

update Mitglied set IsActive = 0 where Id % 5 = 0;
