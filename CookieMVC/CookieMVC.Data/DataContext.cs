using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace CookieMVC.Data
{
    public class DataContext : DbContext
    {
        // Your context has been configured to use a 'DataContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'CookieMVC.Data.DataContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DataContext' 
        // connection string in the application configuration file.
        public DataContext()
            : base("name=DataContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }


        public int SaveChanges(bool verboseLogging)
        {
            try
            {
                return SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, Environment.NewLine, "The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }
    }
}