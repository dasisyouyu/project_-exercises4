-- テーブルを作成するクエリを記述したファイルです

-- ユーザー情報を格納するためのテーブルです。
CREATE TABLE `Users`(
    `Id`    VARCHAR(255) NOT NULL,
    `UserName`  VARCHAR(255) NOT NULL,
    `Password`  VARCHAR(255) NOT NULL,
    `MailAddress` VARCHAR(255) NOT NULL,
    `IsValid` BOOLEAN DEFAULT TRUE,
    `DateCreated` DATETIME DEFAULT CURRENT_TIMESTAMP,
    `LastUpdated` TIMESTAMP NOT NULL ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Id PRIMARY KEY (`Id`)
)ENGINE=InnoDB;

-- Logの種別を表すマスターテーブルです。
CREATE TABLE `MstLogTypes` (
    `Id`    INT NOT NULL,
    `Name`  VARCHAR(16) NOT NULL,
    CONSTRAINT  PRIMARY KEY (`Id`)
)ENGINE=InnoDB;

INSERT INTO `MstLogTypes` (
    `Id`, `Name`
) VALUES
(0, 'DEBUG'),
(1, 'WARNING'),
(2, 'ERROR');

-- Logの種別を表すマスターテーブルです。
CREATE TABLE `Logs` (
    `Id`    INT NOT NULL,
    `LogTypeId`  INT NOT NULL,
    `Message`  VARCHAR(255) NOT NULL,
    `Contents`   TEXT    DEFAULT NULL,
    `DateCreated`   DATETIME    NOT NULL,
    CONSTRAINT  PRIMARY KEY (`Id`),
    CONSTRAINT  FOREIGN KEY (`LogTypeId`) REFERENCES `MstLogTypes`(`Id`)
)ENGINE=InnoDB;
