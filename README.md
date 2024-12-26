-- Таблица пользователей
--CREATE TABLE Players (
--    Id INT IDENTITY(1,1) PRIMARY KEY,
--    FullName NVARCHAR(100),
--    BirthDate DATE,
--    Gender NVARCHAR(10),
--    Region NVARCHAR(10),
--    Login NVARCHAR(50) UNIQUE,
--    Password NVARCHAR(50),
--    IsBlocked BIT DEFAULT 0
--);

-- Таблица истории входов
--CREATE TABLE LoginHistory (
--    Id INT IDENTITY(1,1) PRIMARY KEY,
--    Login NVARCHAR(50),
--    Region NVARCHAR(10),
--    Server NVARCHAR(50),
--    LoginTime DATETIME
--);

-- Таблица серверов
--CREATE TABLE Servers (
--    Id INT IDENTITY(1,1) PRIMARY KEY,
--    Region NVARCHAR(10),
--    ServerName NVARCHAR(50)
--);

-- Добавление серверов
--INSERT INTO Servers (Region, ServerName) VALUES ('EU', 'EU-1'), ('EU', 'EU-2'), ('EU', 'EU-3');
--INSERT INTO Servers (Region, ServerName) VALUES ('AS', 'AS-1'), ('AS', 'AS-2'), ('AS', 'AS-3');
--INSERT INTO Servers (Region, ServerName) VALUES ('AM', 'AM-1'), ('AM', 'AM-2'), ('AM', 'AM-3');


INSERT INTO Players (FullName, BirthDate, Gender, Region, Login, Password, IsBlocked)
VALUES ('Administrator', '1900-01-01', 'M', 'EU', 'admin', 'admin123', 0);
