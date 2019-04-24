create table RideCategories(
RideCategoryId int not null primary key identity(1,1),
[Name] nvarchar(50) not null,
[Description] nvarchar(MAX) not null
);

create table Rides(
RideId int not null primary key identity(1,1),
[Name] nvarchar(50) not null,
[Description] nvarchar(MAX) not null,
CategoryId int foreign key references RideCategories(RideCategoryId) not null,
Status int not null
);

create table Reports(
ReportId int not null primary key identity(1,1),
[Status] int not null,
ReportTime DateTime2(7) not null,
Notes nvarchar(MAX) not null,
RideId int foreign key references Rides(RideId)
);