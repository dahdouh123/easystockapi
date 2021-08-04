﻿using System;
using Microsoft.EntityFrameworkCore;
using SmartRestaurant.Domain.Entities;
using SmartRestaurant.Domain.Enums;

namespace SmartRestaurant.Infrastructure.Persistence
{
    public static class SeedData
    {
        public const string TajMhal_FoodBusinessId = "3cbf3570-4444-4673-8746-29b7cf568093";
        public const string Mcdonald_FoodBusinessId = "66bf3570-440d-4673-8746-29b7cf568099";
        public const string BigMama_FoodBusinessId = "88bc7853-220f-9173-3246-afb7cf595022";

        public const string TajMhal_FoodBusinessAdministrator_UserId = "3cbf3570-4444-4444-8746-29b7cf568093";
        public const string Mcdonald_FoodBusinessAdministrator_UserId = "44bf3570-0d44-4673-8746-29b7cf568088";
        public const string BigMama_FoodBusinessAdministrator_UserId = "08a1a626-7f8e-4b51-84fc-fc51b6302cca";

        public const string TajMhal_FoodBusinessManager_UserId = "a1997466-cedc-4850-b18d-0ac4f4102cff";
        public const string Mcdonald_FoodBusinessManager_UserId = "b2207466-ceda-4b50-b18d-0ac4f4102caa";
        public const string BigMama_SalimFoodBusinessManager_UserId = "64fed988-6f68-49dc-ad54-0da50ec02319";

        public const string Diner_UserId_01 = "5a84cd00-59f0-4b22-bfce-07c080829118";
        public const string Diner_UserId_02 = "6b14cd00-59f0-4422-bfce-07c080829987";

        public const string TajMhal_VipZone_Id = "32bccd11-59fd-3304-bfaa-07c08082abc0";
        public const string TajMhal_FamilyZone_Id = "32bccd11-59fd-3304-bfaa-07c08082abc1";
        public const string TajMhal_NormalZone_Id = "32bccd11-59fd-3304-bfaa-07c08082abc2";
        public const string TajMhal_OutdoorZone_Id = "32bccd11-59fd-3304-bfaa-07c08082abc3";
        public const string Mcdonald_NormalZone_Id = "32bccd11-59fd-33ff-bfaa-07c08082aba1";
        public const string Mcdonald_OutdoorZone_Id = "32bccd11-59fd-33ff-bfaa-07c08082aba2";
        public const string BigMama_SharedZone_Id = "f60d55e2-4e54-4896-9632-98d36d7680c3";

        public const string TajMhal_VipZone_TableId = "44aecd78-59bb-7504-bfff-07c07129ab00";
        public const string TajMhal_FamilyZone_TableId = "44aecd78-59bb-7504-bfff-07c07129ab01";
        public const string TajMhal_NormalZone_TableId = "44aecd78-59bb-7504-bfff-07c07129ab02";
        public const string TajMhal_OutdoorZone_TableId = "44aecd78-59bb-7504-bfff-07c07129ab03";
        public const string Mcdonald_NormalZone_TableId = "44aecd78-59bb-7504-bfff-07c07129aba2";
        public const string Mcdonald_OutdoorZone_TableId = "44aecd78-59bb-7504-bfff-07c07129aba3";
        public const string BigMama_SharedZone_TableId = "b006e2c5-5b8e-4584-8021-3cecd76c9ca6";

        public const string TajMhal_DishesMenu_Id = "ccaecd78-ccbb-ee04-56ff-88887129aaba";
        public const string TajMhal_PizzaMenu_Id = "ccaecd78-ccbb-ee04-56ff-88887129aabb";
        public const string TajMhal_SandwichesMenu_Id = "ccaecd78-ccbb-ee04-56ff-88887129aabc";
        public const string TajMhal_BeverageMenu_Id = "ccaecd78-ccbb-ee04-56ff-88887129aabd";
        public const string TajMhal_DessertMenu_Id = "ccaecd78-ccbb-ee04-56ff-88887129aabe";
        public const string Mcdonald_SandwichesMenu_Id = "ccaecd78-ccbb-ee04-56ff-88887129aa00";
        public const string Mcdonald_BeverageMenu_Id = "ccaecd78-ccbb-ee04-56ff-88887129aa01";
        public const string Mcdonald_DessertMenu_Id = "ccaecd78-ccbb-ee04-56ff-88887129aa02";
        public const string BigMama_SandwichesMenu_Id = "e2289d77-b8e1-4476-bf66-e64f1a23d752";
        public const string BigMama_BeverageMenu_Id = "8f8c0139-1f90-40f3-ab88-5db2de45ff2e";
        public const string BigMama_DessertMenu_Id = "45051fc7-6983-44a5-9c12-66116c4533bf";

