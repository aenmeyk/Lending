namespace DataAccess.Repositories
{
    public class RawDataRepository : RepositoryBase
    {
        protected override string TableName
        {
            get { return "dbo._RawLoanStats"; }
        }
    }
}
