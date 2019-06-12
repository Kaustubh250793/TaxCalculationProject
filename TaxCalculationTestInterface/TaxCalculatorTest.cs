using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MuncipalityTaxCalculationService.DataEntries;
using MuncipalityTaxCalculationService.DataModels;
using MuncipalityTaxCalculationService.TaxCalculator;

namespace TaxCalculationTestInterface
{
  [TestClass]
  public class TaxCalculatorTest
  {
    private Mock<IMunicipalityTaxScheduleEntries> municipalityTaxScheduleEntriesMock;

    [TestInitialize]
    public void TestSetUp()
    {
      municipalityTaxScheduleEntriesMock = new Mock<IMunicipalityTaxScheduleEntries>();
    }

    [TestMethod]
    public void Null_TaxScheduleEntries_ThrowException()
    {
      Assert.ThrowsException<ArgumentNullException>(() => new TaxCalculator(null));
    }

    [TestMethod]
    public void MunicipalityDoesNotExist_Returns_ZeroTaxes()
    {
      //Arrange
      municipalityTaxScheduleEntriesMock
        .Setup(s => s.GetMunicipalityTaxesSchedules(It.IsAny<string>(), It.IsAny<DateTime>()))
        .Returns(new List<MunicipalityTaxSchedule>());

      //Act
      var result = new TaxCalculator(municipalityTaxScheduleEntriesMock.Object)
        .CalculateTax("Unknown", DateTime.Now);

      //Assert
      Assert.AreEqual(0M, result);
    }

    [TestMethod]
    public void MunicipalityExists_ReturnTaxes()
    {
      //Arrange
      var municipality = "Vilnius";
      var date = new DateTime(2016, 03, 04);
      var tax = 0.2M;

      municipalityTaxScheduleEntriesMock
        .Setup(s => s.GetMunicipalityTaxesSchedules(municipality, date))
        .Returns(() => new[]
        {
          new MunicipalityTaxSchedule
          {
            Municipality = municipality,
            Type = TaxScheduleType.Yearly,
            Tax = tax,
            StartDate = new DateTime(2016, 01, 01),
            EndDate = new DateTime(2016, 12, 31)
          }
        });

      //Act
      var result = new TaxCalculator(municipalityTaxScheduleEntriesMock.Object)
        .CalculateTax(municipality, date);

      //Assert
      Assert.AreEqual(tax, result);
    }

    [TestMethod]
    public void YearlyAndMonthly_OverlappingSchedules_ReturnsMonthly()
    {
      //Arrange
      var municipality = "Vilnius";
      var date = new DateTime(2016, 03, 04);
      var monthlyTax = 0.4M;

      municipalityTaxScheduleEntriesMock
        .Setup(s => s.GetMunicipalityTaxesSchedules(municipality, date))
        .Returns(() => new[]
        {
          new MunicipalityTaxSchedule
          {
            Municipality = municipality,
            Type = TaxScheduleType.Yearly,
            Tax = 0.2M,
            StartDate = new DateTime(2016, 01, 01),
            EndDate = new DateTime(2016, 12, 31)
          },
          new MunicipalityTaxSchedule
          {
            Municipality = municipality,
            Type = TaxScheduleType.Monthly,
            Tax = monthlyTax,
            StartDate = new DateTime(2016, 05, 01),
            EndDate = new DateTime(2016, 05, 31)
          }
        });

      //Act
      var result = new TaxCalculator(municipalityTaxScheduleEntriesMock.Object)
        .CalculateTax(municipality, date);

      //Assert
      Assert.AreEqual(monthlyTax, result);
    }

    [TestMethod]
    public void MonthlyAndWeekly_OverlappingSchedules_ReturnsWeekly()
    {
      //Arrange
      var municipality = "Vilnius";
      var date = new DateTime(2016, 03, 04);
      var weeklyTax = 0.3M;

      municipalityTaxScheduleEntriesMock
        .Setup(s => s.GetMunicipalityTaxesSchedules(municipality, date))
        .Returns(() => new[]
        {
          new MunicipalityTaxSchedule
          {
            Municipality = municipality,
            Type = TaxScheduleType.Monthly,
            Tax = 0.4M,
            StartDate = new DateTime(2016, 03, 01),
            EndDate = new DateTime(2016, 03, 31)
          },
          new MunicipalityTaxSchedule
          {
            Municipality = municipality,
            Type = TaxScheduleType.Weekly,
            Tax = weeklyTax,
            StartDate = new DateTime(2016, 02, 29),
            EndDate = new DateTime(2016, 03, 06)
          }
        });

      //Act
      var result = new TaxCalculator(this.municipalityTaxScheduleEntriesMock.Object)
        .CalculateTax(municipality, date);

      //Assert
      Assert.AreEqual(weeklyTax, result);
    }

    [TestMethod]
    public void WeeklyAndDaily_OverlappingSchedules_ReturnsDaily()
    {
      //Arrange
      var municipality = "Vilnius";
      var date = new DateTime(2016, 03, 04);
      var dailyTax = 0.1M;

      municipalityTaxScheduleEntriesMock
        .Setup(s => s.GetMunicipalityTaxesSchedules(municipality, date))
        .Returns(() => new[]
        {
          new MunicipalityTaxSchedule
          {
            Municipality = municipality,
            Type = TaxScheduleType.Weekly,
            Tax = 0.3M,
            StartDate = new DateTime(2016, 02, 29),
            EndDate = new DateTime(2016, 03, 06)
          },
          new MunicipalityTaxSchedule
          {
            Municipality = municipality,
            Type = TaxScheduleType.Daily,
            Tax = dailyTax,
            StartDate = new DateTime(2016, 03, 04),
            EndDate = new DateTime(2016, 03, 04)
          }
        });

      //Act
      var result = new TaxCalculator(municipalityTaxScheduleEntriesMock.Object)
        .CalculateTax(municipality, date);

      //Assert
      Assert.AreEqual(dailyTax, result);
    }
  }
}
