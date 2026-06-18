using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface ICountryInfoService
    {
        Task<string?> GetCountryNameBy(string countryCode);

    }
}
