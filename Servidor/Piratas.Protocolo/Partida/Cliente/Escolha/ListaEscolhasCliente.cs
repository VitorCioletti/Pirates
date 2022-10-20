namespace Piratas.Protocolo.Partida.Cliente.Escolha
{
    using System.Collections.Generic;

    public class ListaEscolhasCliente : BaseEscolha
    {
        public List<string> Escolhas { get; private set; }

        public ListaEscolhasCliente(TipoEscolha tipo, List<string> escolhas) : base(tipo)
        {
            Escolhas = escolhas;
        }
    }
}
