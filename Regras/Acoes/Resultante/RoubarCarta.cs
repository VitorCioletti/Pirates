namespace ServidorPiratas.Regras.Acoes.Tipos.Resultante
{
    using Cartas;
    using Regras;
    using System;

    public class RoubarCarta: Acao
    {
        public string IdAcaoOrigem { get; private set; }

        public Carta CartaRoubada { get; private set; }

        public RoubarCarta(Jogador realizador, Jogador alvo) : base(realizador, alvo) {}

        public override void AplicaRegra(Mesa mesa)
        {
            if (Alvo.CartasNaMao.Remove(CartaRoubada))
                Realizador.CartasNaMao.Add(CartaRoubada);
            else
                throw new Exception(
                    $"Carta \"{CartaRoubada.Nome}\" não está na mão do jogador \"{Alvo.Id}\".");
        }
    }
}