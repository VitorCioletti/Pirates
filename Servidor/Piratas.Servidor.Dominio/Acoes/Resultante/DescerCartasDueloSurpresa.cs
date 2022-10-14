namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using Cartas.Tipos;
    using System.Collections.Generic;
    using Tipos;

    public class DescerCartasDueloSurpresa : Resultante
    {
        public List<DueloSurpresa> DuelosSurpresa { get; private set; }

        public DescerCartasDueloSurpresa(Acao origem, Jogador realizador) : base(origem, realizador, null)
        {

        }

        public override IEnumerable<Acao> AplicarRegra(Mesa mesa)
        {
            DuelosSurpresa.ForEach(c => c.AplicarEfeito(this, mesa));

            yield return null;
        }
    }
}
