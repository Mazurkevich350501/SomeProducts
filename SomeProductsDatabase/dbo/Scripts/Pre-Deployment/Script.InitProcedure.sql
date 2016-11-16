/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

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
