using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExtremeBookHolding
{
    public class AccountingRecord : INotifyPropertyChanged
    {
        Account account;
        public Account Account
        {
            get => account;
            set
            {
                if(account != value)
                {
                    account = value;
                    RaisePropertyChanged();
                }
            }
        }

        decimal value;
        public decimal Value
        {
            get => this.value;
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    RaisePropertyChanged();
                }
            }
        }

        string text;
        public string Text
        {
            get => text;
            set
            {
                if (text != value)
                {
                    text = value;
                    RaisePropertyChanged();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
