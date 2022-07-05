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

        static public bool PassesLuhnCheck(string value)
        {
            long sum = 0;

            for (int i = 0; i < value.Length; i++)
            {
                var digit = value[value.Length - 1 - i] - '0';
                sum += (i % 2 != 0) ? GetDouble(digit) : digit;
            }

            return sum % 10 == 0;
        }

        static private int GetDouble(long i)
        {
            switch (i)
            {
                case 0: return 0;
                case 1: return 2;
                case 2: return 4;
                case 3: return 6;
                case 4: return 8;
                case 5: return 1;
                case 6: return 3;
                case 7: return 5;
                case 8: return 7;
                case 9: return 9;
                default: return 0;
            }
        }

    }
}
