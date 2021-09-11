namespace Piratas.Servidor.Dominio.Cartas.Tipos
{
    using System;

    public abstract class Embarcacao : Carta
    {
        public int Vida { get; private set; } = 3;
 
        public Embarcacao(string nome) : base(nome) { }

        public void Danificar(int dano)
        {
            if (Vida == 0)
                throw new Exception("Embarcacao está com a vida zerada.");

            Vida -= dano;
        }
    }
}