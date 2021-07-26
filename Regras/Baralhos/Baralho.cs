namespace ServidorPiratas.Regras.Baralhos
{
    using Cartas;
    using System.Collections.Generic;
    using System;

    public abstract class Baralho
    {
        protected LinkedList<Carta> Cartas;
    
        public void Embaralhar() => throw new NotImplementedException();

        public void InserirTopo(Carta carta) => InserirTopo(new List<Carta>(){ carta });

        public void InserirTopo(List<Carta> cartas) => _inserir(cartas, true);

        public void InserirFundo(List<Carta> cartas) => _inserir(cartas, false);
 
        private void _inserir(List<Carta> cartas, bool topo)
        {
            cartas.ForEach(c => 
                {
                    if (topo)
                        Cartas.AddFirst(c);
                    else
                        Cartas.AddLast(c);
                }
            );
        }
    }
}