namespace UpidaExampleKnockout.Domain
{
	public class Util
	{
		public static bool AreSame<T>(T a, T b)
		{
			if (null == a || null == b)
			{
				return false;
			}

			return object.Equals(a, b);
		}
	}
}