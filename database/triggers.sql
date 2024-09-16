-- Trigger para atualização de produtos
DELIMITER //

CREATE TRIGGER after_product_update
AFTER UPDATE ON Product
FOR EACH ROW
BEGIN
    INSERT INTO LogProduct (
        ProductId,
        OldCodigo,
        NewCodigo,
        OldDescricao,
        NewDescricao,
        OldPreco,
        NewPreco,
        OldStatus,
        NewStatus,
        OldCodigoDepartamento,
        NewCodigoDepartamento,
        ChangedAt,
        ChangeType
    ) VALUES (
        OLD.Id,
        OLD.Codigo,
        NEW.Codigo,
        OLD.Descricao,
        NEW.Descricao,
        OLD.Preco,
        NEW.Preco,
        OLD.Status,
        NEW.Status,
        OLD.CodigoDepartamento,
        NEW.CodigoDepartamento,
        NOW(),
        'UPDATE'
    );
END//

DELIMITER ;

-- Trigger para exclusão de produtos
DELIMITER //

CREATE TRIGGER after_product_delete
AFTER DELETE ON Product
FOR EACH ROW
BEGIN
    INSERT INTO LogProduct (
        ProductId,
        OldCodigo,
        NewCodigo,
        OldDescricao,
        NewDescricao,
        OldPreco,
        NewPreco,
        OldStatus,
        NewStatus,
        OldCodigoDepartamento,
        NewCodigoDepartamento,
        ChangedAt,
        ChangeType
    ) VALUES (
        OLD.Id,
        OLD.Codigo,
        OLD.Codigo, 
        OLD.Descricao,
        OLD.Descricao,
        OLD.Preco,
        OLD.Preco,
        OLD.Status,
        OLD.Status,
        OLD.CodigoDepartamento,
        OLD.CodigoDepartamento,
        NOW(),
        'DELETE'
    );
END//

DELIMITER ;
