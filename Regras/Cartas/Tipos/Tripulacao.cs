namespace Piratas.Servidor.Regras.Cartas.Tipos
{
    using Acoes.Tipos;
    using Acoes;
    using Cartas.Tripulacao;
    using System;
    using System.Collections.Generic;

    public abstract class Tripulacao : Carta
    {
        public int Tiros { get; protected set; }

        public bool Afogavel { get; protected set; }

        public Tripulacao(string nome) : base(nome)
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
                    throw new Exception($"Não é possível descer \"{this}\" no próprio campo.");

                campoRealizador.Adicionar(this);
            }

            yield return null;
        }
    }
}