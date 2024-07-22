namespace Piratas.Servidor.Dominio.Excecoes.Mao
{
    public class HandCardLimitReachedException : BaseHandException
    {
        public HandCardLimitReachedException() : base("hand-card-limit-reached", "Card limit reached.")
        {
        }
    }
}
