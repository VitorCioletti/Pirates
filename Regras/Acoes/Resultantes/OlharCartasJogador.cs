namespace ServidorPiratas.Regras.Acoes.Resultantes
{
    using Cartas;
    using Regras;
    using System.Collections.Generic;
    using Tipos;

    public class OlharCartasJogador : Resultante
    {
        public List<Carta> Cartas { get; private set; }

        public OlharCartasJogador(Jogador realizador, List<Carta> cartas) : base(realizador) => Cartas = cartas;

        public override Resultante AplicarRegra(Mesa mesa) => null;
    }
}