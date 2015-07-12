using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Common.Models;
using DataAccess.Repositories;

namespace NeuralNet
{
    public class NetworkManager
    {
        private HiddenWeightsRepository _hiddenWeightsRepository = new HiddenWeightsRepository();
        private OutputWeightsRepository _outputWeightsRepository = new OutputWeightsRepository();
        private HiddenBiasesRepository _hiddenBiasesRepository = new HiddenBiasesRepository();
        private OutputBiasesRepository _outputBiasesRepository = new OutputBiasesRepository();

        public void TrainNetwork(Core core, IEnumerable<NeuralNetworkItem> neuralNetworkItems)
        {
            var itemCount = neuralNetworkItems.Count();

            Parallel.For(0, ProcessingSettings.Trials, trial =>
            //for (var trial = 0; trial < ProcessingSettings.Trials; trial++)
            {
                var inputValues = new double[itemCount][];
                var outputValues = new double[itemCount];
                PopulateValues(neuralNetworkItems, inputValues, outputValues);
                var network = new Network(core, inputValues, outputValues);

                network.LearningRate = ProcessingSettings.LearningRate; // * Math.Pow(ProcessingSettings.LearningRate, trial);

                for (int record = 0; record < network.InputValues.Count(); record++)
                {
                    NetworkOperations.RunHiddenLayer(network, record);
                    NetworkOperations.RunOutputLayer(network, record);
                    NetworkOperations.CalculateDelta(network, record);
                    NetworkOperations.BackPropogate(network, record);
                }

            //};
            });
                PersistCoreValues(core);
        }

        public double Predict(Core core, NeuralNetworkItem neuralNetworkItem)
        {
            var inputValues = ConvertToInputValues(neuralNetworkItem);
            var network = new Network(core, inputValues);

            NetworkOperations.RunHiddenLayer(network, 0);
            NetworkOperations.RunOutputLayer(network, 0);

            return network.OutputOutput[0];
        }

        public void PersistCoreValues(Core core)
        {
            var hiddenWeights = new Collection<NeuronValue>();
            var outputWeights = new Collection<NeuronValue>();
            var hiddenBiases = new Collection<NeuronValue>();

            for (int hiddenNeuron = 0; hiddenNeuron < NetworkSettings.HiddenNeuronCount; hiddenNeuron++)
            {
                for (int inputNeuron = 0; inputNeuron < NetworkSettings.InputNeuronCount; inputNeuron++)
                {
                    hiddenWeights.Add(new NeuronValue
                    {
                        HiddenNeuronIndex = hiddenNeuron,
                        InputNeuronIndex = inputNeuron,
                        Value = core.HiddenWeight[hiddenNeuron][inputNeuron]
                    });
                }

                outputWeights.Add(new NeuronValue
                {
                    HiddenNeuronIndex = hiddenNeuron,
                    Value = core.OutputWeight[hiddenNeuron]
                });

                hiddenBiases.Add(new NeuronValue
                {
                    HiddenNeuronIndex = hiddenNeuron,
                    Value = core.HiddenBias[hiddenNeuron]
                });
            }

            var outputBias = new NeuronValue
            {
                Value = core.OutputBias
            };

            _hiddenWeightsRepository.Truncate();
            _outputWeightsRepository.Truncate();
            _hiddenBiasesRepository.Truncate();
            _outputBiasesRepository.Truncate();

            _hiddenWeightsRepository.InsertNeuronValues(hiddenWeights);
            _outputWeightsRepository.InsertNeuronValues(outputWeights);
            _hiddenBiasesRepository.InsertNeuronValues(hiddenBiases);
            _outputBiasesRepository.InsertNeuronValues(outputBias);
        }

        public void LoadNetworkValues(string symbol, Core core)
        {
            var hiddenWeights = _hiddenWeightsRepository.Get<NeuronValue>();
            var outputWeights = _outputWeightsRepository.Get<NeuronValue>();
            var hiddenBiases = _hiddenBiasesRepository.Get<NeuronValue>();
            var outputBiases = _outputBiasesRepository.Get<NeuronValue>();

            core.InitializeArrays();
            core.OutputBias = outputBiases.Single().Value;

            for (int i = 0; i < NetworkSettings.HiddenNeuronCount; i++)
            {
                core.HiddenWeight[i] = new double[NetworkSettings.InputNeuronCount];
            }

            foreach (var neuronValue in hiddenWeights)
            {
                core.HiddenWeight[neuronValue.HiddenNeuronIndex][neuronValue.InputNeuronIndex] = neuronValue.Value;
            }

            foreach (var neuronValue in outputWeights)
            {
                core.OutputWeight[neuronValue.HiddenNeuronIndex] = neuronValue.Value;
            }

            foreach (var neuronValue in hiddenBiases)
            {
                core.HiddenBias[neuronValue.HiddenNeuronIndex] = neuronValue.Value;
            }
        }

