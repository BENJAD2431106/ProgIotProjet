USE PROG3A25_Production_AllysonJad;
GO
CREATE PROCEDURE UP_ConnexionUtilisateur 
	@courriel NVARCHAR(255), 
	@motDePasse NVARCHAR(255),
	@noUtilisateur INT OUTPUT -- changer ça
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @sel UNIQUEIDENTIFIER

	IF NOT EXISTS(SELECT * FROM Utilisateurs WHERE @courriel=courriel)
		BEGIN
			SET @noUtiliateur = -1; 
		RETURN;
	END 
	ELSE 
		SET @noUtiliateur =(SELECT noUtilisateur FROM Utilisateurs WHERE @noUtiliateur = HASHBYTES('SHA2_512', @motDePasse + CAST(@sel AS NVARCHAR(36))));
END;

exec UP_ConnexionUtilisateur