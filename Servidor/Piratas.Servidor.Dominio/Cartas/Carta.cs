namespace Piratas.Servidor.Dominio.Cartas
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Acoes;

    public abstract class Carta
    {
        public string Id { get; private set; }

        protected Carta()
        {
            string nomeTipo = GetType().Name;

            string id = Regex.Replace(nomeTipo, @"([a-z0–9])([A-Z])", "$1-$2").ToLowerInvariant();

            Id = id;
        }

        public abstract List<BaseAcao> AplicarEfeito(BaseAcao acao, Mesa mesa);
    }
}
