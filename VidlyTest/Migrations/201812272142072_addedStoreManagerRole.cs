namespace VidlyTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedStoreManagerRole : DbMigration
    {
        public override void Up()
        {
            Sql(@"
            INSERT INTO[dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES(N'010bc224-ac28-4915-8074-74d01c50d08e', N'employee@test.com', 0, N'AP6crXCOKvZXoqK/8AfS1NJpg1rRPmouN13H39OCp34+ImAmwA4rf2AMlfs/c/m5oQ==', N'cb5b22ea-42ab-46b6-83bb-994acf10d9c4', NULL, 0, 0, NULL, 1, 0, N'employee@test.com')
            INSERT INTO[dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES(N'3b3459b7-b6cd-4b19-b664-a5357ceef212', N'admin@test.com', 0, N'ADRQ/urqbIBrRCHe/8ftZmUGjMCtka7jI3jVwIA7Hk7cjGAdYzDm3+LolKNKc+2Inw==', N'4d9529b2-4ea5-4bc2-ba24-23794d0a689e', NULL, 0, 0, NULL, 1, 0, N'admin@test.com')

            INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'4ea7be53-86dd-4a53-a84d-e0c41587559a', N'StoreManager')

            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'3b3459b7-b6cd-4b19-b664-a5357ceef212', N'4ea7be53-86dd-4a53-a84d-e0c41587559a')

        ");
        }
        
        public override void Down()
        {
        }
    }
}
