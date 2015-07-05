using System.Collections.ObjectModel;
using Common.Models;
using DataAccess.Repositories;
using Trader.Translators;
using NeuralNet;
using System.Collections.Generic;
using System.Linq;

namespace Trader.Strategies
{
    public class NeuralNetworkRunner
    {
        private RawDataRepository _rawDataRepository = new RawDataRepository();
        private NetworkManager _networkManager = new NetworkManager();

        public void Run()
        {
            const int trainingItems = 100000;

            var core = new Core();
            core.InitializeArrays();
            core.InitializeRandomValues();

            var neuralNetworkItems = GetNeuralNetworkItems();
            _networkManager.TrainNetwork(core, neuralNetworkItems.Take(trainingItems));

            var predictions = new Collection<Prediction>();
            var testItems = neuralNetworkItems.Skip(trainingItems);

            foreach (var testItem in testItems)
            {
                var prediction = new Prediction();
                prediction.Id = testItem.Id;
                prediction.PredictedValue = _networkManager.Predict(core, testItem);
                prediction.PercentDefaulted = testItem.Defaulted;
                prediction.PercentRecovered = testItem.PercentRecovered;

                predictions.Add(prediction);
            }

            var predictionResultRepository = new PredictionResultRepository();
            predictionResultRepository.Truncate();
            predictionResultRepository.InsertPredictionValues(predictions);
        }

        private IEnumerable<NeuralNetworkItem> GetNeuralNetworkItems()
        {
            var rawData = _rawDataRepository.GetCompletedLoans<RawDataItem>();
            var neuralNetworkItems = new Collection<NeuralNetworkItem>();

            foreach (var rawDataItem in rawData)
            {
                var neuralNetworkitem = new NeuralNetworkItem();
                neuralNetworkitem.Id = rawDataItem.id;
                neuralNetworkitem.AnnualIncome = NodeTranslators.AnnualIncome(rawDataItem);
                neuralNetworkitem.Collections = NodeTranslators.Collections(rawDataItem);
                neuralNetworkitem.Delinquencies = NodeTranslators.Delinquencies(rawDataItem);
                neuralNetworkitem.Dti = NodeTranslators.Dti(rawDataItem);
                neuralNetworkitem.EarliestCreditLine = NodeTranslators.EarliestCreditLine(rawDataItem);
                neuralNetworkitem.EmploymentLength = NodeTranslators.EmploymentLength(rawDataItem);
                neuralNetworkitem.IsEmploymed = NodeTranslators.IsEmploymed(rawDataItem);
                neuralNetworkitem.FicoLow = NodeTranslators.FicoLow(rawDataItem);
                neuralNetworkitem.FicoHigh = NodeTranslators.FicoHigh(rawDataItem);
                neuralNetworkitem.Grade = NodeTranslators.Grade(rawDataItem);
                neuralNetworkitem.SubGrade = NodeTranslators.SubGrade(rawDataItem);
                neuralNetworkitem.HomeOwn = NodeTranslators.HomeOwn(rawDataItem);
                neuralNetworkitem.HomeMortgage = NodeTranslators.HomeMortgage(rawDataItem);
                neuralNetworkitem.HomeRent = NodeTranslators.HomeRent(rawDataItem);
                neuralNetworkitem.InitialListStatus = NodeTranslators.InitialListStatus(rawDataItem);
                neuralNetworkitem.Inquiries = NodeTranslators.Inquiries(rawDataItem);
                neuralNetworkitem.Installment = NodeTranslators.Installment(rawDataItem);
                neuralNetworkitem.InterestRate = NodeTranslators.InterestRate(rawDataItem);
                neuralNetworkitem.VerificationStatus = NodeTranslators.VerificationStatus(rawDataItem);
                neuralNetworkitem.LoanAmount = NodeTranslators.LoanAmount(rawDataItem);
                neuralNetworkitem.MonthsSinceDelinquent = NodeTranslators.MonthsSinceDelinquent(rawDataItem);
                neuralNetworkitem.MonthsSinceDerogatoryRemark = NodeTranslators.MonthsSinceDerogatoryRemark(rawDataItem);
                neuralNetworkitem.MonthsSincePublicRecord = NodeTranslators.MonthsSincePublicRecord(rawDataItem);
                neuralNetworkitem.OpenAccounts = NodeTranslators.OpenAccounts(rawDataItem);
                neuralNetworkitem.DerogatoryPublicRecords = NodeTranslators.DerogatoryPublicRecords(rawDataItem);
                neuralNetworkitem.PurposeCar = NodeTranslators.PurposeCar(rawDataItem);
                neuralNetworkitem.PurposeCreditCard = NodeTranslators.PurposeCreditCard(rawDataItem);
                neuralNetworkitem.PurposeDebtConsolidation = NodeTranslators.PurposeDebtConsolidation(rawDataItem);
                neuralNetworkitem.PurposeHomeImprovement = NodeTranslators.PurposeHomeImprovement(rawDataItem);
                neuralNetworkitem.PurposeHouse = NodeTranslators.PurposeHouse(rawDataItem);
                neuralNetworkitem.PurposeMajorPurchase = NodeTranslators.PurposeMajorPurchase(rawDataItem);
                neuralNetworkitem.PurposeMedical = NodeTranslators.PurposeMedical(rawDataItem);
                neuralNetworkitem.PurposeMoving = NodeTranslators.PurposeMoving(rawDataItem);
                neuralNetworkitem.PurposeOther = NodeTranslators.PurposeOther(rawDataItem);
                neuralNetworkitem.PurposeRenewableEnergy = NodeTranslators.PurposeRenewableEnergy(rawDataItem);
                neuralNetworkitem.PurposeSmallBusiness = NodeTranslators.PurposeSmallBusiness(rawDataItem);
                neuralNetworkitem.PurposeVacation = NodeTranslators.PurposeVacation(rawDataItem);
                neuralNetworkitem.PurposeWedding = NodeTranslators.PurposeWedding(rawDataItem);
                neuralNetworkitem.RevolvingCreditBalance = NodeTranslators.RevolvingCreditBalance(rawDataItem);
                neuralNetworkitem.RevolvingCreditUtilization = NodeTranslators.RevolvingCreditUtilization(rawDataItem);
                neuralNetworkitem.Term = NodeTranslators.Term(rawDataItem);
                neuralNetworkitem.CreditLines = NodeTranslators.CreditLines(rawDataItem);
                neuralNetworkitem.Defaulted = NodeTranslators.Defaulted(rawDataItem);
                neuralNetworkitem.PercentRecovered = NodeTranslators.PercentRecovered(rawDataItem);

                neuralNetworkItems.Add(neuralNetworkitem);
            }

            return neuralNetworkItems;
        }
    }
}