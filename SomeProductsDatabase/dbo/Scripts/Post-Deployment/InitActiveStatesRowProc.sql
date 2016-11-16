IF EXISTS ( 
  SELECT *
  FROM sys.objects
  WHERE object_id = OBJECT_ID(N'initActiveStatesRow')
      AND type IN ( N'P', N'PC' )) 
DROP PROC [dbo].[initActiveStatesRow];
GO
CREATE PROC [dbo].[initActiveStatesRow](@id INT, @value nvarchar(10))  
AS
BEGIN  
  UPDATE [dbo].[ActiveStates]
  SET Value = @value
  WHERE Id = @id AND NOT Value = @value;

  IF NOT EXISTS (SELECT * FROM [dbo].[ActiveStates] 
                 WHERE Id = @id AND Value = @value)
  BEGIN
     INSERT INTO [dbo].[ActiveStates] (Id, Value)
     VALUES (@id, @value)
  END
END;  
GO