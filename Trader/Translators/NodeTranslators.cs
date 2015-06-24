using Common.Models;

namespace Trader.Translators
{
    public static class NodeTranslators
    {
        private static double _baseFicoScore = 600;
        private static double _adjustedHighFicoScore = 850 - _baseFicoScore;

        public static double AnnualIncome(RawDataItem rawDataItem)
        {
            var nodeValue = (double)rawDataItem.annual_inc / 250000;
            return TrimReturnValue(nodeValue);
        }

        public static double Collections(RawDataItem rawDataItem)
        {
            var nodeValue = rawDataItem.collections_12_mths_ex_med / 3.0;
            return TrimReturnValue(nodeValue);
        }

        public static double Delinquencies(RawDataItem rawDataItem)
        {
            var nodeValue = rawDataItem.delinq_2yrs / 5.0;
            return TrimReturnValue(nodeValue);
        }

        public static double Dti(RawDataItem rawDataItem)
        {
            return (double)rawDataItem.dti;
        }

        public static double EarliestCreditLine(RawDataItem rawDataItem)
        {
            var nodeValue = (rawDataItem.issue_d_date - rawDataItem.earliest_cr_line_date).TotalDays / 365 / 30.0;
            return TrimReturnValue(nodeValue);
        }

        public static double EmploymentLength(RawDataItem rawDataItem)
        {
            const double numberOfItems = 11;
            double nodeValue = 0;

            switch (rawDataItem.emp_length)
            {
                case "< 1 year":
                    nodeValue = 1 / numberOfItems;
                    break;
                case "1 year":
                    nodeValue = 2 / numberOfItems;
                    break;
                case "2 years":
                    nodeValue = 3 / numberOfItems;
                    break;
                case "3 years":
                    nodeValue = 4 / numberOfItems;
                    break;
                case "4 years":
                    nodeValue = 5 / numberOfItems;
                    break;
                case "5 years":
                    nodeValue = 6 / numberOfItems;
                    break;
                case "6 years":
                    nodeValue = 7 / numberOfItems;
                    break;
                case "7 years":
                    nodeValue = 8 / numberOfItems;
                    break;
                case "8 years":
                    nodeValue = 9 / numberOfItems;
                    break;
                case "9 years":
                    nodeValue = 10 / numberOfItems;
                    break;
                case "10+ years":
                    nodeValue = 11 / numberOfItems;
                    break;
                default:
                    nodeValue = 0 / numberOfItems;
                    break;
            }

            return nodeValue;
        }

        public static double IsEmploymed(RawDataItem rawDataItem)
        {
            return rawDataItem.emp_length == @"n/a" ? 0 : 1;
        }

        public static double FicoLow(RawDataItem rawDataItem)
        {
            return (rawDataItem.fico_range_low - _baseFicoScore) / _adjustedHighFicoScore;
        }

        public static double FicoHigh(RawDataItem rawDataItem)
        {
            return (rawDataItem.fico_range_high - _baseFicoScore) / _adjustedHighFicoScore;
        }

        public static double Grade(RawDataItem rawDataItem)
        {
            const double numberOfItems = 6;
            double nodeValue = 0;

            switch (rawDataItem.grade)
            {
                case "A":
                    nodeValue = 6 / numberOfItems;
                    break;
                case "B":
                    nodeValue = 5 / numberOfItems;
                    break;
                case "C":
                    nodeValue = 4 / numberOfItems;
                    break;
                case "D":
                    nodeValue = 3 / numberOfItems;
                    break;
                case "E":
                    nodeValue = 2 / numberOfItems;
                    break;
                case "F":
                    nodeValue = 1 / numberOfItems;
                    break;
                case "G":
                    nodeValue = 0 / numberOfItems;
                    break;
            }

            return nodeValue;
        }

