namespace Piratas.Protocolo.Servidor
{
    using Cartas;

    public class Evento
    {
        public LocalEvento Local { get; private set; }

        public Carta Carta { get; private set; }

        public bool Adicionado { get; private set; }

        public Evento(LocalEvento local, Carta carta, bool adicionado)
        {
            Local = local;
            Carta = carta;
            Adicionado = adicionado;
        }
    }
}
