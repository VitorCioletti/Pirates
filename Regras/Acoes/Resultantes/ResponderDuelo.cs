namespace ServidorPiratas.Regras.Acoes.Resultantes
{
    using Cartas.Tipos;
    using Regras;
    using System;
    using System.Collections.Generic;
    using Tipos;

    public class ResponderDuelo : Resultante
    {
        private int _limiteCartasResposta = 2;

        public Jogador Vitorioso { get; private set; }

        public Jogador Perdedor { get; private set; }

        public List<Duelo> CartasResposta { get; private set; }

        public ResponderDuelo(Jogador realizador, Jogador alvo) : base(realizador, alvo) {}

        public override Resultante AplicarRegra(Mesa mesa)
        {
            if (CartasResposta.Count > _limiteCartasResposta)
                throw new Exception("Limite de cartas resposta atigindo.");

            CartasResposta.ForEach(c => c.AplicarEfeito(this, mesa));

            Vitorioso = Realizador.Campo.CalcularPontosDuelo() > Alvo.Campo.CalcularPontosDuelo() ? Realizador : Alvo;
            Perdedor = Vitorioso == Realizador ? Realizador : Alvo;

            Perdedor.Campo.AfogarTripulacao();
            Perdedor.Campo.DanificarEmbarcacao();

            mesa.SairModoDuelo();

            return new RoubarCarta(Vitorioso, Perdedor);
        }
    }
}