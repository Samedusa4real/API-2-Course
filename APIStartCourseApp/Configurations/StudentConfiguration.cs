using APIStartCourseApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIStartCourseApp.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(x=>x.Name).HasMaxLength(15).IsRequired(true);
            builder.Property(x => x.SurName).HasMaxLength(15).IsRequired(true);
            builder.Property(x => x.Email).HasMaxLength(50).IsRequired(true);
            builder.Property(x => x.AveragePoint).IsRequired(true);
            builder.HasOne(x => x.Group).WithMany(x => x.Students).HasForeignKey(x => x.GroupId).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
