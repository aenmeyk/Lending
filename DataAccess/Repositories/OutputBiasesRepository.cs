﻿using System.Data;
using Common.Models;

namespace DataAccess.Repositories
{
    public class OutputBiasesRepository : RepositoryBase
    {
        protected override string TableName
        {
            get { return "nn.OutputBiases"; }
        }

        public void InsertNeuronValues(NeuronValue neuronValue)
        {
            var dataTable = new DataTable
            {
                Columns =
                {
                    new DataColumn("Value", typeof(decimal))
                }
            };

            dataTable.Rows.Add(neuronValue.Value);

            BulkInsert(dataTable);
        }
    }
}
