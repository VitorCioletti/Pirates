namespace Piratas.Servidor.Regras.Acoes.Resultante
{
    using Regras;
    using System.Collections.Generic;
    using Tipos;

    public class DanificarEmbarcacao : Resultante
    {
        public DanificarEmbarcacao(Acao origem, Jogador realizador) : base(origem, realizador) {}

        public override IEnumerable<Resultante> AplicarRegra(Mesa mesa)
        {
            Realizador.Campo.DanificarEmbarcacao();

            yield return null;
        }
    }
}