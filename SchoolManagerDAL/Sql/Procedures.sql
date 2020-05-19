IF OBJECT_ID('ValidatePaginator', 'P') IS NOT NULL 
  DROP PROC ValidatePaginator; 
GO 

CREATE PROCEDURE ValidatePaginator
	@rowCount INT,
	@pageNumber INT OUT,
	@pageSize INT OUT,
	@pageCount INT OUT
AS
BEGIN
	IF @rowCount <= 0
	BEGIN
		SET @pageNumber = 1;
		SET @pageSize = 10;
		SET @pageCount = 1;
		RETURN
	END

	IF @pageNumber < 1
		SET @pageNumber = 1;

	IF @pageSize < 10
		SET @pageSize = 10;

	SET @pageCount = @rowCount / @pageSize;

	IF (@rowCount % @pageSize) > 0
		SET @pageCount = @pageCount + 1;

	IF @pageNumber > @pageCount
		SET @pageNumber = @pageCount;
END
GO

-- GetStudents
IF OBJECT_ID('GetStudents', 'P') IS NOT NULL 
  DROP PROC GetStudents; 
GO 

CREATE PROCEDURE GetStudents
	@pageNumber INT = 1,
	@pageSize INT = 10
AS 
BEGIN

	DECLARE @pageCount INT;
	DECLARE @rowCount INT;
	SET @rowCount = (SELECT COUNT(1) FROM  [dbo].School sch 
	INNER JOIN [dbo].[Class] c ON sch.Id = c.SchoolId
	INNER JOIN [dbo].[Student] s ON c.Id = s.ClassId);
	EXEC ValidatePaginator @rowCount, @pageNumber OUT, @pageSize OUT, @pageCount OUT;

	SELECT @pageCount as pageCount, @pageSize as pageSize, @pageNumber as pageNumber;

	SELECT  s.Id, s.LastName, s.FirstName, s.Patronymic, s.Phone, c.Name as Class, sch.Number as School, c.Id as ClassId, sch.Id as SchoolId 
	FROM  [dbo].School sch 
	INNER JOIN [dbo].[Class] c ON sch.Id = c.SchoolId
	INNER JOIN [dbo].[Student] s ON c.Id = s.ClassId
	ORDER BY School, Class, s.LastName
	OFFSET (@pageNumber - 1) * @pageSize ROWS 
	FETCH NEXT @pageSize ROWS ONLY;
END;
GO

-- EditStudent
IF OBJECT_ID('EditStudent', 'P') IS NOT NULL 
  DROP PROC EditStudent; 
GO 

CREATE PROCEDURE EditStudent
	@id INT,
	@lastName nchar(50),
	@firstName nchar(50),
	@patronymic nchar(50),
	@phone char(10),
	@classId INT
AS 
BEGIN
	UPDATE Student 
		SET FirstName = @firstName,
			LastName = @lastName,
			Patronymic = @patronymic,
			Phone = @phone,
			ClassId = @classId
	WHERE Id = @id
END
GO

IF OBJECT_ID('GetSchoolIdNumberByNumber', 'P') IS NOT NULL
  DROP PROC GetSchoolIdNumberByNumber; 
GO 

CREATE PROCEDURE dbo.GetSchoolIdNumberByNumber 
    @number NVARCHAR(10),
    @count INT = 10
AS
BEGIN
    SELECT TOP(@count) Id, [Number] FROM School 
	WHERE [Number] LIKE @number + '%';
END
GO

IF OBJECT_ID('GetClassIdNameByNameSchoolId', 'P') IS NOT NULL
  DROP PROC GetClassIdNameByNameSchoolId; 
GO 

CREATE PROCEDURE dbo.GetClassIdNameByNameSchoolId
	@schoolId INT,
    @name NVARCHAR(5),
    @count INT = 10
AS
BEGIN
    SELECT TOP(@count) Id, [Name] FROM Class 
	WHERE SchoolId = @schoolId AND
	[Name] LIKE '%' + @name + '%';
END
GO

IF OBJECT_ID('GetStudent', 'P') IS NOT NULL
  DROP PROC GetStudent; 
GO 

CREATE PROCEDURE dbo.GetStudent 
    @id int
AS
BEGIN
	SELECT  s.Id, s.LastName, s.FirstName, s.Patronymic, s.Phone, c.Name as Class, sch.Number as School, c.Id as ClassId, sch.Id as SchoolId 
	FROM  [dbo].School sch 
	INNER JOIN [dbo].[Class] c ON sch.Id = c.SchoolId
	INNER JOIN [dbo].[Student] s ON c.Id = s.ClassId
	WHERE s.Id = @id
END
GO

-- CreateStudent
IF OBJECT_ID('CreateStudent', 'P') IS NOT NULL 
  DROP PROC CreateStudent; 
GO 

CREATE PROCEDURE CreateStudent
	@lastName nchar(50),
	@firstName nchar(50),
	@patronymic nchar(50),
	@phone char(10),
	@classId INT
AS 
BEGIN
	INSERT INTO Student(LastName, FirstName, Patronymic, Phone, ClassId)
	VALUES(@lastName, @firstName, @patronymic, @phone, @classId);
	SELECT SCOPE_IDENTITY();
END
GO

-- DeleteStudent
IF OBJECT_ID('DeleteStudent', 'P') IS NOT NULL 
  DROP PROC DeleteStudent; 
GO 
CREATE PROCEDURE DeleteStudent
	@id INT
AS 
BEGIN
	DELETE FROM Student WHERE Id = @id;
END
GO