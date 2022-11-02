namespace Piratas.Servidor.Dominio.Acoes.Resultante.Base
{
    using Enums;

    public abstract class BaseResultanteComEscolha : BaseResultante
    {
        public TipoEscolha TipoEscolha { get; private set; }

        protected BaseResultanteComEscolha(
            Acao origem,
            Jogador realizador,
            TipoEscolha tipoEscolha,
            Jogador alvo = null)
            : base(
                origem,
                realizador,
                alvo)
        {
            TipoEscolha = tipoEscolha;
        }
    }
}
