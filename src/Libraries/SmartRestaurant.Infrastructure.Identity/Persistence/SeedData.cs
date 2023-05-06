﻿using Microsoft.EntityFrameworkCore;
using SmartRestaurant.Domain.Identity.Entities;
using SmartRestaurant.Infrastructure.Identity.Enums;

namespace SmartRestaurant.Infrastructure.Identity.Persistence
{
    public static class SeedData
    {
        public const string TajMhal_FoodBusinessAdministrator_UserId = "3cbf3570-4444-4444-8746-29b7cf568093";
        public const string Mcdonald_FoodBusinessAdministrator_UserId = "44bf3570-0d44-4673-8746-29b7cf568088";
        public const string BigMama_FoodBusinessAdministrator_UserId = "08a1a626-7f8e-4b51-84fc-fc51b6302cca";

        public const string TajMhal_FoodBusinessManager_UserId = "a1997466-cedc-4850-b18d-0ac4f4102cff";
        public const string Mcdonald_FoodBusinessManager_UserId = "b2207466-ceda-4b50-b18d-0ac4f4102caa";
        public const string BigMama_SalimFoodBusinessManager_UserId = "64fed988-6f68-49dc-ad54-0da50ec02319";

        public const string Diner_UserId_01 = "5a84cd00-59f0-4b22-bfce-07c080829118";
        public const string Diner_UserId_02 = "6b14cd00-59f0-4422-bfce-07c080829987";

        public const string CEVITAL_UserId = "ba89dc5f-dfd1-4c87-9372-233c611cc756";
        public const string Sonatrach_UserId = "a3dbd500-eab0-4233-86fd-7f1a4195f9a9";
        public const string HotelManager_userID = "6d215fd5-e7b4-4afd-aa6c-334a37d3874d";
        public const string HotelReceptionist_UserId = "83e72357-25b8-4e2a-8728-3e25365608e2";
        public const string HotelServiceAdmin_userId = "C4EAACBE-A5C5-47E8-8DED-508709D7A50F";
        public const string ClientHotel_UserId = "FB8EC84D-3A8F-4FC6-B21E-7141B48A164D";
        public const string HotelServiceTechnique_UserId = "acd04fc6-99da-436f-a011-191b7e92aa23";
        public const string HouseKeeping_UserId = "7d33ae49-68a8-4c10-bc57-b09da6f9f016";

