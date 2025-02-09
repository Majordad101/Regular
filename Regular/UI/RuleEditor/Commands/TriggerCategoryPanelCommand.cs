﻿using System;
using System.Windows;
using System.Windows.Input;
using Regular.UI.RuleEditor.ViewModel;

namespace Regular.UI.RuleEditor.Commands
{
    public class TriggerCategoryPanelCommand : ICommand
    {
        private readonly RuleEditorViewModel ruleEditorViewModel;

        public TriggerCategoryPanelCommand(RuleEditorViewModel ruleEditorViewModel)
        {
            this.ruleEditorViewModel = ruleEditorViewModel;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            const int minWindowWidth = 436;
            const int maxWindowWidth = 701;
            const int columnCategoriesPanelWidth = 250;
            const int columnMarginWidth = 15;
            
            // We trigger the Boolean state 
            ruleEditorViewModel.CategoriesPanelExpanded = !ruleEditorViewModel.CategoriesPanelExpanded;

            if (ruleEditorViewModel.CategoriesPanelExpanded)
            {
                // If we expanded the panel
                ruleEditorViewModel.CategoriesPanelButtonText = "Hide Categories";
                ruleEditorViewModel.WindowMinWidth = maxWindowWidth;
                ruleEditorViewModel.WindowMaxWidth = maxWindowWidth;
                ruleEditorViewModel.ColumnCategoriesPanelWidth = new GridLength(columnCategoriesPanelWidth);
                ruleEditorViewModel.ColumnMarginWidth = new GridLength(columnMarginWidth);
                ruleEditorViewModel.ButtonsColumnNumber = 3;
                ruleEditorViewModel.DockPanelColumnSpan = 4;
                ruleEditorViewModel.TextBlockExampleMaxWidth = 350;
            }
            else
            {
                // If we collapsed the panel
                ruleEditorViewModel.CategoriesPanelButtonText = "Show Categories";
                ruleEditorViewModel.WindowMinWidth = minWindowWidth;
                ruleEditorViewModel.WindowMaxWidth = minWindowWidth;
                ruleEditorViewModel.ColumnCategoriesPanelWidth = new GridLength(0);
                ruleEditorViewModel.ColumnMarginWidth = new GridLength(0);
                ruleEditorViewModel.ButtonsColumnNumber = 1;
                ruleEditorViewModel.DockPanelColumnSpan = 2;
                ruleEditorViewModel.TextBlockExampleMaxWidth = 170;
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
