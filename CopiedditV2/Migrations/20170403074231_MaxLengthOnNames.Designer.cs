using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CopiedditV2.Data;

namespace CopiedditV2.Migrations
{
    [DbContext(typeof(CopiedditV2Context))]
    [Migration("20170403074231_MaxLengthOnNames")]
    partial class MaxLengthOnNames
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CopiedditV2.Models.Comment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime>("DateCreated");

                    b.Property<int?>("ParentID");

                    b.Property<int>("PostID");

                    b.HasKey("ID");

                    b.HasIndex("PostID");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("CopiedditV2.Models.Post", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Title")
                        .HasMaxLength(50);

                    b.Property<string>("Url");

                    b.HasKey("ID");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("CopiedditV2.Models.Comment", b =>
                {
                    b.HasOne("CopiedditV2.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
