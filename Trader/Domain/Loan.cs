using Common.Models;

namespace Trader.Domain
{
    public class Loan
    {
        private decimal _loanAmount;
        private decimal _rate;
        private decimal _installment;
        private decimal _totalAmountPaidAtCompletion;
        private int _term;

        public Loan(RawDataItem rawDataItem)
        {
            _loanAmount = rawDataItem.funded_amnt;
            _rate = rawDataItem.int_rate;
            _installment = rawDataItem.installment;
            _totalAmountPaidAtCompletion = rawDataItem.total_pymnt;
            _term = rawDataItem.term.Contains(Terms.MONTHS36) ? 36 : 60;
        }

        public decimal PurchasePrice
        {
            get { return _loanAmount; }
        }

        public decimal CalculatePayment(int month)
        {
            // Get the amount the borrower should have paid up until this point
            var amountBorrowerShouldHavePaidToDate = (month - 1) * _installment;

            // If the total amount the borrow should have paid is greater than the amount they did pay
            if(amountBorrowerShouldHavePaidToDate + _installment > _totalAmountPaidAtCompletion)
            {
                // Then return the amount remaining if it is greater than 0;
                var amountRemaining = _totalAmountPaidAtCompletion - amountBorrowerShouldHavePaidToDate;

                return amountRemaining > 0
                    ? amountRemaining
                    : 0;
            }

            return _installment;
        }
    }
}
