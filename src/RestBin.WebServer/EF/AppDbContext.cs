using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RestBin.Common.Exceptions;
using RestBin.Common.Models;
using RestBin.Common.Utils;

namespace RestBin.WebServer.EF
{
    /// <summary>
    ///     You can add profile data for the user by adding more properties to your ApplicationUser class, please visit
    ///     http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.You can add profile data for the user by adding more
    ///     properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    /// </summary>
    public class AppDbContext : DbContext
    {

        public AppDbContext()
        {
            Database.Log += delegate (string s)
            {
                Logging.Info(s);
            };
            Configuration.AutoDetectChangesEnabled = false;
        }

        public DbSet<HeaderModel> Headers { get; set; }

        public DbSet<TradeRecordModel> TradeRecords { get; set; }
         
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() => SaveChanges(), cancellationToken);
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
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
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                Logging.Error(exceptionMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new AppException(exceptionMessage);
            }
        }
    }
}