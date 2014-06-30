using System.Configuration;
using System.Web;
using UpidaExampleAngularEF.Domain;

namespace UpidaExampleAngularEF.Dao.Support
{
	public class DbSessionFactory
	{
		private const string SESSION_KEY = "EF_CONTEXT";
		public virtual DbSession GetCurrentSession()
		{
			object session = HttpContext.Current.Items[SESSION_KEY];
			if (null != session)
			{
				return session as DbSession;
			}

			lock (this)
			{
				session = HttpContext.Current.Items[SESSION_KEY];
				if (null == session)
				{
					session = new DbSession(ConfigurationManager.ConnectionStrings["Upida"].ConnectionString);
					HttpContext.Current.Items[SESSION_KEY] = session;
				}
			}

			return session as DbSession;
		}
	}
}