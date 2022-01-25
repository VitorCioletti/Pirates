namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using Dominio;
    using System.Collections.Generic;
    using Tipos;

    public class CopiarPrimaria : Resultante
    {
        public Primaria Copiada { get; private set; }

        public CopiarPrimaria(Acao origem, Jogador realizador, Primaria copiada) : base(origem, realizador) =>
            Copiada = copiada;

        public override IEnumerable<Resultante> AplicarRegra(Mesa mesa) => Copiada.AplicarRegra(mesa);
    }
}
