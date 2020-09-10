-- CREATE
-- DROP
-- ALTER TABLE COLUMN

-- DELETE
-- SELECT
-- UPDATE
-- INSERT

-- https://docs.microsoft.com/en-us/sql/t-sql/statements/create-table-transact-sql?view=sql-server-ver15
CREATE TABLE Autor(
	Id	INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Nome NVARCHAR(50),
	UltimoNome NVARCHAR(50),
	Nascimento DATETIME2
);

-- https://www.w3schools.com/sql/sql_insert.asp
INSERT INTO Autor
	(Nome, UltimoNome, Nascimento)
	VALUES ('Felipe1', 'Andrade1', '1988-02-23');

-- https://www.w3schools.com/sql/sql_select.asp
SELECT * FROM Autor;
SELECT * FROM Autor WHERE Id = 2;

-- https://www.w3schools.com/sql/sql_delete.asp
DELETE FROM Autor WHERE Id > 2;
DELETE FROM Autor WHERE Nome = 'Felipe1';

-- https://www.w3schools.com/sql/sql_update.asp
UPDATE Autor
	SET Nome = 'João', UltimoNome = 'Das Neves'
	WHERE Id = 12;
UPDATE Autor
	Set Nome = 'Felipe', UltimoNome = 'Andrade'
	WHERE Id = 1;

-- https://www.w3schools.com/sql/sql_drop_table.asp
DROP TABLE Autor;

-- https://www.w3schools.com/sql/sql_alter.asp
ALTER TABLE Autor
	ALTER COLUMN Nome NVARCHAR(255);