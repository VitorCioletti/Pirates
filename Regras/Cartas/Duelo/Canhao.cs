namespace ServidorPiratas.Regras.Cartas.Tipos
{
    public abstract class Canhao : Duelo
    {
        public Canhao(string nome, int tiros) : base(nome) { Tiros = tiros; }
    }
}