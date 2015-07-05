using System;

namespace Common.Models
{
    public class Prediction
    {
        /// <summary>
        /// A unique Lending Club assigned ID for the loan listing.
        /// </summary>
        public int Id { get; set; }
        public double PredictedValue { get; set; }
        public double PercentDefaulted { get; set; }
        public double PercentRecovered { get; set; }
    }
}
