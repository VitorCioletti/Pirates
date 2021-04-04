namespace ServidorPiratas.Entidades.Cartas.Tipos.ResolucaoImediata
{
    public abstract class ResolucaoImediata : Carta
    {
        public ResolucaoImediata(string nome) : base(nome) { }

        public abstract void AplicarRegra();
    }
}