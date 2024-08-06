﻿using Gym.Application.Gyms.Commands.AddTrainer;
using Gym.Application.Gyms.Commands.CreateGym;
using Gym.Application.Gyms.Commands.DeleteGym;
using Gym.Application.Gyms.Queries.GetGym;
using Gym.Application.Gyms.Queries.ListGyms;
using Gym.Contracts.Gyms;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gym.API.Controllers;

[ApiController]
[Route("subscriptions/{subscriptionId:guid}/gyms")]
public class GymsController : ControllerBase
{
    private readonly ISender _mediator;

    public GymsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateGym(
        CreateGymRequest request,
        Guid subscriptionId)
    {
        var command = new CreateGymCommand(request.Name, subscriptionId);

        var createGymResult = await _mediator.Send(command);

        return createGymResult.Match(
            gym => CreatedAtAction(
                nameof(GetGym),
                new { subscriptionId, GymId = gym.Id },
                new GymResponse(gym.Id, gym.Name)),
            _ => Problem());
    }

    [HttpDelete("{gymId:guid}")]
    public async Task<IActionResult> DeleteGym(Guid subscriptionId, Guid gymId)
    {
        var command = new DeleteGymCommand(subscriptionId, gymId);

        var deleteGymResult = await _mediator.Send(command);

        return deleteGymResult.Match<IActionResult>(
            _ => NoContent(),
            _ => Problem());
    }

    [HttpGet]
    public async Task<IActionResult> ListGyms(Guid subscriptionId)
    {
        var command = new ListGymsQuery(subscriptionId);

        var listGymsResult = await _mediator.Send(command);

        return listGymsResult.Match(
            gyms => Ok(gyms.ConvertAll(gym => new GymResponse(gym.Id, gym.Name))),
            _ => Problem());
    }

    [HttpGet("{gymId:guid}")]
    public async Task<IActionResult> GetGym(Guid subscriptionId, Guid gymId)
    {
        var command = new GetGymQuery(subscriptionId, gymId);

        var getGymResult = await _mediator.Send(command);

        return getGymResult.Match(
            gym => Ok(new GymResponse(gym.Id, gym.Name)),
            _ => Problem());
    }

    [HttpPost("{gymId:guid}/trainers")]
    public async Task<IActionResult> AddTrainer(AddTrainerRequest request, Guid subscriptionId, Guid gymId)
    {
        var command = new AddTrainerCommand(subscriptionId, gymId, request.TrainerId);

        var addTrainerResult = await _mediator.Send(command);

        return addTrainerResult.MatchFirst<IActionResult>(
            success => Ok(),
            error => Problem());
    }
}
