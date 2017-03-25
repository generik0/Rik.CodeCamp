using System.ComponentModel.DataAnnotations;
using Smooth.IoC.UnitOfWork;

namespace Rik.Codecamp.Entities
{
    public class New : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public double Value { get; set; }
    }
}
