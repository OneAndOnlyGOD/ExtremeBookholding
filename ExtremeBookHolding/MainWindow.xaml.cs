using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExtremeBookHolding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Journal> JournalList { get; set; } = new ObservableCollection<Journal>();




        public decimal ActivAccountingRecordsSummary => ActivAccountingRecords.Sum(x => x.Value);
        public List<AccountingRecord> OrderedActivAccountingRecords => ActivAccountingRecords.OrderBy(x => x.Account.ID).ToList();
        private ObservableCollection<AccountingRecord> ActivAccountingRecords { get; set; } = new ObservableCollection<AccountingRecord>();

        public decimal PassivAccountingRecordsSummary => PassivAccountingRecords.Sum(x => x.Value);
        public List<AccountingRecord> OrderedPassivAccountingRecords => PassivAccountingRecords.OrderBy(x => x.Account.ID).ToList();
        private ObservableCollection<AccountingRecord> PassivAccountingRecords { get; set; } = new ObservableCollection<AccountingRecord>();


        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            PrepareAccountList();
            LoadJournalExampleData();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void LoadJournalExampleData()
        {
            foreach (Account account in accounts.ItemsSource)
            {
                JournalList.Add(new Journal()
                {
                    Account = account,
                    HABENAccountingRecords = new ObservableCollection<AccountingRecord> {
                    new AccountingRecord() { Account = account, Text = "Test1Haben", Value = 11},
                    new AccountingRecord() { Account = account, Text = "Test2Haben", Value = 22},
                    new AccountingRecord() { Account = account, Text = "Test2Haben", Value = 33},
                },
                    SOLLAccountingRecords = new ObservableCollection<AccountingRecord>
                {
                    new AccountingRecord() { Account = account, Text = "Test1Soll", Value = 101},
                    new AccountingRecord() { Account = account, Text = "Test2Soll", Value = 202},
                    new AccountingRecord() { Account = account, Text = "Test2Soll", Value = 303},
                }
                });
            }

        }


        private void PrepareAccountList()
        {
            accounts.ItemsSource = new List<Account>() {
            new Account() {ID= 1, Name = "Kasse" , Type = AccountType.Activ},
            new Account() {ID= 2, Name = "Post" , Type = AccountType.Activ},
            new Account() {ID= 3, Name = "Bank" , Type = AccountType.Both},
            new Account() {ID= 4, Name = "FLL" , Type = AccountType.Activ},
            new Account() {ID= 5, Name = "Warenbestand" , Type = AccountType.Activ},
            new Account() {ID= 6, Name = "Mobilien" , Type = AccountType.Activ},
            new Account() {ID= 7, Name = "Immobilien" , Type = AccountType.Activ},
            new Account() {ID= 8, Name = "VLL" , Type = AccountType.Passiv},
            new Account() {ID= 9, Name = "Darlehensschuld" , Type = AccountType.Passiv},
            new Account() {ID= 10, Name = "Hypotheken" , Type = AccountType.Passiv},
            new Account() {ID= 99, Name = "Eigenkapital" , Type = AccountType.Passiv}
            };
            accounts.DisplayMemberPath = nameof(accounts.Name);
        }

        private void OnEnterButtonClicked(object sender, RoutedEventArgs e)
        {
            if (accountValue.Value.HasValue && accountValue.Value != 0)
            {
                if (accounts.SelectedItem != null && accounts.SelectedItem is Account selectedAccount)
                {
                    if (selectedAccount.Type == AccountType.Activ)
                    {
                        InserAccountRecordWithAccountValueToAccountRecordList(ActivAccountingRecords, selectedAccount);
                        RaisePropertyChanged(nameof(OrderedActivAccountingRecords));
                        RaisePropertyChanged(nameof(ActivAccountingRecordsSummary));
                    }
                    else if (selectedAccount.Type == AccountType.Passiv)
                    {
                        InserAccountRecordWithAccountValueToAccountRecordList(PassivAccountingRecords, selectedAccount);
                        RaisePropertyChanged(nameof(OrderedPassivAccountingRecords));
                        RaisePropertyChanged(nameof(PassivAccountingRecordsSummary));
                    }
                    else
                    {
                        //TODO: Type Both noch umsetzen (z.B Als Typ Both gilt Bank, da es aktiv und passiv sein kann)
                    }
                }
            }
        }

        private void InserAccountRecordWithAccountValueToAccountRecordList(ObservableCollection<AccountingRecord> accountingRecordList, Account account)
        {
            var accountingRecord = accountingRecordList.FirstOrDefault(x => x.Account == account);
            if (accountingRecord != null)
            {
                accountingRecord.Value += (decimal)accountValue.Value;
            }
            else
            {
                accountingRecordList.Add(new AccountingRecord() { Account = account, Value = (decimal)accountValue.Value, Text = "Anfangsbilanz" });
            }
        }
    }
}
