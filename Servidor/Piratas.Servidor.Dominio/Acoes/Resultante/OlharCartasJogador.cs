namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using Cartas;
    using Dominio;
    using System.Collections.Generic;
    using Tipos;

    public class OlharCartasJogador : Resultante
    {
        public List<Carta> Cartas { get; private set; }

        public OlharCartasJogador(Acao origem, Jogador realizador, List<Carta> cartas)
            : base(origem, realizador) => Cartas = cartas;

        public override List<Acao> AplicarRegra(Mesa mesa) => null;
    }
}
