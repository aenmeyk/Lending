using Common.Models;
using DataAccess.Repositories;

namespace Trader.Strategies
{
    public class StrategyRunner
    {
        private RawDataRepository _rawDataRepository = new RawDataRepository();

        public void Run()
        {
            var rawData = _rawDataRepository.Get<RawDataItem>();
        }
    }
}