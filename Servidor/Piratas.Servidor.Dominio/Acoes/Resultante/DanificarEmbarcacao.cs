namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using Dominio;
    using System.Collections.Generic;
    using Tipos;

    public class DanificarEmbarcacao : Resultante
    {
        public DanificarEmbarcacao(Acao origem, Jogador realizador) : base(origem, realizador)
        {
        }

        public override List<Acao> AplicarRegra(Mesa mesa)
        {
            Realizador.Campo.DanificarEmbarcacao();

            return null;
        }
    }
}
