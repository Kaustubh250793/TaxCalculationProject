using System;
using System.Collections.Generic;
using System.Linq;
using MuncipalityTaxCalculationService.DataModels;

namespace MuncipalityTaxCalculationService.DataEntries
{
  public class MunicipalityTaxScheduleEntries : IMunicipalityTaxScheduleEntries
  {
    private readonly TaxSchedulesContext context;

    /// <summary>
    /// Initializes a new instance of <see cref="MunicipalityTaxScheduleEntries"/> class./>
    /// </summary>
    /// <param name="context">Entries context.</param>
    public MunicipalityTaxScheduleEntries(TaxSchedulesContext context)
    {
      this.context = context;
    }

    /// <summary>
    /// Gets all municipality taxes schedules where <paramref name="date"/> is between 
    /// <see cref="MunicipalityTaxSchedule.StartDate"/> and <see cref="MunicipalityTaxSchedule.EndDate"/> dates.
    /// </summary>
    /// <returns>Given municipality taxes schedules or nothing.</returns>
    public IEnumerable<MunicipalityTaxSchedule> GetMunicipalityTaxesSchedules(string municipality, DateTime date)
    {
      if (string.IsNullOrEmpty(municipality))
      {
        return Enumerable.Empty<MunicipalityTaxSchedule>();
      }

      var universalDate = date.ToUniversalTime().Date;

      return context.MunicipalitiesTaxesSchedules
        .Where(s =>
          s.Municipality.Equals(municipality)
          && s.StartDate.ToUniversalTime().Date <= universalDate
          && universalDate <= s.EndDate.ToUniversalTime().Date);
    }
  }
}
