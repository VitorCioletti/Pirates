namespace Piratas.Servidor.Regras.Acoes
{
    using System;

    public abstract class Acao
    {
        public string Id { get; private set; }

        public DateTime DataHora { get; private set; }

        // TODO: melhorar nome
        public Jogador Realizador { get; private set; }

        public Jogador Alvo { get; private set; }

        // TODO: init only setter? Qualquer um pode setar :(
        public int Turno { get; set; }

        public Acao(Jogador realizador, Jogador alvo = null)
        {
            Id = Guid.NewGuid().ToString(); 
            DataHora = DateTime.UtcNow;

            Realizador = realizador;
            Alvo = alvo;
        }

        public abstract Tipos.Resultante AplicarRegra(Mesa mesa);
    }
}