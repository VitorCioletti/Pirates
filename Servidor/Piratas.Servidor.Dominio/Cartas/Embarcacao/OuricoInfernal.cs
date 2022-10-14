namespace Piratas.Servidor.Dominio.Cartas.Embarcacao
{
    using Acoes.Tipos;
    using Acoes;
    using Cartas.Tipos;
    using System.Collections.Generic;

    public class OuricoInfernal : Embarcacao
    {
        public int Tiros { get; private set; } = 3;

        public override IEnumerable<Acao> AplicarEfeito(Acao acao, Mesa mesa) => null;
    }
}
