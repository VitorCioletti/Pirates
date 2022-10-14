namespace Piratas.Servidor.Dominio.Cartas.Embarcacao
{
    using Acoes.Tipos;
    using Acoes;
    using Cartas.Tipos;
    using System.Collections.Generic;

    public class GuerrilhaNaval : Embarcacao
    {
        public int TirosAdicionais { get; private set; } = 2;

        public override List<Acao> AplicarEfeito(Acao acao, Mesa mesa) => null;
    }
}
