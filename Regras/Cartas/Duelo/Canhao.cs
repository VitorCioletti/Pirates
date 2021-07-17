namespace ServidorPiratas.Regras.Cartas.Tipos
{
    public abstract class Canhao : Duelo
    {
        public Canhao(string nome) : base(nome) { Tiros = 1; }
    }
}