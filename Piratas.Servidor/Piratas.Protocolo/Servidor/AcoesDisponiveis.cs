namespace Piratas.Protocolo.Servidor
{
    using System.Collections.Generic;
    using Piratas.Servidor.Dominio.Acoes;

    public class AcoesDisponiveis
    {
        public List<Acao> Acoes { get; private set; }

        public AcoesDisponiveis(List<Acao> acoes)
        {
            Acoes = acoes;
        }
    }
}