        public static double SubGrade(RawDataItem rawDataItem)
        {
            const double numberOfItems = 34;
            double nodeValue = 0;

            switch (rawDataItem.grade)
            {
                case "A1":
                    nodeValue = 34 / numberOfItems;
                    break;
                case "A2":
                    nodeValue = 33 / numberOfItems;
                    break;
                case "A3":
                    nodeValue = 32 / numberOfItems;
                    break;
                case "A4":
                    nodeValue = 31 / numberOfItems;
                    break;
                case "A5":
                    nodeValue = 30 / numberOfItems;
                    break;
                case "B1":
                    nodeValue = 29 / numberOfItems;
                    break;
                case "B2":
                    nodeValue = 28 / numberOfItems;
                    break;
                case "B3":
                    nodeValue = 27 / numberOfItems;
                    break;
                case "B4":
                    nodeValue = 26 / numberOfItems;
                    break;
                case "B5":
                    nodeValue = 25 / numberOfItems;
                    break;
                case "C1":
                    nodeValue = 24 / numberOfItems;
                    break;
                case "C2":
                    nodeValue = 23 / numberOfItems;
                    break;
                case "C3":
                    nodeValue = 22 / numberOfItems;
                    break;
                case "C4":
                    nodeValue = 21 / numberOfItems;
                    break;
                case "C5":
                    nodeValue = 20 / numberOfItems;
                    break;
                case "D1":
                    nodeValue = 19 / numberOfItems;
                    break;
                case "D2":
                    nodeValue = 18 / numberOfItems;
                    break;
                case "D3":
                    nodeValue = 17 / numberOfItems;
                    break;
                case "D4":
                    nodeValue = 16 / numberOfItems;
                    break;
                case "D5":
                    nodeValue = 15 / numberOfItems;
                    break;
                case "E1":
                    nodeValue = 14 / numberOfItems;
                    break;
                case "E2":
                    nodeValue = 13 / numberOfItems;
                    break;
                case "E3":
                    nodeValue = 12 / numberOfItems;
                    break;
                case "E4":
                    nodeValue = 11 / numberOfItems;
                    break;
                case "E5":
                    nodeValue = 10 / numberOfItems;
                    break;
                case "F1":
                    nodeValue = 9 / numberOfItems;
                    break;
                case "F2":
                    nodeValue = 8 / numberOfItems;
                    break;
                case "F3":
                    nodeValue = 7 / numberOfItems;
                    break;
                case "F4":
                    nodeValue = 6 / numberOfItems;
                    break;
                case "F5":
                    nodeValue = 5 / numberOfItems;
                    break;
                case "G1":
                    nodeValue = 4 / numberOfItems;
                    break;
                case "G2":
                    nodeValue = 3 / numberOfItems;
                    break;
                case "G3":
                    nodeValue = 2 / numberOfItems;
                    break;
                case "G4":
                    nodeValue = 1 / numberOfItems;
                    break;
                case "G5":
                    nodeValue = 0 / numberOfItems;
                    break;
            }

            return nodeValue;
        }

        public static double HomeOwn(RawDataItem rawDataItem)
        {
            return rawDataItem.home_ownership == "OWN" ? 1 : 0;
        }

        public static double HomeMortgage(RawDataItem rawDataItem)
        {
            return rawDataItem.home_ownership == "MORTGAGE" ? 1 : 0;
        }

        public static double HomeRent(RawDataItem rawDataItem)
        {
            return rawDataItem.home_ownership == "RENT" ? 1 : 0;
        }

        public static double InitialListStatus(RawDataItem rawDataItem)
        {
            const double numberOfItems = 2;
            double nodeValue;

            switch (rawDataItem.initial_list_status)
            {
                case "f":
                    nodeValue = 0 / numberOfItems;
                    break;
                case "w":
                    nodeValue = 2 / numberOfItems;
                    break;
                default:
                    nodeValue = 1 / numberOfItems;
                    break;
            }

            return nodeValue;
        }

        public static double Inquiries(RawDataItem rawDataItem)
        {
            var nodeValue = (rawDataItem.inq_last_6mths ?? 0) / 5.0;
            return TrimReturnValue(nodeValue);
        }

        public static double Installment(RawDataItem rawDataItem)
        {
            return (double)((rawDataItem.installment * 12) / rawDataItem.annual_inc);
        }

        public static double InterestRate(RawDataItem rawDataItem)
        {
            return (double)rawDataItem.int_rate;
        }

        public static double VerificationStatus(RawDataItem rawDataItem)
        {
            const double numberOfItems = 2;
            double nodeValue = 0;

            switch (rawDataItem.initial_list_status)
            {
                case "VERIFIED - income":
                    nodeValue = 0 / numberOfItems;
                    break;
                case "VERIFIED - income source":
                    nodeValue = 1 / numberOfItems;
                    break;
                case "not verified":
                    nodeValue = 2 / numberOfItems;
                    break;
            }

            return nodeValue;
        }

