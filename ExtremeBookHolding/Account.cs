using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtremeBookHolding
{
    public enum AccountType { Activ, Passiv, Both}

    public class Account
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public AccountType Type { get; set; }
    }
}
