using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using FirstApp.Data;

namespace FirstApp.Data.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("FirstApp.Models.Entities.Thread", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreatorId");

                    b.Property<string>("Topic");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Thread");
                });

            modelBuilder.Entity("FirstApp.Models.Entities.ThreadReply", b =>
                {
                    b.Property<int>("Id");

                    b.Property<bool>("Anonymous");

                    b.Property<int?>("AuthorId");

                    b.Property<string>("Message");

                    b.Property<string>("PosterId");

                    b.Property<string>("PosterName");

                    b.Property<int>("ThreadId");

                    b.HasKey("Id");

                    b.HasIndex("ThreadId");

                    b.ToTable("ThreadReply");
                });

            modelBuilder.Entity("FirstApp.Models.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsActive");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Salt")
                        .IsRequired();

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(24);

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("FirstApp.Models.Entities.Thread", b =>
                {
                    b.HasOne("FirstApp.Models.Entities.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FirstApp.Models.Entities.ThreadReply", b =>
                {
                    b.HasOne("FirstApp.Models.Entities.Thread")
                        .WithMany("Replies")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FirstApp.Models.Entities.Thread", "Thread")
                        .WithMany()
                        .HasForeignKey("ThreadId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
