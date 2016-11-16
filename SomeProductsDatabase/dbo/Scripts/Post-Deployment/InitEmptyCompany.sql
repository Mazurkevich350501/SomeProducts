IF EXISTS ( 
  SELECT *
  FROM sys.objects
  WHERE object_id = OBJECT_ID(N'initEmptyCompany')
      AND type IN ( N'P', N'PC' )) 
DROP PROC [dbo].[initEmptyCompany];
GO
CREATE PROC [dbo].[initEmptyCompany](@id INT, @value nvarchar(50))  
AS
BEGIN  
  UPDATE [dbo].[Companies]
  SET CompanyName = @value, ActiveStateId = 1
  WHERE Id = @id AND NOT (CompanyName = @value AND ActiveStateId = 1);
  
  SET IDENTITY_INSERT [dbo].[Companies] ON
  IF NOT EXISTS (SELECT * FROM [dbo].[Companies] 
                 WHERE Id = @id AND CompanyName = @value)
  BEGIN
     INSERT INTO [dbo].[Companies] (Id, CompanyName, ActiveStateId)
     VALUES (@id, @value, DEFAULT)
  END
  SET IDENTITY_INSERT [dbo].[Companies] OFF
END;  
GO 