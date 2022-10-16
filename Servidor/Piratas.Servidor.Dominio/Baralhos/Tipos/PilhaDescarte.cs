namespace Piratas.Servidor.Dominio.Baralhos.Tipos
{
    using System.Collections.Generic;
    using System.Linq;
    using Cartas;
    using Excecoes;

    public class PilhaDescarte : Baralho
    {
        public PilhaDescarte() => Cartas = new LinkedList<Carta>();

        public List<T> ObterTodas<T>() where T : Carta
        {
            var cartas = (List<T>)Cartas.Select(c => c is T);

            if (cartas.Count == 0)
                throw new CartaNaoEncontradaNaPilhaDescarteExcecao(typeof(T).ToString());

            return cartas;
        }
    }
}
