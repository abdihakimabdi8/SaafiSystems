using System;
using System.ComponentModel.DataAnnotations;

namespace SaafiSystems.Models.MonthlyRevenueViewModel
{
    public class MonthlyRevenue
    {
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        public int RevenueTotal { get; set; }
    }
}