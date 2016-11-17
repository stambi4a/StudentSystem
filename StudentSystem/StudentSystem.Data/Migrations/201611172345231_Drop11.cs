namespace StudentSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Drop11 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Licenses", "ResourceId", "dbo.Resources");
            DropIndex("dbo.Homeworks", new[] { "StudentId" });
            DropIndex("dbo.Homeworks", new[] { "CourseId" });
            DropIndex("dbo.Resources", new[] { "CourseId" });
            RenameColumn(table: "dbo.StudentCourses", name: "StudentId", newName: "Student_Id");
            RenameColumn(table: "dbo.StudentCourses", name: "CourseId", newName: "Course_Id");
            RenameIndex(table: "dbo.StudentCourses", name: "IX_StudentId", newName: "IX_Student_Id");
            RenameIndex(table: "dbo.StudentCourses", name: "IX_CourseId", newName: "IX_Course_Id");
            AlterColumn("dbo.Homeworks", "StudentId", c => c.Int(nullable: false));
            AlterColumn("dbo.Homeworks", "CourseId", c => c.Int(nullable: false));
            AlterColumn("dbo.Resources", "CourseId", c => c.Int(nullable: false));
            CreateIndex("dbo.Homeworks", "StudentId");
            CreateIndex("dbo.Homeworks", "CourseId");
            CreateIndex("dbo.Resources", "CourseId");
            AddForeignKey("dbo.Licenses", "ResourceId", "dbo.Resources", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Licenses", "ResourceId", "dbo.Resources");
            DropIndex("dbo.Resources", new[] { "CourseId" });
            DropIndex("dbo.Homeworks", new[] { "CourseId" });
            DropIndex("dbo.Homeworks", new[] { "StudentId" });
            AlterColumn("dbo.Resources", "CourseId", c => c.Int());
            AlterColumn("dbo.Homeworks", "CourseId", c => c.Int());
            AlterColumn("dbo.Homeworks", "StudentId", c => c.Int());
            RenameIndex(table: "dbo.StudentCourses", name: "IX_Course_Id", newName: "IX_CourseId");
            RenameIndex(table: "dbo.StudentCourses", name: "IX_Student_Id", newName: "IX_StudentId");
            RenameColumn(table: "dbo.StudentCourses", name: "Course_Id", newName: "CourseId");
            RenameColumn(table: "dbo.StudentCourses", name: "Student_Id", newName: "StudentId");
            CreateIndex("dbo.Resources", "CourseId");
            CreateIndex("dbo.Homeworks", "CourseId");
            CreateIndex("dbo.Homeworks", "StudentId");
            AddForeignKey("dbo.Licenses", "ResourceId", "dbo.Resources", "Id", cascadeDelete: true);
        }
    }
}
