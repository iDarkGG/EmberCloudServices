/*
    Lista de Querys, views y stored Procedures para la base de datos de Ember Cloud Services
    Por favor tenga en cuenta que esta no es una lista extensiva y solo es una demostracion de la relacionalidad
    y estructura de la base de datos, dado que la mayoria de estas consultas son realizadas automaticamente por la API
*/
use DB_EmberCloudServices

--Consulta con JOIN para obtener información de instancias y sus clientes
SELECT
    I.InstanceID,
    I.InstanceName,
    I.CreationDate AS InstanceCreationDate,
    C.ClientName,
    D.DataCenterName,
    D.Location
FROM
    Instance I
JOIN
    Client C ON I.ClientID = C.ClientID
JOIN
    DataCenter D ON I.DataCenterID = D.DataCenterID
WHERE
    C.Status = 1; -- Clientes activos

--VIEW para ver todas las facturas y sus detalles
CREATE VIEW vw_FacturasDetalle AS
SELECT
    F.FacturaID,
    F.FacturaDate,
    F.FacturaMonto,
    FD.DetalleF,
    FD.PeriodoFacturado,
    FD.MontoFacturado
FROM
    Factura F
JOIN
    FacturaDetalle FD ON F.FacturaDetalleID = FD.FacturaDetalleID;

select * from vw_FacturasDetalle

--VIEW para listar el estado de los tickets y los empleados asignados
CREATE VIEW vw_TicketStatus AS
SELECT
    T.ticketID,
    T.ticketTitle,
    T.ticketCreationDate,
    T.ticketStatus,
    E.EmployeeName AS AssignedTo
FROM
    Ticket T
LEFT JOIN
    Employee E ON T.AssignedTo = E.EmployeeID;

select *
from vw_TicketStatus;

--STORED PROCEDURE para cerrar un ticket (actualizar su estado)
CREATE PROCEDURE CloseTicket
    @TicketID INT
AS
BEGIN
    UPDATE Ticket
    SET ticketStatus = 0
    WHERE ticketID = @TicketID;
END;


--STORED PROCEDURE para obtener facturas de un cliente específico
CREATE PROCEDURE GetClientInvoices
    @ClientID INT
AS
BEGIN
    SELECT
        F.FacturaID,
        F.FacturaDate,
        F.FacturaMonto,
        FD.DetalleF,
        FD.PeriodoFacturado,
        FD.MontoFacturado
    FROM
        Factura F
    JOIN
        FacturaDetalle FD ON F.FacturaDetalleID = FD.FacturaDetalleID
    WHERE
        F.FacturaID IN (SELECT FacturaID FROM PaymentInfo WHERE ClientID = @ClientID);
END;

--STORED PROCEDURE para obtener facturas de un cliente específico
CREATE PROCEDURE GetClientInvoices
    @ClientID INT
AS
BEGIN
    SELECT
        F.FacturaID,
        F.FacturaDate,
        F.FacturaMonto,
        FD.DetalleF,
        FD.PeriodoFacturado,
        FD.MontoFacturado
    FROM
        Factura F
    JOIN
        FacturaDetalle FD ON F.FacturaDetalleID = FD.FacturaDetalleID
    WHERE
        F.FacturaID IN (SELECT FacturaID FROM PaymentInfo WHERE ClientID = @ClientID);
END;


--Consulta para ver el total facturado por cliente
SELECT
    P.ClientID,
    C.ClientName,
    SUM(F.FacturaMonto) AS TotalFacturado
FROM
    PaymentInfo P
JOIN
    Client C ON P.ClientID = C.ClientID
JOIN
    Factura F ON P.ClientID = F.FacturaID
GROUP BY
    P.ClientID, C.ClientName;

    


