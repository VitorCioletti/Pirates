namespace Piratas.Protocolo.Servidor.Partida
{
    public class Evento
    {
        public LocalEvento Local { get; private set; }

        public string IdCarta { get; private set; }

        public bool Adicionado { get; private set; }

        public Evento(LocalEvento local, string idCarta, bool adicionado)
        {
            Local = local;
            IdCarta = idCarta;
            Adicionado = adicionado;
        }
    }
}
