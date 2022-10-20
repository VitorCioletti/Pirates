namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using Dominio;
    using System.Collections.Generic;
    using Base;

    public class CopiarPrimaria : BaseResultante
    {
        public Primaria Copiada { get; private set; }

        public CopiarPrimaria(Acao origem, Jogador realizador, Primaria copiada) : base(origem, realizador) =>
            Copiada = copiada;

        public override List<Acao> AplicarRegra(Mesa mesa) => Copiada.AplicarRegra(mesa);
    }
}
