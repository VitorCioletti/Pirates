namespace ServidorPiratas.Entidades.Jogadas
{
    using System;

    public abstract class Jogada
    {

        public string Id { get; private set; }

        public DateTime DataHora { get; private set; }

        // melhorar nome
        public Jogador Jogador { get; private set; }

        public Jogada(Jogador jogador)
        {
            Id = Guid.NewGuid().ToString(); 
            DataHora = DateTime.UtcNow;

            Jogador = jogador;
        }

        public abstract void AplicarRegra(Mesa mesa);
    }
}