using APIStartCourseApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIStartCourseApp.Configurations
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.Property(x => x.FullName).HasMaxLength(20).IsRequired(true);
            builder.HasOne(x => x.Group).WithMany(x => x.Teachers).HasForeignKey(x => x.GroupId).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
