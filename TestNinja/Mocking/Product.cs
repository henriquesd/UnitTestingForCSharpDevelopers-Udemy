namespace TestNinja.Mocking
{
    public class Product
    {
        public float ListPrice { get; set; }

        public float GetPrice(Customer customer)
        {
            if (customer.IsGold)
                return ListPrice * 0.7f;

            return ListPrice;
        }

        // This commented code was used for example of abusing mock;
        //public float GetPrice(ICustomer customer)
        //{
        //    if (customer.IsGold)
        //        return ListPrice * 0.7f;

        //    return ListPrice;
        //}
    }

    public class Customer
    {
        public bool IsGold { get; set; }
    }

    // This commented code was used for example of abusing mock;
    //public interface ICustomer
    //{
    //    bool IsGold { get; set; }
    //}
}