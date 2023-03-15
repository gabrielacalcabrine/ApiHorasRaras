namespace Loucademy.HorasRaro.Domain.Utils
{
    public static class CodigoUtil
    {
        public static string GeraCodigo()
        {
            Random rnd = new Random();
            return rnd.Next(100000, 999999).ToString();
        }
    }
}
