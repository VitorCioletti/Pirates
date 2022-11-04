namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Enums;

    public class EscolherAcao : BaseResultanteComListaEscolhas
    {
        private List<BaseAcao> _acoesOpcao { get; set; }

        public EscolherAcao(BaseAcao origem, Jogador realizador, params BaseAcao[] acoesOpcao)
            : base(
                origem,
                realizador,
                TipoEscolha.Acao,
                acoesOpcao.Select(r => r.Id).ToList())
        {
            _acoesOpcao = acoesOpcao.ToList();
        }

        public override List<BaseAcao> AplicarRegra(Mesa mesa)
        {
            string escolha = Escolhas.First();

            BaseAcao acaoEscolhida = _acoesOpcao.First(r => r.Id == escolha);

            return acaoEscolhida.AplicarRegra(mesa);
        }
    }
}
