namespace ServidorPiratas.Regras.Cartas.Embarcacao
{
    using Acoes.Resultantes;
    using Acoes.Tipos;
    using Acoes;
    using Cartas.Tipos;
    using System.Linq;
    using System;

    public class OlhoDeCiclope : Embarcacao
    {
        public OlhoDeCiclope(string nome) : base(nome) { }

        public override Resultante AplicarEfeito(Acao acao, Mesa mesa) => _aplicarEfeito(acao.Realizador, mesa);

        internal Resultante _aplicarEfeito(Jogador realizador, Mesa mesa)
        {
            Func<Jogador, Resultante> olharCartas = (jogador) => 
                new OlharCartasJogador(realizador, jogador.Mao.ObterTodas());

            var outrosJogadoresMesa = mesa.Jogadores.Where(j => j != realizador).ToList();

            return new EscolherJogador(realizador, outrosJogadoresMesa, olharCartas);
        }
    }
}