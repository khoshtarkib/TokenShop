USE [TokenShop]
GO

/****** Object:  StoredProcedure [dbo].[proDeleteOneProcedure]    Script Date: 11/27/2018 1:02:09 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create procedure [dbo].[proDeleteOneProcedure]
 @spname sysname

AS

BEGIN

  EXEC('DROP PROCEDURE ' + @spname);
END
GO


