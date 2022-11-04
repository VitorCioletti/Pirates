namespace Piratas.Servidor.Dominio.Acoes.Resultante.Base
{
    using System.Collections.Generic;
    using Enums;
    using Excecoes.Acoes;

    public abstract class BaseResultanteComListaEscolhas : BaseResultante
    {
        protected List<string> Escolhas { get; private set; }

        public List<string> Opcoes { get; private set; }

        public int LimiteEscolhas { get; private set; }

        protected BaseResultanteComListaEscolhas(
            BaseAcao origem,
            Jogador realizador,
            TipoEscolha tipoEscolha,
            List<string> opcoes,
            int limiteEscolhas = 1,
            Jogador alvo = null)
            : base(
                origem,
                realizador,
                tipoEscolha,
                alvo)
        {
            LimiteEscolhas = limiteEscolhas;
            Opcoes = opcoes;
        }

        public void PreencherEscolhas(List<string> idsEscolhas)
        {
            if (idsEscolhas.Count > LimiteEscolhas)
                throw new LimiteEscolhaAtingidoExcecao(this, idsEscolhas.Count);

            foreach (string idEscolha in idsEscolhas)
            {
                if (!Opcoes.Contains(idEscolha))
                    throw new EscolhaNaoEUmaOpcaoExcecao(this, idEscolha);
            }

            Escolhas = idsEscolhas;
        }
    }
}
