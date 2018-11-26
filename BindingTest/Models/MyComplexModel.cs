namespace BindingTest.Models
{
    public class MyComplexModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [FormatNumber(NumberFormat.E164)]
        public string PhoneNumber { get; set; }
    }
}
