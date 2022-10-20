namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using Cartas;
    using Dominio;
    using System.Collections.Generic;
    using Base;

    public class OlharCartasJogador : BaseResultante
    {
        private List<Carta> _cartas { get; set; }

        public OlharCartasJogador(Acao origem, Jogador realizador, List<Carta> cartas)
            : base(origem, realizador) => _cartas = cartas;

        public override List<Acao> AplicarRegra(Mesa mesa) => null;
    }
}
