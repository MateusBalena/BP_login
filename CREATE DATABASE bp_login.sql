--CREATE DATABASE bp_login
--USE bp_login

CREATE TABLE usuario_login (
id INT IDENTITY(1,1),
login VARCHAR(10),
senha VARCHAR(255)

CONSTRAINT PK_usuario_login PRIMARY KEY (id),
CONSTRAINT UK_usuario_login UNIQUE (login)
)

CREATE TABLE usuario_info (
id int identity(1,1),
nome VARCHAR(255),
data_aniversario DATE,
ultimaSessao DATETIME,
login INT

CONSTRAINT PK_usuario_info PRIMARY KEY (id),
CONSTRAINT FK_usuario_info_login FOREIGN KEY (login) REFERENCES usuario_login(id)
	ON DELETE CASCADE
)

