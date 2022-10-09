namespace Piratas.Protocolo.Cliente.Partida
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
