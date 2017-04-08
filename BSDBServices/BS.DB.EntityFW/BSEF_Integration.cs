namespace BS.DB.EntityFW
{
    using CommonTypes;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class BSDBEntities : DbContext
    {
        protected override System.Data.Entity.Validation.DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, System.Collections.Generic.IDictionary<object, object> items)
        {
            if (entityEntry.Entity is BSEntityInterface)
            {
                var Gnd = (BSEntityInterface)entityEntry.Entity;

                return new System.Data.Entity.Validation.DbEntityValidationResult(entityEntry, Gnd.IsValid());
            }
                return base.ValidateEntity(entityEntry, items);
        }
    }
}