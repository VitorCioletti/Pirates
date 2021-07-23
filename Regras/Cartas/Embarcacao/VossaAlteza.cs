namespace ServidorPiratas.Regras.Cartas.Embarcacao
{
    using Acoes.Resultantes;
    using Acoes.Tipos;
    using Acoes;
    using Cartas.Tipos;
    using System.Collections.Generic;
    using System.Linq;
    using System;

    public class VossaAlteza : Embarcacao
    {
        private int _cartasMinimasNaMao = 5;
 
        public VossaAlteza(string nome) : base(nome) { }

        public override Resultante AplicarEfeito(Acao acao, Mesa mesa) => 
            _aplicarEfeito(acao.Realizador, mesa.Jogadores);

        internal Resultante _aplicarEfeito(Jogador realizador, List<Jogador> jogadoresNaMesa)
        {
            var jogadoresOpcao = 
                jogadoresNaMesa.Where(j => j.Mao.QuantidadeCartas() >= _cartasMinimasNaMao && j != realizador).ToList();

            // TODO: Randômico ou permite escolha?
            Func<Jogador, Resultante> roubarCarta = (jogadorAlvo) => new RoubarCarta(realizador, jogadorAlvo);

            return new EscolherJogador(realizador, jogadoresOpcao, roubarCarta);
        }
    }
}