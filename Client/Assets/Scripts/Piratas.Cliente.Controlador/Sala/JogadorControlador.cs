namespace Piratas.Cliente.Controlador.Sala
{
    using TMPro;
    using UnityEngine;

    public class JogadorControlador : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _nomeJogadorTexto;

        public void Init(string nomeJogador)
        {
            _nomeJogadorTexto.text = nomeJogador;
        }
    }
}
