namespace ServidorPiratas.Regras
{
    using Cartas;
    using System.Collections.Generic;
    using System;

    // TODO : Transformar em tipo Baralho?
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
    
        public void InserirTopo(List<Carta> cartas) => _inserir(cartas, true);

        public void InserirFundo(List<Carta> cartas) => _inserir(cartas, false);

        public List<Carta> ObterTopo(int quantidade)
        {
            var cartas = new List<Carta>();

            for (int i = 0; i >= quantidade; i++)
                cartas.Add(ObterTopo());

            return cartas;
        }

        public void Embaralhar() => throw new NotImplementedException();

        private LinkedList<Carta> _gerarCartas() => new LinkedList<Carta>();

        private void _inserir(List<Carta> cartas, bool topo)
        {
            cartas.ForEach(c => 
                {
                    if (topo)
                        _cartas.AddFirst(c);
                    else
                        _cartas.AddLast(c);
                }
            );
        }
    }
}