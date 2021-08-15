namespace ServidorPiratas.Regras.Cartas.Embarcacao
{
    using Acoes.Resultante;
    using Acoes.Tipos;
    using Acoes;
    using Cartas.Tipos;
    using System.Linq;
    using System;

    public class OlhoDeCiclope : Embarcacao
    {
        public OlhoDeCiclope(string nome) : base(nome) { }

        public override Resultante AplicarEfeito(Acao acao, Mesa mesa) => _aplicarEfeito(acao, mesa);

        internal Resultante _aplicarEfeito(Acao acao, Mesa mesa)
        {
            var realizador = acao.Realizador;

            Func<Acao, Jogador, Resultante> olharCartas = (acao, jogador) => 
                new OlharCartasJogador(acao, realizador, jogador.Mao.ObterTodas());

            var outrosJogadoresMesa = mesa.Jogadores.Where(j => j != realizador).ToList();

            return new EscolherJogador(acao, realizador, outrosJogadoresMesa, olharCartas);
        }
    }
}