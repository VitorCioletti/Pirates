namespace Piratas.Servidor.Servico.Excecoes
{
    using System;

    public class JogadorSemAcaoPendenteException : BaseServicoException
    {
        public JogadorSemAcaoPendenteException(Guid idJogador)
            : base("jogador-sem-acao-pendente", $" Jogador \"{idJogador}\" sem ação pendente.")
        {
        }
    }
}
