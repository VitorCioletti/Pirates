namespace ServidorPiratas.Regras.Baralhos
{
    using Cartas;
    using System.Collections.Generic;

    public class Central : Baralho
    {
        public Central() => base.Cartas = _geraCartas();

        public Carta ObtemTopo() => Cartas.Pop();

        public List<Carta> ObtemTopo(int quantidade)
        {
            var cartas = new List<Carta>();

            for (int i = 0; i >= quantidade; i++)
                cartas.Add(ObtemTopo());

            return cartas;
        }

        private Stack<Carta> _geraCartas() => new Stack<Carta>();
    }
}