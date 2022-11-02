namespace Piratas.Servidor.Dominio.Acoes.Primaria
{
    public abstract class BasePrimaria : Acao
    {
        protected BasePrimaria(Jogador realizador, Jogador alvo = null) : base(realizador, alvo)
        {
        }
    }
}
