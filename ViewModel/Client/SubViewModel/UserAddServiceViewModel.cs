using DAL.AdditionalEntities;
using DAL.Entities;
using HM2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HM2.ViewModel
{
    public class UserAddServiceViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string caller = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }

        private ObservableCollection<StringServiceExtension> strUserServices;
        public ObservableCollection<StringServiceExtension> UserStringServices
        {
            get
            {
                return strUserServices;
            }
            set
            {
                strUserServices = value;
            }
        }

        private UserAddServiceModel userAddServiceModel;
        // Просмотр выбранных услуг клиентом в личном кабинете
        public UserAddServiceViewModel(WindowContext windowContext)
        {
            UserStringServices = new ObservableCollection<StringServiceExtension>();
            userAddServiceModel = new UserAddServiceModel();

            var userBookingExtension = (UserBookingExtension)windowContext.GetResourse("USER_BOOKING_EXTENSION");

            List<StringServiceExtension> listStrServices = userAddServiceModel.GetStrServices(userBookingExtension.Id);
            foreach (StringServiceExtension strService in listStrServices)
            {
                UserStringServices.Add(strService);
            }
        }
    }
}
