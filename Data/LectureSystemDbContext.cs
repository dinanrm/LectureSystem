using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using LectureSystem.Models;

namespace LectureSystem.Data
{
    public partial class LectureSystemDbContext : DbContext
    {
        public LectureSystemDbContext()
        {
        }

        public LectureSystemDbContext(DbContextOptions<LectureSystemDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Attendances> Attendances { get; set; }
        public virtual DbSet<ClassSchedules> ClassSchedules { get; set; }
        public virtual DbSet<Classrooms> Classrooms { get; set; }
        public virtual DbSet<CourseScores> CourseScores { get; set; }
        public virtual DbSet<Courses> Courses { get; set; }
        public virtual DbSet<Fields> Fields { get; set; }
        public virtual DbSet<Files> Files { get; set; }
        public virtual DbSet<FinalScores> FinalScores { get; set; }
        public virtual DbSet<Lecturers> Lecturers { get; set; }
        public virtual DbSet<Scores> Scores { get; set; }
        public virtual DbSet<Semesters> Semesters { get; set; }
        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<Takes> Takes { get; set; }
        public virtual DbSet<Teaches> Teaches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendances>(entity =>
            {
                entity.HasKey(e => e.AttendId);

                entity.HasIndex(e => e.ClassScheduleId)
                    .HasName("fkIdx_154");

                entity.HasIndex(e => e.LecturerId)
                    .HasName("fkIdx_250");

                entity.HasIndex(e => e.StudentId)
                    .HasName("fkIdx_151");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Reason).HasColumnType("text");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.ClassSchedule)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.ClassScheduleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_154");

                entity.HasOne(d => d.Lecturer)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.LecturerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_250");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_151");
            });

            modelBuilder.Entity<ClassSchedules>(entity =>
            {
                entity.HasKey(e => e.ClassScheduleId)
                    .HasName("PK_Teachers");

                entity.HasIndex(e => e.ClassroomId)
                    .HasName("fkIdx_78");

                entity.HasIndex(e => e.CourseId)
                    .HasName("fkIdx_74");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Day)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Classroom)
                    .WithMany(p => p.ClassSchedules)
                    .HasForeignKey(d => d.ClassroomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_78");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.ClassSchedules)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_74");
            });

            modelBuilder.Entity<Classrooms>(entity =>
            {
                entity.HasKey(e => e.ClassroomId)
                    .HasName("PK_Classes");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CourseScores>(entity =>
            {
                entity.HasKey(e => e.CourseScoreId);

                entity.HasIndex(e => e.ScoreId)
                    .HasName("fkIdx_229");

                entity.HasIndex(e => e.TakeId)
                    .HasName("fkIdx_226");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Score)
                    .WithMany(p => p.CourseScores)
                    .HasForeignKey(d => d.ScoreId)
                    .HasConstraintName("FK_229");

                entity.HasOne(d => d.Take)
                    .WithMany(p => p.CourseScores)
                    .HasForeignKey(d => d.TakeId)
                    .HasConstraintName("FK_226");
            });

            modelBuilder.Entity<Courses>(entity =>
            {
                entity.HasKey(e => e.CourseId)
                    .HasName("PK_Class");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Curriculum)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Fields>(entity =>
            {
                entity.HasKey(e => e.FieldId);

                entity.HasIndex(e => e.CourseId)
                    .HasName("fkIdx_143");

                entity.HasIndex(e => e.LecturerId)
                    .HasName("fkIdx_262");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Fields)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_143");

                entity.HasOne(d => d.Lecturer)
                    .WithMany(p => p.Fields)
                    .HasForeignKey(d => d.LecturerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_262");
            });

            modelBuilder.Entity<Files>(entity =>
            {
                entity.HasKey(e => e.FileId);

                entity.HasIndex(e => e.CourseId)
                    .HasName("fkIdx_160");

                entity.Property(e => e.Author)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Files)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_160");
            });

            modelBuilder.Entity<FinalScores>(entity =>
            {
                entity.HasKey(e => e.FinalScoreId)
                    .HasName("PK_FinalScore");

                entity.HasIndex(e => e.SemesterId)
                    .HasName("fkIdx_204");

                entity.HasIndex(e => e.StudentId)
                    .HasName("fkIdx_201");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.GradePoint).HasColumnType("float");

                entity.Property(e => e.GradePointAverage).HasColumnType("float");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.FinalScores)
                    .HasForeignKey(d => d.SemesterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_204");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.FinalScores)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_201");
            });

            modelBuilder.Entity<Lecturers>(entity =>
            {
                entity.HasKey(e => e.LecturerId);

                entity.Property(e => e.Address).HasColumnType("text");

                entity.Property(e => e.Birthdate).HasColumnType("date");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Uuid)
                    .HasColumnName("UUID")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Scores>(entity =>
            {
                entity.HasKey(e => e.ScoreId);

                entity.Property(e => e.Alphabet)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Semesters>(entity =>
            {
                entity.HasKey(e => e.SemesterId)
                    .HasName("PK_Grades");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Students>(entity =>
            {
                entity.HasKey(e => e.StudentId);

                entity.Property(e => e.Address).HasColumnType("text");

                entity.Property(e => e.Birthdate).HasColumnType("date");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UUID)
                    .HasColumnName("UUID")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Takes>(entity =>
            {
                entity.HasKey(e => e.TakeId);

                entity.HasIndex(e => e.CourseId)
                    .HasName("fkIdx_167");

                entity.HasIndex(e => e.SemesterId)
                    .HasName("fkIdx_177");

                entity.HasIndex(e => e.StudentId)
                    .HasName("fkIdx_142");

                entity.Property(e => e.AcademicYear)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Takes)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_167");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.Takes)
                    .HasForeignKey(d => d.SemesterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_177");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Takes)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_142");
            });

            modelBuilder.Entity<Teaches>(entity =>
            {
                entity.HasKey(e => e.TeachId);

                entity.HasIndex(e => e.ClassScheduleId)
                    .HasName("fkIdx_220");

                entity.HasIndex(e => e.LecturerId)
                    .HasName("fkIdx_253");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.ClassSchedule)
                    .WithMany(p => p.Teaches)
                    .HasForeignKey(d => d.ClassScheduleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_220");

                entity.HasOne(d => d.Lecturer)
                    .WithMany(p => p.Teaches)
                    .HasForeignKey(d => d.LecturerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_253");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
