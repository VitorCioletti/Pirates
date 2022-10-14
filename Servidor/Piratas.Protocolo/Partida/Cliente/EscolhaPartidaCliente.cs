namespace Piratas.Protocolo.Partida.Cliente
{
    public class EscolhaPartidaCliente
    {
        public TipoEscolha Tipo { get; private set; }

        public string Escolhido { get; private set; }

        public EscolhaPartidaCliente(TipoEscolha tipo, string escolhido)
        {
            Tipo = tipo;
            Escolhido = escolhido;
        }
    }
}
