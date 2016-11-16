/*
Post-Deployment Script Template				
*/
:r .\InitActiveStatesRowProc.sql
:r .\InitAuditEntitiesRowProc.sql
:r .\InitAuditStatusRowProc.sql
:r .\InitEmptyCompany.sql
:r .\InitRolesRowProc.sql


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