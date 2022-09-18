namespace Piratas.Servidor.Dominio.Cartas.Embarcacao
{
    using Acoes.Resultante;
    using Acoes.Tipos;
    using Acoes;
    using Cartas.Tipos;
    using System.Collections.Generic;
    using System.Linq;

    public class VossaAlteza : Embarcacao
    {
        private readonly int _cartasMinimasNaMao = 5;

        public override IEnumerable<Resultante> AplicarEfeito(Acao acao, Mesa mesa) =>
            _aplicarEfeito(acao, mesa.Jogadores);

        internal IEnumerable<Resultante> _aplicarEfeito(Acao acao, List<Jogador> jogadoresNaMesa)
        {
            var realizador = acao.Realizador;

            var jogadoresOpcao =
                jogadoresNaMesa.Where(j => j.Mao.QuantidadeCartas() >= _cartasMinimasNaMao && j != realizador).ToList();

            // TODO: Randômico ou permite escolha?
            IEnumerable<Resultante> roubarCarta(Acao acao, Jogador alvo)
            {
                var roubarCarta = new RoubarCarta(acao, realizador, alvo) as IEnumerable<Resultante>;

                return roubarCarta;
            }

            yield return new EscolherJogador(acao, realizador, jogadoresOpcao, roubarCarta);
        }
    }
}
