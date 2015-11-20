namespace Ctrip.SOA.Infratructure.Entity
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IDataMapperProvider
    {
        MapResult<TEntity> Map<TEntity>(IRowDataSource dataSource);
    }
}