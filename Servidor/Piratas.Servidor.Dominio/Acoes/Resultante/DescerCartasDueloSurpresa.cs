namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using System.Collections.Generic;
    using Base;
    using Cartas;
    using Cartas.Extensao;
    using Cartas.Tipos;
    using Enums;

    public class DescerCartasDueloSurpresa : BaseResultanteComListaEscolhas
    {
        public DescerCartasDueloSurpresa(BaseAcao origem, Jogador realizador)
            : base(
                origem,
                realizador,
                TipoEscolha.Carta,
                realizador.Mao.ObterTodas<Duelo>().ObterIds(),
                2)
        {
        }

        public override List<BaseAcao> AplicarRegra(Mesa mesa)
        {
            foreach (string dueloSurpresaEscolhido in Escolhas)
            {
                Carta dueloSurpresa = Realizador.Mao.ObterPorId(dueloSurpresaEscolhido);

                dueloSurpresa.AplicarEfeito(this, mesa);
            }

            return null;
        }
    }
}
