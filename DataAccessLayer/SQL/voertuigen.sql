CREATE TABLE [voertuigen]
(
	[Chassisnummer] NVARCHAR(50) NOT NULL, 
    [Merk] NVARCHAR(50) NOT NULL, 
    [Model] NVARCHAR(50) NOT NULL, 
    [Nummerplaat] NVARCHAR(15) NOT NULL, 
    [Brandstof] NVARCHAR(50) NOT NULL, 
    [Typewagen] NVARCHAR(50) NOT NULL, 
    [Kleur] NVARCHAR(50) NULL, 
    [AantalDeuren] INT NULL, 
	PRIMARY KEY CLUSTERED ([Chassisnummer] ASC)
)