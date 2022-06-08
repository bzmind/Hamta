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
            .UseHiLo("CommentHiLoSequence")
            .UseIdentityColumn(1);

        builder.Property(comment => comment.CreationDate)
            .HasColumnType("datetime2(0)");

        builder.Property(comment => comment.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(comment => comment.Description)
            .IsRequired()
            .HasMaxLength(1500);

        builder.OwnsMany(comment => comment.CommentHints, options =>
        {
            options.ToTable("Hints", "comment");

            options.HasKey(hint => hint.Id);

            options.Property(hint => hint.Id)
                .UseIdentityColumn(1);

            options.Property(hint => hint.CreationDate)
                .HasColumnType("datetime2(0)");

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

            options.HasKey(reaction => reaction.Id);

            options.Property(reaction => reaction.Id)
                .UseIdentityColumn(1);

            options.Property(reaction => reaction.CreationDate)
                .HasColumnType("datetime2(0)");

            options.Property(reaction => reaction.Reaction)
                .HasMaxLength(10);
        });
    }
}