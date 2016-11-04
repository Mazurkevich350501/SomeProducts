using System;

namespace SomeProducts.DAL.Models.Audit
{
    public class EntityAttribute : Attribute
    {
        public Entity Entity { get; }

        public EntityAttribute(Entity entity)
        {
            Entity = entity;
        }
    }
}
