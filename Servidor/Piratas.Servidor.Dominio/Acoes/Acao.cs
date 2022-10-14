namespace Piratas.Servidor.Dominio.Acoes
{
    using System.Collections.Generic;
    using System;

    public abstract class Acao
    {
        public string Id { get; private set; }

        public DateTime DataHora { get; private set; }

        // TODO: melhorar nome
        public virtual Jogador Realizador { get; private set; }

        public Jogador Alvo { get; private set; }

        // TODO: init only setter? Qualquer um pode setar :(
        public int Turno { get; set; }

        public Acao()
        {
            Id = string.Empty;
            DataHora = DateTime.MinValue;

            Realizador = null;
            Alvo = null;
        }

        public Acao(Jogador realizador, Jogador alvo = null) : this()
        {
            Id = Guid.NewGuid().ToString();
            DataHora = DateTime.UtcNow;

            Realizador = realizador;
            Alvo = alvo;
        }

        public abstract List<Acao> AplicarRegra(Mesa mesa);
    }
}
