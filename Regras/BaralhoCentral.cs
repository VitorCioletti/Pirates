namespace ServidorPiratas.Regras
{
    using Cartas;
    using System.Collections.Generic;

    public class BaralhoCentral
    {
        private Stack<Carta> _cartas;

        public BaralhoCentral() => _cartas = _geraCartas();

        public Carta ObtemTopo() => _cartas.Pop();

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