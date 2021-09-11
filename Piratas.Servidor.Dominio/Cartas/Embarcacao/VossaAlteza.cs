namespace Piratas.Servidor.Dominio.Cartas.Embarcacao
{
    using Acoes.Resultante;
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

        public override IEnumerable<Resultante> AplicarEfeito(Acao acao, Mesa mesa) => 
            _aplicarEfeito(acao, mesa.Jogadores);

        internal IEnumerable<Resultante> _aplicarEfeito(Acao acao, List<Jogador> jogadoresNaMesa)
        {
            var realizador = acao.Realizador;

            var jogadoresOpcao = 
                jogadoresNaMesa.Where(j => j.Mao.QuantidadeCartas() >= _cartasMinimasNaMao && j != realizador).ToList();

            // TODO: Randômico ou permite escolha?
            Func<Acao, Jogador, IEnumerable<Resultante>> roubarCarta = (acao, jogadorAlvo) =>
                new RoubarCarta(acao, realizador, jogadorAlvo) as IEnumerable<Resultante>;

            yield return new EscolherJogador(acao, realizador, jogadoresOpcao, roubarCarta);
        }
    }
}