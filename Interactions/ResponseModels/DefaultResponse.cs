using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interactions.ResponseModels
{
    public class DefaultResponse
    {
        public DefaultResponse(Error? error)
        {
            Error = error;
            Success = (error == null);
        }

        public bool Success { get; set;}
        // If success == false, error must not be null
        public Error? Error { get; set;}
    }
}
