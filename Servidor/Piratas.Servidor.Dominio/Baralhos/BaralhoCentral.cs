namespace Piratas.Servidor.Dominio.Baralhos
{
    using System;
    using Cartas;
    using System.Collections.Generic;
    using System.Linq;

    public class BaralhoCentral : BaseBaralho
    {
        public BaralhoCentral()
        {
            List<Carta> novasCartas = GeradorCartas.Gerar();

            IEnumerable<Carta> cartasEmbaralhadas = _embaralhar(novasCartas);

            Cartas = new LinkedList<Carta>(cartasEmbaralhadas);
        }

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

        private IEnumerable<Carta> _embaralhar(IEnumerable<Carta> cartas)
        {
            var random = new Random();

            List<Carta> cartasEmbaralhadas = cartas.OrderBy(_ => random.Next()).ToList();

            return cartasEmbaralhadas;
        }
    }
}
