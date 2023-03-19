using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsManager.Entities
{
    
    public class Group
    {
        public Guid Id { get; init; }
        public string? Name { get; set; }
        public List<Student>? Students { get; set; }
        public int? StudentsCount => Students?.Count;

        public override string ToString()
        {
            return $"{Name} ({StudentsCount})";
        }
    }
}
