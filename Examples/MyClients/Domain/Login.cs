﻿using System;
using Upida;
using Upida.Validation;
using MyClients.Validation;
using iesi = Iesi.Collections.Generic;

namespace MyClients.Domain
{
    [ValidateWith(typeof(LoginSaveValidator), Groups.SAVE)]
    [ValidateWith(typeof(LoginMergeValidator), Groups.MERGE)]
    public class Login : Dtobase
    {
        [Dto(Levels.ID)]
        public virtual int? Id { get; set; }

        [Dto(Levels.GRID)]
        public virtual string Name { get; set; }

        [Dto(Levels.GRID)]
        public virtual string Password { get; set; }

        [Dto(Levels.GRID)]
        public virtual string Enabled { get; set; }

        [Dto(Levels.NEVER)]
        public virtual Client Client { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Login)) return false;
            return Util.AreSame(this.Id, (obj as Login).Id);
        }
    }
}