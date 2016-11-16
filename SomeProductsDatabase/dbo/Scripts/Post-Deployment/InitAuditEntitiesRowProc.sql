IF EXISTS ( 
  SELECT *
  FROM sys.objects
  WHERE object_id = OBJECT_ID(N'initAuditEntitiesRow')
      AND type IN ( N'P', N'PC' )) 
DROP PROC [dbo].[initAuditEntitiesRow];
GO
CREATE PROC [dbo].[initAuditEntitiesRow](@id INT, @name nvarchar(50))  
AS
BEGIN  
  UPDATE [dbo].[AuditEntities]
  SET Name = @name
  WHERE Id = @id AND NOT Name = @name;

  IF NOT EXISTS (SELECT * FROM [dbo].[AuditEntities] 
                 WHERE Id = @id AND Name = @name)
  BEGIN
     INSERT INTO [dbo].[AuditEntities] (Id, Name)
     VALUES (@id, @name)
  END
END;  
GO 