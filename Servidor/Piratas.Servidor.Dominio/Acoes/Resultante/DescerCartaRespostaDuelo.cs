namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using System.Collections.Generic;
    using Base;
    using Cartas;
    using Cartas.Duelo;
    using Cartas.Extensao;
    using Cartas.Tipos;
    using Enums;
    using Imediata;

    public class DescerCartaRespostaDuelo : BaseResultanteComListaEscolhas
    {
        public DescerCartaRespostaDuelo(
            BaseAcao origem,
            Jogador realizador,
            Jogador alvo)
            : base(
                origem,
                realizador,
                TipoEscolha.Carta,
                alvo.Mao.ObterTodas<Canhao>().ObterIds(),
                alvo: alvo)
        {
        }

        public override List<BaseAcao> AplicarRegra(Mesa mesa)
        {
            var acoesResultantes = new List<BaseAcao>();

            foreach (string cartaDueloEscolhida in Escolhas)
            {
                Carta cartaDuelo = Alvo.Mao.ObterPorId(cartaDueloEscolhida);

                cartaDuelo.AplicarEfeito(this, mesa);
            }

            bool realizadorPossuiDueloSurpresa = Realizador.Mao.Possui<DueloSurpresa>();
            bool alvoPossuiDueloSurpresa = Alvo.Mao.Possui<DueloSurpresa>();

            var calcularResultadoDuelo = new CalcularResultadoDuelo(Realizador, Alvo);

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
