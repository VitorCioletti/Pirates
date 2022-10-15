namespace Piratas.Servidor.Servico.Partida.Excecoes
{
    using System;

    public class JogadorSemAcaoPendenteException : BasePartidaException
    {
        public JogadorSemAcaoPendenteException(Guid idJogador)
            : base("jogador-sem-acao-pendente", $" Jogador \"{idJogador}\" sem ação pendente.")
        {
        }
    }
}
