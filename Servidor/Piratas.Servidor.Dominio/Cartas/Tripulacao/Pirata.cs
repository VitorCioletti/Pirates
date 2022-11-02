namespace Piratas.Servidor.Dominio.Cartas.Tripulacao
{
    using System.Collections.Generic;
    using Acoes;

    public class Pirata : BaseTripulante
    {
        public override List<BaseAcao> AplicarEfeito(BaseAcao baseAcao, Mesa mesa)
        {
            Campo campoRealizador = baseAcao.Realizador.Campo;

            campoRealizador.Adicionar(this);

            return null;
        }
    }
}
