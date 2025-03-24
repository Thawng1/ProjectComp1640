﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectComp1640.Model;

namespace ProjectComp1640.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
        {

        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Tutor> Tutors { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<ClassStudent> ClassStudents { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Messages>()
                .HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Messages>()
                .HasOne(m => m.Receiver)
                .WithMany()
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ClassStudent>()
                .HasKey(sc => new { sc.StudentId, sc.ClassId });
            builder.Entity<ClassStudent>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.ClassStudents)
                .HasForeignKey(sc => sc.StudentId);
            builder.Entity<ClassStudent>()
                .HasOne(sc => sc.Class)
                .WithMany(c => c.ClassStudents)
                .HasForeignKey(sc => sc.ClassId);
            builder.Entity<Tutor>()
                .HasMany(e => e.Classes)
                .WithOne(e => e.Tutor)
                .HasForeignKey(e => e.TutorId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Subject>()
                .HasMany(e => e.Classes)
                .WithOne(e => e.Subject)
                .HasForeignKey(e => e.SubjectId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Schedule>()
                .HasOne(s => s.Class)
                .WithMany(s => s.Schedules)
                .HasForeignKey(s => s.ClassId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Classroom>()
                .HasMany(clr => clr.Schedules)
                .WithOne(clr => clr.Classroom)
                .HasForeignKey(clr => clr.ClassroomId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Blog>()
               .HasOne(b => b.User)
               .WithMany()
               .HasForeignKey(b => b.UserId)
               .OnDelete(DeleteBehavior.Restrict);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "Tutor",
                    NormalizedName = "TUTOR"
                },
                new IdentityRole
                {
                     Name = "Student",
                    NormalizedName = "STUDENT"
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
