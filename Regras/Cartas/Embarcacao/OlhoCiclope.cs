namespace Piratas.Servidor.Regras.Cartas.Embarcacao
{
    using Acoes.Resultante;
    using Acoes.Tipos;
    using Acoes;
    using Cartas.Tipos;
    using System.Collections.Generic;
    using System.Linq;
    using System;

    public class OlhoCiclope : Embarcacao
    {
        public OlhoCiclope(string nome) : base(nome) { }

        public override IEnumerable<Resultante> AplicarEfeito(Acao acao, Mesa mesa) => _aplicarEfeito(acao, mesa);

        internal IEnumerable<Resultante> _aplicarEfeito(Acao acao, Mesa mesa)
        {
            var realizador = acao.Realizador;

            Func<Acao, Jogador, IEnumerable<Resultante>> olharCartas = (acao, jogador) =>
                new OlharCartasJogador(acao, realizador, jogador.Mao.ObterTodas()) as IEnumerable<Resultante>;

            var outrosJogadoresMesa = mesa.Jogadores.Where(j => j != realizador).ToList();

            yield return new EscolherJogador(acao, realizador, outrosJogadoresMesa, olharCartas);
        }
    }
}