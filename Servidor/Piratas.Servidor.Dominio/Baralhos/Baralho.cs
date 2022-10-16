namespace Piratas.Servidor.Dominio.Baralhos
{
    using Cartas;
    using System.Collections.Generic;
    using System;

    public abstract class Baralho
    {
        protected LinkedList<Carta> Cartas { get; set; }

        public void Embaralhar() => throw new NotImplementedException();

        public void InserirTopo(Carta carta) => InserirTopo(new List<Carta> { carta });

        public void InserirTopo(List<Carta> cartas) => _inserir(cartas, true);

        public void InserirFundo(List<Carta> cartas) => _inserir(cartas, false);

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
