namespace Pirates.Server.Service.Match
{
    using System;
    using System.Collections.Generic;
    using Exception.Match;
    using Protocol.Match.Client;
    using Protocol.Match.Server;

    public static class MatchServiceManager
    {
        private static Dictionary<Guid, MatchService> _ongoingMatches { get; }

        static MatchServiceManager()
        {
            _ongoingMatches = new Dictionary<Guid, MatchService>();
        }

        public static List<ServerMatchMessage> ProcessClientMessage(ClientMatchMessage clientMatchMessage)
        {
            Guid matchid = clientMatchMessage.RoomId;

            _checkIfMatchExists(matchid);

            MatchService matchService = _ongoingMatches[matchid];

            return matchService.ProcessClientMessage(clientMatchMessage);
        }

        public static Guid CreateMatch(List<string> players)
        {
            var newMatch = new MatchService(players);

            _ongoingMatches[newMatch.Id] = newMatch;

            return newMatch.Id;
        }

        public static void RemoveMatch(Guid matchId)
        {
            _checkIfMatchExists(matchId);

            _ongoingMatches.Remove(matchId);
        }

        private static void _checkIfMatchExists(Guid matchId)
        {
            if (!_ongoingMatches.ContainsKey(matchId))
                throw new MatchNotFoundException(matchId);
        }
    }
}
