namespace Piratas.Servidor.Servico.Excecoes.Sala
{
    using System;

    public class RoomNotFoundException : BaseRoomException
    {
        public RoomNotFoundException(Guid idSala) :
            base("room-not-found", $"Room \"{idSala}\" was not found.")
        {
        }
    }
}
