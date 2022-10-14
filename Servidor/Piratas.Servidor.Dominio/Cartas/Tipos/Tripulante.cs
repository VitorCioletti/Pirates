namespace Piratas.Servidor.Dominio.Cartas.Tipos
{
    using System.Collections.Generic;
    using Acoes;
    using Acoes.Tipos;

    public abstract class Tripulante : Carta
    {
        public int Tiros { get; protected set; }

        public bool Afogavel { get; protected set; }

        public Tripulante()
        {
            Afogavel = true;
        }

        public override IEnumerable<Acao> AplicarEfeito(Acao acao, Mesa mesa) =>
            _aplicarEfeito(acao.Realizador.Campo);

        internal IEnumerable<Resultante> _aplicarEfeito(Campo campo)
        {
            campo.Adicionar(this);

            yield return null;
        }
    }
}
