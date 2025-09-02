namespace Exame.Application.DTOs;

public record MovimentoManualDto(
    int DAT_MES,
    int DAT_ANO,
    int NUM_LANCAMENTO,
    string COD_PRODUTO,
    string COD_COSIF,
    decimal VAL_VALOR,
    string DES_DESCRICAO,
    DateTime DAT_MOVIMENTO,
    string COD_USUARIO,
    string DES_PRODUTO,
    string COD_CLASSIFICACAO
);
