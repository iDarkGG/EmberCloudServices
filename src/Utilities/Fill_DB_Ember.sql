INSERT INTO Employee (EmployeeName, EmployeeRole) VALUES
('Alice', 'System Admin'),
('Bob', 'Support Agent'),
('Charlie', 'Developer'),
('Diana', 'QA Engineer'),
('Eve', 'Support Agent');

INSERT INTO Client (ClientName, ClientContactNumber, ClientEmail, Status, CreationDate) VALUES
('TechCorp', '123-456-7890', 'contact@techcorp.com', 1, '2023-05-01'),
('HealthPlus', '234-567-8901', 'support@healthplus.com', 1, '2022-11-10'),
('EduWorld', '345-678-9012', 'info@eduworld.com', 0, '2022-02-15'),
('GreenEnergy', '456-789-0123', 'service@greenenergy.com', 1, '2023-03-21'),
('SecureNet', '567-890-1234', 'contact@securenet.com', 1, '2023-07-30');


INSERT INTO DataCenter (DataCenterID, DataCenterName, Capacity, Location) VALUES
('DC1', 'Central', 5000, 'New York, USA'),
('DC2', 'East', 3000, 'London, UK'),
('DC3', 'West', 2000, 'Tokyo, Japan');

INSERT INTO Instance (ClientID, DataCenterID, InstanceName, CreationDate) VALUES
(1, 'DC1', 'TechCorp-Inst1', '2023-05-02'),
(1, 'DC1', 'TechCorp-Inst2', '2023-06-14'),
(2, 'DC2', 'HealthPlus-Inst1', '2022-11-15'),
(4, 'DC3', 'GreenEnergy-Inst1', '2023-03-25'),
(5, 'DC2', 'SecureNet-Inst1', '2023-08-05');

INSERT INTO DBRoles (RoleName) VALUES
('System Admin'),
('ReadOnly');

INSERT INTO CreatedUsers (userNameHash, userPasswordHash, DBRolesID, InstanceID) VALUES
('5e884898da28047151d0e56f8dc6292773603d0d6aabbdd7b8a06ed78fd896d7', '6bb4837eb74329105ee4568dda7dc67ed2ca2ad9e1f45f5e27c74f231e2aac9d', 1, 1),
('7c6a180b36896a0a8c02787eeafb0e4c80e2b969a2ec9f9e655fdf3d98dfb54b', 'bf6b9ea90232d63e0b5e87e4b9fbb96b58b8f7b5a1637f61842b5e3c3c3b9a3c', 2, 2),
('3c6e0b8a9c15224a8228b9a98ca1531dbaaaab1498a9ac7d81a17c8885ea7a13', '12dada62dff992e5db5f204ba5a0dbb9e329e428336b56c45b5ae1dcf4de5d1f', 1, 3),
('c33367701511b4f6020ec61ded352059b1ffdeffa2d7d5a8c36f8c02f4dd2b9d', 'd80b88508d1e23e66c5716dbf54b571fb1f1691e4f7d992d53120fc3d9d66a7a', 2, 4);


INSERT INTO PaymentInfo (ClientID, PaymentData, PaymentExpireDate, paymentPIN) VALUES
(1, 'Credit Card', '2024-06-01', '1234'),
(2, 'PayPal', '2024-12-15', '5678'),
(4, 'Bank Transfer', '2025-01-10', '91011'),
(5, 'Credit Card', '2023-11-25', '1213');

INSERT INTO TicketDetails (TicketMSG, MsgDate,EmployeeID,ClientID,TicketID) VALUES
('Initial setup issue', '2023-08-01', null  , 1,1),
('Need more storage', '2023-08-05', null,2,2),
('Server downtime', '2023-08-07', null,3,3),
('Billing inquiry', '2023-08-10', null,4,4);


INSERT INTO Ticket (ticketTitle, ticketCreationDate, ticketStatus, AssignedTo) VALUES
('Setup Issue', '2023-08-01', 1, 2),
('Storage Expansion', '2023-08-05', 0, 1),
('Downtime', '2023-08-07', 1, 3),
('Billing Inquiry', '2023-08-10', 1, 4);


INSERT INTO FacturaDetalle (DetalleF, PeriodoFacturado, MontoFacturado) VALUES
('Monthly subscription', '2023-07', 150.00),
('Data overage', '2023-07', 25.00),
('Monthly subscription', '2023-08', 150.00),
('One-time setup fee', '2023-06', 50.00);


INSERT INTO Factura (FacturaID, FacturaDate, FacturaMonto, FacturaDetalleID) VALUES
(1, '2023-07-01', 150.00, 1),
(2, '2023-07-15', 25.00, 2),
(3, '2023-08-01', 150.00, 3),
(4, '2023-06-01', 50.00, 4);


