using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using SomeProducts.CrossCutting.Helpers;
using SomeProducts.DAL.IDao;
using SomeProducts.DAL.Models.Audit;
using SomeProducts.DAL.Repository.Interface;

namespace SomeProducts.DAL.Dao
{
    public class AuditDao : IAuditDao
    {
        private readonly IRepository<AuditItem> _repository;
        private readonly IUserHelper _user;

        public AuditDao(IRepository<AuditItem> repository, IUserHelper user)
        {
            _repository = repository;
            _user = user;
        }

        public void CreateCreateAuditItem<T>(T createdObject)
        {
            var auditItem = new AuditItem()
            {
                AuditEntityId = GetEntity(typeof(T)),
                EntityId = GetObjectId(createdObject),
                StatusId = Status.Create,
                UserId = _user.GetUserId(),
                ModifiedDateTime = DateTime.Now
            };
            CreateAuditItem(auditItem);
        }

        public void CreateDeleteAuditItem<T>(T removingObject)
        {
            var auditItem = new AuditItem()
            {
                AuditEntityId = GetEntity(typeof(T)),
                EntityId = GetObjectId(removingObject),
                StatusId = Status.Delete,
                UserId = _user.GetUserId(),
                ModifiedDateTime = DateTime.Now
            };
            CreateAuditItem(auditItem);
        }

        public int CreateEditAuditItems<T>(T previousObject, T nextObject)
        {
            if (GetObjectId(previousObject) != GetObjectId(nextObject))
            {
                return 0;
            }
            var count = 0;
            var properties = previousObject.GetType().GetProperties();
            foreach (var property in properties)
            {
                var auditItem = CreatePropertyAuditItem(property, previousObject, nextObject);
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

        private AuditItem CreatePropertyAuditItem<T>(PropertyInfo property, T previousObject, T nextObject)
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
                UserId = _user.GetUserId(),
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

        public IQueryable<AuditItem> GetCompanyItems(int companyId)
        {
            return GetAllItems().Where(i => i.User.CompanyId == companyId);
        }
    }
}
