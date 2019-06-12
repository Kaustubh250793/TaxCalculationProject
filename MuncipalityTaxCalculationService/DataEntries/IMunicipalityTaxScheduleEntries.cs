using System;
using System.Collections.Generic;
using MuncipalityTaxCalculationService.DataModels;

namespace MuncipalityTaxCalculationService.DataEntries
{
  /// <summary>
  /// Represents municipalities taxes schedules storage.
  /// </summary>
  public interface IMunicipalityTaxScheduleEntries
  {
    /// <summary>
    /// Gets all municipality taxes schedules where <paramref name="date"/> is between 
    /// <see cref="MunicipalityTaxSchedule.StartDate"/> and <see cref="MunicipalityTaxSchedule.EndDate"/> dates.
    /// </summary>
    /// <returns>Given municipality taxes schedules or nothing.</returns>
    IEnumerable<MunicipalityTaxSchedule> GetMunicipalityTaxesSchedules(string municipality, DateTime date);
  }
}
