namespace Piratas.Protocolo.Partida.Servidor.Escolha
{
    using System.Collections.Generic;

    public class ServerDictionaryChoices : BaseChoice
    {
        public ChoiceType KeyType { get; private set; }

        public ChoiceType ValueType { get; private set; }

        public List<string> KeysChoices { get; private set; }

        public List<string> ValuesChoices { get; private set; }

        public int LimitPerKey { get; private set; }

        public ServerDictionaryChoices(
            ChoiceType type,
            ChoiceType valueType,
            ChoiceType keyType,
            int limitPerKey,
            List<string> valuesChoices,
            List<string> keysChoices) : base(type)
        {
            LimitPerKey = limitPerKey;
            ValuesChoices = valuesChoices;
            KeysChoices = keysChoices;
            ValueType = valueType;
            KeyType = keyType;
        }
    }
}
