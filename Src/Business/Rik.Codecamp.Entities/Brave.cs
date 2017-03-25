using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dapper.FastCrud;
using Newtonsoft.Json;
using Smooth.IoC.Cqrs.Query;
using Smooth.IoC.Cqrs.Requests;

namespace Rik.Codecamp.Entities
{
    public class Brave : IRequest, IQuery
    {
        [Key, DatabaseGeneratedDefaultValue]
        public int Id { get; set; }
        [ForeignKey("New")]
        public int NewId { get; set; }
        public New New { get; set; }
        [ForeignKey("World")]
        public int WorldId { get; set; }
        public World World { get; set; }

        [NotMapped, JsonIgnore]
        public int Version { get; } = 0;
        [NotMapped, JsonIgnore]
        public Guid QueryId { get; } = Guid.NewGuid();
        [NotMapped, JsonIgnore]
        public Guid RequestId { get; } = Guid.NewGuid();
    }
}
