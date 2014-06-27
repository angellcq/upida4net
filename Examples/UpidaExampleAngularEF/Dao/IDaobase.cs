using System;
using UpidaExampleAngularEF.Dao.Support;

namespace UpidaExampleAngularEF.Dao
{
	public interface IDaobase<T>
	{
		MyContext Session { get; }
	}
}