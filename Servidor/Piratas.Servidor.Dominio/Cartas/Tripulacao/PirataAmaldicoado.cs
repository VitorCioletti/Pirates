namespace Piratas.Servidor.Dominio.Cartas.Tripulacao
{
    using Acoes.Tipos;
    using Acoes;
    using System.Collections.Generic;
    using Tipos;

    public class PirataAmaldicoado : Tripulante
    {
        public PirataAmaldicoado() => Tiros = -1;

        public override IEnumerable<Acao> AplicarEfeito(Acao acao, Mesa mesa) => _aplicarEfeito(acao.Alvo.Campo);
    }
}
