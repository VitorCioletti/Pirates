namespace Piratas.Servidor.Dominio.Cartas.Tripulacao
{
    using System.Collections.Generic;
    using Acoes;

    public class Pirata : BaseTripulante
    {
        public override List<Acao> AplicarEfeito(Acao acao, Mesa mesa)
        {
            Campo campoRealizador = acao.Realizador.Campo;

            campoRealizador.Adicionar(this);

            return null;
        }
    }
}
