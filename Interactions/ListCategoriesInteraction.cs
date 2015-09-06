using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories;
using Entities;

namespace Interactions
{
    public class ListCategoriesInteraction<RepositoryType> :
        AbstractInteraction<Object, ResponseModels.CategoryListResponse, RepositoryType> where RepositoryType : ICategoryRepository
    {
        public ListCategoriesInteraction(RepositoryType repository) : base(null, repository)
        {
        }

        public override RepositoryType Repository { get; set; }

        public override void performAction()
        {
            List<string> categoryNameList = Repository.GetCategories().Select(x => x.Name).ToList();
            ResponseModel = new ResponseModels.CategoryListResponse(categoryNameList);
        }
    }

    public class ListCategoriesInteraction : ListCategoriesInteraction<EFRepositories.CategoryRepository> 
    { 
        public ListCategoriesInteraction() : 
            base(new EFRepositories.CategoryRepository())
        {
        }
    }

}
