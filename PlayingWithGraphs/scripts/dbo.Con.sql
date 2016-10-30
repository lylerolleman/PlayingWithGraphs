USE [Graph]
GO

/****** Object: Table [dbo].[Con] Script Date: 10/28/2016 4:57:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Con];


GO
CREATE TABLE [dbo].[Con] (
    [cid] INT IDENTITY (1, 1) NOT NULL,
    [n1]  INT NULL,
    [n2]  INT NULL
);


