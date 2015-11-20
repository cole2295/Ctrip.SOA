using Ctrip.SOA.Infratructure.Utility;

namespace Ctrip.SOA.Infratructure.Common.Search
{
    /// <summary>
    /// 排序类型。
    /// </summary>
    public enum SortingType
    {
        /// <summary>
        /// 升序
        /// </summary>
        [EnumItem("ASC", "升序")]
        ASC,

        /// <summary>
        /// 降序
        /// </summary>
        [EnumItem("DESC", "降序")]
        DESC
    }

    /// <summary>
    /// 排序字段。
    /// </summary>
    /// <typeparam name="TFieldType"></typeparam>
    public struct SortField<TFieldType>
    {
        public SortField(TFieldType fieldType, SortingType sortingType)
            : this()
        {
            FieldType = fieldType;
            SortingType = sortingType;
        }

        /// <summary>
        /// 字段类型。
        /// </summary>
        public TFieldType FieldType { get; set; }

        /// <summary>
        /// 排序方向。
        /// </summary>
        public SortingType SortingType { get; set; }
    }
}