namespace Piratas.Servidor.Servico.Excecoes.Partida
{
    public class ChoiceTypeNotFound : BaseMatchException
    {
        public ChoiceTypeNotFound(int choiceType) :
            base("choice-type-not-found", $"Choice type \"{choiceType}\" not found.")
        {
        }
    }
}
