using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace WpfApp1
{
    public abstract class Check
    {
        static public bool NameCheck(string name)
        {
            bool Truthlength = Regex.IsMatch(name , @"^[a-zA-Z/ ]{3,32}$");
            return Truthlength;
        }

        static public bool EmailCheck(string email)
        {
            bool Truthemail = Regex.IsMatch(email, @"^.{1,32}@.{1,32}\..{1,32}$");
            return Truthemail;
        }

        static public bool PasswordCheck(string password)
        {
            bool TruthPassword = Regex.IsMatch(password , @"^(?=.*[a-z])(?=.*[A-Z]).{8,40}$");
            return TruthPassword;
        }

        static public bool CVVCheck(string CVV)
        {
            if (Regex.IsMatch(CVV, "^[0-9]{3,4}$")) return true; else return false;
        }

        static public bool NumberCheck(string number)
        {
            if(Regex.IsMatch(number, @"^09[0-9]{9}$")) return true; else return false;
        }

        public static bool checkid(string id)
        {
            string[] vs = id.Split(' ');
            string ID = "";
            for (int i = 0; i < vs.Length; i++)
            {
                ID += vs[i];
            }
            int sum = 0;
            for (int i = ID.Length - 1; i >= 0; i--)
            {
                int j = 0;
                try
                {
                    j = int.Parse(ID[i].ToString());
                }
                catch { return false; }
                if (i % 2 == 0)
                {
                    sum += digitsum(j * 2);
                }
                else
                    sum += j;
            }
            if (sum % 10 == 0)
            {
                return true;
            }
            else return false;
        }

        public static int digitsum(int n)
        {
            int m = 0;
            while (n > 0)
            {
                m += n % 10;
                n /= 10;
            }
            return m;
        }

    }
}
