//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using SaafiSystems.Models;

//namespace SaafiSystems.ViewModels
//{
//    public class SearchLoadsViewModel
//    {
//        // TODO #7.1 - Extract members common to JobFieldsViewModel
//        // to BaseViewModel

//        // The search results
//        public List<Load> Loads { get; set; }

//        // The column to search, defaults to all
//      //  public LoadFieldType Column { get; set; } = LoadFieldType.All;

//        // The search value
//        [Display(Name = "Keyword:")]
//        public string Value { get; set; } = "";

//        // All columns, for display
//    //    public List<LoadFieldType> Columns { get; set; }

//        // View title
//        public string Title { get; set; } = "";

//        public SearchLoadsViewModel()
//        {
//            // Populate the list of all columns

//            Columns = new List<LoadFieldType>();

//            foreach (LoadFieldType enumVal in Enum.GetValues(typeof(LoadFieldType)))
//            {
//                Columns.Add(enumVal);
//            }


//        }
//    }
//}