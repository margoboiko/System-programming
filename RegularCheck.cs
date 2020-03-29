using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Replace
{
    class RegularCheck
    {
        public string replaceMask(string mask)
        {
            Regex rgx = new Regex(mask);
            return mask;
        }
    }
}