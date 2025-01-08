using ECommerce.Catalog.Domain.Aggregates;
using ECommerce.Shared.Library.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Catalog.Infrastructure.Persistence
{
    public class CatalogDbContext : DbContext
    {
        private readonly IMediator _mediator;


        public CatalogDbContext(DbContextOptions<CatalogDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }





        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var domainEntities = ChangeTracker.Entries<IAggregateRoot>()
                                              .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Count > 0).ToList();

            var domainEvents = domainEntities.SelectMany(x => x.Entity.DomainEvents).ToList();

            domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                _mediator.Publish(domainEvent);
            }

            return await base.SaveChangesAsync(cancellationToken);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);

            var categories = new Category[]
            {
                new Category("Elektronik"),
                new Category("Kırtasiye"),
                
            };

            var products = new Product[]
            {
                new Product("Iphone 12", "Apple Iphone 12", 10000, 100, 1, "iphone12.jpg"),
                new Product("Samsung S21", "Samsung S21", 9000, 100, 1, "samsungs21.jpg"),
                new Product("Kalem", "Kırmızı Kalem", 10, 100, 2, "kalem.jpg"),
                new Product("Defter", "A5 Defter", 20, 100, 2, "defter.jpg"),
            };

            modelBuilder.Entity<Category>().HasData(categories);
            modelBuilder.Entity<Product>().HasData(products);


        }
    }
}
