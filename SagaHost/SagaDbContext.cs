namespace SagaHost;

using Microsoft.EntityFrameworkCore;

public class SagaDbContext : DbContext
{
    public SagaDbContext(DbContextOptions<SagaDbContext> options)
        : base(options) { }

    public DbSet<SampleSaga> Saga { get; set; }
}