        public static void Seed(this ModelBuilder modelBuilder)
        {
            #region Create application Roles

            modelBuilder.Entity<ApplicationRole>().HasData(
                new ApplicationRole
                {
                    Id = ((long) Roles.SuperAdmin).ToString(),
                    Name = Roles.SuperAdmin.ToString(),
                    NormalizedName = Roles.SuperAdmin.ToString().ToUpper(),
                    ConcurrencyStamp = "88f0dec2-5364-4881-4817-1f2a135a8641"
                },
                new ApplicationRole
                {
                    Id = ((long) Roles.SupportAgent).ToString(),
                    Name = Roles.SupportAgent.ToString(),
                    NormalizedName = Roles.SupportAgent.ToString().ToUpper(),
                    ConcurrencyStamp = "emec7115-422c-487d-65b0-58cfa8e66a94"
                },
                new ApplicationRole
                {
                    Id = ((long) Roles.SalesMan).ToString(),
                    Name = Roles.SalesMan.ToString(),
                    NormalizedName = Roles.SalesMan.ToString().ToUpper(),
                    ConcurrencyStamp = "emrc7115-422c-487d-75b0-58cfa8e66a94"
                },
                new ApplicationRole
                {
                    Id = ((long) Roles.Photograph).ToString(),
                    Name = Roles.Photograph.ToString(),
                    NormalizedName = Roles.Photograph.ToString().ToUpper(),
                    ConcurrencyStamp = "emtc7115-422c-487d-85b0-58cfa8e66a94"
                },
                new ApplicationRole
                {
                    Id = ((long) Roles.FoodBusinessAdministrator).ToString(),
                    Name = Roles.FoodBusinessAdministrator.ToString(),
                    NormalizedName = Roles.FoodBusinessAdministrator.ToString().ToUpper(),
                    ConcurrencyStamp = "5719c2b8-22fd-4eee-9c21-4bfbd2ce18d2"
                },
                new ApplicationRole
                {
                    Id = ((long) Roles.FoodBusinessManager).ToString(),
                    Name = Roles.FoodBusinessManager.ToString(),
                    NormalizedName = Roles.FoodBusinessManager.ToString().ToUpper(),
                    ConcurrencyStamp = "emcc7115-422c-487d-95b0-58cfa8e66a94"
                },
                new ApplicationRole
                {
                    Id = ((long) Roles.FoodBusinessOwner).ToString(),
                    Name = Roles.FoodBusinessOwner.ToString(),
                    NormalizedName = Roles.FoodBusinessOwner.ToString().ToUpper(),
                    ConcurrencyStamp = "emcb7115-422c-487d-95c0-58cfa8m66a94"
                },
                new ApplicationRole
                {
                    Id = ((long) Roles.Cashier).ToString(),
                    Name = Roles.Cashier.ToString(),
                    NormalizedName = Roles.Cashier.ToString().ToUpper(),
                    ConcurrencyStamp = "encc7115-422c-487d-95b0-58cfa8e66a95"
                },
                new ApplicationRole
                {
                    Id = ((long) Roles.Chef).ToString(),
                    Name = Roles.Chef.ToString(),
                    NormalizedName = Roles.Chef.ToString().ToUpper(),
                    ConcurrencyStamp = "elcc7115-422c-487d-95b0-58cfa8e66a96"
                },
                new ApplicationRole
                {
                    Id = ((long) Roles.Waiter).ToString(),
                    Name = Roles.Waiter.ToString(),
                    NormalizedName = Roles.Waiter.ToString().ToUpper(),
                    ConcurrencyStamp = "ekcc7115-422c-487d-95b0-58cfa8e66a97"
                },
                new ApplicationRole
                {
                    Id = ((long) Roles.Diner).ToString(),
                    Name = Roles.Diner.ToString(),
                    NormalizedName = Roles.Diner.ToString().ToUpper(),
                    ConcurrencyStamp = "edcc7115-422c-487d-95b0-58cfa8e66a98"
                },
                new ApplicationRole
                {
                    Id = ((long) Roles.Anounymous).ToString(),
                    Name = Roles.Anounymous.ToString(),
                    NormalizedName = Roles.Anounymous.ToString().ToUpper(),
                    ConcurrencyStamp = "educ7115-422c-487d-25b0-58cfa8e66a98"
                },
                new ApplicationRole
                {
                    Id = ((long) Roles.Organization).ToString(),
                    Name = Roles.Organization.ToString(),
                    NormalizedName = Roles.Organization.ToString().ToUpper(),
                    ConcurrencyStamp = "edpc7115-422c-487d-15b0-58cfa8e66a98"
                },
                new ApplicationRole
                {
                    Id = ((long)Roles.HotelManager).ToString(),
                    Name = Roles.HotelManager.ToString(),
                    NormalizedName = Roles.HotelManager.ToString().ToUpper(),
                    ConcurrencyStamp = "edpc7115-422c-487d-15b0-58cfa8e66a98"
                }
                ,
                new ApplicationRole
                {
                    Id = ((long)Roles.HotelReceptionist).ToString(),
                    Name = Roles.HotelReceptionist.ToString(),
                    NormalizedName = Roles.HotelReceptionist.ToString().ToUpper(),
                    ConcurrencyStamp = "edpc7115-422c-487d-15b0-58cfa8e66a98"
                }
                ,
                new ApplicationRole
                {
                    Id = ((long)Roles.HotelServiceAdmin).ToString(),
                    Name = Roles.HotelServiceAdmin.ToString(),
                    NormalizedName = Roles.HotelServiceAdmin.ToString().ToUpper(),
                    ConcurrencyStamp = "edpc7115-422c-487d-15b0-58cfa8e66a98"
                }
                ,
                new ApplicationRole
                {
                    Id = ((long)Roles.HotelClient).ToString(),
                    Name = Roles.HotelClient.ToString(),
                    NormalizedName = Roles.HotelClient.ToString().ToUpper(),
                    ConcurrencyStamp = "edpc7115-422c-487d-15b0-58cfa8e66a98"
                },
                new ApplicationRole
                {
                    Id = ((long)Roles.HotelServiceTechnique).ToString(),
                    Name = Roles.HotelServiceTechnique.ToString(),
                    NormalizedName = Roles.HotelServiceTechnique.ToString().ToUpper(),
                    ConcurrencyStamp = "270b4553-f9b8-48e0-b244-af2ce4bc5ca9"
                },
                 new ApplicationRole
                 {
                     Id = ((long)Roles.HouseKeeping).ToString(),
                     Name = Roles.HouseKeeping.ToString(),
                     NormalizedName = Roles.HouseKeeping.ToString().ToUpper(),
                     ConcurrencyStamp = "2622be83-085c-4339-ae68-ffa9d5cd2fa8"
                 }
            );

            #endregion


            #region Create applications users

            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = "3cbf3570-0d44-4673-8746-29b7cf568093",
                    UserName = "SuperAdmin@SmartRestaurant.io",
                    Email = "SuperAdmin@SmartRestaurant.io",
                    NormalizedUserName = "SUPERADMIN@SMARTRESTAURANT.IO",
                    NormalizedEmail = "SUPERADMIN@SMARTRESTAURANT.IO",
                    PasswordHash =
                        "AQAAAAEAACcQAAAAEAzFpmzMtMiw0wHV6b0aUzFLF9Pw7B2u+DswRHttAU2nH22NHBsc/hSSvKUqmRWGZA==",
                    EmailConfirmed = true
                },
                new ApplicationUser
                {
                    Id = "d466ef00-61f1-4e77-801a-b016f0f12323",
                    UserName = "SupportAgent@SmartRestaurant.io",
                    Email = "SupportAgent@SmartRestaurant.io",
                    NormalizedUserName = "SUPPORTAGENT@SMARTRESTAURANT.IO",
                    NormalizedEmail = "SUPPORTAGENT@SMARTRESTAURANT.IO",
                    PasswordHash =
                        "AQAAAAEAACcQAAAAEE2YnCbwcY+aBvcZq2dTXfaPqZnSgNoXFKtyI0hIdVJI3tTBvln+3oc+p1Ijr/ckMw==",
                    EmailConfirmed = true
                },
                new ApplicationUser
                {
                    Id = "d466ef00-61f1-4e77-801a-b516f0f12323",
                    UserName = "Waiter@SmartRestaurant.io",
                    Email = "Waiter@SmartRestaurant.io",
                    NormalizedUserName = "WAITER@SMARTRESTAURANT.IO",
                    NormalizedEmail = "WAITER@SMARTRESTAURANT.IO",
                    PasswordHash =
                        "AQAAAAEAACcQAAAAEE2YnCbwcY+aBvcZq2dTXfaPqZnSgNoXFKtyI0hIdVJI3tTBvln+3oc+p1Ijr/ckMw==",
                    EmailConfirmed = true
                },

