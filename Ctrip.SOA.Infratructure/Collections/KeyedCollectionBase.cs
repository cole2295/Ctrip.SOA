using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Ctrip.SOA.Infratructure.Utility;

namespace Ctrip.SOA.Infratructure.Collections
{
    [Serializable]
    public abstract class KeyedCollectionBase<TKey, T> : KeyedCollection<TKey, T>
    {
        /// <summary>
        /// 初始化使用默认相等比较器的 <b>KeyedCollection</b> 类的新实例。 
        /// </summary>
        public KeyedCollectionBase()
            : base()
        {
        }

        /// <summary>
        /// 初始化使用指定字符串相等比较器的 <b>KeyedCollection</b> 类的新实例。 
        /// </summary>
        /// <param name="comparer">比较键时要使用的 <see cref="comparer"/>。</param>
        public KeyedCollectionBase(IEqualityComparer<TKey> comparer)
            : base(comparer)
        {
        }

        /// <summary>
        /// 将元素插入集合的指定索引处。
        /// </summary>
        /// <param name="index">从零开始的索引，应在该位置插入 item。</param>
        /// <param name="item">要插入的对象。</param>
        protected override void InsertItem(int index, T item)
        {
            Guard.ArgumentNotNull(item, "item");

            TKey key = this.GetKeyForItem(item);
            if (this.Contains(key))
                throw new DuplicateKeyException(key.ToString(), this.GetType().FullName);

            base.InsertItem(index, item);
        }

        /// <summary>
        /// 获取具有指定键的元素。
        /// </summary>
        /// <param name="key">要获取的元素的键。</param>
        /// <returns>带有指定键的元素。如果未找到具有指定键的元素，则默认为键值类型初始默认值。</returns>
        public new T this[TKey key]
        {
            get
            {
                T item = default(T);
                if (Contains(key))
                    item = base[key];

                return item;
            }
        }
    }
}
