namespace eCom.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeUserIdFromIntToStringInReviewsTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reviews", "UserId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reviews", "UserId", c => c.Int(nullable: false));
        }
    }
}
