namespace Piratas.Protocolo.Cliente
{
    public class EscolhaCliente
    {
        public TipoEscolha Tipo { get; private set; }

        public string Escolhido { get; private set; }

        public EscolhaCliente(TipoEscolha tipo, string escolhido)
        {
            Tipo = tipo;
            Escolhido = escolhido;
        }
    }
}
