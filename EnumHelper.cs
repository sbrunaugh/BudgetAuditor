using System;
using System.Collections.Generic;
using System.Text;
using BudgetAutomator.Enums;

namespace BudgetAutomator
{
    public class EnumHelper
    {
        public Category CategoryFromStr(string str)
        {
            if (str == "Grocery")
            {
                return Category.Grocery;
            } 
            else if (str == "Electricty")
            {
                return Category.Electricty;
            }
            else if (str == "Water")
            {
                return Category.Water;
            }
            else if (str == "Tithe")
            {
                return Category.Tithe;
            }
            else if (str == "Misc")
            {
                return Category.Misc;
            }
            else if (str == "Necessary Misc")
            {
                return Category.NecessaryMisc;
            }
            else if (str == "Auto Insurance")
            {
                return Category.AutoInsurance;
            }
            else if (str == "Home Insurance")
            {
                return Category.HomeInsurance;
            }
            else if (str == "Gas and Oil")
            {
                return Category.GasAndOil;
            }
            else if (str == "Phone Payment")
            {
                return Category.PhonePayment;
            }
            else if (str == "Restaurant")
            {
                return Category.Restaurant;
            }
            else if (str == "Wi-Fi")
            {
                return Category.WiFi;
            }
            else if (str == "Vacation")
            {
                return Category.Vacation;
            }
            else if (str == "Mortgage")
            {
                return Category.Mortgage;
            }
            else if (str == "Gifts")
            {
                return Category.Gifts;
            }
            else if (str == "Kids")
            {
                return Category.Kids;
            }
            else 
            {
                throw new ArgumentException();
            }
        }
    
        public Method MethodFromStr(string str)
        {
            if (str == "Card")
            {
                return Method.Card;
            }
            else if (str == "Check")
            {
                return Method.Check;
            }
            else if (str == "Cash")
            {
                return Method.Cash;
            }
            else if (str == "Paypal")
            {
                return Method.Paypal;
            }
            else if (str == "Other")
            {
                return Method.Other;
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
