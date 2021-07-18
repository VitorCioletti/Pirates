namespace ServidorPiratas.Regras.Acoes.Resultantes
{
    using Cartas.Tipos;
    using Regras.Cartas.ResolucaoImediata;
    using Regras.Cartas;
    using Regras;
    using System.Collections.Generic;
    using System;
    using Tipos;

    public class ResponderDuelo : Resultante
    {
        public Jogador Vitorioso { get; private set; }

        public Jogador Perdedor { get; private set; }

        public Carta CartaResposta { get; private set; }

        private List<Type> CartasRespostaPermitidas;

        public ResponderDuelo(Jogador realizador, Jogador alvo) : base(realizador, alvo) 
        {
            CartasRespostaPermitidas = new List<Type>
            {
                typeof(Canhao),
                typeof(Timoneiro),
            };
        }

        public override Resultante AplicarRegra(Mesa mesa)
        {
            if (!CartasRespostaPermitidas.Contains(CartaResposta?.GetType()))
                throw new Exception($"Não é possível usar \"{CartaResposta.Nome}\" em resposta a um duelo.");

            CartaResposta?.AplicarEfeito(this, mesa);

            Vitorioso = Realizador.Campo.CalcularPontosDuelo() > Alvo.Campo.CalcularPontosDuelo() ? Realizador : Alvo;
            Perdedor = Vitorioso == Realizador ? Realizador : Alvo;

            Alvo.Campo.RemoverTodaTripulacao();
            Realizador.Campo.RemoverTodaTripulacao();

            mesa.EmDuelo = false;
            mesa.Duelistas = null;

            return new RoubarCarta(Vitorioso, Perdedor);
        }
    }
}