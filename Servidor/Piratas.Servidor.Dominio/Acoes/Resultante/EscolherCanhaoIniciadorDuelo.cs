namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using System.Collections.Generic;
    using Base;
    using Cartas.Duelo;
    using Cartas.Tipos;
    using Enums;
    using Imediata;

    public class EscolherCanhaoIniciadorDuelo : BaseResultanteComListaEscolhas
    {
        public EscolherCanhaoIniciadorDuelo(
            BaseAcao origem,
            Jogador realizador,
            TipoEscolha tipoEscolha,
            List<string> opcoes)
            : base(
                origem,
                realizador,
                tipoEscolha,
                opcoes)
        {
        }

        public override List<BaseAcao> AplicarRegra(Mesa mesa)
        {
            var canhaoIniciador = (Canhao)Realizador.Mao.ObterPorId(Opcoes[0]);

            canhaoIniciador.AplicarEfeito(this, mesa);

            mesa.EntrarModoDuelo(Realizador, Alvo);

            BaseAcao proximaAcao = !Alvo.Mao.Possui<Duelo>()
                ? new CalcularResultadoDuelo(Realizador, Alvo)
                : new DescerCartaRespostaDuelo(this, Alvo, Realizador);

            var acoesResultantes = new List<BaseAcao> {proximaAcao};

            return acoesResultantes;
        }
    }
}
