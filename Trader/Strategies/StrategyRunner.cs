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

        public void Run()
        {
            var loans = GetLoans();

        }

        private IEnumerable<Loan> GetLoans()
        {
            var loans = new Collection<Loan>();
            var rawData = _rawDataRepository.GetCompletedLoans<RawDataItem>();

            foreach (var rawDataItem in rawData)
            {
                var loan = new Loan(rawDataItem);
                loans.Add(loan);
            }

            return loans;
        }
    }
}