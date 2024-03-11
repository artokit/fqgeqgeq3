using FluentMigrator;
namespace WebApplication9.Migrations;

[Migration(1)]
public class M0000_InitialMigration : Migration
{
    public override void Up()
    {
        Create.Table("products")
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("name").AsString().NotNullable()
            .WithColumn("description").AsString().NotNullable()
            .WithColumn("category").AsString().NotNullable()
            .WithColumn("price").AsDouble().NotNullable()
            .WithColumn("file_name").AsString().Nullable();
    }

    public override void Down()
    {
        Delete.Table("products");
    }
}