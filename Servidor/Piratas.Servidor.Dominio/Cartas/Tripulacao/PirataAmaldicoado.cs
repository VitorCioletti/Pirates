namespace Piratas.Servidor.Dominio.Cartas.Tripulacao
{
    using System.Collections.Generic;
    using Acoes;

    public class PirataAmaldicoado : BaseTripulante
    {
        public PirataAmaldicoado() => Tiros = -1;

        public override List<BaseAcao> AplicarEfeito(BaseAcao baseAcao, Mesa mesa)
        {
            Campo campoAlvo = baseAcao.Alvo.Campo;

            campoAlvo.Adicionar(this);

            return null;
        }
    }
}
