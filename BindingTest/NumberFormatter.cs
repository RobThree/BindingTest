using System.Linq;

namespace BindingTest
{
    public interface INumberFormatter
    {
        string FormatNumber(string phoneNumber);
    }

    public class NumberFormatter : INumberFormatter
    {
        public string FormatNumber(string phoneNumber)
        {
            // Dummy function; as an example we simply reverse the number
            return string.Join(string.Empty, phoneNumber?.Reverse());
        }
    }
}
