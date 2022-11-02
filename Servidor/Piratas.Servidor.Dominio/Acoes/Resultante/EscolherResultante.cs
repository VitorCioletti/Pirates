namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Enums;

    public class EscolherResultante : BaseResultanteComListaEscolhas
    {
        private List<BaseResultante> _resultantesOpcao { get; set; }

        public EscolherResultante(BaseAcao origem, Jogador realizador, params BaseResultante[] resultantesOpcao)
            : base(
                origem,
                realizador,
                TipoEscolha.Acao,
                resultantesOpcao.Select(r => r.Id).ToList())
        {
            _resultantesOpcao = resultantesOpcao.ToList();
        }

        public override List<BaseAcao> AplicarRegra(Mesa mesa)
        {
            string escolha = Escolhas.First();

            BaseResultante resultanteEscolhida = _resultantesOpcao.First(r => r.Id == escolha);

            return resultanteEscolhida.AplicarRegra(mesa);
        }
    }
}
