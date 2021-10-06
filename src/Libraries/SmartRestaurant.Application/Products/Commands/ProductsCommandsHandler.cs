﻿using AutoMapper;
using MediatR;
using SmartRestaurant.Application.Common.Exceptions;
using SmartRestaurant.Application.Common.Interfaces;
using SmartRestaurant.Application.Common.Tools;
using SmartRestaurant.Application.Common.WebResults;
using SmartRestaurant.Domain.Entities;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SmartRestaurant.Application.Products.Commands
{
    public class ProductsCommandsHandler :
         IRequestHandler<CreateProductCommand, Created>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public ProductsCommandsHandler(IApplicationDbContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }
        public async Task<Created> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateProductCommandValidator();
            var result = await validator.ValidateAsync(request, cancellationToken).ConfigureAwait(false);
            if (!result.IsValid) throw new ValidationException(result);

            var userId = ChecksHelper.GetUserIdFromToken_ThrowExceptionIfUserIdIsNullOrEmpty(_userService);

            if(request.SectionId != null) {
                var section = _context.Sections.Where(x => x.SectionId == Guid.Parse(request.SectionId)).FirstOrDefault();
                if (section == null)
                    throw new NotFoundException(nameof(Section), request.SectionId);
            }
            if (request.SubSectionId != null) {
                var subSection = _context.SubSections.Where(x => x.SubSectionId == Guid.Parse(request.SubSectionId)).FirstOrDefault();
                if (subSection == null)
                    throw new NotFoundException(nameof(SubSection), request.SubSectionId);
            }
            
            var product = _mapper.Map<Product>(request);          
            using (var ms = new MemoryStream())
            {
                request.Picture.CopyTo(ms);
                product.Picture = ms.ToArray();
                product.CreatedBy = userId;
                product.CreatedAt = DateTime.Now;
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return default;
        }
    }
}
