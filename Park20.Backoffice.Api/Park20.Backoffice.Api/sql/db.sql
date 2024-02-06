CREATE TABLE [dbo].[Park] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [NumberFloors] INT            NOT NULL,
    [ParkName]     NVARCHAR (255) NOT NULL,
    [Latitude]     FLOAT (53)     NOT NULL,
    [Longitude]    FLOAT (53)     NOT NULL,
    [OpeningTime]  DATETIME       NOT NULL,
    [ClosingTime]  DATETIME       NOT NULL,
    [IsActive]     BIT            NOT NULL,
    [NightFee]     FLOAT (53)     NOT NULL,
    [PriceTableId] INT            NOT NULL,
    [Location]     VARCHAR (250)  NULL,
    CONSTRAINT [PK_Park] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[ParkingSpot] (
    [Id] INT IDENTITY (1, 1) NOT NULL,
    [Status] BIT NOT NULL,
    [FloorNumber] INT NOT NULL,
    [VehicleType] NVARCHAR (255) NOT NULL,
    [ParkId] INT NOT NULL,
    CONSTRAINT [PK_ParkingSpot] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ParkId] FOREIGN KEY ([ParkId]) REFERENCES [dbo].[Park] ([Id])
);

CREATE TABLE [dbo].[PriceTable] (
    [Id] INT IDENTITY (1, 1) NOT NULL,
    [InitialDate] DATETIME NOT NULL,
    CONSTRAINT [PK_PriceTable] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[LinePriceTable] (
    [Id] INT IDENTITY (1, 1) NOT NULL,
    [PeriodId] INT NOT NULL,
	[PriceTableId] INT NOT NULL,
    CONSTRAINT [PK_LinePriceTable] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_PriceTableId] FOREIGN KEY ([PriceTableId]) REFERENCES [dbo].[PriceTable] ([Id])
);

CREATE TABLE [dbo].[Period] (
    [Id] INT IDENTITY (1, 1) NOT NULL,
    [InitialTime] TIME NOT NULL,
    [FinalTime] TIME NOT NULL,
    CONSTRAINT [PK_Period] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Fraction] (
    [Id] INT IDENTITY (1, 1) NOT NULL,
    [Order] INT NOT NULL,
    [Minutes] TIME NOT NULL,
    [VehicleType] NVARCHAR (255) NOT NULL,
	[PeriodId] INT NOT NULL,
    [Price]       FLOAT (53)     NULL,
    CONSTRAINT [PK_Fraction] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_PeriodId] FOREIGN KEY ([PeriodId]) REFERENCES [dbo].[Period]([Id])
);


-- Mock data for Park table
INSERT INTO [dbo].[Park] ([NumberFloors], [ParkName], [Latitude], [Longitude], [OpeningTime], [ClosingTime], [IsActive], [NightFee], [PriceTableId])
VALUES (5, 'City Park', 41.1496, -8.6109, '2023-11-14T08:00:00', '2023-11-14T18:00:00', 1, 5.0, 1),
       (3, 'Central Park', 41.1492, -8.6115, '2023-11-14T09:00:00', '2023-11-14T17:00:00', 1, 3.5, 2);

-- Mock data for ParkingSpot table
INSERT INTO [dbo].[ParkingSpot] ([Status], [FloorNumber], [VehicleType], [ParkId])
VALUES 
    (1, 1, '2', 1),
    (0, 2, '0', 1),
    (1, 1, '1', 2),
    (0, 2, '0', 2),
    (1, 3, '1', 2);

-- Mock data for PriceTable table
INSERT INTO [dbo].[PriceTable] ([InitialDate])
VALUES ('2023-11-14T00:00:00'),
       ('2023-11-14T00:00:00');

-- Mock data for LinePriceTable table
INSERT INTO [dbo].[LinePriceTable] ([PeriodId], [PriceTableId])
VALUES (1, 1),
       (2, 1),
       (3, 1),
       (1, 2),
       (2, 2),
       (3, 2);

-- Mock data for Period table
INSERT INTO [dbo].[Period] ([InitialTime], [FinalTime])
VALUES ('08:00:00', '12:00:00'),
       ('12:00:00', '16:00:00'),
       ('16:00:00', '20:00:00'),
       ('09:00:00', '12:00:00'),
       ('12:00:00', '15:00:00'),
       ('15:00:00', '18:00:00');

-- Mock data for Fraction table
INSERT INTO [dbo].[Fraction] ([Order], [Minutes], [VehicleType], [PeriodId])
VALUES (1, '00:15:00', '0', 1),
       (2, '00:30:00', '1', 1),
       (3, '00:45:00', '2', 1),
       (4, '01:00:00', '3', 1),
       (1, '00:20:00', '0', 2),
       (2, '00:40:00', '1', 2),
       (3, '01:00:00', '2', 2),
       (4, '01:20:00', '3', 2),
       (1, '00:10:00', '0', 3),
       (2, '00:20:00', '1', 3),
       (3, '00:30:00', '2', 3),
       (4, '00:40:00', '3', 3),
       (1, '00:15:00', '0', 4),
       (2, '00:30:00', '1', 4),
       (3, '00:45:00', '2', 4),
       (4, '01:00:00', '3', 4),
       (1, '00:10:00', '0', 5),
       (2, '00:20:00', '1', 5),
       (3, '00:30:00', '2', 5),
       (4, '00:40:00', '3', 5),
       (1, '00:15:00', '0', 6),
       (2, '00:30:00', '1', 6),
       (3, '00:45:00', '2', 6);

