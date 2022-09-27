namespace Piratas.Servidor.Dominio.Excecoes.Acoes
{
    using Dominio.Acoes;
    using Dominio.Acoes.Tipos;

    public class ResultanteNaoEUmaOpcaoException : BaseAcoesException
    {
        public ResultanteNaoEUmaOpcaoException(Acao acao, Resultante resultante)
            : base(acao, "resultante-nao-e-uma-opcao", $"Resultante \"{resultante.Id}\" não é uma opção.")
        {
        }
    }
}
