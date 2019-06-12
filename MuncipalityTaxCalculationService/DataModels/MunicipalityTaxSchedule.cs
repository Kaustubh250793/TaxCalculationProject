using System;
using System.Collections.Generic;
using System.Text;

namespace MuncipalityTaxCalculationService.DataModels
{
  public class MunicipalityTaxSchedule
  {
    /// <summary>
    /// Gets or sets id of the municipality tax schedule.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets municipality name.
    /// </summary>
    public string Municipality { get; set; }

    /// <summary>
    /// Gets or sets municipality tax schedule type.
    /// </summary>
    public TaxScheduleType Type { get; set; }

    /// <summary>
    /// Gets or sets municipality tax.
    /// </summary>
    public decimal Tax { get; set; }

    /// <summary>
    /// Gets or sets date since when the tax is active.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets date since when the tax is no longer active.
    /// </summary>
    public DateTime EndDate { get; set; }
  }
}
