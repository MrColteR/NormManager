using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormManager.Models
{
    public class ParamTreeElement
    {
        public string ParamName { get; set; }

        public int LowerBound { get; set; }

        public int UpperBound { get; set; }

        public string ParamType { get; set; }

        public string ParamVarian { get; set; }
    }
}
