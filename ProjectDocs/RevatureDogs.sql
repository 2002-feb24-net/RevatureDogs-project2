CREATE SCHEMA RD;
GO

DROP TABLE RDTricksKnown
DROP TABLE RDPetStatus
DROP TABLE RDStatus
DROP TABLE RDTricks
DROP TABLE RDUserPets
DROP TABLE RDUsers
DROP TABLE RDPetsAvailable

CREATE TABLE RD.Users (
	Id int PRIMARY KEY IDENTITY,
	UserName nvarchar,
	FirstName nvarchar,
	LastName nvarchar,
	Email nvarchar,
);
CREATE TABLE RD.PetsAvailable (
	Id int PRIMARY KEY IDENTITY,
	Breed nvarchar NOT NULL,
	Size int
);
CREATE TABLE RD.Pets (
	Id int PRIMARY KEY IDENTITY,
	PetsId int FOREIGN KEY REFERENCES RD.PetsAvailable(Id),
	UserId int FOREIGN KEY REFERENCES RD.Users(Id),
	PetName nvarchar NOT NULL, /* User has to give their pet a name when adopting. */
	Hunger int DEFAULT 100, /* Hunger level ranges from 0 to 100. */
	Mood nvarchar DEFAULT 'Happy',
	AdoptionDate DATETIME DEFAULT GETDATE(),
	Age int DEFAULT 2 /* Age in months */
);
CREATE TABLE RD.Tricks (
	Id int PRIMARY KEY IDENTITY,
	TrickName nvarchar,
	TrickBenefit nvarchar,
);
CREATE TABLE RD.TricksKnown (
	Id int PRIMARY KEY IDENTITY,
	PetId int FOREIGN KEY REFERENCES RD.Pets(Id),
	TrickId int FOREIGN KEY REFERENCES RD.Tricks(Id)
);