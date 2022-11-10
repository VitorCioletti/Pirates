namespace Piratas.Servidor.Dominio.Acoes.Imediata
{
    public abstract class BaseImediata : BaseAcao
    {
        protected BaseImediata(Jogador realizador, Jogador alvo = null) : base(realizador, alvo)
        {
        }
    }
}
