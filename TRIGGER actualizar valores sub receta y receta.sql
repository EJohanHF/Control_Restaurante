CREATE TRIGGER [dbo].[trg_UpdateCabeceraSubReceta] ON [dbo].[SF_DETALLE_SUBRECETA_INSUMO]
AFTER UPDATE
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE SF_SUB_RECETA
	SET SR_COSTO = (
			SELECT sum(DSI_COSTO_INS)
			FROM SF_DETALLE_SUBRECETA_INSUMO
			WHERE DSI_ID_SUB_RECETA = (
					SELECT i.DSI_ID_SUB_RECETA
					FROM inserted i
					)
			)
	WHERE ID = (
			SELECT i.DSI_ID_SUB_RECETA
			FROM inserted i
			)
END;

GO;

CREATE TRIGGER [dbo].[trg_UpdateValueReceteaInsumo] 
   ON  [dbo].[SF_SUB_RECETA]
   AFTER UPDATE
AS 
BEGIN
	
	SET NOCOUNT ON;
	 UPDATE SF_RECETA
	SET RE_COSTO_RECETA = (
			SELECT i.SR_COSTO
			FROM inserted i
			)
	WHERE RE_ID_SUB_RECETA = (
			SELECT i.ID
			FROM inserted i
			)

   
END

GO;


ALTER TRIGGER [dbo].[trg_UpdatePrecioCostoInsumo] ON [dbo].[SF_INSUMO]
AFTER UPDATE
AS
BEGIN
	SET NOCOUNT ON;
	/Actualiza precio en las tabla recetas/
	UPDATE SF_RECETA
	SET RE_COSTO_RECETA = (
			SELECT i.INS_PRECIO
			FROM inserted i
			) * RE_CANT_INS
	WHERE RE_ID_INS = (
			SELECT i.ID
			FROM inserted i
			)
	/Actualiza precio en las tabla sub recetas/
	UPDATE SF_DETALLE_SUBRECETA_INSUMO
	SET DSI_COSTO_INS = (
			SELECT i.INS_PRECIO
			FROM inserted i
			) * DSI_CANT_INS
	WHERE DSI_ID_INS = (
			SELECT i.ID
			FROM inserted i
			)
END;

