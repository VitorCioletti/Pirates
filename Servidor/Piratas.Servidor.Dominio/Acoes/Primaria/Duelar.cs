namespace Piratas.Servidor.Dominio.Acoes.Primaria
{
    using System.Collections.Generic;
    using Cartas.Duelo;
    using Cartas.Tipos;
    using Excecoes.Acoes;
    using Resultante;
    using Resultante.Base;

    public class Duelar : BasePrimaria
    {
        public Duelo CartaIniciadora { get; private set; }

        public Duelar(Jogador realizador, Jogador alvo, Duelo cartaIniciadora) : base(realizador, alvo) =>
            CartaIniciadora = cartaIniciadora;

        public override List<Acao> AplicarRegra(Mesa mesa)
        {
            if (CartaIniciadora is Timoneiro)
                throw new CartaProibidaIniciarDueloExcecao(this, CartaIniciadora);

            mesa.EntrarModoDuelo(Realizador, Alvo);

            if (!Alvo.Mao.Possui<Duelo>())
                return null;

            var descerCartasDuelo = new DescerCartasDuelo(this, Alvo, Realizador);
            var acoesResultantes = new List<Acao> {descerCartasDuelo};

            return acoesResultantes;
        }
    }
}
