namespace Piratas.Servidor.Regras.Cartas.Embarcacao
{
    using Acoes.Tipos;
    using Acoes;
    using Cartas.Tipos;
    using System.Collections.Generic;

    public class OuricoInfernal : Embarcacao
    {
        public int Tiros { get; private set; } = 3;

        public OuricoInfernal(string nome) : base(nome) { }

        public override IEnumerable<Resultante> AplicarEfeito(Acao acao, Mesa mesa) => null;
    }
}