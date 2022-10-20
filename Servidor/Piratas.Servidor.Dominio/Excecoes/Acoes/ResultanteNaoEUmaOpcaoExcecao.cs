namespace Piratas.Servidor.Dominio.Excecoes.Acoes
{
    using Dominio.Acoes;
    using Dominio.Acoes.Resultante.Base;

    public class ResultanteNaoEUmaOpcaoExcecao : BaseAcoesExcecao
    {
        public ResultanteNaoEUmaOpcaoExcecao(Acao acao, BaseResultante baseResultante)
            : base(acao, "resultante-nao-e-uma-opcao", $"Resultante \"{baseResultante.Id}\" não é uma opção.")
        {
        }
    }
}
