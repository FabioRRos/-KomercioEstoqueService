CREATE TABLE listas (
    id_lista SERIAL PRIMARY KEY,
    nome_lista VARCHAR(50) NOT NULL,
    data_criacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status_lista BOOLEAN DEFAULT TRUE -- TRUE = Aberta, FALSE = Finalizada
);

CREATE TABLE listadecompras (
    id_item_compra SERIAL PRIMARY KEY,
    id_lista INTEGER NOT NULL,
    descricao_produto VARCHAR(50) NOT NULL,
    codbar VARCHAR(15), -- Removi o NOT NULL caso o produto não tenha código
    quantidade INTEGER NOT NULL DEFAULT 1,
    status_item BOOLEAN DEFAULT FALSE, -- FALSE = Pendente, TRUE = No Carrinho
    obs VARCHAR(100),
    
    -- Chave Estrangeira: Se a lista for apagada, os itens somem juntos (CASCADE)
    CONSTRAINT fk_lista FOREIGN KEY (id_lista) REFERENCES listas(id_lista) ON DELETE CASCADE
);