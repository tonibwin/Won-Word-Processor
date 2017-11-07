/* Editor.cs
 * Pushed: 10/31/17
 * Description: Controls the Editor Button Functions for Won
 * TODO: Needs implementation for Save/Load, Text Styles(Bold, Italics, Underline), and Fonts(Styles and Size)
 * Implications: This controls the GUI button functionality and allows other features normally disabled from our GUI's controls to function
 * References: Uses reference from Visual Studio 2006 WPF Tutorial available at http://www.wpf-tutorial.com/rich-text-controls/how-to-creating-a-rich-text-editor/
 * 
 * TODO: modules seem to integrate together for the most part, I noticed that they break if there is an active selection during save/load
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace Won
{

   /* Editor
    * Controls the state of the Won editor
    * 
    * Last Updated v1.6 11/6/17  Demo2
    * Core Features:
    * Save/Load allow the saving and loading of Rtf(Rich Text Format) files(Note, occasionally crashes if there is an active selection when saving loading)
    * Bold, Italics, and Underline connected to togglebuttons: Their state should be controled and maintained while the toggle is active
    * Font Size and Font Family combo boxes allow a selection of several font families and sizes. Font size is also editable in the editor and can be customized
    * 
    * Planned Functional Features: Printing, Highlighting, Export to PDF
    * Planned Quality Features: Scrolling, Document maintains text when window resizes
    */
   public partial class Editor : Window{


      /* Written By Jordan Leibman
       * 11/6/17
       * The editor constructor sets up the editor and specifies defaults for the font size and font family.
       * TODO: have default settings actually enforced in the document. Currently the default settings control the UI element, but do not actually modify the document
       */
      public Editor(){
         //initializes the XAML code, do not delete this
         InitializeComponent();
         //Uses SystemFontFamilies class to retrieve different styles
         cmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
         cmbFontFamily.SelectedItem = Fonts.SystemFontFamilies.FirstOrDefault(family => family.Source == "Times New Roman");
         //List of all possible sizes for font
         List<double> Size = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
         cmbFontSize.ItemsSource = Size;
         cmbFontSize.SelectedItem = Size.FirstOrDefault(size => size == 12);
      }


      /* Written By Jordan Leibman
       * 11/6/17
       * Open_Executed causes a open dialog box to appear and allows someone to specify a file to load
       * After the load is complete the document should load into the current window
       * TODO: Integration testing: Currently the loaded file causes an exception in some cases
       * seems to occur when text is selected right after loading
       */
      private void Open_Executed(object sender, ExecutedRoutedEventArgs e){
         OpenFileDialog dlg = new OpenFileDialog();
         dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
         if (dlg.ShowDialog()==true){
            FileStream filestream = new FileStream(dlg.FileName, FileMode.Open);
            TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
            range.Load(filestream, DataFormats.Rtf);
         }
      }

      /* Written By Jordan Leibman
       * 11/6/17
       * Save_Executed opens a dialog box to save the document. The current document is written to a rich text file
       * TODO: Integration testing
       */
      private void Save_Executed(object sender, ExecutedRoutedEventArgs e){
         SaveFileDialog dlg = new SaveFileDialog();
         dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
         if (dlg.ShowDialog() == true)
         {
            FileStream filestream = new FileStream(dlg.FileName, FileMode.Create);
            TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
            range.Save(filestream, DataFormats.Rtf);
         }
      }

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


      /* Written By Jordan Leibman
       * 11/6/17
       * rtbEditor_SelectionChanged
       * Modifies UI elements to update based on current selection
       */
      private void rtbEditor_SelectionChanged(object sender, RoutedEventArgs e)
      {
         //Property controls for Font Styles(Bold/Italics/Underline)
         object temp = rtbEditor.Selection.GetPropertyValue(Inline.FontWeightProperty);
         btnBold.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold));
         temp = rtbEditor.Selection.GetPropertyValue(Inline.FontStyleProperty);
         btnItalic.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic));
         temp = rtbEditor.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
         btnUnderline.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline));

         //Property controls for Font Size and Family
         temp = rtbEditor.Selection.GetPropertyValue(Inline.FontFamilyProperty);
         cmbFontFamily.SelectedItem = temp;
         temp = rtbEditor.Selection.GetPropertyValue(Inline.FontSizeProperty);
         cmbFontSize.Text = temp.ToString();
      }

      /* 
         * Written By: Michael Curtis
         * Date: 11/5/17
         * This function is used to take in a text object and update the GUI to reflect
         * if it is currently bolded.
         */

      private void btn_checkBold(object sender, RoutedEventArgs e)
      {
         object temp = rtbEditor.Selection.GetPropertyValue(Inline.FontWeightProperty);

         //Check if the current text is Bold.
         btnBold.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold));
         temp = rtbEditor.Selection.GetPropertyValue(Inline.FontStyleProperty);

         //Update the current state of the text.
         cmbFontFamily.SelectedItem = temp;
         temp = rtbEditor.Selection.GetPropertyValue(Inline.FontSizeProperty);
         cmbFontSize.Text = temp.ToString();

      }

      /* 
      * Written By: Michael Curtis
      * Date: 11/5/17
      * This function is used to take in a text object and update the GUI to reflect
      * if it is currently in Italics.
      */

      private void btn_checkItalics(object sender, RoutedEventArgs e)
      {
         object temp = rtbEditor.Selection.GetPropertyValue(Inline.FontWeightProperty);

         //Check if the current text is in Italics.
         btnItalic.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic));
         temp = rtbEditor.Selection.GetPropertyValue(Inline.TextDecorationsProperty);

         //Update the current state of the text.
         cmbFontFamily.SelectedItem = temp;
         temp = rtbEditor.Selection.GetPropertyValue(Inline.FontSizeProperty);
         cmbFontSize.Text = temp.ToString();
      }

      /* 
      * Written By: Michael Curtis
      * Date: 11/5/17
      * This function is used to take in a text object and update the GUI to reflect
      * if it is currently underlined.
      */

      private void btn_checkUnderline(object sender, RoutedEventArgs e)
      {
         object temp = rtbEditor.Selection.GetPropertyValue(Inline.FontWeightProperty);

         //Check if the current text is underlined.
         btnUnderline.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline));
         temp = rtbEditor.Selection.GetPropertyValue(Inline.FontFamilyProperty);

         //Update the current state of the text.
         cmbFontFamily.SelectedItem = temp;
         temp = rtbEditor.Selection.GetPropertyValue(Inline.FontSizeProperty);
         cmbFontSize.Text = temp.ToString();
      }
   }
}