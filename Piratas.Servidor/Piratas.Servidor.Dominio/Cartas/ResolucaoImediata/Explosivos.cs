namespace Piratas.Servidor.Dominio.Cartas.ResolucaoImediata
{
    using Acoes.Resultante;
    using Acoes.Tipos;
    using Acoes;
    using Baralhos.Tipos;
    using Cartas.Tipos;
    using System.Collections.Generic;

    public class Explosivos : ResolucaoImediata
    {
        private readonly int _cartasObtidas = 3;

        public Explosivos(string nome) : base(nome) { }

        public override IEnumerable<Resultante> AplicarEfeito(Acao acao, Mesa mesa) =>
            _aplicarEfeito(acao, mesa.BaralhoCentral, mesa.Jogadores);

        internal IEnumerable<Resultante> _aplicarEfeito(
            Acao acao, BaralhoCentral baralhoCentral, List<Jogador> jogadores)
        {
            var cartas = baralhoCentral.ObterTopo(_cartasObtidas);

            yield return new DistribuirCartas(acao, acao.Realizador, jogadores, cartas);
        }
    }
}
