using System.Collections.Generic;
using System.Collections.ObjectModel;
using Common.Models;
using DataAccess.Repositories;
using Trader.Domain;

namespace Trader.Strategies
{
    public class StrategyRunner
    {
        private RawDataRepository _rawDataRepository = new RawDataRepository();
        private Account _account = new Account();

        public void Run()
        {
            PurchaseLoans();

            for(int month = 0; month < 60; month++)
            {
                _account.AdvanceMonth();
            }
        }

        private void PurchaseLoans()
        {
            var rawData = _rawDataRepository.GetCompletedLoans<RawDataItem>();

            foreach (var rawDataItem in rawData)
            {
                var loan = new Loan(rawDataItem);
                _account.PurchaseLoan(loan);
            }
        }
    }
}