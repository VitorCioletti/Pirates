namespace Piratas.Servidor.Dominio.Excecoes.Acoes
{
    using Dominio.Acoes;
    using Dominio.Acoes.Tipos;

    public class ResultanteNaoEUmaOpcaoExcecao : BaseAcoesExcecao
    {
        public ResultanteNaoEUmaOpcaoExcecao(Acao acao, Resultante resultante)
            : base(acao, "resultante-nao-e-uma-opcao", $"Resultante \"{resultante.Id}\" não é uma opção.")
        {
        }
    }
}
