using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.CommentAggregate;

namespace Shop.Infrastructure.Persistence.EF.Comments;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments", "comment");

        builder.Property(comment => comment.Id)
            .UseHiLo("CommentHiLoSequence");

        builder.Property(comment => comment.Id)
            .UseIdentityColumn(1);

        builder.Property(comment => comment.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(comment => comment.Description)
            .IsRequired()
            .HasMaxLength(1500);

        builder.OwnsMany(comment => comment.CommentHints, options =>
        {
            options.ToTable("Hints", "comment");

            options.Property(hint => hint.Status)
                .HasMaxLength(10);

            options.Property(hint => hint.Hint)
                .HasMaxLength(200);
        });

        builder.Property(comment => comment.Status)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(comment => comment.Recommendation)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(comment => comment.Likes)
            .IsRequired();

        builder.Property(comment => comment.Dislikes)
            .IsRequired();

        builder.OwnsMany(comment => comment.CommentReactions, options =>
        {
            options.ToTable("Reactions", "comment");
        });
    }
}