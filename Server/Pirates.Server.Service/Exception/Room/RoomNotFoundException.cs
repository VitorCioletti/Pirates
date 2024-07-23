namespace Pirates.Server.Service.Exception.Room
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
