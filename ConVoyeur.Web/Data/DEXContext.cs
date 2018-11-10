using ConVoyeur.Web.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConVoyeur.Data
{
    public class DEXContext : IdentityDbContext<ConUser, ConRole, int>
    {
        private readonly AppSettings appSettings;

        public DbSet<Event> Events { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Activity> Activities { get; set; }
        //public DbSet<ConUser> Users { get; set; }
        //public DbSet<Visitor> Visitors { get; set; }
        public DbSet<ActivityEntry> ActivityEntries { get; set; }
        public DbSet<AvailabilityEntry> AvailabilityEntries { get; set; }



        public DEXContext(DbContextOptions<DEXContext> options, IOptions<AppSettings> appSettings) : base(options)
        {
            this.appSettings = appSettings.Value;
        }
        
        /// <summary>
        /// Returns  available activities at a certain time with an offset from settings
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public IQueryable<Activity> GetAvailableActivitiesAtTime(DateTime time)
        {
            return this.Activities.Where(t => t.Availability.Any(a => a.AvailabilityEntry.ActiveFrom.Add(-appSettings.ActivityAvailableOffset) > time && a.AvailabilityEntry.ActiveTo.Add(appSettings.ActivityAvailableOffset) < time));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Fix for MySql key size
            modelBuilder.Entity<ConUser>(entity => entity.Property(m => m.NormalizedEmail).HasMaxLength(200));
            modelBuilder.Entity<ConUser>(entity => entity.Property(m => m.NormalizedUserName).HasMaxLength(200));
            modelBuilder.Entity<ConRole>(entity => entity.Property(m => m.NormalizedName).HasMaxLength(200));
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(200));
            //modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(200));
            //modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(200));
            //modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(200));
            modelBuilder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(200));
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(200));
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.HasKey(k => k.ProviderKey));

            //Application data rules
            modelBuilder.Entity<Event>().Property(b => b.ActiveFrom).HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Location>().HasOne<Event>(nameof(Location.Event)).WithMany(nameof(Event.Locations)).HasForeignKey(nameof(Location.EventId));

            // Activity <-> Visitor
            modelBuilder.Entity<ActivityEntry>().HasOne<Activity>(nameof(ActivityEntry.Activity)).WithMany(nameof(Activity.Visitors)).HasForeignKey(nameof(ActivityEntry.ActivityId));
            modelBuilder.Entity<ActivityEntry>().HasOne<ConUser>(nameof(ActivityEntry.Visitor)).WithMany(nameof(ConUser.Activities)).HasForeignKey(nameof(ActivityEntry.VisitorId));
            modelBuilder.Entity<ActivityEntry>().HasOne<ConUser>(nameof(ActivityEntry.ActivatedUser)).WithMany().HasForeignKey(nameof(ActivityEntry.ActivatedUserId));
            modelBuilder.Entity<ActivityEntry>().HasOne<Location>(nameof(ActivityEntry.ActivatedLocation)).WithMany().IsRequired(false).HasForeignKey(nameof(ActivityEntry.ActivatedLocationId));
            modelBuilder.Entity<ActivityEntryReview>().HasOne<ActivityEntry>(nameof(ActivityEntryReview.ActivityEntry)).WithOne(nameof(ActivityEntry.Review)).HasForeignKey<ActivityEntryReview>(aer=>aer.ActivityEntryId).OnDelete(DeleteBehavior.Cascade);

            // Activity <-> Location
            modelBuilder.Entity<Activity>().HasMany<ActivityLocation>(nameof(Activity.Locations)).WithOne(nameof(ActivityLocation.Activity)).HasForeignKey(nameof(ActivityLocation.ActivityId));
            modelBuilder.Entity<Location>().HasMany<ActivityLocation>(nameof(Location.Activities)).WithOne(nameof(ActivityLocation.Location)).HasForeignKey(nameof(ActivityLocation.LocationId));
            modelBuilder.Entity<ActivityLocation>().HasKey(nameof(ActivityLocation.LocationId), nameof(ActivityLocation.ActivityId));

            // Activity <-> AvailabilityEntry
            modelBuilder.Entity<Activity>().HasMany<ActivityAvailabilityEntry>(nameof(Activity.Availability)).WithOne(nameof(ActivityAvailabilityEntry.Activity)).HasForeignKey(nameof(ActivityAvailabilityEntry.ActivityId));
            modelBuilder.Entity<AvailabilityEntry>().HasMany<ActivityAvailabilityEntry>(nameof(AvailabilityEntry.Activities)).WithOne(nameof(ActivityAvailabilityEntry.AvailabilityEntry)).HasForeignKey(nameof(ActivityAvailabilityEntry.AvailabilityEntryId));
            modelBuilder.Entity<ActivityAvailabilityEntry>().HasKey(nameof(ActivityAvailabilityEntry.ActivityId), nameof(ActivityAvailabilityEntry.AvailabilityEntryId));

            // Event <-> User
            modelBuilder.Entity<Event>().HasMany<ConUser>(nameof(Event.Users)).WithOne(nameof(ConUser.Event)).HasForeignKey(nameof(ConUser.EventId));
            //modelBuilder.Entity<ConRole>().HasMany<ConUserRole>(nameof(ConRole.Event)).WithOne(nameof(ConUserRole.User)).HasForeignKey(nameof(ConUserRole.UserId));
        }
    }
}
