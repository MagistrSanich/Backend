using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Pr_DataBase_5_student.Models
{
    public class StudentsContext : DbContext
    {
        public DbSet<Student>Students { get; set; }
        public DbSet<Course>Courses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasMany(c => c.Students)
            .WithMany(s => s.Courses)
            .Map(t => t.MapLeftKey("CourseId")
            .MapRightKey("StudentId")
            .ToTable("CourseStudent"));

            /*Создает новую таблицу со связью многие-ко-многим. В ней будут сопоставляться 
             Ид студентов и ИД курсов*/
        }

    }
    public class CourseDbInitializer : DropCreateDatabaseAlways<StudentsContext>
    {
        protected override void Seed(StudentsContext context)
        {
            Student s1 = new Student { Id = 1, Name = "Егор", Surname = "Иванов" };
            Student s2 = new Student { Id = 2, Name = "Мария", Surname = "Васильева" };
            Student s3 = new Student { Id = 3, Name = "Олег", Surname = "Кузнецов" };
            Student s4 = new Student { Id = 4, Name = "Ольга", Surname = "Петрова" };

            context.Students.Add(s1);
            context.Students.Add(s2);
            context.Students.Add(s3);
            context.Students.Add(s4);

            Course c1 = new Course
            {
                Id = 1,
                Name = "Операционные системы",
                Students = new List<Student>() { s1, s2, s3 }
            };
            Course c2 = new Course
            {
                Id = 2,
                Name = "Алгоритмы и структуры данных",
                Students = new List<Student>() { s2, s4 }
            };
            Course c3 = new Course
            {
                Id = 3,
                Name = "Основы HTML и CSS",
                Students = new List<Student>() { s3, s4, s1 }
            };

            context.Courses.Add(c1);
            context.Courses.Add(c2);
            context.Courses.Add(c3);

            base.Seed(context);
        }
    }
}