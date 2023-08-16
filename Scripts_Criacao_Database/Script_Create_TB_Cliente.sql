/* Object:  Table [dbo].[TB_Cliente]	*/
/* Script Date: 15/08/2023 04:42:23	*/
/* Creator:  Edinaldo Malheiros	*/

USE DB_DKP

DECLARE @DROP_AND_CREATE_TABLE BIT = 0;

BEGIN TRY
	BEGIN TRAN
		--EXECUTE usp_Erro; 
		IF OBJECT_ID('TB_Cliente') IS NULL OR @DROP_AND_CREATE_TABLE = 1
			BEGIN
				USE [DB_DKP]

				IF @DROP_AND_CREATE_TABLE = 1 BEGIN DROP TABLE [TB_Cliente] END
	
				SET ANSI_NULLS ON
	
				SET QUOTED_IDENTIFIER ON
	
				CREATE TABLE [dbo].[TB_Cliente](
					[Id] [int] IDENTITY(1,1) NOT NULL,
					[CNPJ] [varchar](50) NOT NULL,
					[RazaoSocial] [varchar](50) NOT NULL,
					[NomeFantasia] [varchar](50) NOT NULL,
					[DataInicio] [datetime] NOT NULL,
					[Status] [bit] NOT NULL
				 CONSTRAINT [PK_teste2] PRIMARY KEY CLUSTERED 
				(
					[Id] ASC
				)WITH (
					PAD_INDEX = OFF
					, STATISTICS_NORECOMPUTE = OFF
					, IGNORE_DUP_KEY = OFF
					, ALLOW_ROW_LOCKS = ON
					, ALLOW_PAGE_LOCKS = ON
					, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
				) ON [PRIMARY]
	
				SET IDENTITY_INSERT [dbo].[TB_Cliente] ON 
	
				INSERT [dbo].[TB_Cliente] ([Id], [CNPJ], [RazaoSocial], [NomeFantasia], [DataInicio], [Status]) VALUES (1, N'95650878000189', N'XPTO SA', N'XPTO', GETDATE(), 1)
	
				SET IDENTITY_INSERT [dbo].[TB_Cliente] OFF

				COMMIT TRAN

				SELECT * FROM [TB_Cliente]
			END
		ELSE
			BEGIN 
				ROLLBACK TRAN
				SELECT 'Erro' AS [ExecutionStatus], 'A tabela "TB_Cliente" já existe!' AS [Message]  UNION ALL
				SELECT 'Ajuda' AS [ExecutionStatus], 'Altere o valor da variável "@DROP_AND_CREATE_TABLE" para 1 (@DROP_AND_CREATE_TABLE = 1) e execute o script novamente.' AS [Message];
		END
END TRY
BEGIN CATCH
	ROLLBACK TRAN
	IF ERROR_NUMBER() = 3701
		BEGIN 
			SELECT 'Erro' AS [ExecutionStatus], 'Não foi possível excluir a tabela "TB_Cliente" pois ela não existe!' AS [Message] UNION ALL
			SELECT 'Ajuda' AS [ExecutionStatus], 'Altere o valor da variável "@DROP_AND_CREATE_TABLE" para 0 (@DROP_AND_CREATE_TABLE = 0) e execute o script novamente.' AS [Message];
		END
	ELSE
		BEGIN
			SELECT
				'Erro' AS [ExecutionStatus]
			   ,ERROR_NUMBER() AS ErrorNumber  
			   ,ERROR_MESSAGE() AS ErrorMessage;
	END
END CATCH