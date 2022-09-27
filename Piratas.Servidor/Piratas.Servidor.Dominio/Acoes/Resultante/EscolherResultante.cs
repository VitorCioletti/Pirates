namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using System.Collections.Generic;
    using System.Linq;
    using Excecoes.Acoes;
    using Tipos;

    public class EscolherResultante : Resultante
    {
        public Resultante ResultanteEscolhida { get; private set; }

        public List<Resultante> ResultantesOpcao { get; private set; }

        public EscolherResultante(
            Acao origem, Jogador realizador, params Resultante[] resultantesOpcao) : base(origem, realizador) =>
            ResultantesOpcao = resultantesOpcao.ToList();

        public override IEnumerable<Resultante> AplicarRegra(Mesa mesa)
        {
            if (!ResultantesOpcao.Contains(ResultanteEscolhida))
                throw new ResultanteNaoEUmaOpcaoException(this, ResultanteEscolhida);

            return ResultanteEscolhida.AplicarRegra(mesa);
        }
    }
}
