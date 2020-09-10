CREATE TABLE Livro(
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Titulo NVARCHAR(100) NOT NULL,
	Isbn NVARCHAR(13) NOT NULL,
	Publicacao DATETIME2 NOT NULL,
	AutorId INT FOREIGN KEY REFERENCES Autor(Id)
	-- https://www.w3schools.com/sql/sql_foreignkey.asp
);

SELECT * FROM Autor;
SELECT * FROM Livro;

INSERT INTO Autor
	(Nome, UltimoNome, Nascimento)
	values ('João', 'Andrade', '2000-01-01');

INSERT INTO Livro
	(Titulo, Isbn, Publicacao, AutorId)
	values ('LivroJoão', '12-12-12', '2005-01-1', 3);

DELETE FROM Livro WHERE Id = 2;
DELETE FROM Autor WHERE Id = 1;

SELECT * FROM Livro WHERE AutorId = 2;

SELECT l.Titulo, a.UltimoNome FROM Livro as l
	INNER JOIN Autor as a
	ON a.Id = l.AutorId
WHERE a.Nome = 'Felipe'