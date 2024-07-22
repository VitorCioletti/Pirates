namespace Piratas.Servidor.Dominio.Acoes.Resultante.Base
{
    using System.Collections.Generic;
    using Enums;
    using Excecoes.Acoes;

    public abstract class BaseResultantWithDictionaryChoice : BaseResultant
    {
        protected Dictionary<string, string> Choices { get; private set; }

        public ChoiceType ChoiceKeyType { get; private set; }

        public ChoiceType ChoiceKeyOption { get; private set; }

        public List<string> KeysOptions { get; private set; }

        public List<string> ValueOptions { get; private set; }

        public int LimitValuePerKey { get; private set; }

        protected BaseResultantWithDictionaryChoice(
            BaseAction origin,
            Player starter,
            ChoiceType choiceType,
            ChoiceType choiceKeyType,
            ChoiceType choiceKeyOption,
            int limitValuePerKey,
            List<string> valueOptions,
            List<string> keysOptions,
            Player target = null)
            : base(
                origin,
                starter,
                choiceType,
                target)
        {
            KeysOptions = keysOptions;
            ValueOptions = valueOptions;

            ChoiceKeyType = choiceKeyType;
            ChoiceKeyOption = choiceKeyOption;

            LimitValuePerKey = limitValuePerKey;
        }

        public void FillChoices(Dictionary<string, string> choicesIds)
        {
            foreach ((string keyId, string keyValue) in choicesIds)
            {
                if (!KeysOptions.Contains(keyId))
                    throw new ChoiceIsNotAnOptionException(this, keyId);

                if (!ValueOptions.Contains(keyValue))
                    throw new ChoiceIsNotAnOptionException(this, keyValue);
            }

            Choices = choicesIds;
        }
    }
}
