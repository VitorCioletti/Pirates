namespace ServidorPiratas.Regras.Acoes.Tipos
{
    public abstract class Resultante : Acao
    {
        public Primaria Origem { get; set; }

        public Resultante(Jogador realizador, Jogador alvo = null) : base(realizador, alvo) {}
    }
}