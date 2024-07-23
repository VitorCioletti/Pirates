namespace Piratas.Cliente.Servico
{
    using System;

    public static class SalaServico
    {
        public static void CriarSala()
        {
            SignalRServico.CriarSala();
        }

        public static void EntrarSala(string idSala)
        {
            Guid guidIdSala = Guid.Parse(idSala);

            SignalRServico.EntrarSala(guidIdSala);
        }

        public static void SairSala()
        {
            SignalRServico.SairSala();
        }

        public static void IniciarPartida(Guid idSala)
        {
            SignalRServico.IniciarPartida(idSala);
        }
    }
}
