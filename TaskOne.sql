-- Код писал в онлайн-компиляторе https://onecompiler.com/sqlserver/ 

-- create
CREATE TABLE A (
  ID INTEGER PRIMARY KEY,
  UserId INTEGER NOT NULL,
  Status INTEGER NOT NULL
);
-- insert
INSERT INTO A VALUES (1, 85, 2);
INSERT INTO A VALUES (2, 85, 3);
INSERT INTO A VALUES (3, 85, 4);
INSERT INTO A VALUES (4, 85, 1);
INSERT INTO A VALUES (5, 86, 2);
INSERT INTO A VALUES (6, 86, 4);
INSERT INTO A VALUES (7, 86, 1);
INSERT INTO A VALUES (8, 86, 3);

-- Добавляем свою колонку для идентификации полей
alter table A add MyId INT IDENTITY

-- Вся таблица, чтобы было удобнее смотреть
SELECT * FROM A 

-- 1. Необходимо из таблицы А найти последние значения Id и Status по UserId = 85
SELECT TOP(1) Id, Status FROM A WHERE UserId = 85 order by MyId desc

-- 2. Посчитать сумму поля Id с накопительным итогом для UserId = 86


-- 3. Вывести только четные записи по каждому UserId
SELECT UserId, Status FROM A WHERE (MyId % 2) = 0

GO
