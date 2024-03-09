using FluentMigrator;

namespace WebApplication9.Migrations;

[Migration(2)]
public class M0001_AddStorageTable : AutoReversingMigration
{
    public override void Up()
    {
        Create.Table("storages").WithColumn("uuid").AsString()
            .WithColumn("product_id").AsInt64();
    }
}