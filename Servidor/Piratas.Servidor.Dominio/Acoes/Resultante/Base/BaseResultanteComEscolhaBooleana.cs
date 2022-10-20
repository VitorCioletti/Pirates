namespace Piratas.Servidor.Dominio.Acoes.Resultante.Base
{
    using Enums;

    public abstract class BaseResultanteComEscolhaBooleana : BaseResultante
    {
        public TipoEscolha TipoEscolha { get; private set; }

        protected bool EscolhaBooleana { get; private set; }

        protected BaseResultanteComEscolhaBooleana(
            Acao origem,
            Jogador realizador,
            TipoEscolha tipoEscolha,
            Jogador alvo = null)
            : base(origem, realizador, alvo)
        {
            TipoEscolha = tipoEscolha;
            EscolhaBooleana = false;
        }

        public void PreencherEscolha(bool escolhaBooleana)
        {
            EscolhaBooleana = escolhaBooleana;
        }
    }
}
