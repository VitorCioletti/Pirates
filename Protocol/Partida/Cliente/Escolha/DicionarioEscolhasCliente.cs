namespace Piratas.Protocolo.Partida.Cliente.Escolha
{
    using System.Collections.Generic;

    public class DicionarioEscolhasCliente : BaseEscolha
    {
        public Dictionary<string, string> Escolhas { get; private set; }

        public DicionarioEscolhasCliente(TipoEscolha tipo, Dictionary<string, string> escolhas) : base(tipo)
        {
            Escolhas = escolhas;
        }
    }
}
