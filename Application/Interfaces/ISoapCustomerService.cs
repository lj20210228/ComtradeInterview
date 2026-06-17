using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface ISoapCustomerService
    {
        Task<string?> GetCustomerNameByIdAsync(string customerId)   ;
    }
}
