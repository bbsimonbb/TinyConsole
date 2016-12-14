using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyConsole;
using TinyIoC;

namespace Helpers
{
    [RegistrationName("En")]
    public class Class1 : ISmoothInterface
    {
        public string Greet()
        {
            return "Can I help you with that?";
        }
    }
}
