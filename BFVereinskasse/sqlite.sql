BEGIN TRANSACTION;
DROP TABLE IF EXISTS "Zahlung";
CREATE TABLE IF NOT EXISTS "Zahlung" (
	"Id"	INTEGER NOT NULL,
	"MitgliedId"	int NOT NULL,
	"Betrag"	money NOT NULL,
	"Datum"	datetime NOT NULL,
	"Beschreibung"	varchar(50),
	PRIMARY KEY("Id" AUTOINCREMENT),
	CONSTRAINT "FK_ZahlungMitglied" FOREIGN KEY("MitgliedId") REFERENCES "Mitglied"("Id")
);
DROP TABLE IF EXISTS "Mitglied";
CREATE TABLE IF NOT EXISTS "Mitglied" (
	"Id"	INTEGER NOT NULL,
	"Vorname"	varchar(255) NOT NULL,
	"Nachname"	varchar(255) NOT NULL,
	"IsActive"	bit NOT NULL,
	"Bild"	varchar(255),
	PRIMARY KEY("Id" AUTOINCREMENT)
);
COMMIT;
