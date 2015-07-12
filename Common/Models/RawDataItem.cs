using System;

namespace Common.Models
{
    public class RawDataItem
    {
        /// <summary>
        /// A unique Lending Club assigned ID for the loan listing.
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// The listed amount of the loan applied for by the borrower. If at some point in time, the credit department reduces the loan amount, then it will be reflected in this value.
        /// </summary>
        public decimal loan_amnt { get; set; }

        /// <summary>
        /// The total amount committed to that loan at that point in time.
        /// </summary>
        public decimal funded_amnt { get; set; }

        /// <summary>
        /// The number of payments on the loan. Values are in months and can be either 36 or 60.
        /// </summary>
        public string term { get; set; }

        /// <summary>
        /// Interest Rate on the loan.
        /// </summary>
        public decimal int_rate { get; set; }

        /// <summary>
        /// The monthly payment owed by the borrower if the loan originates.
        /// </summary>
        public decimal installment { get; set; }

        /// <summary>
        /// Lending Club assigned loan grade.
        /// </summary>
        public string grade { get; set; }

        /// <summary>
        /// Lending Club assigned loan subgrade
        /// </summary>
        public string sub_grade { get; set; }

        /// <summary>
        /// Employment length in years. Possible values are between 0 and 10 where 0 means less than one year and 10 means ten or more years. 
        /// </summary>
        public string emp_length { get; set; }

        /// <summary>
        /// The home ownership status provided by the borrower during registration. Our values are: RENT, OWN, MORTGAGE, OTHER.
        /// </summary>
        public string home_ownership { get; set; }

        /// <summary>
        /// The annual income provided by the borrower during registration.
        /// </summary>
        public decimal? annual_inc { get; set; }

        public string verification_status { get; set; }

        /// <summary>
        /// The month which the loan was funded.
        /// </summary>
        public DateTime issue_d_date { get; set; }

        /// <summary>
        /// Current status of the loan
        /// </summary>
        public string loan_status { get; set; }

        /// <summary>
        /// A category provided by the borrower for the loan request. 
        /// </summary>
        public string purpose { get; set; }

        /// <summary>
        /// A ratio calculated using the borrower’s total monthly debt payments on the total debt obligations, excluding mortgage and the requested Lending Club loan, divided by the borrower’s self-reported monthly income.
        /// </summary>
        public decimal dti { get; set; }

        /// <summary>
        /// The number of 30+ days past-due incidences of delinquency in the borrower's credit file for the past 2 years
        /// </summary>
        public int? delinq_2yrs { get; set; }

        /// <summary>
        /// The month the borrower's earliest reported credit line was opened
        /// </summary>
        public DateTime earliest_cr_line_date { get; set; }

        /// <summary>
        /// The lower boundary of range the borrower’s FICO belongs to.
        /// </summary>
        public int fico_range_low { get; set; }

        /// <summary>
        /// The upper boundary of range the borrower’s FICO belongs to.
        /// </summary>
        public int fico_range_high { get; set; }

        /// <summary>
        /// The number of inquiries by creditors during the past 6 months.
        /// </summary>
        public int? inq_last_6mths { get; set; }

        /// <summary>
        /// The number of months since the borrower's last delinquency.
        /// </summary>
        public int? mths_since_last_delinq { get; set; }

        /// <summary>
        /// The number of months since the last public record.
        /// </summary>
        public int? mths_since_last_record { get; set; }

        /// <summary>
        /// The number of open credit lines in the borrower's credit file.
        /// </summary>
        public int? open_acc { get; set; }

        /// <summary>
        /// Number of derogatory public records.
        /// </summary>
        public int? pub_rec { get; set; }

        /// <summary>
        /// Total credit revolving balance.
        /// </summary>
        public decimal revol_bal { get; set; }

        /// <summary>
        /// Revolving line utilization rate, or the amount of credit the borrower is using relative to all available revolving credit.
        /// </summary>
        public decimal revol_util { get; set; }

        /// <summary>
        /// The total number of credit lines currently in the borrower's credit file.
        /// </summary>
        public int? total_acc { get; set; }

        /// <summary>
        /// The initial listing status of the loan. Possible values are – “F” for fractional, “W” for whole.
        /// </summary>
        public string initial_list_status { get; set; }

        /// <summary>
        /// Remaining outstanding principal for total amount funded.
        /// </summary>
        public decimal out_prncp { get; set; }

        /// <summary>
        /// Payments received to date for total amount funded.
        /// </summary>
        public decimal total_pymnt { get; set; }

        /// <summary>
        /// Principal received to date.
        /// </summary>
        public decimal total_rec_prncp { get; set; }

        /// <summary>
        /// Interest received to date.
        /// </summary>
        public decimal total_rec_int { get; set; }

        /// <summary>
        /// Late fees received to date.
        /// </summary>
        public decimal total_rec_late_fee { get; set; }

        /// <summary>
        /// Post charge off gross recovery
        /// </summary>
        public decimal recoveries { get; set; }

        /// <summary>
        /// Number of collections in 12 months excluding medical collections.
        /// </summary>
        public int? collections_12_mths_ex_med { get; set; }

        /// <summary>
        /// Months since most recent 90-day or worse rating.
        /// </summary>
        public int? mths_since_last_major_derog { get; set; }

        /// <summary>
        /// Last month payment was received.
        /// </summary>
        public DateTime last_pymnt_d_date { get; set; }
    }
}
