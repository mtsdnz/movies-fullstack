using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviesAPI.Domain;

namespace MoviesAPI.Infra.EntityConfigurations;

public class MovieEntityTypeConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Name)
            .IsRequired();

        builder.Property(m => m.Description)
            .IsRequired();

        builder.ToTable("Movie");
    }
}

public class MovieActorEntityTypeConfiguration : IEntityTypeConfiguration<MovieActor>
{
    public void Configure(EntityTypeBuilder<MovieActor> builder) 
    {
        builder.HasKey("MovieId", "ActorId");

        builder.HasOne(ma => ma.Movie).WithMany(m => m.Actors)
            .HasForeignKey(m => m.MovieId);

        builder.HasOne(ma => ma.Actor).WithMany(m => m.Movies)
            .HasForeignKey(a => a.ActorId);

        builder.ToTable("MovieActor");
    }
}