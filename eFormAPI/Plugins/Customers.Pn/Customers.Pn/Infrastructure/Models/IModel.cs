using System;
using System.Collections.Generic;
using System.Text;
using Customers.Pn.Infrastructure.Data;

namespace Customers.Pn.Infrastructure.Models
{
    public interface IModel
    {
        void Save(CustomersPnDbAnySql _dbContext);

        void Update(CustomersPnDbAnySql _dbContext);

        void Delete(CustomersPnDbAnySql _dbContext);

    }
}
