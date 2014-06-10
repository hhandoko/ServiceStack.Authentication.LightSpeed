/* ************************************************** *
 * DROP TABLES IF EXISTS
 *
 * ************************************************** */
IF OBJECT_ID('UserAuthRole', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserAuthRole];

IF OBJECT_ID('UserAuthDetails', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserAuthDetails];

IF OBJECT_ID('UserAuth', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserAuth];
GO -- Execute batch

/* ************************************************** *
 * CREATE DATA TABLES
 *
 * ************************************************** */
-- Group: User Auth
CREATE TABLE [dbo].[UserAuth] (
    [Id]                   INT            IDENTITY (1, 1) NOT NULL,
    [UserName]             VARCHAR (8000) NULL,
    [Email]                VARCHAR (8000) NULL,
    [PrimaryEmail]         VARCHAR (8000) NULL,
	[PhoneNumber]          VARCHAR (8000) NULL,
    [FirstName]            VARCHAR (8000) NULL,
    [LastName]             VARCHAR (8000) NULL,
    [DisplayName]          VARCHAR (8000) NULL,
	[Company]              VARCHAR (8000) NULL,
    [BirthDate]            DATETIME       NULL,
    [BirthDateRaw]         VARCHAR (8000) NULL,
	[Address]              VARCHAR (8000) NULL,
	[Address2]             VARCHAR (8000) NULL,
	[City]                 VARCHAR (8000) NULL,
	[State]                VARCHAR (8000) NULL,
    [Country]              VARCHAR (8000) NULL,
    [Culture]              VARCHAR (8000) NULL,
    [FullName]             VARCHAR (8000) NULL,
    [Gender]               VARCHAR (8000) NULL,
    [Language]             VARCHAR (8000) NULL,
    [MailAddress]          VARCHAR (8000) NULL,
    [Nickname]             VARCHAR (8000) NULL,
    [PostalCode]           VARCHAR (8000) NULL,
    [TimeZone]             VARCHAR (8000) NULL,
    [Salt]                 VARCHAR (8000) NULL,
    [PasswordHash]         VARCHAR (8000) NULL,
    [DigestHA1Hash]        VARCHAR (8000) NULL,
    [Roles]                VARCHAR (8000) NULL,
    [Permissions]          VARCHAR (8000) NULL,
    [CreatedDate]          DATETIME       NOT NULL,
    [ModifiedDate]         DATETIME       NOT NULL,
	[InvalidLoginAttempts] INT            NOT NULL,
    [LastLoginAttempt]     DATETIME       NULL,
    [LockedDate]           DATETIME       NULL,
    [RecoveryToken]        VARCHAR (8000) NULL,
    [RefId]                INT            NULL,
    [RefIdStr]             VARCHAR (8000) NULL,
    [Meta]                 VARCHAR (8000) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[UserAuthDetails] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [UserAuthId]         INT            NOT NULL,
    [Provider]           VARCHAR (8000) NULL,
    [UserId]             VARCHAR (8000) NULL,
    [UserName]           VARCHAR (8000) NULL,
    [FullName]           VARCHAR (8000) NULL,
    [DisplayName]        VARCHAR (8000) NULL,
    [FirstName]          VARCHAR (8000) NULL,
    [LastName]           VARCHAR (8000) NULL,
	[Company]            VARCHAR (8000) NULL,
    [Email]              VARCHAR (8000) NULL,
	[PhoneNumber]        VARCHAR (8000) NULL,
    [BirthDate]          DATETIME       NULL,
    [BirthDateRaw]       VARCHAR (8000) NULL,
	[Address]            VARCHAR (8000) NULL,
	[Address2]           VARCHAR (8000) NULL,
	[City]               VARCHAR (8000) NULL,
	[State]              VARCHAR (8000) NULL,
    [Country]            VARCHAR (8000) NULL,
    [Culture]            VARCHAR (8000) NULL,
    [Gender]             VARCHAR (8000) NULL,
    [Language]           VARCHAR (8000) NULL,
    [MailAddress]        VARCHAR (8000) NULL,
    [Nickname]           VARCHAR (8000) NULL,
    [PostalCode]         VARCHAR (8000) NULL,
    [TimeZone]           VARCHAR (8000) NULL,
    [RefreshToken]       VARCHAR (8000) NULL,
    [RefreshTokenExpiry] DATETIME       NULL,
    [RequestToken]       VARCHAR (8000) NULL,
    [RequestTokenSecret] VARCHAR (8000) NULL,
    [Items]              VARCHAR (8000) NULL,
    [AccessToken]        VARCHAR (8000) NULL,
    [AccessTokenSecret]  VARCHAR (8000) NULL,
    [CreatedDate]        DATETIME       NOT NULL,
    [ModifiedDate]       DATETIME       NOT NULL,
    [RefId]              INT            NULL,
    [RefIdStr]           VARCHAR (8000) NULL,
    [Meta]               VARCHAR (8000) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserAuthDetails_UserAuth] FOREIGN KEY ([UserAuthId]) REFERENCES [dbo].[UserAuth]([Id])
);

CREATE TABLE [dbo].[UserAuthRole] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [UserAuthId]   INT            NOT NULL,
    [Role]         VARCHAR (8000) NULL,
	[Permission]   VARCHAR (8000) NULL,
    [CreatedDate]  DATETIME       NOT NULL,
    [ModifiedDate] DATETIME       NOT NULL,
    [RefId]        INT            NULL,
    [RefIdStr]     VARCHAR (8000) NULL,
    [Meta]         VARCHAR (8000) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserAuthRole_UserAuth] FOREIGN KEY ([UserAuthId]) REFERENCES [dbo].[UserAuth]([Id])
);
GO -- Execute batch