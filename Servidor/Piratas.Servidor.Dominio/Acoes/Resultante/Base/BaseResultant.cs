namespace Piratas.Servidor.Dominio.Acoes.Resultante.Base
{
    using Enums;

    public abstract class BaseResultant : BaseAction
    {
        protected BaseAction Origin { get; private set; }

        public ChoiceType ChoiceType { get; private set; }

        protected BaseResultant(
            BaseAction origin,
            Player starter,
            ChoiceType choiceType,
            Player target = null)
            : base(starter, target)
        {
            Origin = origin;
            ChoiceType = choiceType;
        }
    }
}
