namespace ServidorPiratas.Regras
{
    using Cartas;
    using System.Collections.Generic;

    public class BaralhoCentral
    {
        private LinkedList<Carta> _cartas;

        public BaralhoCentral() => _cartas = _gerarCartas();

        public Carta ObterTopo()
        {
            var ultimoNodo = _cartas.Last;
            _cartas.RemoveLast();

            return ultimoNodo.Value;
        }

        public void InserirFundo(List<Carta> cartas) => cartas.ForEach(c => _cartas.AddFirst(c));

        public List<Carta> ObterTopo(int quantidade)
        {
            var cartas = new List<Carta>();

            for (int i = 0; i >= quantidade; i++)
                cartas.Add(ObterTopo());

            return cartas;
        }

        private LinkedList<Carta> _gerarCartas() => new LinkedList<Carta>();
    }
}