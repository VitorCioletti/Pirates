namespace Piratas.Servidor.Dominio.Baralhos
{
    using Cartas;
    using System.Collections.Generic;
    using System;
    using System.Linq;

    public abstract class BaseBaralho
    {
        public int QuantidadeCartas => Cartas.Count;

        protected LinkedList<Carta> Cartas { get; set; }

        public void InserirTopo(Carta carta) => InserirTopo(new List<Carta> {carta});

        public void InserirTopo(List<Carta> cartas) => _inserir(cartas, true);

        public void InserirFundo(List<Carta> cartas) => _inserir(cartas, false);

        protected IEnumerable<Carta> Embaralhar(IEnumerable<Carta> cartas)
        {
            var random = new Random();

            List<Carta> cartasEmbaralhadas = cartas.OrderBy(_ => random.Next()).ToList();

            return cartasEmbaralhadas;
        }

        private void _inserir(List<Carta> cartas, bool topo)
        {
            foreach (Carta carta in cartas)
            {
                if (topo)
                    Cartas.AddFirst(carta);
                else
                    Cartas.AddLast(carta);
            }
        }
    }
}
