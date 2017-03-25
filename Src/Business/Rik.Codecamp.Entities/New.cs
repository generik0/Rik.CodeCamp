using System.ComponentModel.DataAnnotations;
using Dapper.FastCrud;
using Smooth.IoC.UnitOfWork;

namespace Rik.Codecamp.Entities
{
    public class New : IEntity<int>
    {
        [Key, DatabaseGeneratedDefaultValue]
        public int Id { get; set; }
        public double Value { get; set; }
    }
}
