namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Cartas;
    using Cartas.Tipos;
    using Enums;
    using Imediata;

    public class DescerCartasDuelo : BaseResultanteComListaEscolhas
    {
        public DescerCartasDuelo(
            Acao origem,
            Jogador realizador,
            Jogador alvo)
            : base(
                origem,
                realizador,
                TipoEscolha.Carta,
                alvo.Mao.ObterTodas<Duelo>().Select(c => c.Id).ToList(),
                alvo: alvo)
        {
        }

        public override List<Acao> AplicarRegra(Mesa mesa)
        {
            var acoesResultantes = new List<Acao>();

            foreach (string cartaDueloEscolhida in Escolhas)
            {
                Carta cartaDuelo = Alvo.Mao.ObterPorId(cartaDueloEscolhida);

                cartaDuelo.AplicarEfeito(this, mesa);
            }

            bool realizadorPossuiDueloSurpresa = Realizador.Mao.Possui<DueloSurpresa>();
            bool alvoPossuiDueloSurpresa = Alvo.Mao.Possui<DueloSurpresa>();

            var calcularResultadoDuelo = new CalcularResultadoDuelo(this, Realizador, Alvo);

            if (!realizadorPossuiDueloSurpresa && !alvoPossuiDueloSurpresa)
                acoesResultantes.Add(calcularResultadoDuelo);

            else
            {
                mesa.RegistrarImediataAposResultantes(calcularResultadoDuelo);

                if (realizadorPossuiDueloSurpresa)
                {
                    var descerCartasDueloSurpresa = new DescerCartasDueloSurpresa(this, Realizador);

                    acoesResultantes.Add(descerCartasDueloSurpresa);
                }

                if (alvoPossuiDueloSurpresa)
                {
                    var descerCartasDueloSurpresa = new DescerCartasDueloSurpresa(this, Alvo);

                    acoesResultantes.Add(descerCartasDueloSurpresa);
                }
            }

            return acoesResultantes;
        }
    }
}
