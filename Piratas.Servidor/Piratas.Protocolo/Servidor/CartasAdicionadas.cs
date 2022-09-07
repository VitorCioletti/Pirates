namespace Piratas.Protocolo.Servidor
{
    using System;
    using System.Collections.Generic;

    public class CartasAdicionadas : Pacote
    {
        public List<Tuple<Enum, int>> Cartas { get; private set; }

        public CartasAdicionadas(Guid idJogador, List<Tuple<Enum, int>> cartas) : base(idJogador)
        {
            Cartas = cartas;
        }
    }
}
