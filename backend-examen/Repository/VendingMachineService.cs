using backend_examen.Domain;


namespace backend_examen.Repository
{
    public class VendingMachineService
    {
        private List<DrinksModel> _drinks;

        public VendingMachineService()
        {
            _drinks = new List<DrinksModel>
            {
                new DrinksModel { Id = 1, Name = "Coca Cola", Price = 800, Quantity = 10 },
                new DrinksModel { Id = 2, Name = "Pepsi", Price = 750, Quantity = 8 },
                new DrinksModel { Id = 3, Name = "Fanta", Price = 950, Quantity = 10 },
                new DrinksModel { Id = 4, Name = "Sprite", Price = 975, Quantity = 15 }
            };
        }

        public List<DrinksModel> GetAvailableDrinks()
        {
            return _drinks;
        }

        public bool BuyDrink(int id)
        {
            var drink = _drinks.FirstOrDefault(d => d.Id == id);
            if (drink == null || drink.Quantity <= 0)
                return false;

            drink.Quantity--;
            return true;
        }

    }
}
