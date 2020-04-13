CREATE SCHEMA RD;
GO
CREATE TABLE RD.Users (
	Id int PRIMARY KEY IDENTITY,
	UserName nvarchar,
	FirstName nvarchar,
	LastName nvarchar,
	Score int DEFAULT 0
);
CREATE TABLE RD.DogTypes (
	Id int PRIMARY KEY IDENTITY,
	Breed nvarchar NOT NULL,
	LifeExpectancy int NOT NULL, /* Life expectancy of a certain breed should be given in months. */
	Size int /* Should be 0, 1, or 2. (Small, Medium, Large) */
);
CREATE TABLE RD.Dogs (
	Id int PRIMARY KEY IDENTITY,
	DogTypeId int FOREIGN KEY REFERENCES RD.DogTypes(Id),
	UserId int FOREIGN KEY REFERENCES RD.Users(Id),
	PetName nvarchar NOT NULL, /* User has to give their pet a name when adopting. */
	Hunger int DEFAULT 100, /* Hunger level ranges from 0 to 100. */
	Mood nvarchar DEFAULT 'Happy',
	IsAlive Bit DEFAULT 1, /* 1 to denote that the dog's alive, 0 to denote that it's dead. */
	AdoptionDate DATETIME DEFAULT GETDATE(),
	Age int DEFAULT 2, /* Age in months. Should die when reaches life expectancy. */
	Energy int DEFAULT 3 /* Denotes how many times a dog can practice a trick every day. */
);
/* Possible tricks include: speak, shake, sit, lie down, roll over */
CREATE TABLE RD.Tricks (
	Id int PRIMARY KEY IDENTITY,
	TrickName nvarchar,
	TrickBenefit nvarchar,
	Diffculty int, /* Denotes how many times a trick has to be practiced to be learned. */
	Points int /* Points are added to the user's score when the trick is taught. */
);
CREATE TABLE RD.TricksProgress (
	Id int PRIMARY KEY IDENTITY,
	PetId int FOREIGN KEY REFERENCES RD.Dogs(Id),
	TrickId int FOREIGN KEY REFERENCES RD.Tricks(Id),
	Progress int DEFAULT 0 /* Increments whenever you practice a trick with your dog. */
);