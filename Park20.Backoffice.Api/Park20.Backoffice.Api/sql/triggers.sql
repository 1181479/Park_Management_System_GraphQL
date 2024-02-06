GO
CREATE TRIGGER tr_ParkyWallet_AfterUpdate
ON dbo.ParkyWallet
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ParkyWalletId INT;
    DECLARE @OldAmount INT;
    DECLARE @NewAmount INT;

    -- Obter os valores antigos e novos
    SELECT @ParkyWalletId = Id, @OldAmount = Amount FROM deleted;
    SELECT @NewAmount = Amount FROM inserted;

    -- Calcular a diferença entre os valores antigos e novos
    DECLARE @Difference INT;
    SET @Difference = @NewAmount - @OldAmount;

    -- Determinar o tipo de movimento
    DECLARE @MovementType VARCHAR(250);
    SET @MovementType = CASE WHEN @Difference >= 0 THEN 'Inbound' ELSE 'Outbound' END;

    -- Inserir um registro na tabela ParkyWalletMovements
    INSERT INTO dbo.ParkyWalletMovements (Amount, Date, MovementType, ParkyWalletId)
    VALUES (ABS(@Difference), GETDATE(), @MovementType, @ParkyWalletId);
END;
GO
CREATE TRIGGER tr_ParkyWallet_AfterInsert
ON dbo.ParkyWallet
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ParkyWalletId INT;
    DECLARE @NewAmount INT;

    -- Obter os valores da nova wallet
    SELECT @ParkyWalletId = Id, @NewAmount = Amount FROM inserted;

    -- Inserir um registro na tabela ParkyWalletMovements
    INSERT INTO dbo.ParkyWalletMovements (Amount, Date, MovementType, ParkyWalletId)
    VALUES (@NewAmount, GETDATE(), 'Inbound', @ParkyWalletId);
END;