namespace Piratas.Servidor.Dominio.Cartas.Tripulacao
{
    using Acoes.Tipos;
    using Acoes;
    using System.Collections.Generic;
    using Tipos;

    public class PirataFantasma : Tripulante
    {
        public PirataFantasma()
        {
            Tiros = 0;
            Afogavel = false;
        }

        public override IEnumerable<Resultante> AplicarEfeito(Acao acao, Mesa mesa) => _aplicarEfeito(acao.Alvo.Campo);
    }
}
