namespace Piratas.Servidor.Dominio.Baralhos
{
    using Cartas;
    using System.Collections.Generic;

    public class BaralhoCentral : BaseBaralho
    {
        public BaralhoCentral() => _gerarCartas();

        public Carta ObterTopo()
        {
            LinkedListNode<Carta> ultimoNodo = Cartas.Last;

            if (ultimoNodo == null)
                return null;

            Cartas.RemoveLast();

            return ultimoNodo.Value;
        }

        public List<Carta> ObterTopo(int quantidade)
        {
            var cartas = new List<Carta>();

            for (int i = 0; i < quantidade; i++)
                cartas.Add(ObterTopo());

            return cartas;
        }

        private void _gerarCartas() => Cartas = new LinkedList<Carta>();
    }
}
