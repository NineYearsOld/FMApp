CREATE TABLE [bestuurders]
(
	[Id] INT NOT NULL IDENTITY (1,1), 
    [Naam] NVARCHAR(50) NOT NULL, 
    [Voornaam] NVARCHAR(50) NOT NULL, 
    [Postcode] INT NULL, 
    [Gemeente] NVARCHAR(50) NULL, 
    [Straat] NVARCHAR(100) NULL, 
    [Huisnummer] NVARCHAR(50) NULL, 
    [Geboortedatum] DATE NOT NULL, 
    [Rijksregisternummer] NVARCHAR(50) NOT NULL, 
    [Rijbewijs] NVARCHAR(50) NOT NULL,
	PRIMARY KEY CLUSTERED ([Id] ASC)
)
