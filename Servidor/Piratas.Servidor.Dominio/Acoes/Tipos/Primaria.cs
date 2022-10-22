namespace Piratas.Servidor.Dominio.Acoes.Resultante.Base
{
    public abstract class Primaria : Acao
    {
        protected Primaria(Jogador realizador, Jogador alvo = null) : base(realizador, alvo)
        {

        }
    }
}
