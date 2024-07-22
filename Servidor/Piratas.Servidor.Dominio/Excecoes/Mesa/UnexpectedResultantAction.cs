namespace Piratas.Servidor.Dominio.Excecoes.Mesa
{
    using Dominio.Acoes.Resultante.Base;

    public class UnexpectedResultantAction : BaseTableException
    {
        public BaseResultant BaseResultant { get; private set; }

        public UnexpectedResultantAction(BaseResultant baseResultant)
            : base("unexpected-resultant-action", $"Unexpected \"{baseResultant.Id}\".")

        {
            BaseResultant = baseResultant;
        }
    }
}
