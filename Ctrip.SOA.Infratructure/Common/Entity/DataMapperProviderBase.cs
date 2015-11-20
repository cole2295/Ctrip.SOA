namespace Ctrip.SOA.Infratructure.Entity
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public abstract class DataMapperProviderBase : IDataMapperProvider
    {
        public abstract MapResult<TEntity> Map<TEntity>(IRowDataSource dataSource);
    }
}