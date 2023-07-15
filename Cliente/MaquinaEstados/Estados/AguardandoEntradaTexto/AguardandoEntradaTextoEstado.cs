namespace Piratas.Cliente.MaquinaEstados.Estados
{
    using System;

    public class AguardandoEntradaTextoEstado : BaseEstado
    {
        private string _textoDigitado;

        private readonly string _mensagemInicial;

        public AguardandoEntradaTextoEstado(
            string mensagemInicial,
            MaquinaEstados maquinaEstados) : base(maquinaEstados)
        {
            _mensagemInicial = mensagemInicial;
        }

        public override void Inicializar()
        {
            Console.WriteLine(_mensagemInicial);

            Console.WriteLine("Aguardando entrada de texto...");
        }

        public override BaseResultadoEstado Limpar()
        {
            return new AguardandoEntradaTextoResultadoEstado(_textoDigitado);
        }

        public override void AoReceberTexto(string texto)
        {
            if (texto is null || texto == String.Empty)
                return;

            _textoDigitado = texto;

            Remover();
        }
    }
}
