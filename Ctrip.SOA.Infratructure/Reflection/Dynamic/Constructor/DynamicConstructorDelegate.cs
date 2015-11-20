namespace Ctrip.SOA.Infratructure.Reflection.Dynamic
{
    /// <summary>
    /// Represents a constructor
    /// </summary>
    /// <param name="args">arguments to be passed to the method</param>
    /// <returns>the new object instance</returns>
    public delegate object ConstructorDelegate(params object[] args);
}