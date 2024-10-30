create database DB_EmberCloudServices
--use DB_EmberCloudServices
create table Employee(
    EmployeeID int primary key identity (1,1),
    EmployeeName varchar(50) not null,
    EmployeeRole varchar(25),

)
create table Client
(
    ClientID     int primary key not null identity (1,1),
    ClientName   nvarchar(50)    not null,
    ClientContactNumber varchar(15),
    ClientEmail varchar(55),
    Status       bit                      default 1,
    --Donde 1 es Activo y 0 Es inactivo.
    CreationDate Date            not null default getdate()
);

create table DataCenter(
    DataCenterID varchar(3) primary key not null,
    DataCenterName varchar(10),
    Capacity int not null,
    --Donde int es la capacidad en GB
    Location nvarchar(100)
);

create table Instance(
     InstanceID int primary key identity (1,1),
     ClientID int,
     DataCenterID varchar(3),
     InstanceName nvarchar(50) unique,
     CreationDate Date not null,
     foreign key (ClientID) references Client(ClientID),
     foreign key (DataCenterID) references DataCenter(DataCenterID)
);

create table DBRoles(
    DBRolesID int primary key identity (1,1),
    RoleName varchar(50) not null
);


create table CreatedUsers(
    CreatedUsersID int primary key identity (1,1),
    userNameHash nvarchar(150) not null unique,
    userPasswordHash nvarchar(200) not null,
    DBRolesID int,
    InstanceID int,
    foreign key (InstanceID) references Instance(InstanceID),
    foreign key (DBRolesID) references DBRoles(DBRolesID)

)

create table PaymentInfo(
    ClientID int,
    PaymentData nvarchar(100) not null,
    PaymentExpireDate DATE not null,
    paymentPIN varchar(100) not null,
    foreign key (ClientID) references Client(ClientID)
)

create table Ticket(
    ticketID int primary key identity (1,1),
    ticketTitle varchar(20),
    ticketCreationDate date not null default  getdate(),
    ticketStatus bit not null,
    --Donde 1 es Abierto y 0 es Cerrado
    AssignedTo int,
    foreign key (AssignedTo) references Employee(EmployeeID)
)

create table TicketDetails (
    TicketDetailsID INT PRIMARY KEY IDENTITY (1,1),
    TicketMSG NVARCHAR(250),
    MsgDate DATE NOT NULL DEFAULT GETDATE(),
    EmployeeID INT NULL,
    ClientID INT NULL,
    FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID),
    FOREIGN KEY (ClientID) REFERENCES Client(ClientID),
    TicketID INT NOT NULL,
    FOREIGN KEY (TicketID) REFERENCES Ticket(ticketID)
);


create table FacturaDetalle(
    FacturaDetalleID int primary key identity (1,1),
    DetalleF nvarchar(100) not null,
    PeriodoFacturado nvarchar(100) not null,
    MontoFacturado money not null,
)

create table Factura(
    FacturaID int primary key not null,
    FacturaDate datetime not null,
    FacturaMonto money not null,
    FacturaDetalleID int,
    ClientID int,
    foreign key (ClientID) references Client(ClientID),
    foreign key (FacturaDetalleID) references FacturaDetalle(FacturaDetalleID)
)

