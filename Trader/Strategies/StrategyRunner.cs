using Common.Models;
using DataAccess.Repositories;
using Trader.Translators;

namespace Trader.Strategies
{
    public class StrategyRunner
    {
        private RawDataRepository _rawDataRepository = new RawDataRepository();

        public void Run()
        {
            var rawData = _rawDataRepository.Get<RawDataItem>();

            foreach (var rawDataItem in rawData)
            {
                var neuralNetworkitem = new NeuralNetworkItem();
                neuralNetworkitem.AnnualIncome = NodeTranslators.AnnualIncome(rawDataItem);
            }
        }
    }
}