﻿using System.Linq;

namespace BindingTest
{
    public interface INumberFormatter
    {
        string FormatNumber(string phoneNumber, NumberFormat numberFormat);
    }

    public class NumberFormatter : INumberFormatter
    {
        public string FormatNumber(string phoneNumber, NumberFormat numberFormat)
        {
            // Dummy function; as an example we simply reverse the number
            return string.Join(string.Empty, phoneNumber?.Reverse());
        }
    }
}
