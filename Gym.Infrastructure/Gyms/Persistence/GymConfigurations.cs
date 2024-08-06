using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Gym.Infrastructure.Common.Persistence;
using GymEntity = Gym.Domain.Gyms;
namespace Gym.Infrastructure.Gyms.Persistence;

public class GymConfigurations : IEntityTypeConfiguration<GymEntity.Gym>
{
    public void Configure(EntityTypeBuilder<GymEntity.Gym> builder)
    {
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Id)
            .ValueGeneratedNever();

        builder.Property("_maxRooms")
            .HasColumnName("MaxRooms");

        builder.Property<List<Guid>>("_roomIds")
            .HasColumnName("RoomIds")
            .HasListOfIdsConverter();

        builder.Property<List<Guid>>("_trainerIds")
            .HasColumnName("TrainerIds")
            .HasListOfIdsConverter();

        builder.Property(g => g.Name);

        builder.Property(g => g.SubscriptionId);
    }
}
