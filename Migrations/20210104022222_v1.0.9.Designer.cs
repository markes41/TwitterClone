﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TwitterClone.Models;

namespace TwitterClone.Migrations
{
    [DbContext(typeof(TwitterContext))]
    [Migration("20210104022222_v1.0.9")]
    partial class v109
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("TwitterClone.Models.Tweet", b =>
                {
                    b.Property<long>("TweetID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Comments")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<string>("OwnerMail")
                        .HasColumnType("TEXT");

                    b.HasKey("TweetID");

                    b.HasIndex("OwnerMail");

                    b.ToTable("Tweets");
                });

            modelBuilder.Entity("TwitterClone.Models.User", b =>
                {
                    b.Property<string>("Mail")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Day")
                        .HasColumnType("TEXT");

                    b.Property<string>("Month")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Year")
                        .HasColumnType("TEXT");

                    b.HasKey("Mail");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("UserUser", b =>
                {
                    b.Property<string>("SeguidoresMail")
                        .HasColumnType("TEXT");

                    b.Property<string>("SeguidosMail")
                        .HasColumnType("TEXT");

                    b.HasKey("SeguidoresMail", "SeguidosMail");

                    b.HasIndex("SeguidosMail");

                    b.ToTable("UserUser");
                });

            modelBuilder.Entity("TwitterClone.Models.Tweet", b =>
                {
                    b.HasOne("TwitterClone.Models.User", "Owner")
                        .WithMany("Tweets")
                        .HasForeignKey("OwnerMail");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("UserUser", b =>
                {
                    b.HasOne("TwitterClone.Models.User", null)
                        .WithMany()
                        .HasForeignKey("SeguidoresMail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TwitterClone.Models.User", null)
                        .WithMany()
                        .HasForeignKey("SeguidosMail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TwitterClone.Models.User", b =>
                {
                    b.Navigation("Tweets");
                });
#pragma warning restore 612, 618
        }
    }
}