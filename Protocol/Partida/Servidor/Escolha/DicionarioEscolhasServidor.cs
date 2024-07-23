namespace Piratas.Protocolo.Partida.Servidor.Escolha
{
    using System.Collections.Generic;

    public class DicionarioEscolhasServidor : BaseEscolha
    {
        public TipoEscolha TipoChave { get; private set; }

        public TipoEscolha TipoValor { get; private set; }

        public List<string> OpcoesChaves { get; private set; }

        public List<string> OpcoesValores { get; private set; }

        public int LimiteValoresPorChave { get; private set; }

        public DicionarioEscolhasServidor(
            TipoEscolha tipo,
            TipoEscolha tipoValor,
            TipoEscolha tipoChave,
            int limiteValoresPorChave,
            List<string> opcoesValores,
            List<string> opcoesChaves) : base(tipo)
        {
            LimiteValoresPorChave = limiteValoresPorChave;
            OpcoesValores = opcoesValores;
            OpcoesChaves = opcoesChaves;
            TipoValor = tipoValor;
            TipoChave = tipoChave;
        }
    }
}
