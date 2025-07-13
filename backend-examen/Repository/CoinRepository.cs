namespace backend_examen.Repository
{
    public class CoinRepository
    {
        private int _coins500 = 20;
        private int _coins100 = 30;
        private int _coins50 = 50;
        private int _coins25 = 25;

        public bool TryGiveChange(int change, out int actualChange)
        {
            int remaining = change;
            int used500 = Math.Min(remaining / 500, _coins500);
            remaining -= used500 * 500;

            int used100 = Math.Min(remaining / 100, _coins100);
            remaining -= used100 * 100;

            int used50 = Math.Min(remaining / 50, _coins50);
            remaining -= used50 * 50;

            int used25 = Math.Min(remaining / 25, _coins25);
            remaining -= used25 * 25;

            if (remaining == 0)
            {
                _coins500 -= used500;
                _coins100 -= used100;
                _coins50 -= used50;
                _coins25 -= used25;

                actualChange = change;
                return true;
            }

            actualChange = 0;
            return false;
        }
    }
}
