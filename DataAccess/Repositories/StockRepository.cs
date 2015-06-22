namespace DataAccess.Repositories
{
    public class StockRepository : RepositoryBase
    {
        protected override string TableName
        {
            get { return "dbo.Stock"; }
        }
    }
}
