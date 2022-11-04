namespace Piratas.Servidor.Dominio.Cartas
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using Acoes;

    public abstract class Carta
    {
        public string Id { get; protected set; }

        protected Carta()
        {
            string nomeTipo = GetType().ToString();

            string id = Regex.Replace(nomeTipo, "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z0-9])", "-$1", RegexOptions.Compiled);

            Id = id.ToLower(CultureInfo.InvariantCulture);
        }

        public abstract List<BaseAcao> AplicarEfeito(BaseAcao baseAcao, Mesa mesa);
    }
}
