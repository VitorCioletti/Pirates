namespace Piratas.Servidor.Dominio.Acoes.Resultante.Base
{
    public abstract class BaseResultante : BaseAcao
    {
        protected BaseAcao Origem { get; private set; }

        protected BaseResultante(
            BaseAcao origem,
            Jogador realizador,
            Jogador alvo = null)
            : base(realizador, alvo) => Origem = origem;
    }
}
