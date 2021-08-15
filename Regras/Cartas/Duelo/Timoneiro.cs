namespace Piratas.Servidor.Regras.Cartas.Duelo
{
    using Acoes.Resultante;
    using Acoes.Tipos;
    using Acoes;
    using System;
    using Tipos;

    public class Timoneiro : Duelo
    {
        public Timoneiro(string nome) : base(nome) { }

        public override Resultante AplicarEfeito(Acao acao, Mesa mesa) => _aplicarEfeito(acao, mesa);

        internal Resultante _aplicarEfeito(Acao acao, Mesa mesa)
        {
            if (!(acao is ResponderDuelo))
                throw new Exception("Carta só pode ser usada em resposta a um duelo.");

            mesa.SairModoDuelo();

            return null;
        }
    }
}