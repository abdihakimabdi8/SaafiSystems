using Microsoft.AspNetCore.Mvc;
using SaafiSystems.Models;
using System.Collections.Generic;
using SaafiSystems.ViewModels;
using SaafiSystems.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SaafiSystems.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly SaafiDbContext context;

        public ExpenseController(SaafiDbContext dbContext)
        {
            this.context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            IList<Expense> expenses = context.Expenses.Include(c => c.ExpenseCategory).ToList();

            return View(expenses);
        }

        public IActionResult Add()
        {
            AddExpenseViewModel addExpenseViewModel =
                new AddExpenseViewModel(context.ExpenseCategories.ToList());
            return View(addExpenseViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddExpenseViewModel addExpenseViewModel)
        {
            if (ModelState.IsValid)
            {
                ExpenseCategory newExpenseCategory =
                    context.ExpenseCategories.Single(c => c.ID == addExpenseViewModel.ExpenseCategoryID);
                // Add the new cheese to my existing cheeses
                Expense newExpense = new Expense
                {

                    Date = addExpenseViewModel.Date,
                    Reference = addExpenseViewModel.Reference,
                    Owner = addExpenseViewModel.Owner,
                    Name = addExpenseViewModel.Name,
                    Description = addExpenseViewModel.Description,
                    Amount = addExpenseViewModel.Amount,
                    ExpenseCategory = newExpenseCategory
                };

                context.Expenses.Add(newExpense);
                context.SaveChanges();

                return Redirect("/Expence");
            }

            return View(addExpenseViewModel);
        }

        public IActionResult Remove()
        {
            ViewBag.title = "Remove Expenses";
            ViewBag.expenses = context.Expenses.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Remove(int[] expenseIds)
        {
            foreach (int expenseId in expenseIds)
            {
                Revenue theRevenue = context.Revenues.Single(c => c.ID == expenseId);
                context.Revenues.Remove(theRevenue);
            }

            context.SaveChanges();

            return Redirect("/");
        }
        [HttpPost]
        public IActionResult Category(int id)
        {
            if (id == 0)
            {
                Redirect("/Category");
            }

            ExpenseCategory theCategory = context.ExpenseCategories
                .Include(cat => cat.Expenses)
                .Single(cat => cat.ID == id);

            /* 
             * IList<Cheese> theCheese = Context.Cheeses
             * .Include(c => c.Category)
             * .Single(c => c.CategoryID ==id)
             .ToList();*/

            ViewBag.Title = "Expenses in Category" + theCategory.Name;
            return View("Index", theCategory.Expenses);
        }

    }
}