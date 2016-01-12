using System;

namespace RevStack.Mvc.Serialization
{
	/// <summary>
	/// Marks a property as optional when being serialized. 
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class OptionalAttribute : Attribute {
	}
}