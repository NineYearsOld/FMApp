CREATE TABLE [bestuurder_tankkaart]
(
	[Kaartnummer] INT NOT NULL, 
    [BestuurderId] INT NOT NULL, 
	PRIMARY KEY CLUSTERED ([Kaartnummer] ASC, [BestuurderId] ASC),
    CONSTRAINT [FK_bestuurder_tankkaart_tankkaarten] FOREIGN KEY ([Kaartnummer]) REFERENCES [dbo].[Tankkaarten] ([Kaartnummer]),
	CONSTRAINT [FK_bestuurder_tankkaart_bestuurders] FOREIGN KEY ([BestuurderId]) REFERENCES [dbo].[Bestuurders] ([Id])
)
