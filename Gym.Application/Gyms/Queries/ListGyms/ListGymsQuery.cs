using ErrorOr;
using Gym.Domain.Gyms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Application.Gyms.Queries.ListGyms;

public record ListGymsQuery(Guid SubscriptionId)
    : IRequest<ErrorOr<List<Gym.Domain.Gyms.Gym>>>;
