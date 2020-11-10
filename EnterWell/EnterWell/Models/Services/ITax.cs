using System.ComponentModel.Composition;

namespace EnterWell.Models.Services
{
    public interface ITax
    {
        decimal CalculateTax(string selectedValue);
    }
}