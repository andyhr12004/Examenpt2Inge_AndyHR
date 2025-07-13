using backend_examen.Repository;

public class GiveChangeCommand
{
    private readonly CoinRepository _coinRepo;

    public GiveChangeCommand(CoinRepository coinRepo)
    {
        _coinRepo = coinRepo;
    }

    public (bool success, int finalChange, Dictionary<int, int>? breakdown) Execute(int change)
    {
        if (_coinRepo.TryGiveChange(change, out int finalChange, out var breakdown))
            return (true, finalChange, breakdown);

        return (false, 0, null);
    }
}