        private void PopulateValues(IEnumerable<NeuralNetworkItem> neuralNetworkItems, double[][] inputValues, double[] outputValues)
        {
            var i = 0;

            foreach (var neuralNetworkItem in neuralNetworkItems)
            {
                inputValues[i] = GetInputValues(neuralNetworkItem, inputValues[i]);
                outputValues[i] = Normalize(neuralNetworkItem.PercentRecovered, 0, 1.52);

                i++;
            }
        }

        private double[] GetInputValues(NeuralNetworkItem neuralNetworkItem, double[] inputValues)
        {
            inputValues = new double[NetworkSettings.InputNeuronCount];
            var k = 0;

            inputValues[k++] = neuralNetworkItem.AnnualIncome;
            inputValues[k++] = neuralNetworkItem.Collections;
            inputValues[k++] = neuralNetworkItem.Delinquencies;
            inputValues[k++] = Normalize(neuralNetworkItem.Dti, 0, 1.52);
            inputValues[k++] = neuralNetworkItem.EarliestCreditLine;
            inputValues[k++] = neuralNetworkItem.EmploymentLength;
            inputValues[k++] = neuralNetworkItem.IsEmploymed;
            inputValues[k++] = neuralNetworkItem.FicoLow;
            inputValues[k++] = neuralNetworkItem.FicoHigh;
            inputValues[k++] = neuralNetworkItem.Grade;
            inputValues[k++] = neuralNetworkItem.SubGrade;
            inputValues[k++] = neuralNetworkItem.HomeOwn;
            inputValues[k++] = neuralNetworkItem.HomeMortgage;
            inputValues[k++] = neuralNetworkItem.HomeRent;
            //inputValues[k++] = neuralNetworkItem.InitialListStatus;
            inputValues[k++] = Normalize(neuralNetworkItem.Inquiries, 0, 1.6);
            inputValues[k++] = Normalize(neuralNetworkItem.Installment, 0.000288947368, 0.320262);
            inputValues[k++] = Normalize(neuralNetworkItem.InterestRate, 0.0542, 0.2489);
            inputValues[k++] = neuralNetworkItem.VerificationStatus;
            inputValues[k++] = Normalize(neuralNetworkItem.LoanAmount, 0.000789473684, 0.83);
            inputValues[k++] = neuralNetworkItem.MonthsSinceDelinquent;
            inputValues[k++] = neuralNetworkItem.MonthsSinceDerogatoryRemark;
            inputValues[k++] = neuralNetworkItem.MonthsSincePublicRecord;
            inputValues[k++] = neuralNetworkItem.OpenAccounts;
            inputValues[k++] = neuralNetworkItem.DerogatoryPublicRecords;
            inputValues[k++] = neuralNetworkItem.PurposeCar;
            inputValues[k++] = neuralNetworkItem.PurposeCreditCard;
            inputValues[k++] = neuralNetworkItem.PurposeDebtConsolidation;
            inputValues[k++] = neuralNetworkItem.PurposeHomeImprovement;
            inputValues[k++] = neuralNetworkItem.PurposeHouse;
            inputValues[k++] = neuralNetworkItem.PurposeMajorPurchase;
            inputValues[k++] = neuralNetworkItem.PurposeMedical;
            inputValues[k++] = neuralNetworkItem.PurposeMoving;
            inputValues[k++] = neuralNetworkItem.PurposeOther;
            inputValues[k++] = neuralNetworkItem.PurposeRenewableEnergy;
            inputValues[k++] = neuralNetworkItem.PurposeSmallBusiness;
            inputValues[k++] = neuralNetworkItem.PurposeVacation;
            inputValues[k++] = neuralNetworkItem.PurposeWedding;
            inputValues[k++] = neuralNetworkItem.RevolvingCreditBalance;
            inputValues[k++] = neuralNetworkItem.RevolvingCreditUtilization;
            //inputValues[k++] = neuralNetworkItem.Term;
            inputValues[k++] = neuralNetworkItem.CreditLines;

            return inputValues;
        }

        private double[][] ConvertToInputValues(NeuralNetworkItem neuralNetworkItem)
        {
            var result = new double[1][];
            result[0] = GetInputValues(neuralNetworkItem, result[0]);

            return result;
        }

        public static double Normalize(double val, double low, double high)
        {
            return (val - low) / (high - low);
        }
    }
}
