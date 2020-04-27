using System;

namespace CommandSample.Domain.Security
{
	public static class ThrowIf
	{
		public static class Argument
		{
			public static void IsNull(object argument, string argumentName)
			{
				if (argument == null)
				{
					throw new ArgumentNullException(argumentName);
				}
			}
		}

		public static class Value
		{
			public static void IsFalse(bool value, string message)
			{
				if (!value)
				{
					throw new ArgumentException(message);
				}
			}
		}
	}
}
