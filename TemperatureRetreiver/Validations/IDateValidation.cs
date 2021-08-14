using System;
using System.Collections.Generic;
using System.Text;

namespace TemperatureRetreiver.Validations
{
    public interface IDateValidation
    {
        bool IsValid(string inputDate);
    }
}
