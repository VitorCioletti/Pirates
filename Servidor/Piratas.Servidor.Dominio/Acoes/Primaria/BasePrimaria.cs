namespace Piratas.Servidor.Dominio.Acoes.Primaria
{
    public abstract class BasePrimaria : BaseAcao
    {
        protected BasePrimaria(Jogador realizador, Jogador alvo = null) : base(realizador, alvo)
        {
        }
    }
}
