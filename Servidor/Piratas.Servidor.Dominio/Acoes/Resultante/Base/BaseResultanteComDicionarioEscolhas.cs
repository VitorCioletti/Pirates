namespace Piratas.Servidor.Dominio.Acoes.Resultante.Base
{
    using System.Collections.Generic;
    using Enums;
    using Excecoes.Acoes;

    public abstract class BaseResultanteComDicionarioEscolhas : BaseResultanteComEscolha
    {
        protected Dictionary<string, string> Escolhas { get; private set; }

        public TipoEscolha TipoEscolhaChaves { get; private set; }

        public TipoEscolha TipoEscolhaOpcoes { get; private set; }

        public List<string> OpcoesChaves { get; private set; }

        public List<string> OpcoesValores { get; private set; }

        public int LimiteValoresPorChave { get; private set; }

        protected BaseResultanteComDicionarioEscolhas(
            BaseAcao origem,
            Jogador realizador,
            TipoEscolha tipoEscolha,
            TipoEscolha tipoEscolhaChaves,
            TipoEscolha tipoEscolhaOpcoes,
            int limiteValoresPorChave,
            List<string> opcoesValores,
            List<string> opcoesChaves,
            Jogador alvo = null)
            : base(
                origem,
                realizador,
                tipoEscolha,
                alvo)
        {
            OpcoesChaves = opcoesChaves;
            OpcoesValores = opcoesValores;

            TipoEscolhaChaves = tipoEscolhaChaves;
            TipoEscolhaOpcoes = tipoEscolhaOpcoes;

            LimiteValoresPorChave = limiteValoresPorChave;
        }

        public void PreencherEscolhas(Dictionary<string, string> idsEscolhas)
        {
            foreach ((string idChave, string idValor) in idsEscolhas)
            {
                if (!OpcoesChaves.Contains(idChave))
                    throw new EscolhaNaoEUmaOpcaoExcecao(this, idChave);

                if (!OpcoesValores.Contains(idValor))
                    throw new EscolhaNaoEUmaOpcaoExcecao(this, idValor);
            }

            Escolhas = idsEscolhas;
        }
    }
}
