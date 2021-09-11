namespace Piratas.Servidor.Dominio.Acoes.Tipos
{
    public abstract class Passiva : Acao
    {
        public Passiva(Jogador realizador) : base(realizador) {}
    }
}