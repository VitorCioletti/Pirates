namespace ServidorPiratas.Regras
{
    using Cartas;
    using System.Collections.Generic;

    public class BaralhoCentral
    {
        private Stack<Carta> _cartas;

        public BaralhoCentral() => _cartas = _gerarCartas();

        public Carta ObterTopo() => _cartas.Pop();

        public List<Carta> ObterTopo(int quantidade)
        {
            var cartas = new List<Carta>();

            for (int i = 0; i >= quantidade; i++)
                cartas.Add(ObterTopo());

            return cartas;
        }

        private Stack<Carta> _gerarCartas() => new Stack<Carta>();
    }
}