                #region Create FoodBusinessAdministrator users

                new ApplicationUser
                {
                    Id = TajMhal_FoodBusinessAdministrator_UserId,
                    UserName = "FoodAdmin@SmartRestaurant.io",
                    Email = "FoodAdmin@SmartRestaurant.io",
                    NormalizedUserName = "FOODADMIN@SMARTRESTAURANT.IO",
                    NormalizedEmail = "FOODADMIN@SMARTRESTAURANT.IO",
                    // Real password is "Supportagent123@"
                    PasswordHash =
                        "AQAAAAEAACcQAAAAEE2YnCbwcY+aBvcZq2dTXfaPqZnSgNoXFKtyI0hIdVJI3tTBvln+3oc+p1Ijr/ckMw==",
                    EmailConfirmed = true
                },
                new ApplicationUser
                {
                    Id = Mcdonald_FoodBusinessAdministrator_UserId,
                    UserName = "McdonaldFoodAdmin@SmartRestaurant.io",
                    Email = "McdonaldFoodAdmin@SmartRestaurant.io",
                    NormalizedUserName = "MCDONALDFOODADMIN@SMARTRESTAURANT.IO",
                    NormalizedEmail = "MCDONALDFOODADMIN@SMARTRESTAURANT.IO",
                    // Real password is "Supportagent123@"
                    PasswordHash =
                        "AQAAAAEAACcQAAAAEE2YnCbwcY+aBvcZq2dTXfaPqZnSgNoXFKtyI0hIdVJI3tTBvln+3oc+p1Ijr/ckMw==",
                    EmailConfirmed = true
                },
                new ApplicationUser
                {
                    Id = BigMama_FoodBusinessAdministrator_UserId,
                    UserName = "BigMamaFoodBusinessAdministrator@SmartRestaurant.io",
                    Email = "BigMamaFoodBusinessAdministrator@SmartRestaurant.io",
                    NormalizedUserName = "BIGMAMAFOODBUSINESSADMINISTRATOR@SMARTRESTAURANT.IO",
                    NormalizedEmail = "BIGMAMAFOODBUSINESSADMINISTRATOR@SMARTRESTAURANT.IO",
                    // Real password is "Supportagent123@"
                    PasswordHash =
                        "AQAAAAEAACcQAAAAEE2YnCbwcY+aBvcZq2dTXfaPqZnSgNoXFKtyI0hIdVJI3tTBvln+3oc+p1Ijr/ckMw==",
                    EmailConfirmed = true
                },

