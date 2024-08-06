using ErrorOr;
using MediatR;

namespace Gym.Application.Rooms.Commands.CreateRoom;

public record CreateRoomCommand(
    Guid GymId, string RoomName) : IRequest<ErrorOr<Gym.Domain.Rooms.Room>>;
