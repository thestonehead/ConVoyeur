using ConVoyeur.Web.Infrastructure.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConVoyeur.Data
{ 
    public static class DbInitializer
    {
        public static async Task Initialize(DEXContext context, UserManager<ConUser> userManager, RoleManager<ConRole> roleManager)
        {
            context.Database.EnsureCreated();

            string testEventName = "SFKDEXP Test";
            var testEvent = context.Events.FirstOrDefault(t => t.Name == testEventName);
            if (testEvent == null)
            {
                testEvent = new Event()
                {
                    ActiveFrom = DateTime.UtcNow,
                    ActiveTo = DateTime.UtcNow.Date.AddYears(1),
                    Name = testEventName
                };

                testEvent.Locations.Add(new Location()
                {
                    Name = "D1",
                    Description = "Main lecture hall",
                    Activities = new List<ActivityLocation>()
                {
                    new ActivityLocation()
                    {
                        Activity = new Activity()
                        {
                            Active = true,
                            Name = "Lecture 1",
                            Availability = GetYearRoundEntries(testEvent, new DateTime(1,1,1,12,15,0)).Select(e=> new ActivityAvailabilityEntry(){
                                AvailabilityEntry = new AvailabilityEntry()
                                {
                                    ActiveFrom = e,
                                    ActiveTo = e.AddHours(1)
                                }
                            }).ToList()
                        },
                    },
                    new ActivityLocation()
                    {
                        Activity = new Activity()
                        {
                            Active = true,
                            Name = "Lecture 2",
                            Availability = GetYearRoundEntries(testEvent, new DateTime(1,1,1,13,30,0)).Select(e=> new ActivityAvailabilityEntry(){
                                AvailabilityEntry = new AvailabilityEntry()
                                {
                                    ActiveFrom = e,
                                    ActiveTo = e.AddHours(2)
                                }
                            }).ToList()
                        },
                    },
                      new ActivityLocation()
                    {
                        Activity = new Activity()
                        {
                            Active = true,
                            Name = "Lecture 3",
                            Availability = GetYearRoundEntries(testEvent, new DateTime(1,1,1,16,0,0)).Select(e=> new ActivityAvailabilityEntry(){
                                AvailabilityEntry = new AvailabilityEntry()
                                {
                                    ActiveFrom = e,
                                    ActiveTo = e.AddHours(.5)
                                }
                            }).ToList()
                        },
                    }
                }
                });

                testEvent.Locations.Add(new Location()
                {
                    Name = "Hol",
                    Description = "Hallways and staircases",
                    Activities = new List<ActivityLocation>()
                {
                     new ActivityLocation()
                    {
                        Activity = new Activity()
                        {
                            Active = true,
                            Name = "Okršaj u Holu",
                            Availability = GetYearRoundEntries(testEvent, new DateTime(1,1,1,12,0,0)).Select(e=> new ActivityAvailabilityEntry(){
                                AvailabilityEntry = new AvailabilityEntry()
                                {
                                    ActiveFrom = e,
                                    ActiveTo = e.AddHours(1.25)
                                }
                            }).ToList()
                        },
                    },
                      new ActivityLocation()
                    {
                        Activity = new Activity()
                        {
                            Active = true,
                            Name = "Kviz na štandu",
                            Availability = GetYearRoundEntries(testEvent, new DateTime(1,1,1,12,0,0)).Select(e=> new ActivityAvailabilityEntry(){
                                AvailabilityEntry = new AvailabilityEntry()
                                {
                                    ActiveFrom = e,
                                    ActiveTo = e.AddHours(6)
                                }
                            }).ToList()
                        },
                    }
                }
                });
                context.Events.Add(testEvent);
                context.SaveChanges();
            }

            // Create roles
            if (await roleManager.FindByNameAsync(Constants.Roles.AdminRoleName) == null)
            {
                await roleManager.CreateAsync(new ConRole(Constants.Roles.AdminRoleName));
            }
            if (await roleManager.FindByNameAsync(Constants.Roles.VolunteerRoleName) == null)
            {
                await roleManager.CreateAsync(new ConRole(Constants.Roles.VolunteerRoleName));
            }
            if (await roleManager.FindByNameAsync(Constants.Roles.VisitorRoleName) == null)
            {
                await roleManager.CreateAsync(new ConRole(Constants.Roles.VisitorRoleName));
            }


            // Create users
            if (await userManager.FindByNameAsync("admin") == null)
            {
                var user = new ConUser("admin", testEvent)
                {
                    Name = "Administrator",
                    Email = "admin@admin.com"
                };
                var result = await userManager.CreateAsync(user, "admin");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Constants.Roles.AdminRoleName);
                }
            }
            string testVolunteerUserName = "testVolunteer1";
            string testPassword = "123456";
            if (await userManager.FindByNameAsync(testVolunteerUserName) == null)
            {
                var user = new ConUser(testVolunteerUserName, testEvent)
                {
                    Name = testVolunteerUserName,
                    Email = testVolunteerUserName+"@test.com"
                };
                var result = await userManager.CreateAsync(user, testPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Constants.Roles.VolunteerRoleName);
                }
            }
            for (int i = 0;i < 5; i++)
            {
                string testVisitorUserName = $"testVisitor{i}";
                if (await userManager.FindByNameAsync(testVisitorUserName) == null)
                {
                    var user = new ConUser(testVisitorUserName, testEvent)
                    {
                        Name = testVisitorUserName + " Jones",
                        Email = testVisitorUserName + "@mailinator.com"
                    };
                    var result = await userManager.CreateAsync(user, testPassword);
                    if (result.Succeeded)
                    {
                       await userManager.AddToRoleAsync(user,Constants.Roles.VisitorRoleName);
                    }
                }
            }

           

        }


        private static IEnumerable<DateTime> GetYearRoundEntries(Event testEvent, DateTime time)
        {
            var date = new DateTime(testEvent.ActiveFrom.Year, testEvent.ActiveFrom.Month, testEvent.ActiveFrom.Day, time.Hour, time.Minute, time.Second, DateTimeKind.Utc);
            do
            {
                yield return date = date.AddDays(1);
            } while (date < testEvent.ActiveTo);
        }
    }
}
