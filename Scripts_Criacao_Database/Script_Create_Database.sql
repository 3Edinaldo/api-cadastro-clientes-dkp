USE master

BEGIN TRY
	CREATE DATABASE DB_DKP
	SELECT 'Sucesso' AS [ExecutionStatus], 'Database criado com sucesso!' AS [Message]
END TRY
BEGIN CATCH
	SELECT
		'Erro' AS [ExecutionStatus]
		,ERROR_NUMBER() AS ErrorNumber  
		,ERROR_MESSAGE() AS ErrorMessage;
END CATCH