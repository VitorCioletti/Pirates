namespace ServidorPiratas.Entidades.Baralhos.Tipos
{
    using Baralhos;
    using Cartas;
    using System.Collections.Generic;

    public class Central : Baralho
    {
        public Central() => base.Cartas = _geraCartas();

        public Carta ObtemTopo() => Cartas.Pop();

        private Stack<Carta> _geraCartas() => new Stack<Carta>();
    }
}