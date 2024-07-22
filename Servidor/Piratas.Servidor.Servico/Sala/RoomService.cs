namespace Piratas.Servidor.Servico.Sala
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Excecoes.Sala;
    using Partida;

    public static class RoomService
    {
        private static Dictionary<Guid, List<string>> _openRooms { get; }

        static RoomService()
        {
            _openRooms = new Dictionary<Guid, List<string>>();
        }

        public static List<string> GetPlayers(Guid roomId)
        {
            return _openRooms[roomId];
        }

        public static Guid Create(string playerId)
        {
            bool isInARoom = _isInARoom(playerId);

            if (isInARoom)
                throw new PlayerIsAlreadyInARoom(playerId);

            var roomId = Guid.NewGuid();

            _openRooms[roomId] = new List<string> {playerId};

            return roomId;
        }

        public static Guid Exit(string playerId)
        {
            Guid roomId = _openRooms.FirstOrDefault(s => s.Value.Contains(playerId)).Key;

            if (roomId == Guid.Empty)
                throw new PlayerIsNotInTheRoom(playerId);

            List<string> room = _openRooms[roomId];

            room.Remove(playerId);

            return roomId;
        }

        public static void Enter(string playerId, Guid roomId)
        {
            bool isInARoom = _isInARoom(playerId);

            if (isInARoom)
                throw new PlayerIsAlreadyInARoom(playerId);

            bool roomExists = !_openRooms.ContainsKey(roomId);

            if (roomExists)
                throw new RoomNotFoundException(roomId);

            _openRooms[roomId].Add(playerId);
        }

        public static Guid StartMatch(string playerId, Guid roomId)
        {
            List<string> players = _openRooms.FirstOrDefault(s => s.Key == roomId).Value;

            if (players is null)
                throw new RoomNotFoundException(roomId);

            bool playerNotFound = players.FirstOrDefault(i => i == playerId) is null;

            if (playerNotFound)
                throw new PlayerIsNotInTheRoom(playerId);

            List<string> playersId = _openRooms[roomId];

            Guid matchId = MatchServiceManager.CreateMatch(playersId);

            _openRooms.Remove(roomId);

            return matchId;
        }

        private static bool _isInARoom(string playerId) => _openRooms.Values.Any(s => s.Contains(playerId));
    }
}
