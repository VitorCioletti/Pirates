namespace ServidorPiratas.Regras.Acoes.Primarias
{
    using System;
    using Acoes.Resultantes;
    using Cartas.Duelo;
    using Cartas.Tipos;
    using Regras;
    using Tipos;

    public class Duelar: Primaria
    {
        public Duelo CartaIniciadora { get; private set; }

        public Duelar(Jogador realizador, Jogador alvo) : base(realizador, alvo) {}

        public override Resultante AplicarRegra(Mesa mesa)
        {
            if (CartaIniciadora is Timoneiro)
                throw new Exception($"\"{nameof(Timoneiro)}\" não pode iniciar um Duelo.");

            mesa.EntrarModoDuelo(Realizador, Alvo);

            return new ResponderDuelo(this, Alvo, Realizador);
        }
    }
}