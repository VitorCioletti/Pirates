namespace Piratas.Servidor.Dominio.Acoes
{
    using System;
    using System.Collections.Generic;

    public abstract class Acao
    {
        public string Id { get; private set; }

        public DateTime DataHora { get; private set; }

        public Jogador Realizador { get; private set; }

        public Jogador Alvo { get; private set; }

        // TODO: init only setter? Qualquer um pode setar :(
        public int Turno { get; set; }

        protected Acao(Jogador realizador, Jogador alvo = null)
        {
            Id = GetType().ToString();
            DataHora = DateTime.UtcNow;

            Realizador = realizador;
            Alvo = alvo;
        }

        public abstract List<Acao> AplicarRegra(Mesa mesa);
    }
}
