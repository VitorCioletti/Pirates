namespace Piratas.Servidor.Dominio.Acoes.Resultante
{
    using System.Collections.Generic;
    using Imediata;
    using Primaria;

    public class CopiarPrimaria : BaseImediata
    {
        private BasePrimaria _copiada { get; set; }

        public CopiarPrimaria(Jogador realizador, BasePrimaria copiada) : base(realizador) =>
            _copiada = copiada;

        public override List<BaseAcao> AplicarRegra(Mesa mesa) => _copiada.AplicarRegra(mesa);
    }
}
