using System;
using System.Linq;
using MuncipalityTaxCalculationService.DataEntries;

namespace MuncipalityTaxCalculationService.TaxCalculator
{
  public class TaxCalculator : ITaxCalculator
  {
    private readonly IMunicipalityTaxScheduleEntries taxEntries;

    public TaxCalculator(IMunicipalityTaxScheduleEntries taxEntries)
    {
      this.taxEntries = taxEntries ?? throw new ArgumentNullException();
    }

    public decimal CalculateTax(string municipality, DateTime date)
    {
      return taxEntries.GetMunicipalityTaxesSchedules(municipality, date).OrderByDescending(x => x.Type)
        .Select(x => x.Tax).FirstOrDefault();
    }
  }
}
