using Microsoft.EntityFrameworkCore;
using MuncipalityTaxCalculationService.DataModels;

namespace MuncipalityTaxCalculationService.DataEntries
{
  public class TaxSchedulesContext : DbContext
  {
    /// <summary>
    /// Initializes a new instance of <see cref="TaxSchedulesContext"/> class./>
    /// </summary>
    /// <param name="options">Context options.</param>
    public TaxSchedulesContext(DbContextOptions options) : base(options)
    {
    }

    /// <summary>
    /// Get or sets Entity Framework for municipalities taxes schedules.
    /// </summary>
    public DbSet<MunicipalityTaxSchedule> MunicipalitiesTaxesSchedules { get; set; }
  }
}
