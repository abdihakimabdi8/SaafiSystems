using System;
using System.Collections.Generic;
using System.Linq;
using SaafiSystems.Models;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SaafiSystems.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList;
using PagedList.Core;
using SaafiSystems.Models.MonthlyRevenueViewModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SaafiSystems.Controllers
{
    public class ReportsController : Controller
    {
        private readonly SaafiDbContext context;
        public ReportsController(SaafiDbContext dbcontext)
        {
            context = dbcontext;
        }

        // GET: Reports
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            ViewData["CurrentSort"] = sortOrder;
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";



            ViewData["OwnerSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Owner_desc" : "";
            ViewData["categorytortParm"] = String.IsNullOrEmpty(sortOrder) ? "category_desc" : "";

            ViewData["CurrentFilter"] = searchString;





            ViewBag.CurrentFilter = searchString;

            var loads = from l in context.Loads
                        select l;


            if (searchString != null)
            {

                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                loads = loads.Where(l => l.Date.ToString().Contains(searchString) || l.Reference.Contains(searchString) || l.Owner.ToUpper().Contains(searchString.ToUpper()) || l.Description.ToUpper().Contains(searchString.ToUpper()) || l.LoadCategory.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Owner_desc":
                    loads = loads.OrderByDescending(l => l.Owner);
                    break;
                case "Date":
                    loads = loads.OrderBy(l => l.Date);
                    break;
                case "date_desc":
                    loads = loads.OrderByDescending(l => l.Date);
                    break;
                default:
                    loads = loads.OrderBy(l => l.LoadCategory);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<Load>.CreateAsync(loads.AsNoTracking(), page ?? 1, pageSize));
        }

        public IActionResult Search(string searchTerm)
        {
            var loads = context.Loads.Where(c => c.Description.Contains(searchTerm)).ToList();
            return View(loads);

        }

        public async Task<ActionResult> MonthlyRevenue()
        {
            IQueryable<MonthlyRevenue> data =
                from load in context.Loads
                group load by load.Date into monthlyRevenueGroup
                select new MonthlyRevenue()
                {
                    Date = monthlyRevenueGroup.Key,
                    RevenueTotal = monthlyRevenueGroup.Count()
                };
            return View(await data.AsNoTracking().ToListAsync());
        }



    }
}








//public ActionResult DailySales()
//{

//    var list = context.Loads.Where(x => DbFunctions.DiffDays(x.Date, DateTime.Now) == 0).ToList();
//    return View(list);
//}



//public ActionResult DailySalesFor(DateTime getDate)
//{
//    var list = db.Sales.Where(x => DbFunctions.DiffDays(x.Date, getDate) == 0).ToList();
//    return PartialView("_DailySalesPartialView", list);
//}

////    //Staffs
////    public ActionResult EmployeesReports()
//    {
//        return null;
//    }
//    /*
//        //Monthly Sales
//        public ActionResult MonthlySalesByDate()
//        {

//            int year = 2014;
//            int month = 12;
//            var query = db.Sales.Where(x => x.Date.Year == year && x.Date.Month == month).GroupBy(x => x.Date).Select(g => new DayTotalVM
//            {
//                Day = g.Key.Day,
//                Total = g.Sum(x => x.GrandTotal)
//            });
//            int daysInMonth = DateTime.DaysInMonth(year, month);
//            List<DayTotalVM> days = new List<DayTotalVM>();
//            for (int i = 1; i < daysInMonth + 1; i++ )
//            {
//                DayTotalVM item = new DayTotalVM() { Day = i};
//                DayTotalVM ex = query.Where(x => x.Day == i).FirstOrDefault();
//                if(ex != null)
//                {
//                    item.Total = ex.Total;
//                }
//                days.Add(item);
//            }
//            SalesVM model = new SalesVM() 
//            { 
//                Date = new DateTime(year, month,1),
//                Days = days
//            };
//            return View(model);
//        }
//*/

//    /// <summary>
//    /// Display monthly Sales for each day in a month
//    /// </summary>
//    /// <returns></returns>
//    public ActionResult MonthlySalesByDate()
//    {
//        int year = 2014;
//        int month = 12;
//        int daysInMonth = DateTime.DaysInMonth(year, month);
//        var days = Enumerable.Range(1, daysInMonth);
//        var query = db.Sales.Where(x => x.Date.Year == year && x.Date.Month == month).OrderBy(x => x.Date).Select(g => new
//        {
//            Day = g.Date.Day,
//            Total = g.GrandTotal
//        });
//        var model = new SalesVM
//        {
//            Date = new DateTime(year, month, 1),
//            Days = days.GroupJoin(query, d => d, q => q.Day, (d, q) => new DayTotalVM
//            {
//                Day = d,
//                Total = q.Sum(x => x.Total)
//            }).ToList()
//        };
//        return View(model);
//    }



//    //Returns Monthly sales based on a parameter
//    [HttpPost]
//    public ActionResult MonthlySalesByDate(string _year, string _month)
//    {
//        //assign incoming values to the variables
//        int year = 0, month = 0;
//        //check if year is null
//        if (string.IsNullOrWhiteSpace(_year) && _month != null)
//        {
//            year = DateTime.Now.Date.Year;
//            month = Convert.ToInt32(_month.Trim());
//        }
//        else
//        {
//            year = Convert.ToInt32(_year.Trim());
//            month = Convert.ToInt32(_month.Trim());
//        }
//        //calculate ttal number of days in a particular month for a that year 
//        int daysInMonth = DateTime.DaysInMonth(year, month);
//        var days = Enumerable.Range(1, daysInMonth);
//        var query = db.Sales.Where(x => x.Date.Year == year && x.Date.Month == month).OrderBy(x => x.Date.Day).Select(g => new
//        {
//            Day = g.Date.Day,
//            Total = g.GrandTotal
//        });
//        var model = new SalesVM
//        {
//            Date = new DateTime(year, month, 1),
//            Days = days.GroupJoin(query, d => d, q => q.Day, (d, q) => new DayTotalVM
//            {
//                Day = d,
//                Total = q.Sum(x => x.Total)
//            }).ToList()
//        };
//        return View(model);
//    }




//    public List<DayTotalVM> MonthlySalesByDate_forCharts(int yr, int mnt)
//    {
//        int year = yr;
//        int month = mnt;
//        int daysInMonth = DateTime.DaysInMonth(year, month);
//        var days = Enumerable.Range(1, daysInMonth);
//        var query = db.Sales.Where(x => x.Date.Year == year && x.Date.Month == month).Select(g => new
//        {
//            Day = g.Date.Day,
//            Total = g.GrandTotal
//        });
//        SalesVM model = new SalesVM
//        {
//            Date = new DateTime(year, month, 1),
//            Days = days.GroupJoin(query, d => d, q => q.Day, (d, q) => new DayTotalVM
//            {
//                Day = d,
//                Total = q.Sum(x => x.Total)
//            }).ToList()
//        };
//        return model.Days.ToList();
//    }
//    //********************************************************************
//    /// <summary>
//    /// Yearly Sales
//    /// </summary>
//    /// <param name="year"></param>
//    /// <returns></returns>
//    public ActionResult YearlySalesByMonth(string year)
//    {
//        int _year = 0;
//        int _toYear = 0;
//        if (string.IsNullOrWhiteSpace(year))
//        {
//            _year = DateTime.Now.Year;
//            _toYear = _year + 1;
//        }
//        else
//        {
//            _year = Convert.ToInt32(year);
//            _toYear = _year + 1;
//        }

//        //    int _year = DateTime.Now.Year;
//        //    int _toYear = _year + 1;
//        var query = db.Sales.Where(s => (s.Date.Year >= _year && s.Date.Year < _toYear));
//        // YearlyReportVM _Model = new YearlyReportVM();
//        List<MonthTotalVM> _Model = new List<MonthTotalVM>();

//        for (int i = 1; i < 13; i++)
//        {
//            decimal _grandTotal = 0;
//            decimal temp = 0;
//            temp = query.Where(x => x.Date.Month == i).Sum(x => (decimal?)(x.GrandTotal)) ?? 0;

//            _grandTotal = temp;

//            MonthTotalVM model = new MonthTotalVM()
//            {
//                Year = _year,
//                Month = i,
//                GrandTotal = _grandTotal
//            };
//            _Model.Add(model);
//        }

//        return View(_Model.ToList());
//    }



//    public List<MonthTotalVM> YearlySalesByMonth_forCharts(string year)
//    {
//        int _year = 0;
//        int _toYear = 0;
//        if (string.IsNullOrWhiteSpace(year))
//        {
//            _year = DateTime.Now.Year;
//            _toYear = _year + 1;
//        }
//        else
//        {
//            _year = Convert.ToInt32(year);
//            _toYear = _year + 1;
//        }

//        var query = db.Sales.Where(s => (s.Date.Year >= _year && s.Date.Year < _toYear));
//        List<MonthTotalVM> _Model = new List<MonthTotalVM>();

//        for (int i = 1; i < 13; i++)
//        {
//            decimal _grandTotal = 0;
//            decimal temp = 0;
//            temp = query.Where(x => x.Date.Month == i).Sum(x => (decimal?)(x.GrandTotal)) ?? 0;

//            _grandTotal = temp;

//            MonthTotalVM model = new MonthTotalVM()
//            {
//                Year = _year,
//                Month = i,
//                GrandTotal = _grandTotal
//            };
//            _Model.Add(model);
//        }
//        return _Model.ToList();
//    }

//    //*********************************************************************
//    /// <summary>
//    /// Purchase Report
//    /// </summary>
//    /// <returns></returns>
//    public ActionResult Purchase()
//    {

//        return View(db.Purchases.ToList());
//    }


//    [HttpPost]
//    public ActionResult Purchase(PurchaseSearchVM vm)
//    {
//        var filter = new PurchaseFilterRepository();
//        var model = filter.FilterPurchase(vm);
//        return View(model);
//    }

//    //*********************************************************************       
//    public ActionResult ProfitAndLoss()
//    {
//        return View();
//    }

//    }
//}
