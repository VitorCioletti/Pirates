namespace Piratas.Servidor.Dominio.Baralhos
{
    using System.Collections.Generic;
    using Cartas;

    public class BaralhoCentral : BaseBaralho
    {
        public void GerarCartas()
        {
            List<Carta> novasCartas = GeradorCartas.Gerar();

            IEnumerable<Carta> cartasEmbaralhadas = Embaralhar(novasCartas);

            Cartas = new LinkedList<Carta>(cartasEmbaralhadas);
        }

        public Carta ObterTopo()
        {
            LinkedListNode<Carta> ultimoNodo = Cartas.Last;

            if (ultimoNodo == null)
                return null;

            Cartas.RemoveLast();

            return ultimoNodo.Value;
        }

        public List<Carta> ObterTopo(int quantidade)
        {
            var cartas = new List<Carta>();

            for (int i = 0; i < quantidade; i++)
                cartas.Add(ObterTopo());

            return cartas;
        }
    }
}
