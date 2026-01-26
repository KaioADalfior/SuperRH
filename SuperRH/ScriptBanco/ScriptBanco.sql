CREATE DATABASE BDSistemaRH;
USE BDSistemaRH;

CREATE TABLE Usuarios (
    idUsuario INT IDENTITY(1,1) PRIMARY KEY, -- ID auto-incremento
    Login VARCHAR(50) NOT NULL UNIQUE, -- Login único para acesso
    SenhaHash VARCHAR(255) NOT NULL,  -- Senha criptografada (nunca texto plano)
    NomeCompleto VARCHAR(100) NOT NULL,
    Email VARCHAR(100),
    NivelAcesso VARCHAR(20) NOT NULL 
        CHECK (NivelAcesso IN ('Admin', 'Gerente', 'Operador')), -- Controle de Permissões
    DataCriacao DATETIME DEFAULT GETDATE(),
    Status BIT DEFAULT 1 -- 1 para Ativo, 0 para Inativo
);
GO

SELECT * FROM Usuarios;


CREATE TABLE Funcionarios (
    idFuncionario INT IDENTITY(1,1) PRIMARY KEY, -- Chave primária única
    
    -- Identificação Civil
    NomeCompleto VARCHAR(150) NOT NULL,
    CPF CHAR(11) NOT NULL UNIQUE,                -- CPF como valor único e obrigatório
    RG VARCHAR(20) NOT NULL UNIQUE,              -- RG obrigatório para identificação
    OrgaoEmissor VARCHAR(20),                    -- Ex: SSP/SP
    DataNascimento DATE NOT NULL,
    
    -- Características e Estado
    Sexo CHAR(1) NOT NULL 
        CHECK (Sexo IN ('M', 'F', 'O')),         -- M: Masculino, F: Feminino, O: Outro
    EstadoCivil VARCHAR(20) 
        CHECK (EstadoCivil IN ('Solteiro', 'Casado', 'Divorciado', 'Viúvo', 'União Estável')),
    Nacionalidade VARCHAR(50) DEFAULT 'Brasileira',
    Naturalidade VARCHAR(50),                    -- Cidade de nascimento
    
    -- Filiação
    NomePai VARCHAR(150),
    NomeMae VARCHAR(150) NOT NULL,

    -- Metadados de Controle
    DataCadastro DATETIME DEFAULT GETDATE(),
    Status BIT DEFAULT 1                         -- 1 para Ativo, 0 para Inativo
);
GO

-- Índices para otimização de busca na listagem corporativa
CREATE INDEX IX_Func_Nome ON Funcionarios(NomeCompleto);
CREATE INDEX IX_Func_CPF ON Funcionarios(CPF);
GO

SELECT * FROM Funcionarios;


CREATE TABLE Historico (
    idHistorico INT IDENTITY(1,1) PRIMARY KEY,
    
    -- Relacionamento
    idFuncionario INT NOT NULL, 

    -- Detalhes da Ocorrência
    Categoria VARCHAR(50) NOT NULL 
        CHECK (Categoria IN ('Admissão', 'Promoção', 'Alteração Salarial', 'Férias', 'Afastamento', 'Advertência', 'Desligamento', 'Outros')),
    
    Titulo VARCHAR(100) NOT NULL, -- Ex: "Aumento de mérito 10%"
    Descricao TEXT,               -- Detalhamento completo do evento
    
    -- Dados Temporais
    DataEvento DATE NOT NULL DEFAULT GETDATE(), -- Quando o fato ocorreu
    DataRegistro DATETIME DEFAULT GETDATE(),    -- Quando foi inserido no sistema
    
    -- Auditoria (Quem realizou a alteração)
    UsuarioResponsavel VARCHAR(50), 

    -- Chave Estrangeira
    CONSTRAINT FK_Historico_Funcionario FOREIGN KEY (idFuncionario) 
        REFERENCES Funcionarios(idFuncionario) ON DELETE CASCADE
);
GO

-- Índice para busca rápida de histórico por funcionário
CREATE INDEX IX_Historico_Funcionario ON Historico(idFuncionario);
GO

SELECT * FROM Historico;


CREATE TABLE PontoBatido (
    idPonto INT IDENTITY(1,1) PRIMARY KEY,
    DiaSemana VARCHAR(10) NOT NULL,       -- Ex: "Segunda-feira"
    Hora TIME NOT NULL,                    -- Hora do ponto
    DataRegistro DATETIME DEFAULT GETDATE() -- Data e hora do registro
);

select * from PontoBatido;

