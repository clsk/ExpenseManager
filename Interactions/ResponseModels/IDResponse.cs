using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interactions.ResponseModels
{
    public class IDResponse : DefaultResponse
    {
        public IDResponse(DefaultResponse baseResponse) : base(baseResponse.Error)
        {
            Id = 0;
        }

        public IDResponse(int id) : base(null)
        {
            Id = id;
        }

        public int Id { get; set;}  
    }
}
