namespace Piratas.Servidor.Dominio.Acoes.Resultante.Base
{
    using Enums;

    public abstract class BaseResultante : BaseAcao
    {
        protected BaseAcao Origem { get; private set; }

        public TipoEscolha TipoEscolha { get; private set; }

        protected BaseResultante(
            BaseAcao origem,
            Jogador realizador,
            TipoEscolha tipoEscolha,
            Jogador alvo = null)
            : base(realizador, alvo)
        {
            Origem = origem;
            TipoEscolha = tipoEscolha;
        }
    }
}
