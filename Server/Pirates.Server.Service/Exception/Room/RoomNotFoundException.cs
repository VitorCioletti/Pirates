namespace Pirates.Server.Service.Exception.Room
{
    using System;

    public class RoomNotFoundException : BaseRoomException
    {
        public RoomNotFoundException(Guid roomId) :
            base("room-not-found", $"Room \"{roomId}\" was not found.")
        {
        }
    }
}
