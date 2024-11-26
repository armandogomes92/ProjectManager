CREATE TABLE Usuario (
    Id SERIAL PRIMARY KEY,
    Nome VARCHAR(255) NOT NULL,
    Cargo INT NOT NULL
);

CREATE TABLE Projeto (
    Id SERIAL PRIMARY KEY,
    Nome VARCHAR(255) NOT NULL,
    Active BOOLEAN NOT NULL,
    UserId INT NOT NULL,
    CONSTRAINT fk_usuario FOREIGN KEY (UserId) REFERENCES Usuario(Id)
);

CREATE TABLE Tarefa (
    Id SERIAL PRIMARY KEY,
    Titulo VARCHAR(255) NOT NULL,
    Descricao TEXT NOT NULL,
    PrazoConclusao TIMESTAMP NOT NULL,
    UltimaAtualizacao TIMESTAMP NOT NULL,
    Status INT NOT NULL,
    Prioridade INT NOT NULL,
    ProjetoId INT NOT NULL,
    CONSTRAINT fk_projeto FOREIGN KEY (ProjetoId) REFERENCES Projeto(Id)
);

CREATE TABLE Comentario (
    Id SERIAL PRIMARY KEY,
    TextoComentario TEXT NOT NULL,
    DataCriacao TIMESTAMP NOT NULL,
    TarefaId INT NOT NULL,
    UsuarioId INT NOT NULL,
    CONSTRAINT fk_tarefa FOREIGN KEY (TarefaId) REFERENCES Tarefa(Id),
    CONSTRAINT fk_usuario FOREIGN KEY (UsuarioId) REFERENCES Usuario(Id)
);

CREATE TABLE HistoricoDaTarefa (
    Id SERIAL PRIMARY KEY,
    ItemModificado VARCHAR(255) NOT NULL,
    ValoresAntigos TEXT NOT NULL,
    ValoresNovos TEXT NOT NULL,
    DataModificacao TIMESTAMP NOT NULL,
    TaskId INT NOT NULL,
    CONSTRAINT fk_tarefa FOREIGN KEY (TaskId) REFERENCES Tarefa(Id)
);

INSERT INTO Usuario (Nome, Cargo) VALUES ('João Silva', 1); -- Cargo 1: Gerente
INSERT INTO Usuario (Nome, Cargo) VALUES ('Maria Oliveira', 2); -- Cargo 2: Analista
