using CarLinePickup.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CarLinePickup.Data.Context
{
    public class CarLinePickupContext : CarLinePickupContextBase
    {
        public CarLinePickupContext()
        {
        }

        public CarLinePickupContext(DbContextOptions<CarLinePickupContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //Get all the models and add a query filter to each
            SetQueryFilter<Address>(modelBuilder);
            SetQueryFilter<AuthenticationUser>(modelBuilder);
            SetQueryFilter<AuthenticationUserProvider>(modelBuilder);
            SetQueryFilter<AuthenticationUserType>(modelBuilder);
            SetQueryFilter<County>(modelBuilder);
            SetQueryFilter<Employee>(modelBuilder);
            SetQueryFilter<EmployeeType>(modelBuilder);
            SetQueryFilter<Family>(modelBuilder);
            SetQueryFilter<FamilyMember>(modelBuilder);
            SetQueryFilter<FamilyMemberTravelType>(modelBuilder);
            SetQueryFilter<FamilyMemberType>(modelBuilder);
            SetQueryFilter<Grade>(modelBuilder);
            SetQueryFilter<Qrcode>(modelBuilder);
            SetQueryFilter<School>(modelBuilder);
            SetQueryFilter<State>(modelBuilder);
            SetQueryFilter<Vehicle>(modelBuilder);

            //Create the relationships that may not exist in the database here
        }

        public override int SaveChanges()
        {
            UpdateSoftDeleteStatuses();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateSoftDeleteStatuses();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void SetQueryFilter<T>(ModelBuilder modelBuilder) where T : class
        {
            modelBuilder.Entity<T>().Property<bool>("Deleted");
            modelBuilder.Entity<T>().HasQueryFilter(m => EF.Property<bool>(m, "Deleted") == false);
        }

        private void UpdateSoftDeleteStatuses()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["Deleted"] = false;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["Deleted"] = true;
                        break;
                }
            }
        }
    }
}
