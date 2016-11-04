using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using SomeProducts.DAL.IDao;
using SomeProducts.DAL.Models.Audit;
using SomeProducts.DAL.Repository.Interface;

namespace SomeProducts.DAL.Dao
{
    public class AuditDao : IAuditDao
    {
        private readonly IRepository<AuditItem> _repository;

        public AuditDao(IRepository<AuditItem> repository)
        {
            _repository = repository;
        }

        public void CreateCreateAuditItem<T>(T createdObject, int userId)
        {
            var auditItem = new AuditItem()
            {
                AuditEntityId = GetEntity(typeof(T)),
                EntityId = GetObjectId(createdObject),
                StatusId = Status.Create,
                UserId = userId,
                ModifiedDateTime = DateTime.Now
            };
            CreateAuditItem(auditItem);
        }

        public void CreateDeleteAuditItem<T>(T removingObject, int userId)
        {
            var auditItem = new AuditItem()
            {
                AuditEntityId = GetEntity(typeof(T)),
                EntityId = GetObjectId(removingObject),
                StatusId = Status.Delete,
                UserId = userId,
                ModifiedDateTime = DateTime.Now
            };
            CreateAuditItem(auditItem);
        }

        public int CreateEditAuditItems<T>(T previousObject, T nextObject, int userId)
        {
            if (GetObjectId(previousObject) != GetObjectId(nextObject))
            {
                return 0;
            }
            var count = 0;
            var properties = previousObject.GetType().GetProperties();
            foreach (var property in properties)
            {
                var auditItem = CreatePropertyAuditItem(property, previousObject, nextObject, userId);
                if(auditItem == null) continue;
                _repository.Create(auditItem);
                count++;
            }
            _repository.Save();
            return count;
        }

        private static bool IsPropertyChange(object previousPropertyValue, object nextPropertyValue)
        {
            if (previousPropertyValue == null || nextPropertyValue == null)
            {
                if (previousPropertyValue == nextPropertyValue)
                {
                    return false;
                }
            }
            else if (previousPropertyValue.Equals(nextPropertyValue))
            {
                return false;
            }
            return true;
        }

        private static AuditItem CreatePropertyAuditItem<T>(PropertyInfo property, T previousObject, T nextObject, int userId)
        {
            if (!IsAuditing(property)) return null;
            var previousPropertyValue = property.GetValue(previousObject);
            var nextPropertyValue = property.GetValue(nextObject);
            if (!IsPropertyChange(previousPropertyValue, nextPropertyValue))
                return null;
            return  new AuditItem()
            {
                AuditEntityId = GetEntity(typeof(T)),
                EntityId = GetObjectId(previousObject),
                StatusId = Status.Edit,
                UserId = userId,
                ModifiedDateTime = DateTime.Now,
                ModifiedField = property.Name,
                PreviousValue = previousPropertyValue?.ToString() ?? "null",
                NextValue = nextPropertyValue?.ToString() ?? "null"
            };
        }

        public IQueryable<AuditItem> GetAllItems()
        {
            return _repository.GetAllItems();
        }

        public AuditItem GetItemByid(int id)
        {
            return _repository.GetById(id);
        }

        private static bool IsAuditing(MemberInfo property)
        {
            var auditPropertyAttr = (AuditPropertyAttribute)property
                .GetCustomAttribute(typeof(AuditPropertyAttribute), false);
            return auditPropertyAttr?.IsAuditing ?? false;
        }

        private static Entity GetEntity(MemberInfo type)
        {
            var entityAttr = (EntityAttribute)type
                .GetCustomAttribute(typeof(EntityAttribute), false);
            return  entityAttr?.Entity ?? 0;
        }

        private void CreateAuditItem(AuditItem item)
        {
            _repository.Create(item);
            _repository.Save();
        }

        private static int GetObjectId<T>(T obj)
        {
            int result = (int)obj.GetType().GetProperty("Id").GetValue(obj);
            if (result < 0 )
            {
                throw new WarningException("Object must have id property type of int and positiv value");
            }
            return result;
        }
    }
}
