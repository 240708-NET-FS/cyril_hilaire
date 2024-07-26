using System;
using System.Text.RegularExpressions;

namespace ContactBook;

public class InputValidation
{
    public static bool CheckFirstname(string fname)
    {
        if (fname == null || fname.Trim().Length == 0)
        {
            // throw new ArgumentNullException("Empty Firsname...");
            Console.WriteLine("Cannot be empty...");
            return false;
        }

        if(fname.Trim().Length < 2)
        {
            // throw new Exception();
            Console.WriteLine("Cannot be 1 character...");
            return false;
        }

        if(isNumeric(fname))
        {
            Console.WriteLine("Cannot be numeric...");
            return false;
        }

        return isAlphaNumeric(fname);
    } 

    public static bool CheckLastname(string lname)
    {
        return CheckFirstname(lname);
    }

    public static bool CheckPhone(string phone)
    {
        return isUsPhoneNumber(phone);
    }

    public static bool CheckEmail(string email)
    {
        if(email == null || email.Trim().Length == 0)
        {
            return true;
        }

        return isEmail(email);
    }

    public static string FormatInputValue(string userInputValue)
    {
        userInputValue = userInputValue.Trim();
        return userInputValue;
    }

    public static bool isAlphaNumeric(string strToCheck)
    {
        Regex rg = new Regex(@"^[a-zA-Z0-9\s]*$");
        return rg.IsMatch(strToCheck);
    }

    public static bool isNumeric(string strToCheck)
    {
        Regex rg = new Regex(@"^[0-9\s]*$");
        return rg.IsMatch(strToCheck);
    }

    public static bool isUsPhoneNumber(string phoneNumber)
    {
        // Validate phone number
        string pattern = "^\\+?\\d{1,4}?[-.\\s]?\\(?\\d{1,3}?\\)?[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,9}$";
        Regex validatePhoneNumberRegex = new Regex(pattern);
        return validatePhoneNumberRegex.IsMatch(phoneNumber);
    }

    public static bool isEmail(string email)
    {
        // Validate if a string is a email 
        string pattern = "^\\S+@\\S+\\.\\S+$";
        Regex validateEmailRegex = new Regex(pattern);
        return validateEmailRegex.IsMatch(email);
    }
}