using FluentMigrator;

namespace WebApplication9.Migrations;

[Migration(3)]
public class M0002_AddUsersTable: AutoReversingMigration
{
    public override void Up()
    {
        Create.Table("users")
            .WithColumn("id").AsInt64().PrimaryKey().Identity().NotNullable()
            .WithColumn("username").AsString().NotNullable().Unique()
            .WithColumn("password").AsString().NotNullable()
            .WithColumn("email").AsString().NotNullable();
    }
}