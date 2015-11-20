using System;
using System.Collections.Generic;

namespace Ctrip.SOA.Infratructure.Reflection.Dynamic
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DynamicConstructorCacheKey
    {
        private int _hashKey;

        public DynamicConstructorCacheKey(Type type, IDictionary<string, object> namedArgValues)
		{
			this.Type = type;
            this.NamedArgumentValues = namedArgValues;

			_hashKey = type.FullName.GetHashCode();
            if (namedArgValues != null)
			{
                foreach (object name in namedArgValues)
				{
                    if (name != null)
					{
                        _hashKey = _hashKey ^ name.GetHashCode();
					}
				}
			}
		}

		public Type Type { get; private set; }
        public IDictionary<string, object> NamedArgumentValues { get; set; }

		public override bool Equals(object obj)
		{
            DynamicConstructorCacheKey key = obj as DynamicConstructorCacheKey;
			if (key != null &&
				key.Type == this.Type &&
				ParametersEquals(key.NamedArgumentValues, this.NamedArgumentValues))
				return true;

			return false;
		}

		public override int GetHashCode()
		{
			return _hashKey;
		}

		#region Helpers

		/// <summary>
		/// 比较参数数组是否相同。
		/// </summary>
        private bool ParametersEquals(IDictionary<string, object> a1, IDictionary<string, object> a2)
		{
			if ((a1 == null && a2 != null) || (a1 != null && a2 == null))
			{
				return false;
			}

			if (a1.Count != a2.Count)
			{
				return false;
			}

			foreach (string paramName in a1.Keys)
			{
                if (!a2.Keys.Contains(paramName))
				{
					return false;
				}
			}

			return true;
		}

		#endregion
    }
}
