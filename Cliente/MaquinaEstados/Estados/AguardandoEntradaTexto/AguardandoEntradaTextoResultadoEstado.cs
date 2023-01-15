namespace Piratas.Cliente.MaquinaEstados.Estados
{
    public class AguardandoEntradaTextoResultadoEstado : BaseResultadoEstado
    {
        public string Texto { get; private set; }

        public AguardandoEntradaTextoResultadoEstado(string texto)
        {
            Texto = texto;
        }
    }
}
