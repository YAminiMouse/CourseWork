using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HM2.View;
using HM2.View.Admin;
using HM2.ViewModel;

namespace DAL.AdditionalEntities
{
    public class WindowsBuilder
    {
        private WindowContext _windowContext;
        public WindowContext WindowContext
        {
            set
            {
                _windowContext = value;
            }
        }
        public WindowsBuilder() 
        { 
            
        }

        public object Build(string windowId , OnWindowClose onWindowClose = null)
        {
            switch(windowId)
            {
                case "MAIN_WINDOW":
                {
                    return new MainWindow(_windowContext);
                }
                case "ADMIN_WINDOW":
                {
                    return new AdminWindow(_windowContext);
                }
                case "BOOKING_PAGE":
                {
                    return new HM2.View.Pages.Booking(_windowContext , onWindowClose);
                }
                case "PERSONAL_ACCOUNT_PAGE":
                {
                    return new HM2.View.Pages.PersonalAccount(_windowContext);
                }
                default: 
                {
                  throw new NotImplementedException();
                }
            }
        }
    }
}
