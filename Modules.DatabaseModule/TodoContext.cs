using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Modules.DatabaseModule.Tables;

namespace Modules.DatabaseModule;

public class TodoContext : DbContext
{
    public TodoContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        string path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "todo.db");
    }

    public DbSet<User> Users { get; set; }
    public DbSet<TodoList> Lists { get; set; }
    public DbSet<Todo> Todos { get; set; }

    public string DbPath { get; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseSqlite($"Data Source={DbPath}");
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        GenerateCreatedAtForAllTables(modelBuilder);
    }

    private static void GenerateCreatedAtForAllTables(ModelBuilder modelBuilder)
    {
        //https://stackoverflow.com/a/44375631
        //https://stackoverflow.com/a/52963640
        //https://stackoverflow.com/a/51764814

        //bütün database  tablolarını gezme
        foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
        {
            //tablo sınıfı, IEntity sınıfından türüyor mu kontrol
            Type type = entityType.ClrType;
            if (!typeof(BaseEntity).IsAssignableFrom(type))
            {
                continue;
            }

            //CreateAt property'sini otomatik database'e hesaplatma
            modelBuilder.Entity(type)
                        .Property<DateTime>(nameof(BaseEntity.CreatedAt))
                        .ValueGeneratedOnAddOrUpdate()
                        .IsConcurrencyToken()
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}