namespace Piratas.Protocolo
{
    using System;
    using BaseInternal;
    using Excecoes;
    using Newtonsoft.Json;

    public static class Parser
    {
        public static T Deserializar<T>(string json) where T : BaseMensagem
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception exception)
            {
                throw new DeserializacaoException(exception);
            }
        }

        public static string Serializar(BaseMensagem baseMensagem)
        {
            try
            {
                return JsonConvert.SerializeObject(baseMensagem);
            }
            catch (Exception exception)
            {
                throw new SerializacaoException(exception);
            }
        }
    }
}
