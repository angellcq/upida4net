using System;
using UpidaExampleAngularEF.Dao.Support;
using UpidaExampleAngularEF.Domain;

namespace UpidaExampleAngularEF.Dao
{
	public interface IDaobase<T>
	{
		DbSession Session { get; }
		void SaveChanges();
	}
}