namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using Cartas.Tipos;
    using Cartas;
    using Dominio;
    using System.Collections.Generic;
    using System;
    using Tipos;

    public class DescartarCarta: Resultante
    {
        public Carta CartaDescartada { get; private set; }

        public DescartarCarta(Acao origem, Jogador realizador, Jogador alvo) : base(origem, realizador, alvo) {}

        public override IEnumerable<Resultante> AplicarRegra(Mesa mesa)
        {
            if (CartaDescartada == null)
                yield return null;

            if (CartaDescartada.GetType() == typeof(Tesouro))
                throw new Exception("Não é possível descartar cartas tesouro.");

            Alvo.Mao.Remover(CartaDescartada);
            mesa.PilhaDescarte.InserirTopo(CartaDescartada);

            yield return null;
        }
    }
}