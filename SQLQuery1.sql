create database Noxun
use  Noxun


CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username VARCHAR(50) ,
    Password VARCHAR(50) ,
    Email VARCHAR(50) 
);


CREATE TABLE Roles (
    RoleID INT PRIMARY KEY IDENTITY(1,1),
    RoleName VARCHAR(30),
    Description VARCHAR(300)
);


CREATE TABLE UserRoles (
    ID INT PRIMARY KEY identity(1,1),
    UserID INT NOT NULL,
    RoleID INT NOT NULL,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)
);


CREATE TABLE Procedures (
    ProcedureID INT PRIMARY KEY IDENTITY(1,1),
    ProcedureName VARCHAR(50) ,
    Description VARCHAR(200),
    CreatedByUserID INT,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    LastModifiedUserID INT,
    LastModifiedDate DATETIME,
    FOREIGN KEY (CreatedByUserID) REFERENCES Users(UserID),
    FOREIGN KEY (LastModifiedUserID) REFERENCES Users(UserID)
);


CREATE TABLE Fields (
    FieldID INT PRIMARY KEY IDENTITY(1,1),
    FieldName VARCHAR(100),
    DataType VARCHAR(50)
);


CREATE TABLE DataSets (
    DataSetID INT PRIMARY KEY IDENTITY(1,1),
    DataSetName VARCHAR(100),
    Description VARCHAR(255),
    ProcedureID INT ,
    FieldID INT,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    LastModifiedDate DATETIME,
    FOREIGN KEY (ProcedureID) REFERENCES Procedures(ProcedureID),
    FOREIGN KEY (FieldID) REFERENCES Fields(FieldID)
);



select * from Users
-- Insertar Usuarios
INSERT INTO Users (Username, Password, Email) VALUES
('admin', 'admin123', 'admin@example.com'),
('user1', 'user123', 'user1@example.com');

-- Insertar Roles
INSERT INTO Roles (RoleName, Description) VALUES
('Admin', 'Administrator role'),
('User', 'Regular user role');

-- Insertar UserRoles
INSERT INTO UserRoles (UserID, RoleID) VALUES
(1, 1),
(2, 2);

-- Insertar Procedures
INSERT INTO Procedures (ProcedureName, Description, CreatedByUserID, LastModifiedUserID) VALUES
('Data Cleaning', 'Removes duplicates and standardizes formats', 1, 1),
( 'Statistical Analysis', 'Performs basic statistical calculations', 1, 1),
( 'Data Visualization', 'Creates charts and graphs from data', 2, 2),
( 'Machine Learning Model', 'Trains and applies ML models to data', 1, 2),
( 'Data Integration', 'Merges data from multiple sources', 2, 1);

-- Insertar Fields
INSERT INTO Fields ( FieldName, DataType) VALUES
('SampleField1', 'varchar'),
('SampleField2', 'int');

-- Insertar DataSets
INSERT INTO DataSets ( DataSetName, Description, ProcedureID, FieldId) VALUES
( 'Sales Data 2023', 'Annual sales figures', 1, 1),
( 'Customer Feedback', 'Survey responses from Q2', 2, 2),
( 'Website Traffic', 'Daily visitor counts', 3, 2),
( 'Product Inventory', 'Current stock levels', 1, 2),
( 'Employee Performance', 'Quarterly KPI data', 2, 1),
( 'Marketing Campaign Results', 'ROI for recent campaigns', 3, 1),
('Supply Chain Data', 'Supplier delivery times', 4, 2),
( 'Customer Segmentation', 'Demographic clusters', 4, 2),
( 'Social Media Metrics', 'Engagement rates across platforms', 3, 1),
( 'Financial Forecasts', 'Projected revenue for next FY', 2, 1),
( 'Product Returns', 'Reasons for customer returns', 1, 1),
( 'Website Heatmaps', 'User interaction patterns', 3, 2),
( 'Customer Churn Prediction', 'ML model input data', 4, 2),
( 'Competitor Price Analysis', 'Market rate comparisons', 2, 2),
( 'IoT Sensor Data', 'Raw data from factory sensors', 5, 2);


SELECT COLUMN_NAME 
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'Procedures';


select * from Procedures

select * from DataSets