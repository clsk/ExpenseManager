using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Repositories;
using Entities;

namespace Interactions
{
    public class AddCategoryInteraction<RepositoryType> :
        AbstractInteraction<RequestModels.AddCategory, ResponseModels.DefaultResponse, RepositoryType> 
        where RepositoryType : ICategoryRepository, new()
    {
        public AddCategoryInteraction(RequestModels.AddCategory requestModel, RepositoryType repository) : base(requestModel, repository)
        {
        }


        public override RepositoryType Repository { get; set; }

        public override void performAction()
        {
            if (RequestModel.Name.Length < 1)
            {
                ResponseModel = new ResponseModels.DefaultResponse(new ResponseModels.Error { Code = ResponseModels.Error.Codes.CATEGORY_NAME_IS_EMPTY, Message = "Category Name is Empty" });
                return;
            }
            else if (!Regex.IsMatch(RequestModel.Name, @"^[a-zA-Z0-9]+$"))
            {
                ResponseModel = new ResponseModels.DefaultResponse(new ResponseModels.Error{ Code = ResponseModels.Error.Codes.CATEGORY_BOGUS_NAME, Message = "Category Name is Empty"});
                return;
            }

            try
            {
                Repository.Add(new Category { Name = RequestModel.Name });
                ResponseModel = new ResponseModels.DefaultResponse(null);
            } catch (Repositories.Exceptions.CategoryAlreadyExistsException)
            {
                ResponseModel = new ResponseModels.DefaultResponse(new ResponseModels.Error { Code = ResponseModels.Error.Codes.CATEGORY_ALREADY_EXISTS, Message = "Category Already Exists" });
            }
        }
    }

    public class AddCategoryInteraction : AddCategoryInteraction<EFRepositories.CategoryRepository> 
    { 
        public AddCategoryInteraction(RequestModels.AddCategory requestModel) : 
            base(requestModel, new EFRepositories.CategoryRepository())
        {
        }
    }
}