﻿using System.Collections.Generic;
using Common.Models;
using System.Data;

namespace DataAccess.Repositories
{
    public class OutputWeightsRepository : RepositoryBase
    {
        protected override string TableName
        {
            get { return "nn.OutputWeights"; }
        }

        public void InsertNeuronValues(IEnumerable<NeuronValue> neuronValues)
        {
            var dataTable = new DataTable
            {
                Columns =
                {
                    new DataColumn("HiddenNeuronIndex", typeof(int)),
                    new DataColumn("Value", typeof(decimal))
                }
            };

            foreach (var neuronValue in neuronValues)
            {
                dataTable.Rows.Add(neuronValue.HiddenNeuronIndex, neuronValue.Value);
            }

            BulkInsert(dataTable);
        }
    }
}
