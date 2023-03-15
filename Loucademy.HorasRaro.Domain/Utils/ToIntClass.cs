using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loucademy.HorasRaro.Domain.Utils
{
    public static class ToIntClass
    {
        public static int? ToInt(this string value)
        {
            int result;
            if (int.TryParse(value, out result))
                return result;
            return null;
        }
    }
}
