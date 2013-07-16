using System;
using System.Collections.Generic;

namespace Upida
{
    public class Dtobase
    {
        private ISet<string> assignedFields;
        private ISet<string> wrongFields;

        public virtual void addAssignedField(String field)
        {
            if (null == this.assignedFields)
            {
                this.assignedFields = new HashSet<String>();
            }

            this.assignedFields.Add(field);
        }

        public virtual void addWrongField(String field)
        {
            if (null == this.wrongFields)
            {
                this.wrongFields = new HashSet<String>();
            }

            this.wrongFields.Add(field);
        }

        public virtual bool isFieldAssigned(String field)
        {
            return null == this.assignedFields
                    ? false
                    : this.assignedFields.Contains(field);
        }

        public virtual bool isFieldWrong(String field)
        {
            return null == this.wrongFields
                    ? false
                    : this.wrongFields.Contains(field);
        }
    }
}