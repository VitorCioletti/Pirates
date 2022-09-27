namespace Piratas.Servidor.Dominio.Acoes.Imediata
{
    using System.Collections.Generic;
    using Resultante;
    using Tipos;

    public class CalcularResultadoDuelo : Imediata
    {
        public Jogador Vitorioso { get; private set; }

        public Jogador Perdedor { get; private set; }

        public CalcularResultadoDuelo(Acao origem, Jogador realizador, Jogador alvo) : base(origem, realizador, alvo)
        {
        }

        public override IEnumerable<Resultante> AplicarRegra(Mesa mesa)
        {
            var pontosDueloRealizador = Realizador.Campo.CalcularPontosDuelo();
            var pontosDueloAlvo = Alvo.Campo.CalcularPontosDuelo();

            if (pontosDueloRealizador > pontosDueloAlvo)
            {
                Vitorioso = Realizador;
                Perdedor = Alvo;
            }
            else
            {
                Vitorioso = Alvo;
                Perdedor = Realizador;
            }

            Vitorioso.Campo.RemoverCartasDuelo();
            Perdedor.Campo.RemoverCartasDuelo();

            Perdedor.Campo.AfogarTripulacao();
            Perdedor.Campo.DanificarEmbarcacao();

            mesa.SairModoDuelo();

            yield return new RoubarCarta(this, Vitorioso, Perdedor);
        }
    }
}
