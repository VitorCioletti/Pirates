namespace ServidorPiratas.Regras.Baralhos.Tipos
{
    using Cartas;
    using System.Collections.Generic;
    using System.Linq;
    using System;

    public class PilhaDescarte : Baralho
    {
        public PilhaDescarte() => Cartas = new LinkedList<Carta>();

        public List<T> ObterTodas<T>() where T : Carta
        {
            var cartas = (List<T>)Cartas.Select(c => c is T);

            if (cartas.Count == 0)
               throw new Exception("Tipo de carta não existe na pilha de descarte.");
            
            return cartas;
        }
    }
}