using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.QuestionAggregate;

namespace Shop.Infrastructure.Persistence.EF.Questions;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable("Questions", "question");

        builder.Property(question => question.Description)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(question => question.Status)
            .IsRequired()
            .HasMaxLength(20);
    }
}