﻿using System;

namespace CommandSample.Server.Extensions
{
	internal static class TypeExtensions
	{
		public static bool IsAssignableToGenericType(this Type givenType, Type genericType)
		{
			var interfaceTypes = givenType.GetInterfaces();

			foreach (var it in interfaceTypes)
			{
				if (it.IsGenericType && it.GetGenericTypeDefinition() == genericType)
				{
					return true;
				}
			}

			if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
			{
				return true;
			}

			var baseType = givenType.BaseType;

			if (baseType == null)
			{
				return false;
			}

			return baseType.IsAssignableToGenericType(genericType);
		}
	}
}
