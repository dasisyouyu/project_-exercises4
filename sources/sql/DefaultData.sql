-- マスターの項目をINSERTするクエリを記述したファイルです
INSERT `Users` (
    `Id`, `UserName`, `Password`, `MailAddress`, `IsValid`, `DateCreated`
) VALUES (
'a092621f-9f86-4a13-a785-2d1ee2b71690', 'himy', 'AM5+JGj5cDwZu+tIL3E4+SWwpYbosw7GR6jd/ikgVw1M/VEoNbqJAcAYhFojSAfbdQ==', 'mail@gmail.com', true, CURRENT_TIMESTAMP);

INSERT INTO `MstLogTypes` (
    `Id`, `Name`
) VALUES
(0, 'DEBUG'),
(1, 'WARNING'),
(2, 'ERROR');