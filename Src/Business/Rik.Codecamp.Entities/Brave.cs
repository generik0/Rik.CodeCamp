using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rik.Codecamp.Entities
{
    public class Brave
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("New")]
        public int NewId { get; set; }
        public New New { get; set; }
        [ForeignKey("World")]
        public int WorldId { get; set; }
        public World World { get; set; }
    }
}