                #endregion

                #region Create FoodBusinessManager users

                new ApplicationUser
                {
                    Id = TajMhal_FoodBusinessManager_UserId,
                    UserName = "TajMhalFoodBusinessManager@SmartRestaurant.io",
                    Email = "TajMhalFoodBusinessManager@SmartRestaurant.io",
                    NormalizedUserName = "TAJMHALFOODBUSINESSMANAGER@SMARTRESTAURANT.IO",
                    NormalizedEmail = "TAJMHALFOODBUSINESSMANAGER@SMARTRESTAURANT.IO",
                    // Real password is "FoodBusinessManager123@"
                    PasswordHash =
                        "AQAAAAEAACcQAAAAEGsuHVzJHLS9jP+mo+zCHk22BZphE5WRR+o2C6Ct4Ektv8zW9DXj1nogD2OdNBjWPA==",
                    EmailConfirmed = true
                },
                new ApplicationUser
                {
                    Id = Mcdonald_FoodBusinessManager_UserId,
                    UserName = "McdonaldFoodBusinessManager@SmartRestaurant.io",
                    Email = "McdonaldFoodBusinessManager@SmartRestaurant.io",
                    NormalizedUserName = "MCDONALDFOODBUSINESSMANAGER@SMARTRESTAURANT.IO",
                    NormalizedEmail = "MCDONALDFOODBUSINESSMANAGER@SMARTRESTAURANT.IO",
                    // Real password is "FoodBusinessManager123@"
                    PasswordHash =
                        "AQAAAAEAACcQAAAAEGsuHVzJHLS9jP+mo+zCHk22BZphE5WRR+o2C6Ct4Ektv8zW9DXj1nogD2OdNBjWPA==",
                    EmailConfirmed = true
                },
                new ApplicationUser
                {
                    Id = BigMama_SalimFoodBusinessManager_UserId,
                    UserName = "BigMamaSalimFoodBusinessManager@SmartRestaurant.io",
                    Email = "BigMamaSalimFoodBusinessManager@SmartRestaurant.io",
                    NormalizedUserName = "BIGMAMASALIMFOODBUSINESSMANAGER@SMARTRESTAURANT.IO",
                    NormalizedEmail = "BIGMAMASALIMFOODBUSINESSMANAGER@SMARTRESTAURANT.IO",
                    // Real password is "Salim123@"
                    PasswordHash =
                        "AQAAAAEAACcQAAAAEO+ouwzSOa+AsCNZrVEhO6Su9q/fX/Q9c9havEvhs5QtXWA6tRdfmqOlemUQphqDnA==",
                    EmailConfirmed = true
                },

                #endregion

                #region Create Diner users

                new ApplicationUser
                {
                    Id = Diner_UserId_01,
                    UserName = "Diner_01@SmartRestaurant.io",
                    Email = "Diner_01@SmartRestaurant.io",
                    NormalizedUserName = "DINER_01@SMARTRESTAURANT.IO",
                    NormalizedEmail = "DINER_01@SMARTRESTAURANT.IO",
                    // Real password is "Diner123@"
                    PasswordHash =
                        "AQAAAAEAACcQAAAAEJFZbbuBIpvoyXKwrceuNsU4cXZ18LLAl8g7s48Pye4EAEXwA2hswtnLMhMS9Q7Cjw ==",
                    EmailConfirmed = true
                },
                new ApplicationUser
                {
                    Id = Diner_UserId_02,
                    UserName = "Diner_02@SmartRestaurant.io",
                    Email = "Diner_02@SmartRestaurant.io",
                    NormalizedUserName = "DINER_02@SMARTRESTAURANT.IO",
                    NormalizedEmail = "DINER_02@SMARTRESTAURANT.IO",
                    // Real password is "Diner123@"
                    PasswordHash =
                        "AQAAAAEAACcQAAAAEJFZbbuBIpvoyXKwrceuNsU4cXZ18LLAl8g7s48Pye4EAEXwA2hswtnLMhMS9Q7Cjw ==",
                    EmailConfirmed = true
                },

