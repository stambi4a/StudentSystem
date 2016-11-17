namespace StudentSystem.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Runtime.Versioning;

    using StudentSystem.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<StudentSystem.Data.StudentSystemContext>
    {
        public Configuration()
        {
            //this.AutomaticMigrationDataLossAllowed = true;
            this.AutomaticMigrationsEnabled = true;
            this.ContextKey = "StudentSystem.Data.StudentSystemContext";
        }

        protected override void Seed(StudentSystemContext context)
        {
            //if (context.Students.Any() || context.Courses.Any() || context.Resources.Any() || context.Homeworks.Any())
            //{
            //    return;
            //}

            var random = new Random();
           
            using (var sr = new StreamReader(@"E:\SoftUni\Databases\Advanced\Relations\Exercises\StudentSystem\students.txt"))
            {
                sr.ReadLine();
                var line = sr.ReadLine();
                while (line != null)
                {
                    var studentParams = line.Split(new[] { ' ' }, 5);
                    var studentName = studentParams[0] + " " + studentParams[1];
                    var phoneNumber = studentParams[2];
                    var birthDay = DateTime.ParseExact(studentParams[3], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    var registrationDate = DateTime.ParseExact(studentParams[4], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    var student = new Student { Name = studentName, PhoneNumber = phoneNumber, BirthDay = birthDay, RegistrationDate = registrationDate };
                    context.Students.AddOrUpdate(s=>s.Name, student);

                    line = sr.ReadLine();
                }
            }

            context.SaveChanges();

            using (var sr = new StreamReader(@"E:\SoftUni\Databases\Advanced\Relations\Exercises\StudentSystem\courses.txt"))
            {
                sr.ReadLine();
                var line = sr.ReadLine();
                var students = context.Students.Local;
                while (line != null)
                {
                    var data = line.Split(new[] { ';' }, 5);
                    var name = data[0];
                    var description = data[1];
                    var startDate = DateTime.ParseExact(data[2], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    var endDate = DateTime.ParseExact(data[3], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    var price = decimal.Parse(data[4]);

                    //var studentsCount = random.Next(0, students.Count());
                    //var courseStudents = new HashSet<Student>();
                    //for (var i = 1; i <= studentsCount; i++)
                    //{
                    //    var studentIndex = random.Next(0, studentsCount);
                    //    var student = students[studentIndex];
                    //    courseStudents.Add(student);
                    //}

                    //var course = new Course()
                    //{
                    //    Name = name,
                    //    Description = description,
                    //    StartDate = startDate,
                    //    EndDate = endDate,
                    //    Price = price,
                    //    Students = courseStudents
                    //};

                    var course = context.Courses.FirstOrDefault(c => c.Name == name) ?? new Course
                                                                                            {
                                                                                                Name = name,
                                                                                                Description = description,
                                                                                                StartDate = startDate,
                                                                                                EndDate = endDate,
                                                                                                Price = price,
                                                                                            };

                    var studentsCount = random.Next(0, students.Count + 1);
                    var courseStudents = course.Students;
                    for (var i = 1; i <= studentsCount; i++)
                    {
                        var studentIndex = random.Next(0, studentsCount);
                        var student = students[studentIndex];
                        courseStudents.Add(student);
                    }

                    context.Courses.AddOrUpdate(c=>c.Name, course);
                    line = sr.ReadLine();
                }
            }

            context.SaveChanges();

            using (var sr = new StreamReader(@"E:\SoftUni\Databases\Advanced\Relations\Exercises\StudentSystem\resources.txt"))
            {
                sr.ReadLine();
                var line = sr.ReadLine();
                var courses = context.Courses.Local;
                var coursesCount = courses.Count;
                while (line != null)
                {
                    var data = line.Split(new[] { ';' }, 4);
                    var name = data[0];
                    var resourceType = (ResourceType)Enum.Parse(typeof(ResourceType), data[1]);
                    var url = data[2];
                    var courseIndex = random.Next(0, coursesCount);
                    var course = courses[courseIndex];

                    //var resource = context.Resources.FirstOrDefault(r => r.Name == name);
                    //if (resource != null)
                    //{
                    //    resource.Course = course;
                    //}
                    //else
                    //{
                    //    resource = new Resource()
                    //    {
                    //        Name = name,
                    //        ResourceType = resourceType,
                    //        Url = url,
                    //        Course = course
                    //    };
                    //}

                    var resource = new Resource
                    {
                        Name = name,
                        ResourceType = resourceType,
                        Url = url,
                        Course = course
                    };

                    context.Resources.AddOrUpdate(r=>r.Name, resource);
                    line = sr.ReadLine();
                }
            }

            context.SaveChanges();


            using (var sr = new StreamReader(@"E:\SoftUni\Databases\Advanced\Relations\Exercises\StudentSystem\homeworks.txt"))
            {
                sr.ReadLine();
                var line = sr.ReadLine();
                var courses = context.Courses.Local;
                var coursesCount = courses.Count;
                //var students = context.Students;
                //var studentsCount = students.Count();
                while (line != null)
                {
                    var data = line.Split(new[] { ';' }, 5);
                    var content = data[0];
                    var contentType = (ContentType)Enum.Parse(typeof(ContentType), data[1]);
                    var submissionDate = DateTime.ParseExact(data[2], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    var courseIndex = random.Next(0, coursesCount);
                    var course = courses[courseIndex];
                    var students = course.Students.ToArray();
                    var studentsCount = students.Count();
                    var studentIndex = random.Next(0, studentsCount);
                    var student = students[studentIndex];
                    //var homework = context.Homeworks.FirstOrDefault(h => h.Content == content);
                    //if (homework != null)
                    //{
                    //    homework.Course = course;
                    //    homework.Student = student;
                    //}
                    //else
                    //{
                    //    homework = new Homework
                    //    {
                    //        Content = content,
                    //        ContentType = contentType,
                    //        SubmissionDate = submissionDate,
                    //        Student = student,
                    //        Course = course
                    //    };
                    //}

                    var homework = new Homework
                    {
                        Content = content,
                        ContentType = contentType,
                        SubmissionDate = submissionDate,
                        Student = student,
                        Course = course
                    };

                    //student.Homeworks.Add(homework);
                    //course.Homeworks.Add(homework);
                    context.Homeworks.AddOrUpdate(h=>h.Content, homework);
                    line = sr.ReadLine();
                }
            }

            context.SaveChanges();

            using (var reader = new StreamReader(@"E:\SoftUni\Databases\Advanced\Relations\Exercises\StudentSystem\licenses.txt"))
            {
                reader.ReadLine();
                var name = reader.ReadLine();
                while (name != null)
                {
                    name = name.Trim();
                    var resources = context.Resources.Local;
                    var resourcesCount = resources.Count;
                    var resourceIndex = random.Next(0, resourcesCount);
                    var resource = resources[resourceIndex];
                    var license = context.Licenses.FirstOrDefault(l => l.Name == name);
                    if (license != null)
                    {
                        license.Resource = resource;
                    }
                    else
                    {
                        license = new License { Name = name, Resource = resource };
                    }

                    context.Licenses.AddOrUpdate(l=>l.Name, license);
                    name = reader.ReadLine();
                }
            }

            context.SaveChanges();
        }
    }
}
