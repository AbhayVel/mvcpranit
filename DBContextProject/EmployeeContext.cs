using FirstEnity;
using FirstEnity.Entity;
using Microsoft.EntityFrameworkCore;
using System;

namespace DBContextProject
{
    public partial class EmployeeContext : DbContext
    {

        public EmployeeContext() : base()
        {
         //   base.c.LazyLoadingEnabled = false;
        }

      

        public EmployeeContext(DbContextOptions<EmployeeContext> options)
            : base(options)
        {
           // this.Configuration.LazyLoadingEnabled = false;
        }


        public virtual DbSet<Department> Departments { get; set; }
       // public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<User> UserDbSet { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

              //  optionsBuilder;
                // optionsBuilder.Use
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=G:\\Abhay\\angular\\indrajeet\\csharp\\test.mdf;Integrated Security=True;Connect Timeout=30");

                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.DeptId).HasColumnName("dept_id");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Dept)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DeptId)
                    .HasConstraintName("FK__Employee__dept_i__398D8EEE");
            });

             OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
