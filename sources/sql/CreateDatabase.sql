-- �g�p����DB���쐬����N�G�����L�q�����t�@�C���ł�

-- HimyDB���쐬���܂��B
CREATE DATABASE IF NOT EXISTS HimyDB DEFAULT CHARACTER SET=UTF8;
GRANT ALL PRIVILEGES ON HimyDB .* TO 'HimyApp'@localhost IDENTIFIED BY '+2QwJpBh';
FLUSH PRIVILEGES;
GRANT ALL PRIVILEGES ON HimyDB .* TO 'HimyApp'@'%' IDENTIFIED BY '+2QwJpBh';
FLUSH PRIVILEGES;

-- HimyTestDB���쐬���܂��B
CREATE DATABASE IF NOT EXISTS HimyTestDB DEFAULT CHARACTER SET=UTF8;
GRANT ALL PRIVILEGES ON HimyTestDB .* TO 'HimyApp'@localhost IDENTIFIED BY '+2QwJpBh';
FLUSH PRIVILEGES;
GRANT ALL PRIVILEGES ON HimyTestDB .* TO 'HimyApp'@'%' IDENTIFIED BY '+2QwJpBh';
FLUSH PRIVILEGES;