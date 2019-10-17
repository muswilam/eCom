namespace eCom.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Name], [Address]) VALUES (N'c1194334-1efa-4ebc-80de-e217874b489d', N'switchadmin@bilaq.com', 0, N'AJ7H5WZHWJIJLS7ArODuyeEA8C28KIlcgke3fx2ZIBR2n5hhb1D7cYkXZ8nk37uGaA==', N'18d809d2-1f8f-461c-912d-32050ced14e4', NULL, 0, 0, NULL, 1, 0, N'switchadmin@bilaq.com', N'Switch admin', N'Cairo')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Name], [Address]) VALUES (N'cb7da20f-186e-4145-be5f-102f167ea827', N'Admin@Bilaq.com', 0, N'ADap5GDsYMeAzYYvz8hVQp/Ear3mr2AIdbJ2HrNNomp2p7dgS0rmYOJ8UjI3RpA/uw==', N'5cbb3cc4-bc05-47fc-93dd-02748bc9a709', NULL, 0, 0, NULL, 1, 0, N'Admin@Bilaq.com', N'Admin', N'Cairo')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'bd552315-25b1-458c-8e80-6057512fa10a', N'Admin')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'cb7da20f-186e-4145-be5f-102f167ea827', N'bd552315-25b1-458c-8e80-6057512fa10a')

");
        }
        
        public override void Down()
        {
        }
    }
}
