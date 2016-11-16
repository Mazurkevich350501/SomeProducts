IF EXISTS ( 
  SELECT *
  FROM sys.objects
  WHERE object_id = OBJECT_ID(N'initAuditStatusRow')
      AND type IN ( N'P', N'PC' )) 
DROP PROC [dbo].[initAuditStatusRow];
GO
CREATE PROC [dbo].[initAuditStatusRow](@id INT, @value nvarchar(50))  
AS
BEGIN  
  UPDATE [dbo].[AuditStatus]
  SET [dbo].[AuditStatus].Value = @value
  WHERE Id = @id AND NOT Value = @value;

  IF NOT EXISTS (SELECT * FROM [dbo].[AuditStatus] 
                 WHERE Id = @id AND Value = @value)
  BEGIN
     INSERT INTO [dbo].[AuditStatus] (Id, Value)
     VALUES (@id, @value)
  END
END;  
GO 
