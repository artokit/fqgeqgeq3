using FluentMigrator;
namespace WebApplication9.Migrations;

[Migration(1)]
public class M0000_InitialMigration : Migration
{
    public override void Up()
    {
        Create.Table("products")
            .WithColumn("id").AsInt64().Identity().PrimaryKey()
            .WithColumn("name").AsString()
            .WithColumn("description").AsString()
            .WithColumn("category").AsString()
            .WithColumn("price").AsDouble();
    }

    public override void Down()
    {
        Delete.Table("products");
    }
}