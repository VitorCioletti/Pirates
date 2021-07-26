namespace ServidorPiratas.Regras.Cartas.Tipos
{
    public abstract class Canhao : Duelo
    {
        public int Tiros { get; private set; }
 
        public Canhao(string nome, int tiros) : base(nome) { Tiros = tiros; }
    }
}