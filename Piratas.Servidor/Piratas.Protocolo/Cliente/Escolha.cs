namespace Piratas.Protocolo.Cliente
{
    public class Escolha
    {
        public TipoEscolha Tipo { get; private set; }

        public string Escolhido { get; private set; }

        public Escolha(TipoEscolha tipo, string escolhido)
        {
            Tipo = tipo;
            Escolhido = escolhido;
        }
    }
}
