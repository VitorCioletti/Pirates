namespace ServidorPiratas.Regras.Acoes.Resultantes
{
    using Cartas.Duelo;
    using Cartas.Tipos;
    using Regras;
    using System;
    using Tipos;

    public class Duelar: Resultante
    {
        public Duelo CartaIniciadora { get; private set; }

        public Duelar(Acao origem, Jogador realizador, Jogador alvo, Duelo cartaIniciadora) : 
            base(origem, realizador, alvo) => CartaIniciadora = cartaIniciadora;

        public override Resultante AplicarRegra(Mesa mesa)
        {
            if (CartaIniciadora is Timoneiro)
                throw new Exception($"\"{nameof(Timoneiro)}\" não pode iniciar um Duelo.");

            mesa.EntrarModoDuelo(Realizador, Alvo);

            return new ResponderDuelo(this, Alvo, Realizador);
        }
    }
}