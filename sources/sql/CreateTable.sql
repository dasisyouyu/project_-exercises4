-- �e�[�u�����쐬����N�G�����L�q�����t�@�C���ł�

-- ���[�U�[�����i�[���邽�߂̃e�[�u���ł��B
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

-- Log�̎�ʂ�\���}�X�^�[�e�[�u���ł��B
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

-- Log�̎�ʂ�\���}�X�^�[�e�[�u���ł��B
CREATE TABLE `Logs` (
    `Id`    INT NOT NULL,
    `LogTypeId`  INT NOT NULL,
    `Message`  VARCHAR(255) NOT NULL,
    `Contents`   TEXT    DEFAULT NULL,
    `DateCreated`   DATETIME    NOT NULL,
    CONSTRAINT  PRIMARY KEY (`Id`),
    CONSTRAINT  FOREIGN KEY (`LogTypeId`) REFERENCES `MstLogTypes`(`Id`)
)ENGINE=InnoDB;
