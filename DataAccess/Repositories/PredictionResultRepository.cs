using System.Collections.Generic;
using System.Data;
using Common.Models;

namespace DataAccess.Repositories
{
    public class PredictionResultRepository : RepositoryBase
    {
        protected override string TableName
        {
            get { return "nn.PredictionResult"; }
        }

        public void InsertPredictionValues(IEnumerable<Prediction> predictionValues)
        {
            var dataTable = new DataTable
            {
                Columns =
                {
                    new DataColumn("Id", typeof(int)),
                    new DataColumn("PredictedValue", typeof(double)),
                    new DataColumn("PercentDefaulted", typeof(double)),
                    new DataColumn("PercentRecovered", typeof(double))
                }
            };

            foreach (var predictionValue in predictionValues)
            {
                dataTable.Rows.Add(predictionValue.Id, predictionValue.PredictedValue, predictionValue.PercentDefaulted, predictionValue.PercentRecovered);
            }

            BulkInsert(dataTable);
        }
    }
}
