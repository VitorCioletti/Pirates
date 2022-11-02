namespace Piratas.Servidor.Dominio.Acoes.Imediata
{
    using System.Collections.Generic;
    using Resultante;

    public class CalcularResultadoDuelo : BaseImediata
    {
        public Jogador Vitorioso { get; private set; }

        public Jogador Perdedor { get; private set; }

        public CalcularResultadoDuelo(Jogador realizador) : base(realizador)
        {
        }

        public override List<Acao> AplicarRegra(Mesa mesa)
        {
            int pontosDueloRealizador = Realizador.Campo.CalcularPontosDuelo();
            int pontosDueloAlvo = Alvo.Campo.CalcularPontosDuelo();

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

            var roubarCarta = new RoubarCarta(this, Vitorioso, Perdedor);
            var acoesResultantes = new List<Acao> {roubarCarta};

            return acoesResultantes;
        }
    }
}
