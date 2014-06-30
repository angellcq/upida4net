using System;

namespace Upida
{
	public interface IChild
	{
		void ConnectToParent(object parent);
	}

	public interface IChildEx : IChild
	{
		void DisconnectFromParrent();
	}
}