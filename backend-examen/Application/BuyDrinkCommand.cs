using backend_examen.Application.DTOs;
using backend_examen.Repository;

namespace backend_examen.Application.Commands
{
    public class BuyDrinkCommand
{
    private readonly VendingMachineService _vendingRepo;
    private readonly CoinRepository _coinRepo;

    public BuyDrinkCommand(VendingMachineService vendingRepo, CoinRepository coinRepo)
    {
        _vendingRepo = vendingRepo;
        _coinRepo = coinRepo;
    }

    public BuyProductsResponseDto Execute(BuyRequestDto dto)
    {
        var drink = _vendingRepo.GetAvailableDrinks().FirstOrDefault(d => d.Id == dto.DrinkId);
        if (drink == null)
            return new() { Success = false, Message = "Producto inválido" };

        int total = drink.Price * dto.Quantity;

        if (dto.InsertedMoney < total)
            return new() { Success = false, Message = $"Saldo insuficiente. Faltan ₡{total - dto.InsertedMoney}" };

        for (int i = 0; i < dto.Quantity; i++)
        {
            if (!_vendingRepo.BuyDrink(dto.DrinkId))
                return new() { Success = false, Message = "No hay suficiente stock" };
        }

        int changeToGive = dto.InsertedMoney - total;
        int finalChange = 0;
        Dictionary<int, int>? breakdown = null;

        if (!_coinRepo.TryGiveChange(changeToGive, out finalChange, out breakdown))
        {
            return new()
            {
                Success = true,
                Message = "Compra realizada, pero no se pudo dar cambio exacto",
                Change = 0
            };
        }

        return new()
        {
            Success = true,
            Message = "Compra realizada",
            Change = finalChange
        };
    }
}

}
