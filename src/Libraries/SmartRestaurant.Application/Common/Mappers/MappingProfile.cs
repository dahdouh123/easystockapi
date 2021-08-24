﻿using AutoMapper;
using SmartRestaurant.Application.Common.Dtos;
using SmartRestaurant.Application.Common.Dtos.ValueObjects;
using SmartRestaurant.Application.LinkedDevice.Commands;
using SmartRestaurant.Application.FoodBusiness.Commands;
using SmartRestaurant.Application.Images.Commands;
using SmartRestaurant.Application.Menus.Commands;
using SmartRestaurant.Application.Reservations.Commands;
using SmartRestaurant.Application.Sections.Commands;
using SmartRestaurant.Application.SubSections.Commands;
using SmartRestaurant.Application.Tables.Commands;
using SmartRestaurant.Application.Zones.Commands;
using SmartRestaurant.Domain.Entities;
using SmartRestaurant.Domain.Identity.Entities;
using SmartRestaurant.Domain.ValueObjects;
using SmartRestaurant.Application.FoodBusinessEmployee.Commands;
using System.Collections.Generic;
using SmartRestaurant.Application.FoodBusiness.Queries;

namespace SmartRestaurant.Application.Common.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Entities.FoodBusiness, FoodBusinessDto>().ReverseMap();
            CreateMap<Domain.Entities.FoodBusiness, CreateFoodBusinessCommand>()
                .ForMember(x => x.Id, o => o.MapFrom(p => p.FoodBusinessId))
                .ReverseMap();
            CreateMap<Domain.Entities.FoodBusiness, UpdateFoodBusinessCommand>()
                .ForMember(x => x.Id, o => o.MapFrom(p => p.FoodBusinessId))
                .ReverseMap();
            CreateMap<GeoPosition, GeoPositionDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<PhoneNumber, PhoneNumberDto>().ReverseMap();
            CreateMap<CreateImageCommand, FoodBusinessImage>().ReverseMap();
            CreateMap<Zone, CreateZoneCommand>()
                .ForMember(x => x.Id, o => o.MapFrom(p => p.ZoneId))
                .ReverseMap();
            CreateMap<Zone, UpdateZoneCommand>()
                .ForMember(x => x.Id, o => o.MapFrom(p => p.ZoneId))
                .ReverseMap();
            CreateMap<ZoneDto, Zone>().ReverseMap();
            CreateMap<TableDto, Table>().ReverseMap();
            CreateMap<Table, CreateTableCommand>()
                .ForMember(x => x.Id, o => o.MapFrom(p => p.TableId))
                .ForMember(x => x.TableState, o => o.MapFrom(p => (short) p.TableState))
                .ReverseMap();
            CreateMap<Table, UpdateTableCommand>()
                .ForMember(x => x.Id, o => o.MapFrom(p => p.TableId))
                .ForMember(x => x.TableState, o => o.MapFrom(p => (short) p.TableState))
                .ReverseMap();
            CreateMap<Menu, CreateMenuCommand>()
                .ForMember(x => x.Id, o => o.MapFrom(p => p.MenuId))
                .ForMember(x => x.MenuState, o => o.MapFrom(p => (int) p.MenuState))
                .ReverseMap();
            CreateMap<Menu, UpdateMenuCommand>()
                .ForMember(x => x.Id, o => o.MapFrom(p => p.MenuId))
                .ForMember(x => x.MenuState, o => o.MapFrom(p => (int) p.MenuState))
                .ReverseMap();
            CreateMap<Menu, MenuDto>().ReverseMap();
            CreateMap<Section, SectionDto>().ReverseMap();
            CreateMap<SubSection, CreateSubSectionCommand>()
                .ForMember(x => x.Id, o => o.MapFrom(p => p.SubSectionId))
                .ReverseMap();
            CreateMap<Section, CreateSectionCommand>()
                .ForMember(x => x.Id, o => o.MapFrom(p => p.SectionId))
                .ReverseMap();
            CreateMap<Section, UpdateSectionCommand>()
                .ForMember(x => x.Id, o => o.MapFrom(p => p.SectionId))
                .ReverseMap();
            CreateMap<CreateReservationCommand, Reservation>()
                .ForMember(x => x.ReservationId, o => o.MapFrom(p => p.Id))
                .ReverseMap();

            CreateMap<UpdateReservationCommand, Reservation>(MemberList.Destination)
                .ForMember(x => x.ReservationId, o => o.MapFrom(p => p.Id))
                .ReverseMap();
            CreateMap<UpdateSubSectionCommand, SubSection>()
                .ForMember(x => x.SubSectionId, o => o.MapFrom(p => p.Id))
                .ReverseMap();
            CreateMap<Reservation, ReservationDto>()
                .ReverseMap();
            CreateMap<Reservation, ReservationClientDto>()
                .ForMember(x => x.FoodBusinessName, o => o.MapFrom(p => p.FoodBusiness.Name));
            CreateMap<CreateLinkedDeviceCommand, Domain.Entities.LinkedDevice> ()
               .ForMember(x => x.LinkedDeviceId, o => o.MapFrom(p => p.Id));
            CreateMap<Domain.Entities.LinkedDevice,LinkedDeviceDto>()
                .ForMember(x => (string)x.IdentifierDevice, o => o.MapFrom(p => (string)p.IdentifierDevice)).ReverseMap();

            CreateMap<UpdateLinkedDeviceCommand, Domain.Entities.LinkedDevice>()
                .ReverseMap();
             
            CreateMap<ApplicationUser, FoodBusinessEmployeesDtos>()
               .ReverseMap();

            CreateMap<InviteUserToJoinOrganizationCommand, ApplicationUser>()
                 .ForMember(x => x.UserName, o => o.MapFrom(p => p.Email));

            CreateMap<UserAcceptsInvitationToJoinOrganizationCommand, ApplicationUser>()
               .ForMember(x => x.Email, o => o.Ignore());

            CreateMap<ApplicationUser, FoodBusinessManagersDto>()
            .ForMember(x => x.FoodBusinesses, o => o.MapFrom(p => new List<Domain.Entities.FoodBusiness>()))
            .ReverseMap();
            CreateMap<UpdateFourDigitCodeCommand, Domain.Entities.FoodBusiness>().
                ForMember(x => x.FoodBusinessId, o => o.MapFrom(p => p.Id)).ReverseMap();

            CreateMap<Domain.Entities.FoodBusiness, FoodBusinessDto>()
                .ReverseMap();


        }
    }
}