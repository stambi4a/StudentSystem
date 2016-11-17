namespace StudentSystem.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;

    using StudentSystem.Data.Migrations;
    using StudentSystem.Models;

    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
            : base("StudentSystemContext")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<StudentSystemContext>());
        }

        public virtual IDbSet<Student> Students { get; set; }

        public virtual IDbSet<Course> Courses { get; set; }

        public virtual IDbSet<Resource> Resources { get; set; }

        public virtual IDbSet<Homework> Homeworks { get; set; }

        public virtual IDbSet<License> Licenses { get; set; }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Homework>()
        //        .HasOptional(h=>h.Course)
        //        .WithMany(c=>c.Homeworks)
        //        .WillCascadeOnDelete(true);

        //    modelBuilder.Entity<Homework>()
        //       .HasOptional(h => h.Student)
        //       .WithMany(c=>c.Homeworks)
        //       .WillCascadeOnDelete(true);

        //    modelBuilder.Entity<Resource>()
        //        .HasOptional(r=>r.Course)
        //        .WithMany(r=>r.Resources)
        //        .WillCascadeOnDelete(true);

        //    modelBuilder.Entity<License>()
        //        .HasOptional(l => l.Resource)
        //        .WithMany(r => r.Licenses)
        //        .WillCascadeOnDelete(true);

        //    modelBuilder.Entity<Student>()
        //        .HasMany(s => s.Courses)
        //        .WithMany(c=>c.Students)
        //        .Map(
        //            cs =>
        //                {
        //                    cs.MapLeftKey("StudentId");
        //                    cs.MapRightKey("CourseId");
        //                    cs.ToTable("StudentCourses");
        //                });
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}