namespace ServidorPiratas.Regras.Baralhos.Tipos
{
    using Cartas;
    using System.Collections.Generic;

    public class BaralhoCentral : Baralho
    {
        public BaralhoCentral() => Cartas = _gerarCartas();

        public Carta ObterTopo()
        {
            var ultimoNodo = Cartas.Last;
            Cartas.RemoveLast();

            return ultimoNodo.Value;
        }
 
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