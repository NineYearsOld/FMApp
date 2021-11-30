CREATE TABLE [Tankkaarten]
(
	[Kaartnummer] INT NOT NULL IDENTITY(1,1), 
    [Geldigheidsdatum] DATE NOT NULL, 
    [Pincode] NVARCHAR(50) NULL, 
    [Brandstof] NVARCHAR(50) NULL, 
	[BestuurderId] INT NOT NULL FOREIGN KEY REFERENCES Bestuurders(Id),
	PRIMARY KEY CLUSTERED ([Kaartnummer] ASC),
)