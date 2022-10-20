namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Cartas;
    using Cartas.Tipos;
    using Enums;

    public class DescerCartasDueloSurpresa : BaseResultanteComListaEscolhas
    {
        public DescerCartasDueloSurpresa(Acao origem, Jogador realizador)
            : base(
                origem,
                realizador,
                TipoEscolha.Carta,
                realizador.Mao.ObterTodas<Duelo>().Select(c => c.Id).ToList(),
                2)
        {
        }

        public override List<Acao> AplicarRegra(Mesa mesa)
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
