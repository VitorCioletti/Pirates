namespace Piratas.Protocolo.Servidor
{
    using System.Collections.Generic;

    public class Escolha
    {
        public TipoEscolha TipoEscolha { get; private set; }

        public List<string> Opcoes { get; private set; }

        public Escolha(TipoEscolha tipoEscolha, List<string> opcoes)
        {
            TipoEscolha = tipoEscolha;
            Opcoes = opcoes;
        }
    }
}
