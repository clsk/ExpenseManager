using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interactions.ResponseModels
{
    public class CategoryListResponse : DefaultResponse
    {
        public CategoryListResponse(Error error) : base(error)
        {
            Categories = null;
        }

        public CategoryListResponse(DefaultResponse baseResponse)
            : base(baseResponse.Error)
        {
            Categories = null;
        }

        public CategoryListResponse(List<string> categories) : base(null)
        {
            Categories = categories;
        }

        public List<string> Categories;
    }
}
