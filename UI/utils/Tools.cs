using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace UI.utils
{
    public class Tools
    {
        public static void DatePickerOptions(object sender, RoutedEventArgs e)
        {
            DatePicker datepicker = (DatePicker)sender;
            DateTime legalAge = DateTime.Today.AddYears(-18);
            datepicker.SelectedDate = legalAge;
            Popup popup = (Popup)datepicker.Template.FindName("PART_Popup", datepicker);
            Calendar cal = (Calendar)popup.Child;
            cal.DisplayMode = CalendarMode.Decade;
        }
        public static void Sort(GridViewColumnHeader columnHeader, ListSortDirection listSortDirection, ItemsControl control)
        {
            if (control.ItemsSource != null) {
                string binding = (columnHeader.Column.DisplayMemberBinding as Binding)?.Path.Path;
                binding = binding ?? columnHeader.Column.Header as string;
                ICollectionView defaultView = CollectionViewSource.GetDefaultView(control.ItemsSource);
                defaultView.SortDescriptions.Clear();
                SortDescription sortDescription = new SortDescription(binding, listSortDirection);
                defaultView.SortDescriptions.Add(sortDescription);
                defaultView.Refresh();
            }
        }
        public static void InputValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}