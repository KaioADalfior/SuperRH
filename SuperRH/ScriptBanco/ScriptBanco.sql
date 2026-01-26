/* ================================
   CRIAÇÃO DO BANCO
================================ */
CREATE DATABASE BDSistemaRH;
GO
USE BDSistemaRH;
GO

/* ================================
   TABELA DE CARGOS
================================ */
CREATE TABLE Cargos (
    idCargo INT IDENTITY(1,1) PRIMARY KEY,
    NomeCargo VARCHAR(100) NOT NULL UNIQUE,
    Area VARCHAR(50) NOT NULL,
    Nivel VARCHAR(30),
    Descricao VARCHAR(255),
    Status BIT DEFAULT 1
);
GO

/* ================================
   INSERT CARGOS (50+)
================================ */
INSERT INTO Cargos (NomeCargo, Area, Nivel, Descricao) VALUES
-- DIRETORIA
('Diretor Executivo (CEO)', 'Diretoria', 'Executivo', 'Estratégia geral da empresa'),
('Diretor Financeiro (CFO)', 'Financeiro', 'Executivo', 'Gestão financeira'),
('Diretor de Tecnologia (CTO)', 'Tecnologia', 'Executivo', 'Gestão de TI'),
('Diretor de Operações (COO)', 'Operações', 'Executivo', 'Gestão operacional'),

-- RH / ADMIN
('Gerente de Recursos Humanos', 'RH', 'Gerencial', 'Gestão de pessoas'),
('Analista de RH', 'RH', 'Pleno', 'Processos de RH'),
('Assistente de RH', 'RH', 'Júnior', 'Apoio RH'),
('Coordenador Administrativo', 'Administrativo', 'Gerencial', 'Gestão administrativa'),
('Auxiliar Administrativo', 'Administrativo', 'Júnior', 'Rotinas administrativas'),

-- FINANCEIRO
('Gerente Financeiro', 'Financeiro', 'Gerencial', 'Controle financeiro'),
('Analista Financeiro', 'Financeiro', 'Pleno', 'Análises financeiras'),
('Auxiliar Financeiro', 'Financeiro', 'Júnior', 'Lançamentos financeiros'),
('Contador', 'Contabilidade', 'Sênior', 'Contabilidade geral'),
('Assistente Contábil', 'Contabilidade', 'Júnior', 'Apoio contábil'),

-- TI
('Gerente de TI', 'Tecnologia', 'Gerencial', 'Gestão de tecnologia'),
('Analista de Sistemas', 'Tecnologia', 'Pleno', 'Sistemas corporativos'),
('Desenvolvedor Backend', 'Tecnologia', 'Pleno', 'Backend'),
('Desenvolvedor Frontend', 'Tecnologia', 'Pleno', 'Frontend'),
('Desenvolvedor Full Stack', 'Tecnologia', 'Sênior', 'Full Stack'),
('Suporte Técnico', 'Tecnologia', 'Júnior', 'Suporte'),
('Administrador de Redes', 'Tecnologia', 'Sênior', 'Redes'),

-- COMERCIAL
('Gerente Comercial', 'Comercial', 'Gerencial', 'Vendas'),
('Supervisor de Vendas', 'Comercial', 'Pleno', 'Supervisão'),
('Vendedor Interno', 'Comercial', 'Júnior', 'Vendas internas'),
('Vendedor Externo', 'Comercial', 'Pleno', 'Vendas externas'),
('Representante Comercial', 'Comercial', 'Pleno', 'Representação'),

-- MARKETING
('Gerente de Marketing', 'Marketing', 'Gerencial', 'Marketing estratégico'),
('Analista de Marketing', 'Marketing', 'Pleno', 'Campanhas'),
('Designer Gráfico', 'Marketing', 'Pleno', 'Design'),
('Social Media', 'Marketing', 'Júnior', 'Redes sociais'),

-- LOGÍSTICA
('Gerente de Logística', 'Logística', 'Gerencial', 'Gestão logística'),
('Analista de Logística', 'Logística', 'Pleno', 'Controle'),
('Auxiliar de Logística', 'Logística', 'Júnior', 'Apoio'),
('Almoxarife', 'Logística', 'Pleno', 'Estoque'),

-- PRODUÇÃO
('Supervisor de Produção', 'Produção', 'Pleno', 'Supervisão'),
('Operador de Máquinas', 'Produção', 'Júnior', 'Operação'),
('Auxiliar de Produção', 'Produção', 'Júnior', 'Apoio'),

