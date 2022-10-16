namespace Piratas.Servidor.Servico.Partida.Excecoes
{
    using System;

    public class JogadorSemAcaoPendenteExcecao : BasePartidaExcecao
    {
        public JogadorSemAcaoPendenteExcecao(Guid idJogador)
            : base("jogador-sem-acao-pendente", $" Jogador \"{idJogador}\" sem ação pendente.")
        {
        }
    }
}
