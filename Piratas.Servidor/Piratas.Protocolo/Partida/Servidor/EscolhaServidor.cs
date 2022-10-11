namespace Piratas.Protocolo.Partida.Servidor
{
    using System.Collections.Generic;

    public class EscolhaServidor
    {
        public TipoEscolha TipoEscolha { get; private set; }

        public List<string> Opcoes { get; private set; }

        public EscolhaServidor(TipoEscolha tipoEscolha, List<string> opcoes)
        {
            TipoEscolha = tipoEscolha;
            Opcoes = opcoes;
        }
    }
}
