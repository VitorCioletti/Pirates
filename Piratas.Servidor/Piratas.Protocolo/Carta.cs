namespace Piratas.Protocolo
{
    using System;

    public class Carta
    {
        public Type Tipo { get; private set; }
        public int Valor { get; private set; }

        public Carta(Type tipo, int valor)
        {
            Tipo = tipo;
            Valor = valor;
        }
    }
}
