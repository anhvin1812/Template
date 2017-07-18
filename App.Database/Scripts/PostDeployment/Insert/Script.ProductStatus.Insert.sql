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
IF NOT EXISTS(SELECT 1 FROM dbo.ProductStatus WHERE Status = N'Còn hàng')
BEGIN
	Insert into ProductStatus values(N'Còn hàng')
END

IF NOT EXISTS(SELECT 1 FROM dbo.ProductStatus WHERE Status = N'Hết hàng')
BEGIN
	Insert into ProductStatus values(N'Hết hàng')
END

/* NewsStatus */
IF NOT EXISTS(SELECT 1 FROM dbo.NewsStatus WHERE Status = N'Private')
BEGIN
	Insert into NewsStatus values(N'Private')
END

IF NOT EXISTS(SELECT 1 FROM dbo.NewsStatus WHERE Status = N'Public')
BEGIN
	Insert into NewsStatus values(N'Public')
END

IF NOT EXISTS(SELECT 1 FROM dbo.NewsStatus WHERE Status = N'Draft')
BEGIN
	Insert into NewsStatus values(N'Draft')
END

IF NOT EXISTS(SELECT 1 FROM dbo.NewsStatus WHERE Status = N'Pending Validation')
BEGIN
	Insert into NewsStatus values(N'Pending Validation')
END
