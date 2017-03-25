using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Smooth.IoC.UnitOfWork;

namespace Rik.Codecamp.Entities
{
    public class World : IEntity<int>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        private DateTime _dateTime;
        public DateTime DateTime
        {
            get { return _dateTime; }
            set { _dateTime = DateTime.SpecifyKind(value, DateTimeKind.Utc); }
        }
    }
}
