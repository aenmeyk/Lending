using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAccess.Repositories
{
    public class RawDataRepository : RepositoryBase
    {
        protected override string TableName
        {
            get { return "dbo._RawLoanStats"; }
        }

        public IEnumerable<T> GetCompletedLoans<T>()
        {
            const string queryTemplate = @"
SELECT *
FROM _RawLoanStats
WHERE initial_list_status = 'f'
    AND (DATEDIFF(m,issue_d_date,'20150331') >= 36 AND term = ' 36 months')
    AND loan_status IN ('Charged Off', 'Fully Paid')";

            var queryText = string.Format(queryTemplate, TableName);
            return ExecuteSelect<T>(queryText);
        }
    }
}
