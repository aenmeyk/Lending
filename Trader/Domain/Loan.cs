using Common.Models;

namespace Trader.Domain
{
    public class Loan
    {
        private decimal _loanAmount;
        private decimal _rate;
        private int _term;

        public Loan(RawDataItem rawDataItem)
        {
            _loanAmount = rawDataItem.funded_amnt;
            _rate = rawDataItem.int_rate;
            _term = rawDataItem.term.Contains("36 months") ? 36 : 60;
        }

        public decimal PurchasePrice
        {
            get { return _loanAmount; }
        }

    }
}
