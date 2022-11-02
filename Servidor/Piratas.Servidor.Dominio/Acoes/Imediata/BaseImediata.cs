namespace Piratas.Servidor.Dominio.Acoes.Imediata
{
    public abstract class BaseImediata : Acao
    {
        protected BaseImediata(Jogador realizador) : base(realizador)
        {
        }
    }
}
