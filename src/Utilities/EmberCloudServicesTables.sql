create database DB_EmberCloudServices
use DB_EmberCloudServices
create table Employee(
    EmployeeID int primary key identity (1,1),
    EmployeeName varchar(10) not null,
    EmployeeRole varchar(25),

)
create table Client
(
    ClientID     int primary key not null identity (1,1),
    ClientName   nvarchar(50)    not null,
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
    RoleName varchar(22) not null
);

insert into DBRoles (RoleName)
values ('System Admin'),('ReadOnly')

create table CreatedUsers(
    CreatedUsersID int primary key identity (1,1),
    userNameHash nvarchar(150) not null unique,
    userPasswordHash nvarchar(200) not null,
    DBRolesID int,
    foreign key (DBRolesID) references DBRoles(DBRolesID)

)

create table PaymentInfo(
    ClientID int,
    PaymentData nvarchar(100) not null,
    PaymentExpireDate DATE not null,
    paymentPIN varchar(100) not null,
    foreign key (ClientID) references Client(ClientID)
)

create table TicketDetails(
    TicketDetailsID int primary key identity (1,1),
    TicketMSG nvarchar(250),
    MsgDate date not null default getdate(),
    sentBy int,
    foreign key (sentBy) references Employee(EmployeeID)
)

create table Ticket(
    ticketID int primary key identity (1,1),
    ticketTitle varchar(20),
    ticketCreationDate date not null default  getdate(),
    ticketStatus bit not null,
    --Donde 1 es Abierto y 0 es Cerrado
    AssignedTo int,
    TicketDetailsID int,
    foreign key  (TicketDetailsID) references TicketDetails(TicketDetailsID),
    foreign key (AssignedTo) references Employee(EmployeeID)
)
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
    foreign key (FacturaDetalleID) references FacturaDetalle(FacturaDetalleID)
)
