namespace SomeProducts.DAL.Models.Audit
{
    public class EntityAttribute : System.Attribute
    {
        public Entity Entity { get; }

        public EntityAttribute(Entity entity)
        {
            Entity = entity;
        }
    }
}
