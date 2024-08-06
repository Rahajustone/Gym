using ErrorOr;
using Gym.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GymEntity = Gym.Domain.Gyms.Gym;

namespace Gym.Application.Gyms.Commands.CreateGym;

public class CreateGymCommandHandler : IRequestHandler<CreateGymCommand, ErrorOr<GymEntity>>
{
    private readonly ISubscriptionRepository _subscriptionsRepository;
    private readonly IGymRepository _gymsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateGymCommandHandler(
        ISubscriptionRepository subscriptionsRepository,
        IGymRepository gymsRepository,
        IUnitOfWork unitOfWork)
    {
        _subscriptionsRepository = subscriptionsRepository;
        _gymsRepository = gymsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<GymEntity>> Handle(CreateGymCommand command, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionsRepository.GetByIdAsync(command.SubscriptionId);

        if (subscription is null)
            return Error.NotFound(description: "Subscription not found");

        var gym = new GymEntity(
            name: command.Name,
            maxRooms: subscription.GetMaxRooms(),
            subscriptionId: subscription.Id);

        var addGymResult = subscription.AddGym(gym);

        if (addGymResult.IsError)
            return addGymResult.Errors;

        await _subscriptionsRepository.UpdateAsync(subscription);
        await _gymsRepository.AddGymAsync(gym);
        await _unitOfWork.CommitChangesAsync();

        return gym;
    }
}
