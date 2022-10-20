namespace Piratas.Servidor.Dominio.Acoes.Resultante.Base
{
    public abstract class BaseResultante : Acao
    {
        protected Acao Origem { get; private set; }

        protected BaseResultante(
            Acao origem,
            Jogador realizador,
            Jogador alvo = null)
            : base(realizador, alvo) => Origem = origem;
    }
}
