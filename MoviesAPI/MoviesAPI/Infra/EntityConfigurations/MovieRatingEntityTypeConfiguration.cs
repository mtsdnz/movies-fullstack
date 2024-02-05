using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviesAPI.Domain;

namespace MoviesAPI.Infra.EntityConfigurations;

public class MovieRatingEntityTypeConfiguration : IEntityTypeConfiguration<MovieRating>
{
    public void Configure(EntityTypeBuilder<MovieRating> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property<Guid>("MovieId")
            .IsRequired();

        builder.Property(r => r.Rating)
            .IsRequired();
        

        builder.ToTable("MovieRating");
    }
}