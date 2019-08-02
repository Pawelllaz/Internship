using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    class TimeHelpers
    {
        public string GetTime(DateTime time)
        {
            string strTime = null;
            if (time != null)
                strTime = time.ToString("HH:mm tt");

            return strTime;
        }

        public string GetDate(DateTime date)
        {
            string strDate = null;
            if (date != null)
                strDate = date.ToString("MM/dd/yyyy");
            
            return strDate;
        }
    }
}
