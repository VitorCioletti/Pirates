namespace Piratas.Servidor.Servico.Excecoes.Partida
{
    using System;

    public class JogadorSemAcaoPendenteExcecao : BasePartidaExcecao
    {
        public JogadorSemAcaoPendenteExcecao(string idJogador)
            : base("jogador-sem-acao-pendente", $" Jogador \"{idJogador}\" sem ação pendente.")
        {
        }
    }
}
