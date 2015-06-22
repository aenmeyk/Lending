using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
            var dateValues = neuralNetworkItems.Select(x => x.DateValue).Distinct();
            var dateCount = dateValues.Count();

            //Parallel.For(0, ProcessingSettings.Trials, trial =>
            for (var trial = 0; trial < ProcessingSettings.Trials; trial++)
            {
                //var shuffledDateValues = dateValues.ToList();
                //shuffledDateValues.Shuffle();

                var inputValues = new double[dateCount][];
                var outputValues = new double[dateCount];
                PopulateValues(dateValues, neuralNetworkItems, inputValues, outputValues);
                var network = new Network(core, inputValues, outputValues);

                network.LearningRate = ProcessingSettings.LearningRate * Math.Pow(ProcessingSettings.LearningRate, trial);

                for (int record = 0; record < network.InputValues.Count(); record++)
                {
                    NetworkOperations.RunHiddenLayer(network, record);
                    NetworkOperations.RunOutputLayer(network, record);
                    NetworkOperations.CalculateDelta(network, record);
                    NetworkOperations.BackPropogate(network, record);
                }

                PersistCoreValues(core);
            };
            //});
        }

        public double Predict(Core core, IEnumerable<NeuralNetworkItem> neuralNetworkItems)
        {
            var dateValues = neuralNetworkItems.Select(x => x.DateValue).Distinct();
            var inputValues = ConvertToInputValues(dateValues, neuralNetworkItems);
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

        private void PopulateValues(IEnumerable<DateTime> dateValues, IEnumerable<NeuralNetworkItem> neuralNetworkItems, double[][] inputValues, double[] outputValues)
        {
            var i = 0;
            var outputValue = 1.0;

            foreach (var date in dateValues)
            {
                var selectedNetworkItems = neuralNetworkItems.Where(x => x.DateValue == date);
                inputValues[i] = GetInputValues(selectedNetworkItems, inputValues[i]);
                var winningNetworkItem = selectedNetworkItems.OrderBy(x => x.FuturePriceChange10).Last();

                // TODO:
                throw new NotImplementedException();
                outputValues[i] = outputValue;

                i++;
            }
        }

        private double[] GetInputValues(IEnumerable<NeuralNetworkItem> neuralNetworkItems, double[] inputValues)
        {
            inputValues = new double[NetworkSettings.InputNeuronCount];
            var k = 0;

            foreach (var item in neuralNetworkItems)
            {
                inputValues[k++] = Normalize(item.PriceChange, 0.973612144175225, 1.02712225182478);
                inputValues[k++] = Normalize(item.VolumeChange, -37.047523678913, 41.991762302913);
                inputValues[k++] = Normalize(item.PriceOverMovingAvg2, 0.972083912610834, 1.02883210538917);
                inputValues[k++] = Normalize(item.PriceOverMovingAvg4, 0.969600351904366, 1.03176755609563);
                inputValues[k++] = Normalize(item.PriceOverMovingAvg8, 0.965449987010085, 1.03686467098992);
                inputValues[k++] = Normalize(item.PriceOverMovingAvg16, 0.958658495392611, 1.04552504860739);
                inputValues[k++] = Normalize(item.PriceOverMovingAvg32, 0.948127666310494, 1.05985254168951);
                inputValues[k++] = Normalize(item.VolumeOverMovingAvg2, -25.3127013173214, 29.3025103393214);
                inputValues[k++] = Normalize(item.VolumeOverMovingAvg4, -26.0643108819069, 29.4014716999069);
                inputValues[k++] = Normalize(item.VolumeOverMovingAvg8, -45.2949029182996, 48.3109386402996);
                inputValues[k++] = Normalize(item.VolumeOverMovingAvg16, -277.763882104693, 281.007762724693);
                inputValues[k++] = Normalize(item.VolumeOverMovingAvg32, -21.3499024658544, 23.8666690918544);
            }

            return inputValues;
        }

        private double[][] ConvertToInputValues(IEnumerable<DateTime> dateValues, IEnumerable<NeuralNetworkItem> neuralNetworkItems)
        {
            var result = new double[dateValues.Count()][];
            var i = 0;

            foreach (var date in dateValues)
            {
                var selectedNetworkItems = neuralNetworkItems.Where(x => x.DateValue == date);
                result[i] = GetInputValues(selectedNetworkItems, result[i]);
                i++;
            }

            return result;
        }

        public static double Normalize(double val, double low, double high)
        {
            return (val - low) / (high - low);
        }
    }
}
