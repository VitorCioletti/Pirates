namespace Piratas.Servidor.Dominio.Excecoes.Acoes
{
    using Dominio.Acoes;

    public class JogadorNaoEUmaOpcaoExcecao : BaseAcoesExcecao
    {
        public JogadorNaoEUmaOpcaoExcecao(Acao acao, Jogador jogador)
            : base(acao, "jogador-nao-e-uma-opcao", $"Jogador \"{jogador.Id}\" não é uma opção.")
        {
        }
    }
}
