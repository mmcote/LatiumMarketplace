using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using LatiumMarketplace.Data;

namespace LatiumMarketplace.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170322024224_city001")]
    partial class city001
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LatiumMarketplace.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("banned");

                    b.Property<string>("description");

                    b.Property<string>("firstName");

                    b.Property<string>("lastName");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("LatiumMarketplace.Models.AssetViewModels.Accessory", b =>
                {
                    b.Property<int>("AccessoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AssetId");

                    b.Property<string>("Name");

                    b.HasKey("AccessoryId");

                    b.HasIndex("AssetId");

                    b.ToTable("Accessory");
                });

            modelBuilder.Entity("LatiumMarketplace.Models.AssetViewModels.Asset", b =>
                {
                    b.Property<int>("assetID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CityId");

                    b.Property<int?>("ImageGalleryId");

                    b.Property<int>("MakeId");

                    b.Property<string>("accessory");

                    b.Property<DateTime>("addDate");

                    b.Property<string>("description");

                    b.Property<string>("location");

                    b.Property<string>("name");

                    b.Property<string>("ownerID");

                    b.Property<decimal>("price");

                    b.Property<decimal>("priceDaily");

                    b.Property<decimal>("priceMonthly");

                    b.Property<decimal>("priceWeekly");

                    b.Property<bool>("request");

                    b.HasKey("assetID");

                    b.HasIndex("CityId");

                    b.HasIndex("ImageGalleryId")
                        .IsUnique();

                    b.HasIndex("MakeId");

                    b.ToTable("Asset");
                });

            modelBuilder.Entity("LatiumMarketplace.Models.AssetViewModels.AssetCategory", b =>
                {
                    b.Property<int>("AssetId");

                    b.Property<int>("CategoryId");

                    b.HasKey("AssetId", "CategoryId");

                    b.HasIndex("AssetId");

                    b.HasIndex("CategoryId");

                    b.ToTable("AssetCategory");
                });

            modelBuilder.Entity("LatiumMarketplace.Models.AssetViewModels.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryName");

                    b.Property<int?>("ParentCategoryId");

                    b.HasKey("CategoryId");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("LatiumMarketplace.Models.AssetViewModels.City", b =>
                {
                    b.Property<int>("CityId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("CityId");

                    b.ToTable("City");
                });

            modelBuilder.Entity("LatiumMarketplace.Models.AssetViewModels.Image", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FileLink")
                        .IsRequired();

                    b.Property<int?>("ImageGalleryId")
                        .IsRequired();

                    b.Property<Guid>("ImageGuid")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title");

                    b.Property<bool>("isMain");

                    b.HasKey("ImageId");

                    b.HasAlternateKey("ImageGuid");

                    b.HasIndex("ImageGalleryId");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("LatiumMarketplace.Models.AssetViewModels.ImageGallery", b =>
                {
                    b.Property<int>("ImageGalleryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Title");

                    b.HasKey("ImageGalleryId");

                    b.ToTable("ImageGallery");
                });

            modelBuilder.Entity("LatiumMarketplace.Models.AssetViewModels.Make", b =>
                {
                    b.Property<int>("MakeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("MakeId");

                    b.ToTable("Make");
                });

            modelBuilder.Entity("LatiumMarketplace.Models.BidViewModels.Bid", b =>
                {
                    b.Property<int>("bidId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AssetId");

                    b.Property<int>("asset_id_model");

                    b.Property<string>("asset_name");

                    b.Property<decimal>("bidPrice");

                    b.Property<string>("bidder");

                    b.Property<string>("description");

                    b.Property<DateTime>("endDate");

                    b.Property<DateTime>("startDate");

                    b.Property<bool>("status");

                    b.HasKey("bidId");

                    b.HasIndex("AssetId");

                    b.ToTable("Bid");
                });

            modelBuilder.Entity("LatiumMarketplace.Models.MessageViewModels.Message", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<Guid?>("MessageThreadid");

                    b.Property<DateTime>("SendDate");

                    b.Property<string>("Subject")
                        .IsRequired();

                    b.HasKey("id");

                    b.HasIndex("MessageThreadid");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("LatiumMarketplace.Models.MessageViewModels.MessageThread", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("Assetid");

                    b.Property<DateTime>("LastUpdateDate");

                    b.Property<string>("RecieverEmail");

                    b.Property<string>("RecieverId")
                        .IsRequired();

                    b.Property<string>("SenderEmail");

                    b.Property<string>("SenderId")
                        .IsRequired();

                    b.HasKey("id");

                    b.HasIndex("Assetid");

                    b.ToTable("MessageThread");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("LatiumMarketplace.Models.AssetViewModels.Accessory", b =>
                {
                    b.HasOne("LatiumMarketplace.Models.AssetViewModels.Asset", "Asset")
                        .WithMany("Accessory")
                        .HasForeignKey("AssetId");
                });

            modelBuilder.Entity("LatiumMarketplace.Models.AssetViewModels.Asset", b =>
                {
                    b.HasOne("LatiumMarketplace.Models.AssetViewModels.City", "City")
                        .WithMany("Assets")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LatiumMarketplace.Models.AssetViewModels.ImageGallery", "ImageGallery")
                        .WithOne("Asset")
                        .HasForeignKey("LatiumMarketplace.Models.AssetViewModels.Asset", "ImageGalleryId");

                    b.HasOne("LatiumMarketplace.Models.AssetViewModels.Make", "Make")
                        .WithMany("Assets")
                        .HasForeignKey("MakeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LatiumMarketplace.Models.AssetViewModels.AssetCategory", b =>
                {
                    b.HasOne("LatiumMarketplace.Models.AssetViewModels.Asset", "Asset")
                        .WithMany("AssetCategories")
                        .HasForeignKey("AssetId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LatiumMarketplace.Models.AssetViewModels.Category", "Category")
                        .WithMany("AssetCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LatiumMarketplace.Models.AssetViewModels.Category", b =>
                {
                    b.HasOne("LatiumMarketplace.Models.AssetViewModels.Category", "ParentCategory")
                        .WithMany("ChildCategory")
                        .HasForeignKey("ParentCategoryId")
                        .HasConstraintName("FK_Category_Category");
                });

            modelBuilder.Entity("LatiumMarketplace.Models.AssetViewModels.Image", b =>
                {
                    b.HasOne("LatiumMarketplace.Models.AssetViewModels.ImageGallery", "ImageGallery")
                        .WithMany("Images")
                        .HasForeignKey("ImageGalleryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LatiumMarketplace.Models.BidViewModels.Bid", b =>
                {
                    b.HasOne("LatiumMarketplace.Models.AssetViewModels.Asset", "asset")
                        .WithMany("Bids")
                        .HasForeignKey("AssetId");
                });

            modelBuilder.Entity("LatiumMarketplace.Models.MessageViewModels.Message", b =>
                {
                    b.HasOne("LatiumMarketplace.Models.MessageViewModels.MessageThread", "messageThread")
                        .WithMany("messages")
                        .HasForeignKey("MessageThreadid");
                });

            modelBuilder.Entity("LatiumMarketplace.Models.MessageViewModels.MessageThread", b =>
                {
                    b.HasOne("LatiumMarketplace.Models.AssetViewModels.Asset", "asset")
                        .WithMany("MessageThreads")
                        .HasForeignKey("Assetid");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("LatiumMarketplace.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("LatiumMarketplace.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LatiumMarketplace.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
