CREATE DATABASE IF NOT EXISTS insurance_partners_db
CHARACTER SET utf8mb4
COLLATE utf8mb4_unicode_ci;

USE insurance_partners_db;

CREATE TABLE Partners (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    FirstName VARCHAR(255) NOT NULL,
    LastName VARCHAR(255) NOT NULL,
    Address VARCHAR(255) NULL,
    PartnerNumber VARCHAR(20) NOT NULL,
    CroatianPIN VARCHAR(11) NULL,
    PartnerTypeId INT NOT NULL,
    CreatedAtUtc DATETIME NOT NULL DEFAULT (UTC_TIMESTAMP()),
    CreatedByUser VARCHAR(255) NOT NULL,
    IsForeign BOOLEAN NOT NULL,
    ExternalCode VARCHAR(20) NOT NULL,
    Gender CHAR(1) NOT NULL,

    CONSTRAINT UQ_Partners_ExternalCode UNIQUE (ExternalCode),
    CONSTRAINT CHK_PartnerNumber_Length CHECK (CHAR_LENGTH(PartnerNumber) = 20),
    CONSTRAINT CHK_PartnerType CHECK (PartnerTypeId IN (1, 2)),
    CONSTRAINT CHK_Gender CHECK (Gender IN ('M', 'F', 'N')),
    CONSTRAINT CHK_ExternalCode_Length CHECK (CHAR_LENGTH(ExternalCode) BETWEEN 10 AND 20)
);

CREATE TABLE Policies (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    PartnerId INT NOT NULL,
    PolicyNumber VARCHAR(15) NOT NULL,
    PolicyAmount DECIMAL(10,2) NOT NULL,
    CreatedAtUtc DATETIME NOT NULL DEFAULT (UTC_TIMESTAMP()),

    CONSTRAINT FK_Policies_Partners 
        FOREIGN KEY (PartnerId) REFERENCES Partners(Id)
        ON DELETE CASCADE,

    CONSTRAINT CHK_PolicyNumber_Length CHECK (CHAR_LENGTH(PolicyNumber) BETWEEN 10 AND 15)
);