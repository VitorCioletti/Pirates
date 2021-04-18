namespace ServidorPiratas.Entidades.Jogadas
{
    using System;

    public abstract class Jogada
    {

        public string Id { get; private set; }

        public DateTime DataHora { get; private set; }

        // melhorar nome
        public Jogador Realizador { get; private set; }

        public Jogador Alvo { get; private set; }

        public Jogada(Jogador realizador, Jogador alvo = null)
        {
            Id = Guid.NewGuid().ToString(); 
            DataHora = DateTime.UtcNow;

            Realizador = realizador;
            Alvo = alvo;
        }

        public abstract void AplicarRegra(Mesa mesa);
    }
}