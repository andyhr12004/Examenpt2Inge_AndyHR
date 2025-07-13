namespace backend_examen.Repository
{
    public class CoinRepository
    {
        private Dictionary<int, int> _coins = new()
        {
            { 500, 20 },
            { 100, 30 },
            { 50, 50 },
            { 25, 25 }
        };


       public bool TryGiveChange(int change, out int finalChange, out Dictionary<int, int>? breakdown)
            {
                breakdown = new();
                finalChange = 0;

                var coins = new[] { 500, 100, 50, 25 };
                var originalState = new Dictionary<int, int>(_coins);

                foreach (var coin in coins)
                {
                    int needed = change / coin;
                    int available = _coins.ContainsKey(coin) ? _coins[coin] : 0;
                    int toUse = Math.Min(needed, available);

                    if (toUse > 0)
                    {
                        _coins[coin] -= toUse;
                        change -= toUse * coin;
                        breakdown[coin] = toUse;
                    }
                }

                if (change == 0)
                {
                    finalChange = breakdown.Sum(x => x.Key * x.Value);
                    return true;
                }

                _coins = new Dictionary<int, int>(originalState);
                breakdown = null;
                return false;
            }
    }
}
