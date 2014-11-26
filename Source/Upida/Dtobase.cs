using System;
using System.Collections.Generic;

namespace Upida
{
    /// <summary>
    /// Represents base DTO class. You must derive your domain/dto classes from Dtobase to make Upida features available.
    /// </summary>
    public class Dtobase
    {
        private ISet<string> assignedFields;
        private ISet<string> wrongFields;

        /// <summary>
        /// Adds field to the list of assigned fields (used internally)
        /// </summary>
        /// <param name="field">field name</param>
        public virtual void AddAssignedField(string field)
        {
            if (null == this.assignedFields)
            {
                this.assignedFields = new HashSet<string>();
            }

            this.assignedFields.Add(field);
        }

        /// <summary>
        /// Adds field to the list of errored fields (used internally)
        /// </summary>
        /// <param name="field">field name</param>
        public virtual void AddWrongField(string field)
        {
            if (null == this.wrongFields)
            {
                this.wrongFields = new HashSet<string>();
            }

            this.wrongFields.Add(field);
        }

        /// <summary>
        /// Checks if field is assigned
        /// </summary>
        /// <param name="field">field name</param>
        /// <returns>true if assigned</returns>
        public virtual bool IsFieldAssigned(string field)
        {
            return null == this.assignedFields
                ? false
                : this.assignedFields.Contains(field);
        }

        /// <summary>
        /// Checks if field is errored
        /// </summary>
        /// <param name="field">field name</param>
        /// <returns>true if errored</returns>
        public virtual bool IsFieldWrong(string field)
        {
            return null == this.wrongFields
                ? false
                : this.wrongFields.Contains(field);
        }

        public virtual ISet<string> GetAssignedFields()
        {
            return this.assignedFields;
        }

        public virtual ISet<string> GetWrongFields()
        {
            return this.wrongFields;
        }
    }
}