create table Funds (Id int NOT NULL, Name nvarchar(64))

create table Contacts (Name nvarchar(64) NOT NULL, Email nvarchar(64), PhoneNumber nvarchar(16))

create table FundsContacts (FundId int NOT NULL, ContactName nvarchar(64) NOT NULL)

insert into Funds (1, 'Test Fund 1')