        public static void Seed(this ModelBuilder modelBuilder)
        {
            #region Create a FoodBusinesses

            modelBuilder.Entity<FoodBusiness>().HasData(

                #region Create a FoodBusiness for TajMhal restaurant

                new FoodBusiness
                {
                    FoodBusinessId = Guid.Parse(TajMhal_FoodBusinessId),
                    Name = "Taj mahal",
                    AcceptsCreditCards = true,
                    AcceptTakeout = true,
                    AverageRating = 4,
                    Description =
                        "Envie de découvrir la cuisine indienne, le restaurant Taj Mahal vous invite à le faire et voyager à travers les odeurs des épices orientales qui se dégagent de ses mets à spécialités indiennes.",
                    HasCarParking = true,
                    IsHandicapFriendly = false,
                    OffersTakeout = true,
                    Tags = "",
                    Website = "https://restoalgerie.com/restaurant/taj-mahal-restaurant-indien/",
                    FoodBusinessAdministratorId = TajMhal_FoodBusinessAdministrator_UserId,
                    FoodBusinessCategory = FoodBusinessCategory.Restaurant
                },

                #endregion

                #region Create a FoodBusiness for Mcdonald's restaurant

                new FoodBusiness
                {
                    FoodBusinessId = Guid.Parse(Mcdonald_FoodBusinessId),
                    Name = "Mcdonald's",
                    AcceptsCreditCards = true,
                    AcceptTakeout = true,
                    AverageRating = 4,
                    Description = "",
                    HasCarParking = true,
                    IsHandicapFriendly = true,
                    OffersTakeout = true,
                    Tags = "",
                    Website = "",
                    FoodBusinessAdministratorId = Mcdonald_FoodBusinessAdministrator_UserId,
                    FoodBusinessCategory = FoodBusinessCategory.Restaurant
                },

                #endregion

                #region Create a FoodBusiness for BigMama restaurant

                new FoodBusiness
                {
                    FoodBusinessId = Guid.Parse(BigMama_FoodBusinessId),
                    Name = "BigMama",
                    AcceptsCreditCards = true,
                    AcceptTakeout = true,
                    AverageRating = 5,
                    Description =
                        "ETuoYe SMdsYsup qqbdspY NEeZvsaI sUcIOE sVmPkJx RZFk FOKzUkG ffAsUB XyINU fhhIB OiIfN Antdhb XHbtaO UlStFP adgVv CRTToT Mcv FAHcd YyGH. CdDIPW TtDBaI qYg wVcSK NAXHnVC xpNBE fRufEW fggeTKc Iqq dfGZPAqoc MYxnH NCLtDA qqV TNYR LbwaYqv cvIiSvl KBTMl xAxHmu dilIqO mGM kxDhvLT PsYPdCB yZE uFfvGxQp uvoeDsAaE QQjgKs CnAnhrs qNPzSuq bvZjqMfy aaEGCqc XrvE KFXnmA mEnN uGHJt WypGwSiJDmP qBDWYau SzbxbSRUb CMwhBXiYA vQCTdtiB oVkRA XpHYTFE BYFpDTVlV zafiNugG YFyiIvYhhgyzj MihfVEqk OWlRLG YAUn sXWO jbKyczKOQfhXa qziTc xxMFCM WfVzT oPdKGSK Zz CzXeis.",
                    HasCarParking = false,
                    IsHandicapFriendly = false,
                    OffersTakeout = true,
                    Tags = "",
                    Website = "https://bigmama.com",
                    FoodBusinessAdministratorId = BigMama_FoodBusinessAdministrator_UserId,
                    FoodBusinessCategory = FoodBusinessCategory.Restaurant
                }

                #endregion

            );

            #endregion


            #region Assigning Employees userId with FoodBusinessId (restaurant).

            modelBuilder.Entity<FoodBusinessUser>().HasData(

                #region Assigning account with Id = [TajMhal_FoodBusinessAdministrator_UserId] to TajMhal restaurant.

                new FoodBusinessUser
                {
                    ApplicationUserId = TajMhal_FoodBusinessAdministrator_UserId,
                    FoodBusinessId = Guid.Parse(TajMhal_FoodBusinessId)
                },

                #endregion

                #region Assigning account with Id = [TajMhal_FoodBusinessManager_UserId] to TajMhal restaurant.

                new FoodBusinessUser
                {
                    ApplicationUserId = TajMhal_FoodBusinessManager_UserId,
                    FoodBusinessId = Guid.Parse(TajMhal_FoodBusinessId)
                },

                #endregion

                #region Assigning account with Id = [Mcdonald_FoodBusinessAdministrator_UserId] to Mcdonald restaurant.

                new FoodBusinessUser
                {
                    ApplicationUserId = Mcdonald_FoodBusinessAdministrator_UserId,
                    FoodBusinessId = Guid.Parse(Mcdonald_FoodBusinessId)
                },

                #endregion

                #region Assigning account with Id = [Mcdonald_FoodBusinessManager_UserId] to Mcdonald restaurant.

                new FoodBusinessUser
                {
                    ApplicationUserId = Mcdonald_FoodBusinessManager_UserId,
                    FoodBusinessId = Guid.Parse(Mcdonald_FoodBusinessId)
                },

                #endregion

                #region Assigning account with Id = [BigMama_FoodBusinessAdministrator_UserId] to Mcdonald restaurant.

                new FoodBusinessUser
                {
                    ApplicationUserId = BigMama_FoodBusinessAdministrator_UserId,
                    FoodBusinessId = Guid.Parse(BigMama_FoodBusinessId)
                },

                #endregion

                #region Assigning account with Id = [BigMama_SalimFoodBusinessManager_UserId] to Mcdonald restaurant.

                new FoodBusinessUser
                {
                    ApplicationUserId = BigMama_SalimFoodBusinessManager_UserId,
                    FoodBusinessId = Guid.Parse(BigMama_FoodBusinessId)
                }

                #endregion

            );

            #endregion


            #region Create reservations

            modelBuilder.Entity<Reservation>().HasData(

                #region Create reservations for user Diner_01

                new Reservation
                {
                    FoodBusinessId = Guid.Parse(TajMhal_FoodBusinessId),
                    ReservationId = Guid.Parse("acbf657b-3398-7a73-8746-77b7cf596300"),
                    ReservationName = "ReservationName_00",
                    NumberOfDiners = 2,
                    ReservationDate = DateTime.Now.AddHours(5),
                    CreatedBy = Diner_UserId_01,
                    CreatedAt = DateTime.Now
                },
                new Reservation
                {
                    FoodBusinessId = Guid.Parse(Mcdonald_FoodBusinessId),
                    ReservationId = Guid.Parse("acbf657b-3398-7a73-8746-77b7cf596301"),
                    ReservationName = "ReservationName_01",
                    NumberOfDiners = 5,
                    ReservationDate = DateTime.Now.AddHours(8),
                    CreatedBy = Diner_UserId_01,
                    CreatedAt = DateTime.Now.AddHours(5)
                },
                new Reservation
                {
                    FoodBusinessId = Guid.Parse(TajMhal_FoodBusinessId),
                    ReservationId = Guid.Parse("acbf657b-3398-7a73-8746-77b7cf596302"),
                    ReservationName = "ReservationName_02",
                    NumberOfDiners = 3,
                    ReservationDate = DateTime.Now.AddDays(1),
                    CreatedBy = Diner_UserId_01,
                    CreatedAt = DateTime.Now.AddDays(-15)
                },
                new Reservation
                {
                    FoodBusinessId = Guid.Parse(Mcdonald_FoodBusinessId),
                    ReservationId = Guid.Parse("acbf657b-3398-7a73-8746-77b7cf596303"),
                    ReservationName = "ReservationName_03",
                    NumberOfDiners = 2,
                    ReservationDate = DateTime.Now.AddYears(5),
                    CreatedBy = Diner_UserId_01,
                    CreatedAt = DateTime.Now.AddDays(15)
                },
                new Reservation
                {
                    FoodBusinessId = Guid.Parse(TajMhal_FoodBusinessId),
                    ReservationId = Guid.Parse("acbf657b-3398-7a73-8746-77b7cf596304"),
                    ReservationName = "ReservationName_04",
                    NumberOfDiners = 8,
                    ReservationDate = DateTime.Now.AddYears(15),
                    CreatedBy = Diner_UserId_01,
                    CreatedAt = DateTime.Now.AddMinutes(36)
                },
                new Reservation
                {
                    FoodBusinessId = Guid.Parse(Mcdonald_FoodBusinessId),
                    ReservationId = Guid.Parse("acbf657b-3398-7a73-8746-77b7cf596305"),
                    ReservationName = "ReservationName_05",
                    NumberOfDiners = 13,
                    ReservationDate = DateTime.Now.AddHours(-3),
                    CreatedBy = Diner_UserId_01,
                    CreatedAt = DateTime.Now.AddDays(-1)
                },
                new Reservation
                {
                    FoodBusinessId = Guid.Parse(TajMhal_FoodBusinessId),
                    ReservationId = Guid.Parse("acbf657b-3398-7a73-8746-77b7cf596306"),
                    ReservationName = "ReservationName_06",
                    NumberOfDiners = 7,
                    ReservationDate = DateTime.Now.AddDays(-12),
                    CreatedBy = Diner_UserId_01,
                    CreatedAt = DateTime.Now.AddMonths(-1)
                },
                new Reservation
                {
                    FoodBusinessId = Guid.Parse(TajMhal_FoodBusinessId),
                    ReservationId = Guid.Parse("acbf657b-3398-7a73-8746-77b7cf596307"),
                    ReservationName = "ReservationName_07",
                    NumberOfDiners = 9,
                    ReservationDate = DateTime.Now.AddDays(-53),
                    CreatedBy = Diner_UserId_01,
                    CreatedAt = DateTime.Now.AddMonths(-2)
                }
                ,
                new Reservation
                {
                    FoodBusinessId = Guid.Parse(TajMhal_FoodBusinessId),
                    ReservationId = Guid.Parse("acbf657b-3398-7a73-8746-77b7cf596308"),
                    ReservationName = "ReservationName_08",
                    NumberOfDiners = 4,
                    ReservationDate = DateTime.Now.AddMonths(5),
                    CreatedBy = Diner_UserId_01,
                    CreatedAt = DateTime.Now.AddMonths(4)
                },

                #endregion

                #region Create reservations for user Diner_02

                new Reservation
                {
                    FoodBusinessId = Guid.Parse(TajMhal_FoodBusinessId),
                    ReservationId = Guid.Parse("acbf657b-3398-7a73-8746-77b7cf596309"),
                    ReservationName = "ReservationName_09",
                    NumberOfDiners = 3,
                    ReservationDate = DateTime.Now.AddHours(4),
                    CreatedBy = Diner_UserId_02,
                    CreatedAt = DateTime.Now
                },
                new Reservation
                {
                    FoodBusinessId = Guid.Parse(Mcdonald_FoodBusinessId),
                    ReservationId = Guid.Parse("acbf657b-3398-7a73-8746-77b7cf596310"),
                    ReservationName = "ReservationName_10",
                    NumberOfDiners = 6,
                    ReservationDate = DateTime.Now.AddHours(7),
                    CreatedBy = Diner_UserId_02,
                    CreatedAt = DateTime.Now.AddHours(5)
                },
                new Reservation
                {
                    FoodBusinessId = Guid.Parse(TajMhal_FoodBusinessId),
                    ReservationId = Guid.Parse("acbf657b-3398-7a73-8746-77b7cf596311"),
                    ReservationName = "ReservationName_11",
                    NumberOfDiners = 4,
                    ReservationDate = DateTime.Now.AddDays(2),
                    CreatedBy = Diner_UserId_02,
                    CreatedAt = DateTime.Now.AddDays(-14)
                },
                new Reservation
                {
                    FoodBusinessId = Guid.Parse(Mcdonald_FoodBusinessId),
                    ReservationId = Guid.Parse("acbf657b-3398-7a73-8746-77b7cf596312"),
                    ReservationName = "ReservationName_12",
                    NumberOfDiners = 3,
                    ReservationDate = DateTime.Now.AddYears(4),
                    CreatedBy = Diner_UserId_02,
                    CreatedAt = DateTime.Now.AddDays(51)
                },
                new Reservation
                {
                    FoodBusinessId = Guid.Parse(TajMhal_FoodBusinessId),
                    ReservationId = Guid.Parse("acbf657b-3398-7a73-8746-77b7cf596313"),
                    ReservationName = "ReservationName_13",
                    NumberOfDiners = 9,
                    ReservationDate = DateTime.Now.AddYears(15),
                    CreatedBy = Diner_UserId_02,
                    CreatedAt = DateTime.Now.AddMinutes(36)
                },
                new Reservation
                {
                    FoodBusinessId = Guid.Parse(Mcdonald_FoodBusinessId),
                    ReservationId = Guid.Parse("acbf657b-3398-7a73-8746-77b7cf596314"),
                    ReservationName = "ReservationName_14",
                    NumberOfDiners = 14,
                    ReservationDate = DateTime.Now.AddHours(-4),
                    CreatedBy = Diner_UserId_02,
                    CreatedAt = DateTime.Now.AddDays(-2)
                },
                new Reservation
                {
                    FoodBusinessId = Guid.Parse(TajMhal_FoodBusinessId),
                    ReservationId = Guid.Parse("acbf657b-3398-7a73-8746-77b7cf596315"),
                    ReservationName = "ReservationName_15",
                    NumberOfDiners = 8,
                    ReservationDate = DateTime.Now.AddDays(-13),
                    CreatedBy = Diner_UserId_02,
                    CreatedAt = DateTime.Now.AddMonths(-1)
                },
                new Reservation
                {
                    FoodBusinessId = Guid.Parse(TajMhal_FoodBusinessId),
                    ReservationId = Guid.Parse("acbf657b-3398-7a73-8746-77b7cf596316"),
                    ReservationName = "ReservationName_16",
                    NumberOfDiners = 10,
                    ReservationDate = DateTime.Now.AddDays(-50),
                    CreatedBy = Diner_UserId_02,
                    CreatedAt = DateTime.Now.AddMonths(-3)
                }
                ,
                new Reservation
                {
                    FoodBusinessId = Guid.Parse(TajMhal_FoodBusinessId),
                    ReservationId = Guid.Parse("acbf657b-3398-7a73-8746-77b7cf596317"),
                    ReservationName = "ReservationName_17",
                    NumberOfDiners = 5,
                    ReservationDate = DateTime.Now.AddMonths(4),
                    CreatedBy = Diner_UserId_02,
                    CreatedAt = DateTime.Now.AddMonths(3)
                },

                #endregion

                #region Create reservations for user TajMhal_FoodBusinessManager

                new Reservation
                {
                    FoodBusinessId = Guid.Parse(TajMhal_FoodBusinessId),
                    ReservationId = Guid.Parse("acbf657b-3398-7a73-8746-77b7cf596327"),
                    ReservationName = "ReservationName_27",
                    NumberOfDiners = 3,
                    ReservationDate = DateTime.Now.AddHours(3),
                    CreatedBy = TajMhal_FoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now
                },
                new Reservation
                {
                    FoodBusinessId = Guid.Parse(TajMhal_FoodBusinessId),
                    ReservationId = Guid.Parse("acbf657b-3398-7a73-8746-77b7cf596328"),
                    ReservationName = "ReservationName_28",
                    NumberOfDiners = 6,
                    ReservationDate = DateTime.Now.AddHours(6),
                    CreatedBy = TajMhal_FoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now.AddHours(4)
                },
                new Reservation
                {
                    FoodBusinessId = Guid.Parse(TajMhal_FoodBusinessId),
                    ReservationId = Guid.Parse("acbf657b-3398-7a73-8746-77b7cf596329"),
                    ReservationName = "ReservationName_29",
                    NumberOfDiners = 4,
                    ReservationDate = DateTime.Now.AddDays(1),
                    CreatedBy = TajMhal_FoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now.AddDays(-14)
                },
                new Reservation
                {
                    FoodBusinessId = Guid.Parse(TajMhal_FoodBusinessId),
                    ReservationId = Guid.Parse("acbf657b-3398-7a73-8746-77b7cf596330"),
                    ReservationName = "ReservationName_30",
                    NumberOfDiners = 3,
                    ReservationDate = DateTime.Now.AddYears(3),
                    CreatedBy = TajMhal_FoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now.AddDays(55)
                },
                new Reservation
                {
                    FoodBusinessId = Guid.Parse(TajMhal_FoodBusinessId),
                    ReservationId = Guid.Parse("acbf657b-3398-7a73-8746-77b7cf596331"),
                    ReservationName = "ReservationName_31",
                    NumberOfDiners = 9,
                    ReservationDate = DateTime.Now.AddYears(13),
                    CreatedBy = TajMhal_FoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now.AddMinutes(15)
                },
                new Reservation
                {
                    FoodBusinessId = Guid.Parse(TajMhal_FoodBusinessId),
                    ReservationId = Guid.Parse("acbf657b-3398-7a73-8746-77b7cf596332"),
                    ReservationName = "ReservationName_32",
                    NumberOfDiners = 14,
                    ReservationDate = DateTime.Now.AddHours(-3),
                    CreatedBy = TajMhal_FoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now.AddDays(-3)
                },
                new Reservation
                {
                    FoodBusinessId = Guid.Parse(TajMhal_FoodBusinessId),
                    ReservationId = Guid.Parse("acbf657b-3398-7a73-8746-77b7cf596333"),
                    ReservationName = "ReservationName_33",
                    NumberOfDiners = 8,
                    ReservationDate = DateTime.Now.AddDays(-10),
                    CreatedBy = TajMhal_FoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now.AddMonths(-1)
                },
                new Reservation
                {
                    FoodBusinessId = Guid.Parse(TajMhal_FoodBusinessId),
                    ReservationId = Guid.Parse("acbf657b-3398-7a73-8746-77b7cf596334"),
                    ReservationName = "ReservationName_34",
                    NumberOfDiners = 10,
                    ReservationDate = DateTime.Now.AddDays(-43),
                    CreatedBy = TajMhal_FoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now.AddMonths(-2)
                }
                ,
                new Reservation
                {
                    FoodBusinessId = Guid.Parse(TajMhal_FoodBusinessId),
                    ReservationId = Guid.Parse("acbf657b-3398-7a73-8746-77b7cf596335"),
                    ReservationName = "ReservationName_35",
                    NumberOfDiners = 5,
                    ReservationDate = DateTime.Now.AddMonths(3),
                    CreatedBy = TajMhal_FoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now.AddMonths(2)
                },

                #endregion

                #region Create reservations for user Mcdonald_FoodBusinessManager

                new Reservation
                {
                    FoodBusinessId = Guid.Parse(Mcdonald_FoodBusinessId),
                    ReservationId = Guid.Parse("acbf657b-3398-7a73-8746-77b7cf596336"),
                    ReservationName = "ReservationName_36",
                    NumberOfDiners = 4,
                    ReservationDate = DateTime.Now.AddHours(1),
                    CreatedBy = Mcdonald_FoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now
                },
                new Reservation
                {
                    FoodBusinessId = Guid.Parse(Mcdonald_FoodBusinessId),
                    ReservationId = Guid.Parse("acbf657b-3398-7a73-8746-77b7cf596337"),
                    ReservationName = "ReservationName_37",
                    NumberOfDiners = 5,
                    ReservationDate = DateTime.Now.AddHours(4),
                    CreatedBy = Mcdonald_FoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now.AddHours(2)
                },
                new Reservation
                {
                    FoodBusinessId = Guid.Parse(Mcdonald_FoodBusinessId),
                    ReservationId = Guid.Parse("acbf657b-3398-7a73-8746-77b7cf596338"),
                    ReservationName = "ReservationName_38",
                    NumberOfDiners = 6,
                    ReservationDate = DateTime.Now.AddDays(5),
                    CreatedBy = Mcdonald_FoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now.AddDays(-14)
                },
                new Reservation
                {
                    FoodBusinessId = Guid.Parse(Mcdonald_FoodBusinessId),
                    ReservationId = Guid.Parse("acbf657b-3398-7a73-8746-77b7cf596339"),
                    ReservationName = "ReservationName_39",
                    NumberOfDiners = 7,
                    ReservationDate = DateTime.Now.AddYears(2),
                    CreatedBy = Mcdonald_FoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now.AddDays(20)
                },
                new Reservation
                {
                    FoodBusinessId = Guid.Parse(Mcdonald_FoodBusinessId),
                    ReservationId = Guid.Parse("acbf657b-3398-7a73-8746-77b7cf596340"),
                    ReservationName = "ReservationName_40",
                    NumberOfDiners = 10,
                    ReservationDate = DateTime.Now.AddYears(10),
                    CreatedBy = Mcdonald_FoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now.AddMinutes(43)
                },
                new Reservation
                {
                    FoodBusinessId = Guid.Parse(Mcdonald_FoodBusinessId),
                    ReservationId = Guid.Parse("acbf657b-3398-7a73-8746-77b7cf596341"),
                    ReservationName = "ReservationName_41",
                    NumberOfDiners = 17,
                    ReservationDate = DateTime.Now.AddHours(-12),
                    CreatedBy = Mcdonald_FoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now.AddDays(-5)
                },
                new Reservation
                {
                    FoodBusinessId = Guid.Parse(Mcdonald_FoodBusinessId),
                    ReservationId = Guid.Parse("acbf657b-3398-7a73-8746-77b7cf596342"),
                    ReservationName = "ReservationName_42",
                    NumberOfDiners = 4,
                    ReservationDate = DateTime.Now.AddDays(-7),
                    CreatedBy = Mcdonald_FoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now.AddMonths(-1)
                },
                new Reservation
                {
                    FoodBusinessId = Guid.Parse(Mcdonald_FoodBusinessId),
                    ReservationId = Guid.Parse("acbf657b-3398-7a73-8746-77b7cf596343"),
                    ReservationName = "ReservationName_43",
                    NumberOfDiners = 11,
                    ReservationDate = DateTime.Now.AddDays(43),
                    CreatedBy = Mcdonald_FoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now.AddMonths(1)
                }
                ,
                new Reservation
                {
                    FoodBusinessId = Guid.Parse(Mcdonald_FoodBusinessId),
                    ReservationId = Guid.Parse("acbf657b-3398-7a73-8746-77b7cf596344"),
                    ReservationName = "ReservationName_44",
                    NumberOfDiners = 2,
                    ReservationDate = DateTime.Now.AddMonths(3),
                    CreatedBy = Mcdonald_FoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now.AddMonths(2)
                }

                #endregion

            );

            #endregion

            #region Create zones

            modelBuilder.Entity<Zone>().HasData(

                #region Create zones for TajMhal_FoodBusiness

                new Zone
                {
                    FoodBusinessId = Guid.Parse(TajMhal_FoodBusinessId),
                    ZoneTitle = "TajMhal VIP Zone",
                    CreatedBy = TajMhal_FoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now,
                    ZoneId = Guid.Parse(TajMhal_VipZone_Id)
                },
                new Zone
                {
                    FoodBusinessId = Guid.Parse(TajMhal_FoodBusinessId),
                    ZoneTitle = "TajMhal FAMILY Zone",
                    CreatedBy = TajMhal_FoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now,
                    ZoneId = Guid.Parse(TajMhal_FamilyZone_Id)
                },
                new Zone
                {
                    FoodBusinessId = Guid.Parse(TajMhal_FoodBusinessId),
                    ZoneTitle = "TajMhal NORMAL Zone",
                    CreatedBy = TajMhal_FoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now,
                    ZoneId = Guid.Parse(TajMhal_NormalZone_Id)
                },
                new Zone
                {
                    FoodBusinessId = Guid.Parse(TajMhal_FoodBusinessId),
                    ZoneTitle = "TajMhal OUTDOOR Zone",
                    CreatedBy = TajMhal_FoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now,
                    ZoneId = Guid.Parse(TajMhal_OutdoorZone_Id)
                },

                #endregion

                #region Create zones for Mcdonald_FoodBusiness

                new Zone
                {
                    FoodBusinessId = Guid.Parse(Mcdonald_FoodBusinessId),
                    ZoneTitle = "Mcdonald NORMAL Zone",
                    CreatedBy = Mcdonald_FoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now,
                    ZoneId = Guid.Parse(Mcdonald_NormalZone_Id)
                },
                new Zone
                {
                    FoodBusinessId = Guid.Parse(Mcdonald_FoodBusinessId),
                    ZoneTitle = "Mcdonald OUTDOOR Zone",
                    CreatedBy = Mcdonald_FoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now,
                    ZoneId = Guid.Parse(Mcdonald_OutdoorZone_Id)
                },

                #endregion

                #region Create zones for BigMama_FoodBusiness

                new Zone
                {
                    FoodBusinessId = Guid.Parse(BigMama_FoodBusinessId),
                    ZoneTitle = "BigMama SHARED Zone",
                    CreatedBy = BigMama_SalimFoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now,
                    ZoneId = Guid.Parse(BigMama_SharedZone_Id)
                }

                #endregion

            );

            #endregion

            #region Create tables

            modelBuilder.Entity<Table>().HasData(

                #region Create tables for TajMhal_FoodBusiness

                new Table
                {
                    ZoneId = Guid.Parse(TajMhal_VipZone_Id),
                    TableNumber = 4,
                    Capacity = 4,
                    CreatedBy = TajMhal_FoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now,
                    TableId = Guid.Parse(TajMhal_VipZone_TableId)
                },
                new Table
                {
                    ZoneId = Guid.Parse(TajMhal_FamilyZone_Id),
                    TableNumber = 5,
                    Capacity = 6,
                    CreatedBy = TajMhal_FoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now,
                    TableId = Guid.Parse(TajMhal_FamilyZone_TableId)
                },
                new Table
                {
                    ZoneId = Guid.Parse(TajMhal_NormalZone_Id),
                    TableNumber = 10,
                    Capacity = 4,
                    CreatedBy = TajMhal_FoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now,
                    TableId = Guid.Parse(TajMhal_NormalZone_TableId)
                },
                new Table
                {
                    ZoneId = Guid.Parse(TajMhal_OutdoorZone_Id),
                    TableNumber = 7,
                    Capacity = 3,
                    CreatedBy = TajMhal_FoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now,
                    TableId = Guid.Parse(TajMhal_OutdoorZone_TableId)
                },

                #endregion

                #region Create tables for Mcdonald_FoodBusiness

                new Table
                {
                    ZoneId = Guid.Parse(Mcdonald_NormalZone_Id),
                    TableNumber = 7,
                    Capacity = 5,
                    CreatedBy = Mcdonald_FoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now,
                    TableId = Guid.Parse(Mcdonald_NormalZone_TableId)
                },
                new Table
                {
                    ZoneId = Guid.Parse(Mcdonald_OutdoorZone_Id),
                    TableNumber = 3,
                    Capacity = 3,
                    CreatedBy = Mcdonald_FoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now,
                    TableId = Guid.Parse(Mcdonald_OutdoorZone_TableId)
                },

                #endregion

                #region Create tables for BigMama_FoodBusiness

                new Table
                {
                    ZoneId = Guid.Parse(BigMama_SharedZone_Id),
                    TableNumber = 8,
                    Capacity = 6,
                    CreatedBy = BigMama_SalimFoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now,
                    TableId = Guid.Parse(BigMama_SharedZone_TableId)
                }

                #endregion

            );

            #endregion

            #region Create menus

            modelBuilder.Entity<Menu>().HasData(

                #region Create menus for TajMhal_FoodBusiness

                new Menu
                {
                    FoodBusinessId = Guid.Parse(TajMhal_FoodBusinessId),
                    Name = "TajMhal Dishes Menu",
                    CreatedBy = TajMhal_FoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now,
                    MenuId = Guid.Parse(TajMhal_DishesMenu_Id)
                },
                new Menu
                {
                    FoodBusinessId = Guid.Parse(TajMhal_FoodBusinessId),
                    Name = "TajMhal Pizza Menu",
                    CreatedBy = TajMhal_FoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now,
                    MenuId = Guid.Parse(TajMhal_PizzaMenu_Id)
                },
                new Menu
                {
                    FoodBusinessId = Guid.Parse(TajMhal_FoodBusinessId),
                    Name = "TajMhal Sandwiches Menu",
                    CreatedBy = TajMhal_FoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now,
                    MenuId = Guid.Parse(TajMhal_SandwichesMenu_Id)
                },
                new Menu
                {
                    FoodBusinessId = Guid.Parse(TajMhal_FoodBusinessId),
                    Name = "TajMhal Beverage  Menu",
                    CreatedBy = TajMhal_FoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now,
                    MenuId = Guid.Parse(TajMhal_BeverageMenu_Id)
                },
                new Menu
                {
                    FoodBusinessId = Guid.Parse(TajMhal_FoodBusinessId),
                    Name = "TajMhal Dessert Menu",
                    CreatedBy = TajMhal_FoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now,
                    MenuId = Guid.Parse(TajMhal_DessertMenu_Id)
                },

                #endregion

                #region Create menus for Mcdonald_FoodBusiness

                new Menu
                {
                    FoodBusinessId = Guid.Parse(Mcdonald_FoodBusinessId),
                    Name = "Mcdonald Sandwiches Menu",
                    CreatedBy = Mcdonald_FoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now,
                    MenuId = Guid.Parse(Mcdonald_SandwichesMenu_Id)
                },
                new Menu
                {
                    FoodBusinessId = Guid.Parse(Mcdonald_FoodBusinessId),
                    Name = "Mcdonald Beverage  Menu",
                    CreatedBy = Mcdonald_FoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now,
                    MenuId = Guid.Parse(Mcdonald_BeverageMenu_Id)
                },
                new Menu
                {
                    FoodBusinessId = Guid.Parse(Mcdonald_FoodBusinessId),
                    Name = "Mcdonald Dessert Menu",
                    CreatedBy = Mcdonald_FoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now,
                    MenuId = Guid.Parse(Mcdonald_DessertMenu_Id)
                },

                #endregion

                #region Create menus for BigMama_FoodBusiness

                new Menu
                {
                    FoodBusinessId = Guid.Parse(BigMama_FoodBusinessId),
                    Name = "BigMama Sandwiches Menu",
                    CreatedBy = BigMama_SalimFoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now,
                    MenuId = Guid.Parse(BigMama_SandwichesMenu_Id)
                },
                new Menu
                {
                    FoodBusinessId = Guid.Parse(BigMama_FoodBusinessId),
                    Name = "BigMama Beverage  Menu",
                    CreatedBy = BigMama_SalimFoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now,
                    MenuId = Guid.Parse(BigMama_BeverageMenu_Id)
                },
                new Menu
                {
                    FoodBusinessId = Guid.Parse(BigMama_FoodBusinessId),
                    Name = "BigMama Dessert Menu",
                    CreatedBy = BigMama_SalimFoodBusinessManager_UserId,
                    CreatedAt = DateTime.Now,
                    MenuId = Guid.Parse(BigMama_DessertMenu_Id)
                }

                #endregion

            );

            #endregion
        }
    }
}