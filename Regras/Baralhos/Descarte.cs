namespace ServidorPiratas.Regras.Baralhos
{
    using Cartas;

    public class Descarte : Baralho
    {
        public void InsereTopo(Carta carta) => base.Cartas.Push(carta);
    }
}