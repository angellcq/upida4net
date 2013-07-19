using System;
using System.Collections.Generic;

namespace Upida
{
    public class Dtobase
    {
        private ISet<string> assignedFields;
        private ISet<string> wrongFields;

        public virtual void addAssignedField(string field)
        {
            if (null == this.assignedFields)
            {
                this.assignedFields = new HashSet<string>();
            }

            this.assignedFields.Add(field);
        }

        public virtual void addWrongField(string field)
        {
            if (null == this.wrongFields)
            {
                this.wrongFields = new HashSet<string>();
            }

            this.wrongFields.Add(field);
        }

        public virtual bool isFieldAssigned(string field)
        {
            return null == this.assignedFields
                ? false
                : this.assignedFields.Contains(field);
        }

        public virtual bool isFieldWrong(string field)
        {
            return null == this.wrongFields
                ? false
                : this.wrongFields.Contains(field);
        }
    }
}