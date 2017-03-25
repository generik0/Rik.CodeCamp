using System;
using System.ComponentModel.DataAnnotations;
using Dapper.FastCrud;
using Smooth.IoC.UnitOfWork;

namespace Rik.Codecamp.Entities
{
    public class World : IEntity<int>
    {
        [Key, DatabaseGeneratedDefaultValue]
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
    }
}