                #endregion

                #region Create FoodBusinessClient Users
                new ApplicationUser
                {
                    Id = Sonatrach_UserId,
                    UserName = "manager@sonatrach.com",
                    Email = "manager@sonatrach.com",
                    NormalizedUserName = "MANAGER@SONATRACH.COM",
                    NormalizedEmail = "MANAGER@SONATRACH.COM",
                    // Real password is "Supportagent123@"
                    PasswordHash =
                        "AQAAAAEAACcQAAAAEE2YnCbwcY+aBvcZq2dTXfaPqZnSgNoXFKtyI0hIdVJI3tTBvln+3oc+p1Ijr/ckMw==",
                    EmailConfirmed = true
                },
                new ApplicationUser
                {
                    Id = CEVITAL_UserId,
                    UserName = "manager@cevital.com",
                    Email = "manager@cevital.com",
                    NormalizedUserName = "MANAGER@CEVITAL.COM",
                    NormalizedEmail = "MANAGER@CEVITAL.COM",
                    // Real password is "Supportagent123@"
                    PasswordHash =
                        "AQAAAAEAACcQAAAAEE2YnCbwcY+aBvcZq2dTXfaPqZnSgNoXFKtyI0hIdVJI3tTBvln+3oc+p1Ijr/ckMw==",
                    EmailConfirmed = true
                },
                #endregion

                #region Create Hotel Users
                new ApplicationUser
                {
                    Id = HotelManager_userID,
                    UserName = "HotelManager@gmail.com",
                    Email = "HotelManager@gmail.com",
                    NormalizedUserName = "HOTELMANAGER@GMAIL.COM",
                    NormalizedEmail = "HOTELMANAGER@GMAIL.COM",
                    // Real password is "FoodBusinessManager123@"
                    PasswordHash =
                        "AQAAAAEAACcQAAAAEGsuHVzJHLS9jP+mo+zCHk22BZphE5WRR+o2C6Ct4Ektv8zW9DXj1nogD2OdNBjWPA==",
                    EmailConfirmed = true
                },
                new ApplicationUser
                {
                    Id = HotelReceptionist_UserId,
                    UserName = "HotelReceptionist@gmail.com",
                    Email = "HotelReceptionist@gmail.com",
                    NormalizedUserName = "HOTELRECEPTIONIST@GMAIL.COM",
                    NormalizedEmail = "HOTELRECEPTIONIST@GMAIL.COM",
                    // Real password is "FoodBusinessManager123@"
                    PasswordHash =
                        "AQAAAAEAACcQAAAAEGsuHVzJHLS9jP+mo+zCHk22BZphE5WRR+o2C6Ct4Ektv8zW9DXj1nogD2OdNBjWPA==",
                    EmailConfirmed = true
                },
                new ApplicationUser
                {
                    Id = HotelServiceAdmin_userId,
                    UserName = "HotelServiceAdmin@gmail.com",
                    Email = "HotelServiceAdmin@gmail.com",
                    NormalizedUserName = "HOTELSERVICEADMIN@GMAIL.COM",
                    NormalizedEmail = "HOTELSERVICEADMIN@GMAIL.COM",
                    // Real password is "FoodBusinessManager123@"
                    PasswordHash =
                        "AQAAAAEAACcQAAAAEGsuHVzJHLS9jP+mo+zCHk22BZphE5WRR+o2C6Ct4Ektv8zW9DXj1nogD2OdNBjWPA==",
                    EmailConfirmed = true
                },
                new ApplicationUser
                {
                    Id = ClientHotel_UserId,
                    UserName = "ClientHotel@gmail.com",
                    Email = "ClientHotel@gmail.com",
                    NormalizedUserName = "CLIENTHOTEL@GMAIL.COM",
                    NormalizedEmail = "CLIENTHOTEL@GMAIL.COM",
                    // Real password is "FoodBusinessManager123@"
                    PasswordHash =
                        "AQAAAAEAACcQAAAAEGsuHVzJHLS9jP+mo+zCHk22BZphE5WRR+o2C6Ct4Ektv8zW9DXj1nogD2OdNBjWPA==",
                    EmailConfirmed = true
                },
                 new ApplicationUser
                 {
                     Id = HotelServiceTechnique_UserId,
                     UserName = "HotelServiceTechnique@gmail.com",
                     Email = "HotelServiceTechnique@gmail.com",
                     NormalizedUserName = "HOTELSERVICETECHNIQUE@GMAIL.COM",
                     NormalizedEmail = "HOTELSERVICETECHNIQUE@GMAIL.COM",
                     // Real password is "FoodBusinessManager123@"
                     PasswordHash =
                        "AQAAAAEAACcQAAAAEGsuHVzJHLS9jP+mo+zCHk22BZphE5WRR+o2C6Ct4Ektv8zW9DXj1nogD2OdNBjWPA==",
                     EmailConfirmed = true
                 },
                  new ApplicationUser
                  {
                      Id = HouseKeeping_UserId,
                      UserName = "HotelMaid@gmail.com",
                      Email = "HotelMaid@gmail.com",
                      NormalizedUserName = "HOTELMAID@GMAIL.COM",
                      NormalizedEmail = "HOTELMAID@GMAIL.COM",
                      // Real password is "FoodBusinessManager123@"
                      PasswordHash =
                        "AQAAAAEAACcQAAAAEGsuHVzJHLS9jP+mo+zCHk22BZphE5WRR+o2C6Ct4Ektv8zW9DXj1nogD2OdNBjWPA==",
                      EmailConfirmed = true
                  }
                  #endregion

            );

