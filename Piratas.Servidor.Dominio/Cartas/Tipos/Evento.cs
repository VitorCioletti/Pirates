namespace Piratas.Servidor.Dominio.Cartas.Tipos
{
    public abstract class Evento : Carta
    {
        public Evento(string nome) : base(nome) { }
    }
}