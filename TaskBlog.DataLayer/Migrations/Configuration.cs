namespace TaskBlog.DataLayer.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<TaskBlog.DataLayer.BlogContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(TaskBlog.DataLayer.BlogContext context)
        {
            //  This method will be called after migrating to the latest version.

            context.Tags.AddOrUpdate(
              p => p.Name,
              new Tag { Name = "�����" },
              new Tag { Name = "��������" },
              new Tag { Name = "��������" },
              new Tag { Name = "���-������" }
            );

            base.Seed(context);
        }
    }
}
