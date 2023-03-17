using Microsoft.EntityFrameworkCore;
using StudentsManager.ValueObjects;
using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace StudentsManager.Entities
{
    [Index(nameof(Name))]
    public class Student
    {
        public Guid Id { get; init; }  //айди неизменяем
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }

        public PassportNumber PassportNumber { get; set; }

        public Group? Group { get; set; }

        public List<Visit>? Visits { get; set; }
        public int? VisitsCount => Visits?.Count;

        public override string ToString() => Name;
    }

}
