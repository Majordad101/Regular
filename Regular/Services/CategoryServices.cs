﻿using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Regular.Models;

namespace Regular.Services
{
    public static class CategoryServices
    {
        public static List<Category> ConvertCategorySetToList(Categories categories)
        {
            return categories.Cast<Category>().ToList();
        }
        public static CategorySet ConvertListToCategorySet(List<Category> categories)
        {
            CategorySet categorySet = RegularApp.RevitApplication.Create.NewCategorySet();
            foreach (Category category in categories)
            {
                categorySet.Insert(category);
            }
            return categorySet;
        }

        public static BuiltInCategory GetBuiltInCategoryFromCategory(Category category)
        {
            return (BuiltInCategory)Enum.Parse(typeof(BuiltInCategory), category.Id.ToString());
        }

        public static ObservableCollection<CategoryObject> GetInitialCategories(string documentGuid)
        {
            // Relevant categories are assigned by the user using a Checkbox List
            // CategoryObjects tie together the cateogry name, its ID and its checked state as a Boolean
            // Here we return all possible category objects for the new RegexRule and set them all to unchecked

            ObservableCollection<CategoryObject> observableObjects = new ObservableCollection<CategoryObject>();
            Document document = DocumentGuidServices.GetRevitDocumentByGuid(documentGuid);

            // Fetching all categories to create ObservableObjects
            List<Category> userVisibleCategories = ConvertCategorySetToList(document.Settings.Categories)
                .Where(x => x.AllowsBoundParameters)
                .OrderBy(x => x.Name)
                .ToList();

            foreach (Category category in userVisibleCategories)
            {
                observableObjects.Add(new CategoryObject
                {
                    CategoryObjectName = category.Name,
                    CategoryObjectId = category.Id.IntegerValue,
                    IsChecked = false
                });
            }
            return observableObjects;
        }
    }
}
