namespace DefaultNamespace
{
	using System.Collections.Generic;
	using UnityEngine;

	public static class Extensions
	{
		public static T GetRandomElement<T>(this List<T> list)
		{
			if (list == null || list.Count <= 0)
			{
				return default(T);
			}
			
			var index = Random.Range(0, list.Count);
			return list[index];
		}
	}
}