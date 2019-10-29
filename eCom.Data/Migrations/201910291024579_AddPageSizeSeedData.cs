namespace eCom.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPageSizeSeedData : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[Configs] ([Key], [Value]) VALUES (N'PageSize', N'')
INSERT INTO [dbo].[Configs] ([Key], [Value]) VALUES (N'ShopPageSize', N'')
");
        }
        
        public override void Down()
        {
        }
    }
}
