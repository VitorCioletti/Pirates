namespace Piratas.Servidor.Dominio.Excecoes.Mesa
{
    public class TurnoDeOutroJogadorExcecao : BaseMesaExcecao
    {
        public TurnoDeOutroJogadorExcecao(Jogador jogador)
            : base("turno-outro-jogador", $"Turno de outro jogador. Jogador \"{jogador.Id}\" tentou jogar.")
        {
        }
    }
}
