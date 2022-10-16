namespace Piratas.Servidor.Dominio.Excecoes.Acoes
{
    using Dominio.Acoes;
    using Dominio.Cartas;

    public class JogadorNaoPossuiCartaExcecao : BaseAcoesExcecao
    {
        public JogadorNaoPossuiCartaExcecao(Acao acao, Jogador jogador, Carta carta)
            : base(acao, "jogador-nao-possui-carta", $"Jogador \"{jogador.Id}\" não possui carta \"{carta.Id}\".")
        {
        }
    }
}
