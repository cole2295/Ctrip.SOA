using System;

namespace Ctrip.SOA.Infratructure
{
    /// <summary>
    /// Interface representing an object whose value set can be merged with that of a parent object.
    /// </summary>
    public interface IMergable
    {
        /// <summary>
        /// Gets a value indicating whether this instance is merge enabled for this instance
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is merge enabled; otherwise, <c>false</c>.
        /// </value>
        bool MergeEnabled
        {
            get;
        }

        /// <summary>
        /// Merges the current value set with that of the supplied object.
        /// </summary>
        /// <remarks>The supplied object is considered the parent, and values in the 
        /// callee's value set must override those of the supplied object.
        /// </remarks>
        /// <param name="parent">The parent object to merge with</param>
        /// <returns>The result of the merge operation</returns>
        /// <exception cref="ArgumentNullException">If the supplied parent is <code>null</code></exception>
        /// <exception cref="InvalidOperationException">If merging is not enabled for this instance,
        /// (i.e. <code>MergeEnabled</code> equals <code>false</code>.</exception>
        object Merge(object parent);
    }
}
