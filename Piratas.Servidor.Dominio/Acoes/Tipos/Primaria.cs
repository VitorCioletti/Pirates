namespace Piratas.Servidor.Dominio.Acoes.Tipos
{
    public abstract class Primaria : Acao
    {
        public Primaria(Jogador realizador, Jogador alvo = null) : base(realizador, alvo) {}
    }
}