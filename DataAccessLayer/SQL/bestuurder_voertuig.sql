CREATE TABLE [bestuurder_voertuig]
(
	[BestuurderId] INT IDENTITY NOT NULL,
	[Chassisnummer] NVARCHAR(50) NOT NULL,
	PRIMARY KEY CLUSTERED ([BestuurderId] ASC, [Chassisnummer] ASC),
	CONSTRAINT [FK_bestuurder_voertuig_bestuurders] FOREIGN KEY ([BestuurderId]) REFERENCES [dbo].[Bestuurders]([Id]),
	CONSTRAINT [FK_bestuurder_voertuig_voertuigen] FOREIGN KEY ([Chassisnummer]) REFERENCES [dbo].[Voertuigen]([Chassisnummer])
)
