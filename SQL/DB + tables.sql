use master
go

create database ImageGallery
on primary 
(
	name = 'ImageGallery',
	filename = 'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\ImageGallery.mdf',
	size = 4MB, 
	maxsize = 100MB,
	filegrowth = 1MB)
log on 
(
	name = 'ImageGallery_log2',
	filename = 'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\ImageGallery_log.ldf',
	size = 10MB,
	filegrowth = 1MB		
)
go

use ImageGallery
go

create table [PASSWORD]
(
[Id] INT IDENTITY(1,1) not null PRIMARY KEY,
[Login] NVARCHAR(50) not null,
[Password] NVARCHAR(100) not null,
CONSTRAINT CONSTRAINT_UNIQUE_LOGIN UNIQUE([Login])
)
go

create table [USER]
(
[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
[Name] NVARCHAR(100) NOT NULL,
[Surname] NVARCHAR(100) NOT NULL,
[Id_Password] INT NOT NULL,
CONSTRAINT F_KEY_PASSWORD FOREIGN KEY ([Id_Password]) REFERENCES [PASSWORD]([Id]) ON DELETE NO ACTION ON UPDATE CASCADE,
CONSTRAINT CONSTRAINT_UNIQUE_USER UNIQUE([Name],[Surname],[Id_Password])
)
go

create table [GRADES]
(
[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
[Grade] INT NOT NULL UNIQUE check([Grade]>-1 and [Grade]<6) DEFAULT 0
)
go

create table [PICTURE]
(
[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
[FullName] NVARCHAR(512) NOT NULL,
CONSTRAINT CONSTRAINT_UNIQUE_PATH UNIQUE([FullName])
)
go

--один пользователь может оценить одну картинку только один раз
create table [PICTURE_GRADES]
(
[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
[Id_User] INT NOT NULL,
[Id_Picture] INT NOT NULL,
[Id_Grade]INT NOT NULL,
CONSTRAINT CONSTRAINT_UNIQUE_ASSESSMENT unique ( [Id_User], [Id_Picture]),
CONSTRAINT F_KEY_USER FOREIGN KEY ([Id_User]) REFERENCES [USER]([Id]),
CONSTRAINT F_KEY_PICTURE FOREIGN KEY ([Id_Picture]) REFERENCES [PICTURE]([Id]),
CONSTRAINT F_KEY_GRADE FOREIGN KEY ([Id_Grade]) REFERENCES [GRADES]([Id])
)
go