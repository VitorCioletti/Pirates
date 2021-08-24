namespace Piratas.Servidor.Regras.Acoes.Primaria
{
    using Acoes.Resultante;
    using Cartas.Duelo;
    using Cartas.Tipos;
    using Regras;
    using System.Collections.Generic;
    using System;
    using Tipos;

    public class Duelar: Primaria
    {
        public Duelo CartaIniciadora { get; private set; }

        public Duelar(Jogador realizador, Jogador alvo, Duelo cartaIniciadora) : base(realizador, alvo) => 
            CartaIniciadora = cartaIniciadora;

        public override IEnumerable<Resultante> AplicarRegra(Mesa mesa)
        {
            if (CartaIniciadora is Timoneiro)
                throw new Exception($"\"{nameof(Timoneiro)}\" não pode iniciar um Duelo.");

            mesa.EntrarModoDuelo(Realizador, Alvo);

            yield return new DescerCartasDuelo(this, Alvo, Realizador);
        }
    }
}