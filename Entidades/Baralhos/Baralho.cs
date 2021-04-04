namespace ServidorPiratas.Entidades.Baralhos
{
    using Cartas;
    using System.Collections.Generic;

    public abstract class Baralho
    {
        public Stack<Carta> Cartas { get; protected set; }
        
    }
    
}