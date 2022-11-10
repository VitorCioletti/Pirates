namespace Piratas.Servidor.Dominio.Cartas.Tipos
{
    using System.Collections.Generic;
    using Acoes;

    public abstract class DueloSurpresa : Duelo
    {
        public int Tiros { get; private set; }

        protected DueloSurpresa() => Tiros = 1;

        public override List<BaseAcao> AplicarEfeito(BaseAcao acao, Mesa mesa)
        {
            Campo campoRealizador = acao.Realizador.Campo;

            campoRealizador.Adicionar(this);

            return null;
        }
    }
}
