using System;
using Upida;
using Upida.Validation;
using MyClients.Validation;
using iesi = Iesi.Collections.Generic;

namespace MyClients.Domain
{
	[ValidateWith(typeof(LoginSaveValidator), Groups.SAVE)]
	[ValidateWith(typeof(LoginMergeValidator), Groups.MERGE)]
	public class Login : Dtobase, IChild
	{
		[Dto(Levels.ID)]
		public virtual int? Id { get; set; }

		[Dto(Levels.LOOKUP)]
		public virtual string Name { get; set; }

		[Dto(Levels.GRID)]
		public virtual string Password { get; set; }

		[Dto(Levels.GRID)]
		public virtual bool Enabled { get; set; }

		[Dto(Levels.NEVER)]
		public virtual Client Client { get; set; }

		public override bool Equals(object obj)
		{
			if (!(obj is Login)) return false;
			return Util.AreSame(this.Id, (obj as Login).Id);
		}

		public virtual void ConnectToParent(object parent)
		{
			if (parent is Client)
			{
				this.Client = parent as Client;
			}
		}
	}
}