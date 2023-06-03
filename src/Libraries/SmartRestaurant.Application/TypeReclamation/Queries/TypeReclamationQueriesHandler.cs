﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRestaurant.Application.Common.Dtos;
using SmartRestaurant.Application.Common.Exceptions;
using SmartRestaurant.Application.Common.Extensions;
using SmartRestaurant.Application.Common.Interfaces;
using SmartRestaurant.Application.Sections.Queries;
using SmartRestaurant.Domain.Entities;

namespace SmartRestaurant.Application.TypeReclamation.Queries
{
    public class TypeReclamationQueriesHandler :
        IRequestHandler<GetTypeReclamationListQuery, PagedListDto<TypeReclamationDto>>,
                IRequestHandler<GetTypeReclamationByIdQuery, TypeReclamationDto>


    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TypeReclamationQueriesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedListDto<TypeReclamationDto>> Handle(GetTypeReclamationListQuery request,
            CancellationToken cancellationToken)
        {
            var query = _context.TypeReclamations.Where(t=>t.HotelId==Guid.Parse(request.HotelId)).OrderBy(s => s.Name)
                .GetPaged(request.Page, request.PageSize);
            var data = _mapper.Map<List<TypeReclamationDto>>(await query.Data.ToListAsync(cancellationToken)
                .ConfigureAwait(false));
            var pagedResult = new PagedListDto<TypeReclamationDto>(query.CurrentPage, query.PageCount, query.PageSize,
                query.RowCount, data);
            return pagedResult;
        }

        public async Task<TypeReclamationDto> Handle(GetTypeReclamationByIdQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetTypeReclamationByIdQueryValidator();
            var result = await validator.ValidateAsync(request, cancellationToken).ConfigureAwait(false);
            if (!result.IsValid) throw new ValidationException(result);

            var typereclamation = await _context.TypeReclamations.FindAsync(Guid.Parse(request.TypeReclamationId)).ConfigureAwait(false);
            if (typereclamation == null)
                throw new NotFoundException(nameof(TypeReclamation), request.TypeReclamationId);

            return _mapper.Map<TypeReclamationDto>(typereclamation);
        }


    }
}