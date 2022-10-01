namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using Cartas;
    using Dominio;
    using System.Collections.Generic;
    using Tipos;

    public class RoubarCarta : Resultante
    {
        public Carta CartaRoubada { get; private set; }

        public RoubarCarta(Acao origem, Jogador realizador, Jogador alvo) : base(origem, realizador, alvo)
        {

        }

        public override IEnumerable<Acao> AplicarRegra(Mesa mesa)
        {
            Realizador.Mao.Adicionar(CartaRoubada);
            Alvo.Mao.Remover(CartaRoubada);

            yield return null;
        }
    }
}
