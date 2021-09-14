using System;

namespace APILibrary.Models.Todos
{
    public class PostQueryDto
    {
        public bool? Completed { get; set; }
        public string CreatedBy { get; set; }
        public bool IsEmpty => Completed is null && CreatedBy is null;
    }
}