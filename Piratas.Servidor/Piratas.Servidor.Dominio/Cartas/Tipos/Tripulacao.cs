namespace Piratas.Servidor.Dominio.Cartas.Tipos
{
    using Acoes.Tipos;
    using Acoes;
    using Cartas.Tripulacao;
    using Excecoes.Cartas;
    using System.Collections.Generic;

    public abstract class Tripulante : Carta
    {
        public int Tiros { get; protected set; }

        public bool Afogavel { get; protected set; }

        public Tripulante()
        {
            Afogavel = true;
        }

        public override IEnumerable<Resultante> AplicarEfeito(Acao acao, Mesa mesa) =>
            _aplicarEfeito(acao.Realizador.Campo, acao.Alvo?.Campo);

        internal IEnumerable<Resultante> _aplicarEfeito(Campo campoRealizador, Campo campoAlvo)
        {
            if (campoAlvo != null)
                campoAlvo.Adicionar(this);
            else
            {
                // TODO: Essa linha funciona feliz?
                if (this is PirataAmaldicoado || this is PirataFantasma)
                    throw new ImpossivelDescerException(this);

                campoRealizador.Adicionar(this);
            }

            yield return null;
        }
    }
}
