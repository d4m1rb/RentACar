using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RentACarApp.MobileUI.Validators.Contracts
{
    public interface IErrorStyle
    {
        void ShowError(View view, string message);
        void RemoveError(View view);
    }
}
