namespace Piratas.Servidor.Dominio.Cartas.Tripulacao
{
    using System.Collections.Generic;
    using Acoes;

    public class PirataNobre : BaseTripulante
    {
        public int Tesouros { get; private set; } = 1;

        public PirataNobre() => Tiros = 0;

        public override List<BaseAcao> AplicarEfeito(BaseAcao baseAcao, Mesa mesa)
        {
            Campo campoRealizador = baseAcao.Realizador.Campo;

            campoRealizador.Adicionar(this);

            return null;
        }
    }
}
