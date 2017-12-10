using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DB.EntityFW.CommonTypes
{
    public class CommonSafeConvert
    {
        public static int ToInt(string val)
        {
            try
            {
                return Convert.ToInt32(val);
            }
            catch
            {
                return -1;
            }
        }
        public static int ToInt(object val)
        {
            try
            {
                return Convert.ToInt32(val);
            }
            catch
            {
                return -1;
            }
        }
    }
}
