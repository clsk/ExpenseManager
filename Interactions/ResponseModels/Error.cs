using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interactions.ResponseModels
{
    public struct Error
    {
        public enum Codes
        {
            CATEGORY_ALREADY_EXISTS = 0,
            CATEGORY_BOGUS_NAME,
            CATEGORY_NAME_IS_EMPTY,
        }

        public Codes Code { get; set; }
        public string Message { get; set; }
    }
}
