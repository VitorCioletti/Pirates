namespace ServidorPiratas.Regras.Acoes.Tipos
{
    public abstract class Primarias : Acao
    {
        public Primarias(Jogador realizador, Jogador alvo = null) : base(realizador, alvo) {}
    }
}