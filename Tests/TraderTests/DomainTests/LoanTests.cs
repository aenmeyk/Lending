using Common.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trader;
using Trader.Domain;

namespace Tests.TraderTests.DomainTests
{
    [TestClass]
    public class LoanTests
    {
        const decimal INSTALLMENT = 3.23M;

        private RawDataItem _rawDataItem = new RawDataItem
        {
            funded_amnt = 100,
            int_rate = 0.1M,
            installment = INSTALLMENT,
            total_pymnt = 116.16M,
            term = Terms.MONTHS36
        };

        [TestMethod]
        public void CalculatePayment_should_return_installment_for_first_month()
        {
            var loan = new Loan(_rawDataItem);
            var payment = loan.CalculatePayment(month: 3);

            Assert.AreEqual(INSTALLMENT, payment);
        }

        [TestMethod]
        public void CalculatePayment_should_return_installment_for_last_month()
        {
            var loan = new Loan(_rawDataItem);
            var payment = loan.CalculatePayment(month: 36);

            Assert.AreEqual(3.11M, payment);
        }

        [TestMethod]
        public void CalculatePayment_should_return_0_if_borrower_defaulted()
        {
            _rawDataItem.total_pymnt = 5;
            var loan = new Loan(_rawDataItem);
            var payment = loan.CalculatePayment(month: 30);

            Assert.AreEqual(0, payment);
        }

        [TestMethod]
        public void CalculatePayment_should_return_amount_remaining_if_borrower_defaulted_in_current_period()
        {
            _rawDataItem.total_pymnt = 33M;
            var loan = new Loan(_rawDataItem);
            var payment = loan.CalculatePayment(month: 11);

            Assert.AreEqual(0.7M, payment);
        }
    }
}
