﻿
namespace SomeProducts.DAL.Models.Audit
{
    public enum Status
    {
        Edit = 1,
        Create,
        Delete
    }

    public class AuditStatus
    {
        public int Id { get; set; }

        public string Value { get; set; }
    }
}
