using RentACarApp.MobileUI.Validators.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentACarApp.MobileUI.Validators.Implementations
{
    public class RequiredValidator : IValidator
    {
        public string Message { get; set; } = "Ovo polje je obavezno.";

        public bool Check(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}
