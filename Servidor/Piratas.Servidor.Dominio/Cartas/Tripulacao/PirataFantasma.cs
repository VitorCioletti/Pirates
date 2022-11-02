namespace Piratas.Servidor.Dominio.Cartas.Tripulacao
{
    using System.Collections.Generic;
    using Acoes;

    public class PirataFantasma : BaseTripulante
    {
        public PirataFantasma()
        {
            Tiros = 0;
            Afogavel = false;
        }

        public override List<Acao> AplicarEfeito(Acao acao, Mesa mesa)
        {
            Campo campoAlvo = acao.Alvo.Campo;

            campoAlvo.Adicionar(this);

            return null;
        }

    }
}
