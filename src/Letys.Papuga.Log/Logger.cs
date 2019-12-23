using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letys.Parrot.Log
{
    public class Logger
    {
        public static void Error(string error)
        {
            using (StreamWriter writer = new StreamWriter("parrot.log",true))
            {
                writer.WriteLine(string.Format("{0:yyyy-MM-dd hh:mm:ss}: {1}",DateTime.Now, error));
            }
        }

        public static void Error(Exception ex)
        {
            using (StreamWriter writer = new StreamWriter("parrot.log", true))
            {
                writer.WriteLine(string.Format("{0:yyyy-MM-dd hh:mm:ss}: {1}", DateTime.Now, ex.ToString()));

                if (!string.IsNullOrEmpty(ex.Message))
                {
                    writer.WriteLine(string.Format("{0}", DateTime.Now, ex.Message));
                }

                if (!string.IsNullOrEmpty(ex.StackTrace))
                {
                    writer.WriteLine(string.Format("{0}", DateTime.Now, ex.StackTrace));
                }
            }
        }

    }
}
