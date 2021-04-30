namespace ServidorPiratas.Entidades.Acoes
{
    using System;

    public abstract class Acao
    {

        public string Id { get; private set; }

        public DateTime DataHora { get; private set; }

        // TODO: melhorar nome
        public Jogador Realizador { get; private set; }

        public Jogador Alvo { get; private set; }

        public Acao(Jogador realizador, Jogador alvo = null)
        {
            Id = Guid.NewGuid().ToString(); 
            DataHora = DateTime.UtcNow;

            Realizador = realizador;
            Alvo = alvo;
        }

        public abstract void AplicaRegra(Mesa mesa);
    }
}