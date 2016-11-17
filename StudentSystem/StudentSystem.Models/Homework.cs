namespace StudentSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Homeworks")]
    public class Homework
    {
        public int Id { get; set; }

        [Required]  
        public string Content { get; set; }

        public ContentType ContentType { get; set; }

        public DateTime SubmissionDate { get; set; }

        public int StudentId { get; set; }

        public virtual Student Student { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }
    }
}
