namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using Dominio;
    using System.Collections.Generic;
    using Base;

    public class DanificarEmbarcacao : BaseResultante
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
