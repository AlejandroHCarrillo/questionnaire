﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Q_EF_DB;

#nullable disable

namespace Q_EF_DB.Migrations
{
    [DbContext(typeof(QContext))]
    partial class QContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Q_EF_DB.Entities.Answer", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"), 1L, 1);

                    b.Property<int?>("QuestionId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Votes")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("UserId");

                    b.ToTable("Answers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            QuestionId = 1,
                            UserId = 1,
                            Value = "Answer 1 to question 1",
                            Votes = 1
                        },
                        new
                        {
                            Id = 2,
                            QuestionId = 1,
                            UserId = 2,
                            Value = "Answer 2 to question 1",
                            Votes = 2
                        },
                        new
                        {
                            Id = 3,
                            QuestionId = 2,
                            UserId = 1,
                            Value = "Answer 1 to question 2",
                            Votes = 11
                        },
                        new
                        {
                            Id = 6,
                            QuestionId = 2,
                            UserId = 2,
                            Value = "Answer 2 to question 2",
                            Votes = 12
                        },
                        new
                        {
                            Id = 4,
                            QuestionId = 2,
                            UserId = 1,
                            Value = "Answer 1 to question 3",
                            Votes = 21
                        },
                        new
                        {
                            Id = 5,
                            QuestionId = 2,
                            UserId = 2,
                            Value = "Answer 2 to question 3",
                            Votes = 22
                        });
                });

            modelBuilder.Entity("Q_EF_DB.Entities.Question", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"), 1L, 1);

                    b.Property<int?>("UserId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Votes")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Questions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            UserId = 1,
                            Value = "Question 1?",
                            Votes = 1
                        },
                        new
                        {
                            Id = 2,
                            UserId = 2,
                            Value = "Question 2?",
                            Votes = 2
                        },
                        new
                        {
                            Id = 3,
                            UserId = 1,
                            Value = "Question 3?",
                            Votes = 3
                        });
                });

            modelBuilder.Entity("Q_EF_DB.Entities.QuestionTag", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"), 1L, 1);

                    b.Property<int?>("QuestionId")
                        .HasColumnType("int");

                    b.Property<int?>("TagId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("TagId");

                    b.ToTable("QuestionTags");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            QuestionId = 1,
                            TagId = 1
                        },
                        new
                        {
                            Id = 2,
                            QuestionId = 2,
                            TagId = 2
                        },
                        new
                        {
                            Id = 3,
                            QuestionId = 3,
                            TagId = 1
                        },
                        new
                        {
                            Id = 4,
                            QuestionId = 3,
                            TagId = 2
                        });
                });

            modelBuilder.Entity("Q_EF_DB.Entities.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tags");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Sports"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Politics"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Religion"
                        });
                });

            modelBuilder.Entity("Q_EF_DB.Entities.User", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Alejandro H. Carrillo"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Elmer O. Mcannon"
                        });
                });

            modelBuilder.Entity("Q_EF_DB.Entities.Answer", b =>
                {
                    b.HasOne("Q_EF_DB.Entities.Question", null)
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId");

                    b.HasOne("Q_EF_DB.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Q_EF_DB.Entities.Question", b =>
                {
                    b.HasOne("Q_EF_DB.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Q_EF_DB.Entities.QuestionTag", b =>
                {
                    b.HasOne("Q_EF_DB.Entities.Question", null)
                        .WithMany("QuestionTags")
                        .HasForeignKey("QuestionId");

                    b.HasOne("Q_EF_DB.Entities.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("Q_EF_DB.Entities.Question", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("QuestionTags");
                });
#pragma warning restore 612, 618
        }
    }
}
