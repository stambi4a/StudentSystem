namespace StudentSystem.ConsoleClient
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using StudentSystem.Data;
    using StudentSystem.Models;

    public class Program
    {
        public static void Main(string[] args)
        {
            var context = new StudentSystemContext();
            context.Database.Initialize(true);
            //context.Students.AddOrUpdate(st=>st.Name, new Student() {Name = "Babatundi Adenidje", BirthDay = new DateTime(1996,11,11), PhoneNumber = "0898773245", RegistrationDate = new DateTime(2014,12,10)});
            //context.SaveChanges();
            //var student = context.Students.Find(1);
            //context.Students.Remove(student);
            //ListStudentsAndTheirHomeworkSubmissions();
            //ListCoursesWithResources();
            //ListCoursesWithMoreThan5Resources();
            //var date = new DateTime(2016, 8, 8);
            //ListCoursesActiveOnAGivenDate(date);
            //ListStudentsWithCourses();
        }

        //1. Lists all students and their homework submissions.
        public static void ListStudentsAndTheirHomeworkSubmissions()
        {
            using (var context = new StudentSystemContext())
            {
                var students = context.Students.Select(st=> new
                                                                {
                                                                    st.Name,
                                                                    st.Homeworks
                                                                });
                foreach (var student in students)
                {
                    Console.WriteLine(student.Name);
                    foreach (var homework in student.Homeworks)
                    {
                        Console.WriteLine($"--{homework.Content} {homework.ContentType}");
                    }
                }
            }
        }

        //2. Lists all courses and their resources.
        public static void ListCoursesWithResources()
        {
            using (var context = new StudentSystemContext())
            {
                var courses = context.Courses.Select(st => new
                {
                    st.Name,
                    st.Description,
                    st.Resources
                });
                foreach (var course in courses)
                {
                    Console.WriteLine($"{course.Name} - {course.Description}");
                    foreach (var resource in course.Resources)
                    {
                        Console.WriteLine($"--{resource.Name};{resource.ResourceType};{resource.Url}");
                    }
                }
            }
        }

        //3. Lists all course with more than 5 resources.
        public static void ListCoursesWithMoreThan5Resources()
        {
            using (var context = new StudentSystemContext())
            {
                var courses =
                    context.Courses.Where(c => c.Resources.Count() > 5)
                        .OrderByDescending(c => c.Resources.Count())
                        .ThenByDescending(c => c.StartDate)
                        .Select(c=>new
                                       {
                                           c.Name,
                                           Count = c.Resources.Count
                                       });
                foreach (var course in courses)
                {
                    Console.WriteLine($"{course.Name} - {course.Count}");
                }
            }
        }

        //4. Courses active on a given date.
        public static void ListCoursesActiveOnAGivenDate(DateTime date)
        {
            using (var context = new StudentSystemContext())
            {
                var courses = context.Courses.Where(c => c.StartDate.CompareTo(date) <= 0 && c.EndDate.CompareTo(date) >= 0)
                    .Select(c => new
                                     {
                                         c.Name,
                                         c.StartDate,
                                         c.EndDate,
                                         Duration = DbFunctions.DiffDays(c.StartDate,c.EndDate),
                                         c.Students.Count
                                     })
                                     .OrderByDescending(c=>c.Count)
                                     .ThenByDescending(c=>c.Duration).ToList();
                foreach (var course in courses)
                {
                    Console.WriteLine($"{course.Name} - {course.StartDate} - {course.EndDate} - {course.Duration} - {course.Count}");
                }
            }
        }

        //5. Students with courses, total courses price and and average course price.
        public static void ListStudentsWithCourses()
        {
            using (var context = new StudentSystemContext())
            {
                var students =
                    context.Students.Select(
                        s =>
                        new
                            {
                                s.Name,
                                s.Courses.Count,
                                TotalPrice = s.Courses.Any() ? s.Courses.Sum(c => c.Price) : 0,
                                AveragePrice = s.Courses.Any() ? s.Courses.Average(c => c.Price) : 0
                            });
                foreach (var student in students)
                {
                    Console.WriteLine($"{student.Name} - {student.Count} - {student.TotalPrice} - {student.AveragePrice}");
                }
            }
        }
    }

    //public class Student
    //{
    //    public Student(string name)
    //    {
    //        this.Name = name;
    //    }

    //    public string Name { get; }

    //    public override bool Equals(object obj)
    //    {
    //        return base.Equals(obj);
    //    }
    //}
}
