namespace Piratas.Servidor.Dominio.Acoes.Primaria
{
    using System.Collections.Generic;
    using Cartas.Duelo;
    using Cartas.Tipos;
    using Excecoes.Acoes;
    using Resultante;
    using Tipos;

    public class Duelar : Primaria
    {
        public Duelo CartaIniciadora { get; private set; }

        public Duelar(Jogador realizador, Jogador alvo, Duelo cartaIniciadora) : base(realizador, alvo) =>
            CartaIniciadora = cartaIniciadora;

        public override IEnumerable<Acao> AplicarRegra(Mesa mesa)
        {
            if (CartaIniciadora is Timoneiro)
                throw new CartaProibidaIniciarDuelo(this, CartaIniciadora);

            mesa.EntrarModoDuelo(Realizador, Alvo);

            yield return new DescerCartasDuelo(this, Alvo, Realizador);
        }
    }
}
