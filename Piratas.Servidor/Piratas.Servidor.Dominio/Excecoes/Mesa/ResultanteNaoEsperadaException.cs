namespace Piratas.Servidor.Dominio.Excecoes.Mesa
{
    using Dominio.Acoes.Tipos;

    public class ResultanteNaoEsperadaException : BaseMesaException
    {
        public Resultante Resultante { get; private set; }

        public ResultanteNaoEsperadaException(Resultante resultante)
            : base("resultante-nao-esperada", $"Resultante \"{resultante.Id}\" não esperada.")

        {
            Resultante = resultante;
        }
    }
}
