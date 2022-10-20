namespace Piratas.Servidor.Dominio.Acoes.Resultante.Base
{
    public abstract class Primaria : Acao
    {
        public Primaria(Jogador realizador, Jogador alvo = null) : base(realizador, alvo)
        {

        }
    }
}
