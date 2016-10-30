USE [Graph]
GO

/****** Object: Table [dbo].[Node] Script Date: 10/28/2016 4:59:56 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Node];


GO
CREATE TABLE [dbo].[Node] (
    [nid]   INT  IDENTITY (1, 1) NOT NULL,
    [ntext] TEXT NULL,
    [x]     INT  NULL,
    [y]     INT  NULL,
    [gid]   INT  NOT NULL
);


