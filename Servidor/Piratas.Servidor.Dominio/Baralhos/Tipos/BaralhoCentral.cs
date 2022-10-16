namespace Piratas.Servidor.Dominio.Baralhos.Tipos
{
    using Cartas;
    using System.Collections.Generic;

    public class BaralhoCentral : Baralho
    {
        public BaralhoCentral() => _gerarCartas();

        public Carta ObterTopo()
        {
            var ultimoNodo = Cartas.Last;

            if (ultimoNodo == null)
                return null;

            Cartas.RemoveLast();

            return ultimoNodo.Value;
        }

        public List<Carta> ObterTopo(int quantidade)
        {
            var cartas = new List<Carta>();

            for (var i = 0; i < quantidade; i++)
                cartas.Add(ObterTopo());

            return cartas;
        }

        private void _gerarCartas() => Cartas = new LinkedList<Carta>();
    }
}
