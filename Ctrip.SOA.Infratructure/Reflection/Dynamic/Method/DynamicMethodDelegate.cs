namespace Ctrip.SOA.Infratructure.Reflection.Dynamic
{
    /// <summary>
    /// Represents a method
    /// </summary>
    /// <param name="target">the target instance when calling an instance method</param>
    /// <param name="args">arguments to be passed to the method</param>
    /// <returns>the value return by the method. <value>null</value> when calling a void method</returns>
    public delegate object MethodDelegate(object target, params object[] args);
}
