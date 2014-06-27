namespace UpidaExampleAngularEF.Dao.Support
{
	public class MyContextFactory
	{
		private const string CONN_STRING = @"name=MyOrdersEntities";

		public virtual MyContext GetCurrentSession()
		{
			MyContext session = new MyContext(CONN_STRING);
			return session;
		}
	}
}