            #endregion


            #region Set Users Roles

            modelBuilder.Entity<ApplicationUserRole>().HasData(
                new ApplicationUserRole
                {
                    UserId = "3cbf3570-0d44-4673-8746-29b7cf568093",
                    RoleId = "1"
                },
                new ApplicationUserRole
                {
                    UserId = "d466ef00-61f1-4e77-801a-b016f0f12323",
                    RoleId = "2"
                },
                new ApplicationUserRole
                {
                    UserId = "d466ef00-61f1-4e77-801a-b516f0f12323",
                    RoleId = "10"
                },

                #region Assign the role FoodBusinessAdministrator to users accounts

                new ApplicationUserRole
                {
                    UserId = TajMhal_FoodBusinessAdministrator_UserId,
                    RoleId = "5"
                },
                new ApplicationUserRole
                {
                    UserId = Mcdonald_FoodBusinessAdministrator_UserId,
                    RoleId = "5"
                },
                new ApplicationUserRole
                {
                    UserId = BigMama_FoodBusinessAdministrator_UserId,
                    RoleId = "5"
                },

                #endregion

                #region Assign the role FoodBusinessManager to users accounts

                new ApplicationUserRole
                {
                    UserId = TajMhal_FoodBusinessManager_UserId,
                    RoleId = "6"
                },
                new ApplicationUserRole
                {
                    UserId = Mcdonald_FoodBusinessManager_UserId,
                    RoleId = "6"
                },
                new ApplicationUserRole
                {
                    UserId = BigMama_SalimFoodBusinessManager_UserId,
                    RoleId = "6"
                },

                #endregion

                #region Assign the role Diner to users accounts

                new ApplicationUserRole
                {
                    UserId = Diner_UserId_01,
                    RoleId = "11"
                }
                ,
                new ApplicationUserRole
                {
                    UserId = Diner_UserId_02,
                    RoleId = "11"
                },

                #endregion

                #region Assign roles to Hotel users
                new ApplicationUserRole
                {
                    UserId = HotelManager_userID,
                    RoleId = "14"
                },
                new ApplicationUserRole
                {
                    UserId = HotelReceptionist_UserId,
                    RoleId = "15"
                },
                new ApplicationUserRole
                {
                    UserId = HotelServiceAdmin_userId,
                    RoleId = "16"
                },
                new ApplicationUserRole
                {
                    UserId = ClientHotel_UserId,
                    RoleId = "17"
                },
                new ApplicationUserRole
                {
                    UserId = HotelServiceTechnique_UserId,
                    RoleId = "18"
                },
                 new ApplicationUserRole
                 {
                     UserId = HouseKeeping_UserId,
                     RoleId = "19"
                 }

                 #endregion
                 #region add rols to organisation user
                ,
                new ApplicationUserRole
                {
                    UserId = CEVITAL_UserId,
                    RoleId = "13"
                },
                new ApplicationUserRole
                {
                    UserId = Sonatrach_UserId,
                    RoleId = "13"
                }
                #endregion

            );

            #endregion
        }
    }
}