-- JURÍDICO
('Gerente Jurídico', 'Jurídico', 'Gerencial', 'Gestão jurídica'),
('Advogado', 'Jurídico', 'Sênior', 'Atuação jurídica'),
('Assistente Jurídico', 'Jurídico', 'Júnior', 'Apoio'),

-- SERVIÇOS
('Porteiro', 'Serviços', 'Operacional', 'Portaria'),
('Vigilante', 'Serviços', 'Operacional', 'Segurança'),
('Auxiliar de Limpeza', 'Serviços', 'Operacional', 'Limpeza'),

-- COMPRAS
('Gerente de Compras', 'Compras', 'Gerencial', 'Compras'),
('Analista de Compras', 'Compras', 'Pleno', 'Negociação'),
('Auxiliar de Compras', 'Compras', 'Júnior', 'Apoio');
GO

/* ================================
   COLABORADORES
================================ */
CREATE TABLE Colaboradores (
    idColaborador INT IDENTITY(1,1) PRIMARY KEY,
    NomeCompleto VARCHAR(150) NOT NULL,
    CPF CHAR(11) NOT NULL UNIQUE,
    RG VARCHAR(20) NOT NULL UNIQUE,
    OrgaoEmissor VARCHAR(20),
    DataNascimento DATE NOT NULL,
    Sexo CHAR(1) CHECK (Sexo IN ('M','F','O')),
    EstadoCivil VARCHAR(20),
    Nacionalidade VARCHAR(50) DEFAULT 'Brasileira',
    Naturalidade VARCHAR(50),
    NomePai VARCHAR(150),
    NomeMae VARCHAR(150) NOT NULL,
    idCargo INT NOT NULL,
    DataCadastro DATETIME DEFAULT GETDATE(),
    Status BIT DEFAULT 1,

    CONSTRAINT FK_Colaborador_Cargo
        FOREIGN KEY (idCargo) REFERENCES Cargos(idCargo)
);
GO

CREATE INDEX IX_Colaborador_Nome ON Colaboradores(NomeCompleto);
GO

INSERT INTO Colaboradores
(NomeCompleto, CPF, RG, OrgaoEmissor, DataNascimento, Sexo, EstadoCivil,
 Naturalidade, NomePai, NomeMae, idCargo)
VALUES
('Carlos Henrique Lima', '11111111111', 'MG123456', 'SSP/MG', '1985-03-12', 'M', 'Casado', 'Belo Horizonte', 'João Lima', 'Maria Lima', 1),
('Fernanda Souza Rocha', '22222222222', 'SP223344', 'SSP/SP', '1990-06-22', 'F', 'Solteiro', 'São Paulo', 'Paulo Rocha', 'Ana Rocha', 5),
('Lucas Andrade Silva', '33333333333', 'RJ334455', 'SSP/RJ', '1994-01-10', 'M', 'Solteiro', 'Rio de Janeiro', 'Marcos Silva', 'Juliana Silva', 17),
('Juliana Martins Costa', '44444444444', 'ES445566', 'SSP/ES', '1992-11-05', 'F', 'Casado', 'Vitória', 'Roberto Costa', 'Lucia Costa', 18),
('Renato Alves Pires', '55555555555', 'SP556677', 'SSP/SP', '1988-08-19', 'M', 'Divorciado', 'Campinas', 'Antonio Pires', 'Sonia Pires', 23),
('Patrícia Nogueira', '66666666666', 'MG667788', 'SSP/MG', '1995-02-28', 'F', 'Solteiro', 'Uberlândia', 'Carlos Nogueira', 'Helena Nogueira', 24),
('André Farias', '77777777777', 'BA778899', 'SSP/BA', '1983-07-14', 'M', 'Casado', 'Salvador', 'José Farias', 'Rita Farias', 9),
('Camila Ribeiro', '88888888888', 'PR889900', 'SSP/PR', '1996-09-30', 'F', 'Solteiro', 'Curitiba', 'Marcio Ribeiro', 'Paula Ribeiro', 27),
('Eduardo Vasconcelos', '99999999999', 'RS990011', 'SSP/RS', '1980-12-02', 'M', 'Casado', 'Porto Alegre', 'Luis Vasconcelos', 'Sandra Vasconcelos', 10),
('Bruna Almeida', '10101010101', 'SP101010', 'SSP/SP', '1993-04-17', 'F', 'Solteiro', 'São Paulo', 'Fernando Almeida', 'Carla Almeida', 11),

