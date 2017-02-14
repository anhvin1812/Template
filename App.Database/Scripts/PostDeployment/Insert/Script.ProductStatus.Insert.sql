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
IF NOT EXISTS(SELECT * FROM dbo.ProductStatus WHERE Status = 'Còn hàng')
BEGIN
	Insert into ProductStatus values(N'Còn hàng')
END

IF NOT EXISTS(SELECT * FROM dbo.ProductStatus WHERE Status = 'Hết hàng')
BEGIN
	Insert into ProductStatus values(N'Hết hàng')
END
