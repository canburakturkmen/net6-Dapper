CREATE PROCEDURE [dbo].[spPerson_InsertSet]
 @peopleBasicUDT readonly
 AS
 BEGIN
	INSERT INTO dbo.People(FirstName,LastName)
	SELECT [FirstName], [LastName]
	From @people;
 END