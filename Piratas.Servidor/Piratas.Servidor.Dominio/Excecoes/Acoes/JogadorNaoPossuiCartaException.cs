namespace Piratas.Servidor.Dominio.Excecoes.Acoes
{
    using Dominio.Acoes;
    using Dominio.Cartas;

    public class JogadorNaoPossuiCartaException : BaseAcoesException
    {
        public JogadorNaoPossuiCartaException(Acao acao, Jogador jogador, Carta carta)
            : base(acao, "jogador-nao-possui-carta", $"Jogador \"{jogador.Id}\" não possui carta \"{carta.Id}\".")
        {
        }
    }
}
