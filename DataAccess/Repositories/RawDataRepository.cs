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
            var queryText = string.Format("SELECT * FROM {0} WHERE loan_status IN ('Charged Off', 'Fully Paid', 'Default') ", TableName);
            return ExecuteSelect<T>(queryText);
        }
    }
}
