using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.QuestionAggregate;

namespace Shop.Infrastructure.Persistence.EF.Questions;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable("Questions", "question");

        builder.Property(question => question.Id)
            .UseIdentityColumn(1);

        builder.Property(question => question.CreationDate)
            .HasColumnType("datetime2(0)");

        builder.Property(question => question.Description)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(question => question.Status)
            .IsRequired()
            .HasMaxLength(20);

        builder.OwnsMany(question => question.Replies, option =>
        {
            option.ToTable("Replies", "question");
            option.HasKey(reply => reply.Id);
            option.Property(reply => reply.Id).UseIdentityColumn();
        });
    }
}