-- More Table Creations

CREATE TABLE [dbo].[Customer] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Username]         NVARCHAR (100) NOT NULL,
    [Email]            NVARCHAR (100) NOT NULL,
    [Password]         NVARCHAR (100) NOT NULL,
    [Name]             NVARCHAR (100) NOT NULL,
    [Blocked]          BIT            DEFAULT ((0)) NOT NULL,
    [Invitecode]       NVARCHAR (100) NULL,
    [RegistrationDate] DATETIME       NULL,
    [ParkyWalletId]    INT            NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Vehicle] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [LicensePlate] NVARCHAR (20)  NOT NULL,
    [Model]        NVARCHAR (255) NOT NULL,
    [Brand]        NVARCHAR (255) NOT NULL,
    [Type]         NVARCHAR (255) NOT NULL,
    [CustomerId]   INT            NOT NULL,
    CONSTRAINT [PK_Vehicle] PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([LicensePlate] ASC),
    CONSTRAINT [FK_Vehicle_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([Id])
);

CREATE TABLE [dbo].[ParkLog] (
    [Id]        INT      IDENTITY (1, 1) NOT NULL,
    [VehicleId] INT      NOT NULL,
    [ParkId]    INT      NOT NULL,
    [StartTime] DATETIME NULL,
    [EndTime]   DATETIME NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ParkLog_Vehicle] FOREIGN KEY ([VehicleId]) REFERENCES [dbo].[Vehicle] ([Id]),
    CONSTRAINT [FK_ParkLog_Park] FOREIGN KEY ([ParkId]) REFERENCES [dbo].[Park] ([Id])
);

CREATE TABLE [dbo].[Invoice] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [ParkLogId]   INT             NOT NULL,
    [TotalAmount] DECIMAL (10, 2) NOT NULL,
    [IsPayed]     BIT             NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Invoice_ParkLog] FOREIGN KEY ([ParkLogId]) REFERENCES [dbo].[ParkLog] ([Id])
);

CREATE TABLE [dbo].[ParkyCoinsConfiguration] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [ConfigName]  NVARCHAR (40)  NOT NULL,
    [Description] NVARCHAR (150) NOT NULL,
    [Amount]      NVARCHAR (255) NOT NULL,
    CONSTRAINT [PK_ParkyCoinsConfiguration] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[ParkyWallet] (
    [Id]     INT IDENTITY (1, 1) NOT NULL,
    [Amount] INT NOT NULL,
    CONSTRAINT [PK_ParkyWallet] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[ParkyWalletMovements] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [Amount]        INT           NOT NULL,
    [Date]          DATETIME      NOT NULL,
    [MovementType]  VARCHAR (250) NOT NULL,
    [ParkyWalletId] INT           NOT NULL,
    CONSTRAINT [PK_ParkyWalletMovements] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ParkyWalletId] FOREIGN KEY ([ParkyWalletId]) REFERENCES [dbo].[ParkyWallet] ([Id])
);

CREATE TABLE [dbo].[PaymentMethod] (
    [PaymentMethodId]    INT           IDENTITY (1, 1) NOT NULL,
    [CustomerId]         INT           NULL,
    [PaymentToken]       VARCHAR (255) NULL,
    [PaymentMethodType]  VARCHAR (50)  NULL,
    [CardLastFourDigits] INT           NULL,
    [FullName]           VARCHAR (255) NULL,
    [ExpirationDate]     DATE          NULL,
    PRIMARY KEY CLUSTERED ([PaymentMethodId] ASC),
    CONSTRAINT [FK_PaymentMethod_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([Id])
);

-- Mock Data

INSERT INTO [dbo].[ParkyWallet] ([Amount])
VALUES (1000),
       (500),
       (2000),
       (1500),
       (3000);

INSERT INTO [dbo].[Customer] ([Username], [Email], [Password], [Name], [Blocked], [Invitecode], [RegistrationDate], [ParkyWalletId])
VALUES ('User1', 'user1@example.com', 'password1', 'John Doe',  0, '', '2023-01-01T00:00:00',  1),
       ('User2', 'user2@example.com', 'password2', 'Jane Smith',  0, '', '2023-01-02T00:00:00',  2),
       ('User3', 'user3@example.com', 'password3', 'Alice Johnson',  0, '', '2023-01-03T00:00:00',  3),
       ('User4', 'user4@example.com', 'password4', 'Bob Brown',  0, '', '2023-01-04T00:00:00',  4),
       ('User5', 'user5@example.com', 'password5', 'Charlie Davis',  0, '', '2023-01-05T00:00:00',  5);