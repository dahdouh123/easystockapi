﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRestaurant.Application.Common.Exceptions;
using SmartRestaurant.Application.Common.Interfaces;
using SmartRestaurant.Domain.Entities;

namespace SmartRestaurant.Application.Zones.Commands
{
    public class ZoneCommandsHandler :
        IRequestHandler<CreateZoneCommand, ValidationResult>,
        IRequestHandler<UpdateZoneCommand, ValidationResult>,
        IRequestHandler<DeleteZoneCommand, ValidationResult>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ZoneCommandsHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ValidationResult> Handle(CreateZoneCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateZoneCommandValidator();
            var result = await validator.ValidateAsync(request, cancellationToken).ConfigureAwait(false);
            if (!result.IsValid) return result;
            //check if foodbusiness id exist in  db
            var foodBusiness = await _context.FoodBusinesses
                .Where(x => x.FoodBusinessId == request.FoodBusinessId)
                .Select(x => x.FoodBusinessId)
                .FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);
            if (foodBusiness == Guid.Empty)
                throw new NotFoundException(nameof(Domain.Entities.FoodBusiness), request.FoodBusinessId);
            var zone = await _context.Zones
                .FirstOrDefaultAsync(
                    x => x.ZoneTitle == request.ZoneTitle && x.FoodBusinessId == request.FoodBusinessId,
                    cancellationToken)
                .ConfigureAwait(false);

            if (zone != null)
                throw new InvalidOperationException("Duplicate names are not allowed");

            var entity = _mapper.Map<Zone>(request);
            _context.Zones.Add(entity);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return default;
        }

        public async Task<ValidationResult> Handle(DeleteZoneCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteZoneCommandValidator();
            var result = await validator.ValidateAsync(request, cancellationToken).ConfigureAwait(false);
            if (!result.IsValid) return result;
            var entity = await _context.Zones.FindAsync(request.CmdId).ConfigureAwait(false);
            if (entity == null)
                throw new NotFoundException(nameof(Zone), request.CmdId);
            _context.Zones.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return default;
        }

        public async Task<ValidationResult> Handle(UpdateZoneCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateZoneCommandValidator();
            var result = await validator.ValidateAsync(request, cancellationToken).ConfigureAwait(false);
            if (!result.IsValid) return result;
            var zone = await _context.Zones.FindAsync(request.CmdId).ConfigureAwait(false);
            if (zone == null)
                throw new NotFoundException(nameof(Zone), request.CmdId);
            var entity = _mapper.Map<Zone>(request);
            _context.Zones.Update(entity);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return default;
        }
    }
}