        public static double LoanAmount(RawDataItem rawDataItem)
        {
            return (double)(rawDataItem.loan_amnt / rawDataItem.annual_inc);
        }

        public static double MonthsSinceDelinquent(RawDataItem rawDataItem)
        {
            var nodeValue = (rawDataItem.mths_since_last_delinq ?? 120.0) / 120.0;
            return TrimReturnValue(nodeValue);
        }

        public static double MonthsSinceDerogatoryRemark(RawDataItem rawDataItem)
        {
            var nodeValue = (rawDataItem.mths_since_last_major_derog ?? 120.0) / 120.0;
            return TrimReturnValue(nodeValue);
        }

        public static double MonthsSincePublicRecord(RawDataItem rawDataItem)
        {
            var nodeValue = (rawDataItem.mths_since_last_record ?? 120.0) / 120.0;
            return TrimReturnValue(nodeValue);
        }

        public static double OpenAccounts(RawDataItem rawDataItem)
        {
            var nodeValue = (rawDataItem.open_acc ?? 0) / 50.0;
            return TrimReturnValue(nodeValue);
        }

        public static double DerogatoryPublicRecords(RawDataItem rawDataItem)
        {
            var nodeValue = (rawDataItem.pub_rec ?? 0) / 6.0;
            return TrimReturnValue(nodeValue);
        }

        public static double PurposeCar(RawDataItem rawDataItem)
        {
            return rawDataItem.purpose == "car" ? 1 : 0;
        }

        public static double Purpose(RawDataItem rawDataItem)
        {
            return rawDataItem.purpose == "" ? 1 : 0;
        }

        public static double PurposeCreditCard(RawDataItem rawDataItem)
        {
            return rawDataItem.purpose == "credit_card" ? 1 : 0;
        }

        public static double PurposeDebtConsolidation(RawDataItem rawDataItem)
        {
            return rawDataItem.purpose == "debt_consolidation" ? 1 : 0;
        }

        public static double PurposeHomeImprovement(RawDataItem rawDataItem)
        {
            return rawDataItem.purpose == "home_improvement" ? 1 : 0;
        }

        public static double PurposeHouse(RawDataItem rawDataItem)
        {
            return rawDataItem.purpose == "house" ? 1 : 0;
        }

        public static double PurposeMajorPurchase(RawDataItem rawDataItem)
        {
            return rawDataItem.purpose == "major_purchase" ? 1 : 0;
        }

        public static double PurposeMedical(RawDataItem rawDataItem)
        {
            return rawDataItem.purpose == "medical" ? 1 : 0;
        }

        public static double PurposeMoving(RawDataItem rawDataItem)
        {
            return rawDataItem.purpose == "moving" ? 1 : 0;
        }

        public static double PurposeOther(RawDataItem rawDataItem)
        {
            return rawDataItem.purpose == "other" ? 1 : 0;
        }

        public static double PurposeRenewableEnergy(RawDataItem rawDataItem)
        {
            return rawDataItem.purpose == "renewable_energy" ? 1 : 0;
        }

        public static double PurposeSmallBusiness(RawDataItem rawDataItem)
        {
            return rawDataItem.purpose == "small_business" ? 1 : 0;
        }

        public static double PurposeVacation(RawDataItem rawDataItem)
        {
            return rawDataItem.purpose == "vacation" ? 1 : 0;
        }

        public static double PurposeWedding(RawDataItem rawDataItem)
        {
            return rawDataItem.purpose == "wedding" ? 1 : 0;
        }

        public static double RevolvingCreditBalance(RawDataItem rawDataItem)
        {
            var nodeValue = (double)rawDataItem.revol_bal / 50000.0;
            return TrimReturnValue(nodeValue);
        }

        public static double RevolvingCreditUtilization(RawDataItem rawDataItem)
        {
            return (double)rawDataItem.revol_util;
        }

        public static double Term(RawDataItem rawDataItem)
        {
            return rawDataItem.term == " 36 months" ? 0 : 1;
        }

        public static double CreditLines(RawDataItem rawDataItem)
        {
            var nodeValue = (rawDataItem.total_acc ?? 0) / 60.0;
            return TrimReturnValue(nodeValue);
        }

        private static double TrimReturnValue(double value)
        {
            if (value >= 1) return 1;
            if (value <= 0) return 0;
            return value;
        }
    }
}
