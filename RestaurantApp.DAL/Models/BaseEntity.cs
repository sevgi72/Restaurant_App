using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp.DAL.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

    }
    public class AuditableEntity : BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
