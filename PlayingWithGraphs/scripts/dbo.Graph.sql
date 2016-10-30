USE [Graph]
GO

/****** Object: Table [dbo].[Graph] Script Date: 10/28/2016 4:59:43 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Graph];


GO
CREATE TABLE [dbo].[Graph] (
    [gid]        INT  IDENTITY (1, 1) NOT NULL,
    [graph_name] TEXT NULL
);


