namespace eCom.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedConfigData : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[Configs] ([Key], [Value]) VALUES (N'CompanyAddress', N'Egypt, Cairo')
INSERT INTO [dbo].[Configs] ([Key], [Value]) VALUES (N'CompanyPhone', N'(+20) 100 466 2373')
INSERT INTO [dbo].[Configs] ([Key], [Value]) VALUES (N'FacebookUrl', N'http/:www.facebook.com/muswilam')
INSERT INTO [dbo].[Configs] ([Key], [Value]) VALUES (N'GithubUrl', N'https://www.github.com/muswilam')
INSERT INTO [dbo].[Configs] ([Key], [Value]) VALUES (N'Mail', N'www.bilaq@mailinator.com')
INSERT INTO [dbo].[Configs] ([Key], [Value]) VALUES (N'LinkedinUrl', N'https://www.linkedin.com/in/muhammad-swilam-a479a1b0/')
INSERT INTO [dbo].[Configs] ([Key], [Value]) VALUES (N'MainImage', N'/Content/Images/508cc890-8ab2-424d-b9d0-344345e6f29a.jpg')
INSERT INTO [dbo].[Configs] ([Key], [Value]) VALUES (N'SecondImage', N'/Content/Images/second-image-shop.jpg')
INSERT INTO [dbo].[Configs] ([Key], [Value]) VALUES (N'TwitterUrl', N'https://www.twitter.com/muswilam')

");
        }
        
        public override void Down()
        {
        }
    }
}
