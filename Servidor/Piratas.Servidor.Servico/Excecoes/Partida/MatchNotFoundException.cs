namespace Piratas.Servidor.Servico.Excecoes.Partida
{
    using System;

    public class MatchNotFoundException : BaseMatchException
    {
        public MatchNotFoundException(Guid matchId) :
            base("match-not-found", $"Match \"{matchId}\" not found.")
        {
        }
    }
}
