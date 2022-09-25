namespace Piratas.Servidor.Dominio.Excecoes.Mesa
{
    public class TurnoDeOutroJogadorException : MesaException
    {
        public TurnoDeOutroJogadorException(Jogador jogador)
            : base("turno-outro-jogador", $"Turno de outro jogador. Jogador \"{jogador.Id}\" tentou jogar.")
        {
        }
    }
}
