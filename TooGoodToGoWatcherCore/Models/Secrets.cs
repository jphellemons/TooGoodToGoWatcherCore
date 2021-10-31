using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TooGoodToGoWatcherCore.Models
{
    public class Secrets
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string IftttEventName { get; set; }
        public string IftttKey { get; set; }
    }
}
