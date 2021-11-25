CREATE TABLE [tankkaarten]
(
	[Kaartnummer] INT IDENTITY (1, 1) NOT NULL, 
    [Geldigheidsdatum] DATE NOT NULL, 
    [Pincode] NVARCHAR(50) NULL, 
    [Brandstof] NVARCHAR(50) NULL, 
	PRIMARY KEY CLUSTERED ([Kaartnummer] ASC)
)
