using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendMulti.Model
{
    class ExtSettings
    {
        string To { get; set; }
        string CC { get; set; }
        string BCC { get; set; }
        string Subject { get; set; }
        string Comment { get; set; }

    }

    class EmailSettings
    {
       public string EmailTo { get; set; }
       public string EmailCC { get; set; }
       public string EmailBCC { get; set; }
       public string EmailSubject { get; set; }
       public string EmailBody { get; set; }

    }
}
