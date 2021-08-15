namespace ServidorPiratas.Regras.Acoes.Resultante
{
    using Cartas;
    using Regras;
    using System.Collections.Generic;
    using Tipos;

    public class OlharCartasJogador : Resultante
    {
        public List<Carta> Cartas { get; private set; }

        public OlharCartasJogador(Acao origem, Jogador realizador, List<Carta> cartas) 
            : base(origem, realizador) => Cartas = cartas;

        public override Resultante AplicarRegra(Mesa mesa) => null;
    }
}