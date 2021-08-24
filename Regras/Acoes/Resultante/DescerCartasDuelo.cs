namespace Piratas.Servidor.Regras.Acoes.Resultante
{
    using Cartas.Tipos;
    using Regras;
    using System.Collections.Generic;
    using Tipos;

    public class DescerCartasDuelo : Resultante
    {
        public List<Duelo> CartasResposta { get; private set; }

        public DescerCartasDuelo(Acao origem, Jogador realizador, Jogador alvo) : base(origem, realizador, alvo) {}

        public override IEnumerable<Resultante> AplicarRegra(Mesa mesa)
        {
            CartasResposta.ForEach(c => c.AplicarEfeito(this, mesa));

            var jogadores = mesa.Jogadores;

            foreach (var jogador in jogadores)
            {
                if (jogador.Mao.Possui<DueloSurpresa>())
                    yield return new DescerCartasDueloSurpresa(this, jogador);
            }
        }
    }
}