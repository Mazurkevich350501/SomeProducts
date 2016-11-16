IF EXISTS ( 
  SELECT *
  FROM sys.objects
  WHERE object_id = OBJECT_ID(N'initRolesRow')
      AND type IN ( N'P', N'PC' )) 
DROP PROC [dbo].[initRolesRow];
GO
CREATE PROC [dbo].[initRolesRow](@id INT, @name nvarchar(50))  
AS
BEGIN  
  UPDATE [dbo].[Roles]
  SET Name = @name
  WHERE Id = @id AND NOT Name = @name;
  
  SET IDENTITY_INSERT [dbo].[Roles] ON
  IF NOT EXISTS (SELECT * FROM [dbo].[Roles] 
                 WHERE Id = @id AND Name = @name)
  BEGIN
     INSERT INTO [dbo].[Roles] (Id, Name)
     VALUES (@id, @name)
  END
  SET IDENTITY_INSERT [dbo].[Roles] OFF
END;  
GO 