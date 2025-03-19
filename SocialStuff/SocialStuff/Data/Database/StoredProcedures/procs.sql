﻿CREATE PROCEDURE GetUsers
AS
BEGIN
    SELECT * FROM Users;
END;
GO

CREATE PROCEDURE GetUser
    @UserID INT
AS
BEGIN
    SELECT * FROM Users WHERE ID = @UserID;
END;
GO

CREATE PROCEDURE AddUser
    @Username NVARCHAR(100),
    @Email NVARCHAR(100)
AS
BEGIN
    INSERT INTO Users (Username, Email)
    VALUES (@Username, @Email);
END;
GO