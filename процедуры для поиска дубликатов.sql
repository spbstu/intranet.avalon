
--DROP PROCEDURE Duplicates_BuildTable;
CREATE PROC Duplicates_BuildTable
  AS
create table Duplicates (FirstName nvarchar(20),LastName nvarchar(20), MiddleName nvarchar(20), DublCount int, checked bit);
Insert INTO Duplicates (FirstName,LastName,MiddleName,DublCount) select FirstName, LastName, MiddleName,count(*) from APersons group by FirstName, LastName,MiddleName having count(*)>1 ORDER BY count(*)DESC;

  

--DROP PROCEDURE Duplicates_DeleteTable;
CREATE PROC Duplicates_DeleteTable
  AS
  drop table Duplicates;
  
  
 
 -- DROP PROCEDURE Duplicates_GetDuplicate;
  CREATE PROC Duplicates_GetDuplicate
  AS
	DECLARE @f nvarchar(20);DECLARE @l nvarchar(20); DECLARE @m nvarchar(20);
	EXEC Duplicates_GetFIO @f OUTPUT,@l OUTPUT,@m OUTPUT
	UPDATE Duplicates SET checked = 1 WHERE FirstName Like @f AND LastName LIKE @l AND MiddleName LIKE @m;
	Select * from APersons WHERE FirstName LIKE @f AND LastName LIKE @l AND MiddleName LIKE @m;
  
  
  
  
  

 -- DROP PROCEDURE Duplicates_GetFIO;
  CREATE PROCEDURE Duplicates_GetFIO
  @fname nvarchar(20) OUTPUT,
  @lname nvarchar(20) OUTPUT,
  @mname nvarchar(20) OUTPUT
  AS
  SELECT TOP 1 @fname = FirstName, @lname= LastName, @mname=MiddleName from Duplicates WHERE checked IS NULL
    
  
  
  
--------------------------------------
--процедура замены--
--------------------------------------
--DROP PROCEDURE UpdatePersonIdInOtherTables 
 CREATE PROCEDURE UpdatePersonIdInOtherTables 
  @newID INT,
  @oldID int
  AS
  UPDATE ExamAssignments set PersonID = @newID where PersonID = @oldID;
  UPDATE Certificates set PersonID = @newID where PersonID = @oldID;
  UPDATE ComplexEduPrograms set PersonID = @newID where PersonID = @oldID;
  UPDATE GroupRegistrations set PersonID = @newID where PersonID = @oldID;
  UPDATE PersonsToExternalPersons set PersonID = @newID where PersonID = @oldID;
  UPDATE PersonContacts set PersonID = @newID where PersonID = @oldID;
  UPDATE GradeBook set StudentID = @newID where StudentID = @oldID;


 

 