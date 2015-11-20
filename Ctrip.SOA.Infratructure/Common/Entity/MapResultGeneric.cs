namespace Ctrip.SOA.Infratructure.Entity
{
    public class MapResult<TEntity> : MapResult
    {
        public MapResult(TEntity entity) : base(typeof(TEntity))
        {
            this.Entity = entity;
        }

        public TEntity Entity { get; set; }           
    }
}