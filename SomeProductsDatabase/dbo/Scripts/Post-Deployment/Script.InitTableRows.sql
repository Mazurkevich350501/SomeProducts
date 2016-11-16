/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

EXEC [dbo].[initAuditStatusRow] 1, 'Edit';
EXEC [dbo].[initAuditStatusRow] 2, 'Create';
EXEC [dbo].[initAuditStatusRow] 3, 'Delete';

EXEC [dbo].[initAuditEntitiesRow] 1, 'Brand';
EXEC [dbo].[initAuditEntitiesRow] 2, 'Product';
EXEC [dbo].[initAuditEntitiesRow] 3, 'User';

EXEC [dbo].[initActiveStatesRow] 1, 'Active';
EXEC [dbo].[initActiveStatesRow] 2, 'Disable';

EXEC [dbo].[initEmptyCompany] 1, 'Empty';

EXEC [dbo].[initRolesRow] 1, 'User';
EXEC [dbo].[initRolesRow] 2, 'Admin';
EXEC [dbo].[initRolesRow] 3, 'SuperAdmin';
GO