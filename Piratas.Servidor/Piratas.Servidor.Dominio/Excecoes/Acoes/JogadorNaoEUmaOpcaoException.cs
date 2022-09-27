namespace Piratas.Servidor.Dominio.Excecoes.Acoes
{
    using Dominio.Acoes;

    public class JogadorNaoEUmaOpcaoException : BaseAcoesException
    {
        public JogadorNaoEUmaOpcaoException(Acao acao, Jogador jogador)
            : base(acao, "jogador-nao-e-uma-opcao", $"Jogador \"{jogador.Id}\" não é uma opção.")
        {
        }
    }
}
