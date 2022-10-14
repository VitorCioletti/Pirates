namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using System.Collections.Generic;
    using Cartas.Tipos;
    using Tipos;

    public class DescerCartasDueloSurpresa : Resultante
    {
        public List<DueloSurpresa> DuelosSurpresa { get; private set; }

        public DescerCartasDueloSurpresa(Acao origem, Jogador realizador) : base(origem, realizador, null)
        {
        }

        public override List<Acao> AplicarRegra(Mesa mesa)
        {
            DuelosSurpresa.ForEach(c => c.AplicarEfeito(this, mesa));

            return null;
        }
    }
}
