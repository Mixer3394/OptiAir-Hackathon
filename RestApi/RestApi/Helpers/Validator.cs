using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RestApi.Helpers
{
    public static class Validator
    {
        public static bool MacValidate(string mAC)
        {
            string pattern = @"(^[a-fA-F0-9:]{17}$)|(^[a-fA-F0-9]{12}$)";
            Regex rgx = new Regex(pattern);

            string sentense = mAC.ToLower();

            var matches = rgx.Matches(sentense);

            if(matches.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
