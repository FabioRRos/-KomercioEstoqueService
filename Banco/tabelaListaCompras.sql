CREATE TABLE listadecompras (
    id_item_compra SERIAL PRIMARY KEY,
    id_lista INTEGER NOT NULL,
    descricao_produto VARCHAR(50) NOT NULL,
    codbar VARCHAR(15) NOT NULL,
    quantidade INTEGER NOT NULL,
    status_compra BOOLEAN DEFAULT FALSE,
    obs VARCHAR(50) NOT NULL
);