('Thiago Moreira', '12121212121', 'RJ121212', 'SSP/RJ', '1987-06-08', 'M', 'Casado', 'Niterói', 'Rogério Moreira', 'Elisa Moreira', 30),
('Mariana Torres', '13131313131', 'MG131313', 'SSP/MG', '1998-10-25', 'F', 'Solteiro', 'Betim', 'Rafael Torres', 'Daniela Torres', 31),
('Gustavo Barros', '14141414141', 'SP141414', 'SSP/SP', '1991-03-03', 'M', 'Solteiro', 'Sorocaba', 'Pedro Barros', 'Cecilia Barros', 33),
('Larissa Teixeira', '15151515151', 'GO151515', 'SSP/GO', '1994-05-15', 'F', 'União Estável', 'Goiânia', 'Marcos Teixeira', 'Renata Teixeira', 34),
('Felipe Araujo', '16161616161', 'DF161616', 'SSP/DF', '1986-01-21', 'M', 'Casado', 'Brasília', 'Otávio Araujo', 'Marta Araujo', 37),
('Aline Pacheco', '17171717171', 'SP171717', 'SSP/SP', '1997-09-09', 'F', 'Solteiro', 'Osasco', 'Ricardo Pacheco', 'Patricia Pacheco', 38),
('Rafael Dantas', '18181818181', 'RN181818', 'SSP/RN', '1989-12-18', 'M', 'Casado', 'Natal', 'Eduardo Dantas', 'Luzia Dantas', 41),
('Daniela Guedes', '19191919191', 'MG191919', 'SSP/MG', '1995-02-07', 'F', 'Solteiro', 'Divinópolis', 'Henrique Guedes', 'Silvia Guedes', 42),
('Vinicius Prado', '20202020202', 'SP202020', 'SSP/SP', '1992-08-11', 'M', 'Solteiro', 'Jundiaí', 'Ronaldo Prado', 'Teresa Prado', 44),
('Simone Freitas', '21212121212', 'RJ212121', 'SSP/RJ', '1984-06-01', 'F', 'Casado', 'Petrópolis', 'Paulo Freitas', 'Regina Freitas', 46);
GO


/* ================================
   USUÁRIOS (VINCULADO AO COLABORADOR)
================================ */
CREATE TABLE Usuarios (
    idUsuario INT IDENTITY(1,1) PRIMARY KEY,
    idColaborador INT NOT NULL UNIQUE,
    Login VARCHAR(50) NOT NULL UNIQUE,
    SenhaHash VARCHAR(255) NOT NULL,
    NomeCompleto VARCHAR(100) NOT NULL,
    Email VARCHAR(100),
    NivelAcesso VARCHAR(20)
        CHECK (NivelAcesso IN ('Admin','Gerente','Operador')),
    DataCriacao DATETIME DEFAULT GETDATE(),
    Status BIT DEFAULT 1,

    CONSTRAINT FK_Usuario_Colaborador
        FOREIGN KEY (idColaborador) REFERENCES Colaboradores(idColaborador)
);
GO

/* ================================
   HISTÓRICO
================================ */
CREATE TABLE Historico (
    idHistorico INT IDENTITY(1,1) PRIMARY KEY,
    idColaborador INT NOT NULL,
    Categoria VARCHAR(50)
        CHECK (Categoria IN
        ('Admissão','Promoção','Alteração Salarial','Férias',
         'Afastamento','Advertência','Desligamento','Outros')),
    Titulo VARCHAR(100) NOT NULL,
    Descricao VARCHAR(255),
    DataEvento DATE DEFAULT GETDATE(),
    DataRegistro DATETIME DEFAULT GETDATE(),
    UsuarioResponsavel VARCHAR(50),

    CONSTRAINT FK_Historico_Colaborador
        FOREIGN KEY (idColaborador)
        REFERENCES Colaboradores(idColaborador)
        ON DELETE CASCADE
);
GO

CREATE INDEX IX_Historico_Colaborador ON Historico(idColaborador);
GO


select * from Historico;


/* ================================
   PONTO BATIDO
================================ */
CREATE TABLE PontoBatido (
    idPonto INT IDENTITY(1,1) PRIMARY KEY,
    idColaborador INT NOT NULL,
    DiaSemana VARCHAR(15),
    Hora TIME NOT NULL,
    DataRegistro DATETIME DEFAULT GETDATE(),

    CONSTRAINT FK_Ponto_Colaborador
        FOREIGN KEY (idColaborador)
        REFERENCES Colaboradores(idColaborador)
);
GO
