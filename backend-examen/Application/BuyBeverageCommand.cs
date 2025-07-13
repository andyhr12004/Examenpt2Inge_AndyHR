using backend_examen.Application.DTOs;
using backend_examen.Repository;

namespace backend_examen.Application
{
    public class BuyBeverageCommand
    {
        private readonly VendingMachineService _repo;

        public BuyBeverageCommand(VendingMachineService repo)
        {
            _repo = repo;
        }

        public BuyProductsResponseDto Execute(int drinkId, int quantity)
        {
            // Llamamos al repo tantas veces como cantidad:
            for (int i = 0; i < quantity; i++)
                if (!_repo.BuyDrink(drinkId))
                    return new() { Success = false, Message = "Stock insuficiente" };

            return new() { Success = true };
        }
    }
}
