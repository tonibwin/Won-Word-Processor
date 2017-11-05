/* Editor.cs
 * Pushed: 10/31/17
 * Description: Controls the Editor Button Functions for Won
 * TODO: Needs implementation for Save/Load, Text Styles(Bold, Italics, Underline), and Fonts(Styles and Size)
 * Implications: This controls the GUI button functionality and allows other features normally disabled from our GUI's controls to function
 * References: Uses reference from Visual Studio 2006 WPF Tutorial available at http://www.wpf-tutorial.com/rich-text-controls/how-to-creating-a-rich-text-editor/
 */

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
//These allow helper functions for the GUI
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Input;
//This allows the use of some OS operations used in Save/Load
using Microsoft.Win32;


namespace Won.Editor{

   public class Editor : Window{

      //Constructor Class
      //Fill this with the data values you need initialized but leave out any component initialization until we bind to the GUI or your program won't run
      public Editor(){
            InitializeComponent();
            //Uses SystemFontFamilies class to retrieve different styles
            cmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            //List of all possible sizes for font
            cmbFontSize.ItemsSource = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };

        }

        //Save/Load Features

        //Styles(Bold, Italics, Underline)

        //Fonts(Style and Size)

        /* Written By Destoni Baldwin
         * 11/3/17
         * Editor_SelectionChanged is to update the editor whenever the cursor moves or when 
         * A selection has been changed. It should immediately update the editor's view whenever
         * the cursor moves or a selection is changed
         * Pre-Conditions: Takes in an object aka a selection or cursor move that is found by 
         * an event. Parameters are event handlers.
         * Post-Conditions: Updates the FontFamily and Font size of selected text.
         */
        private void UpdateFontSizeAndFamily(object sender, RoutedEventArgs e)
        {
            //Font Family
            object temp = rtbEditor.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            cmbFontFamily.SelectedItem = temp;

            //Font Size
            temp = rtbEditor.Selection.GetPropertyValue(Inline.FontSizeProperty);
            cmbFontSize.Text = temp.ToString();
        }

        /* Written By Destoni Baldwin
         * Purpose of function is to be able to select a font family from the toolbar.
         * Pre-Conditions: Parameters are eventhandlers in which takes in a selected change 
         * of Font Family.
         * Post-Conditions: Updates Font Family selection in toolbar.
         */
        private void selectFontFamily(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFontFamily.SelectedItem != null)
                rtbEditor.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, cmbFontFamily.SelectedItem);
        }

        /* Written By Destoni Baldwin
         * Purpose of function is to be able to select a font size from the toolbar.
         * Pre-Conditions: Parameters are eventhandlers in which takes in a selected change 
         * of Font size.
         * Post-Conditions: Updates Font size selection in toolbar.
         */
        private void selectFontSize(object sender, TextChangedEventArgs e)
        {
            rtbEditor.Selection.ApplyPropertyValue(Inline.FontSizeProperty, cmbFontSize.Text);
        }
    }
}
