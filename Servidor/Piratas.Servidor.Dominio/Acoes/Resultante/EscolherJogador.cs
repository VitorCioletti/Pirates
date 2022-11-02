namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Enums;

    public class EscolherJogador : BaseResultanteComListaEscolhas
    {
        private Func<BaseAcao, Jogador, List<BaseAcao>> _resultanteAposEscolha { get; set; }

        public EscolherJogador(
            BaseAcao origem,
            Jogador realizador,
            List<string> jogadoresOpcao,
            Func<BaseAcao, Jogador, List<BaseAcao>> resultanteAposEscolha)
            : base(
                origem,
                realizador,
                TipoEscolha.Jogador,
                jogadoresOpcao)
        {
            _resultanteAposEscolha = resultanteAposEscolha;
        }

        public override List<BaseAcao> AplicarRegra(Mesa mesa)
        {
            string escolha = Escolhas.First();

            Jogador jogadorEscolhido = mesa.Jogadores.First(j => j.Id.ToString() == escolha);

            return _resultanteAposEscolha(this, jogadorEscolhido);
        }
    }
}
