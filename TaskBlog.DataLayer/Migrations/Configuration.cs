namespace TaskBlog.DataLayer.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

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
              new Tag { Name = "Спорт" },
              new Tag { Name = "Политика" },
              new Tag { Name = "Культура" },
              new Tag { Name = "Шоу-бизнес" }
            );

            base.Seed(context);
        }
    }
}
