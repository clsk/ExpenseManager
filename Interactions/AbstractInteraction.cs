using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interactions
{
    public abstract class AbstractInteraction<RequestModelType, ResponseModelType, RepositoryType> 
    {
        public AbstractInteraction(RequestModelType requestModel, RepositoryType repository)
        {
            RequestModel = requestModel;
            Repository = repository;
        }

        public virtual RepositoryType Repository { get; set; }
        public RequestModelType RequestModel { get; set; }
        public ResponseModelType ResponseModel { get; set; }

        public abstract void performAction();
    }
}
