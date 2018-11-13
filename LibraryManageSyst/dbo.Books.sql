CREATE TABLE [dbo].[Books] (
    [accNo]     INT          NOT NULL,
    [isbn]      VARCHAR (10) NULL,
    [Name]      VARCHAR (30) NULL,
    [Author]    VARCHAR (30) NULL,
    [Publisher] VARCHAR (30) NULL,
    [dId]       INT          NULL,
    PRIMARY KEY CLUSTERED ([accNo] ASC),
    CONSTRAINT [FK_Books_ToTable] FOREIGN KEY (dId) REFERENCES [dbo].Department(dep_Id)
);

