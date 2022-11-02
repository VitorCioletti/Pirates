namespace Piratas.Servidor.Dominio.Acoes.Resultante.Base
{
    using Enums;

    public abstract class BaseResultanteComEscolhaBooleana : BaseResultanteComEscolha
    {
        protected bool EscolhaBooleana { get; private set; }

        protected BaseResultanteComEscolhaBooleana(
            BaseAcao origem,
            Jogador realizador,
            TipoEscolha tipoEscolha,
            Jogador alvo = null)
            : base(
                origem,
                realizador,
                tipoEscolha,
                alvo)
        {
            EscolhaBooleana = false;
        }

        public void PreencherEscolha(bool escolhaBooleana)
        {
            EscolhaBooleana = escolhaBooleana;
        }
    }
}
