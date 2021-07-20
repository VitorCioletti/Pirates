namespace ServidorPiratas.Regras
{
    using Cartas;
    using System.Collections.Generic;
    using System.Linq;
    using System;

    // TODO : Transformar em tipo Baralho?
    public class PilhaDescarte
    {
        private List<Carta> _cartas;

        public PilhaDescarte() => _cartas = new List<Carta>();

        public void InserirTopo(List<Carta> cartas) => cartas.ForEach(c => InserirTopo(c));

        public void Embaralhar() => throw new NotImplementedException();

        public void InserirTopo(Carta carta) => _cartas.Add(carta);

        public List<T> ObterTodas<T>() where T : Carta
        {
            var cartas = (List<T>)_cartas.Select(c => c is T);

            if (cartas.Count == 0)
               throw new Exception("Tipo de carta não existe na pilha de descarte.");
            
            return cartas;
        }
    }
}