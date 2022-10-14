namespace Piratas.Servidor.Dominio.Cartas.Tripulacao
{
    using System.Collections.Generic;
    using Acoes;
    using Tipos;

    public class PirataAmaldicoado : Tripulante
    {
        public PirataAmaldicoado() => Tiros = -1;

        public override List<Acao> AplicarEfeito(Acao acao, Mesa mesa)
        {
            Campo campoAlvo = acao.Alvo.Campo;

            campoAlvo.Adicionar(this);

            return null;
        }
    }
}
