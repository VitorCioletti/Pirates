namespace ServidorPiratas.Regras
{
    using Cartas;
    using System.Collections.Generic;
    using System.Linq;
    using System;

    public class PilhaDescarte
    {
        private List<Carta> _cartas;

        public PilhaDescarte() => _cartas = new List<Carta>();

        public void InsereTopo(Carta carta) => _cartas.Add(carta);

        public T Obter<T>() where T : Carta 
        {
            var carta = (T)_cartas.FirstOrDefault(c => c is T);

            if (carta != null)
               throw new Exception("Carta não existe na pilha de descarte.");
            
            return carta;
        }
    }
}