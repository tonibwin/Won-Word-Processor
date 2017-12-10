/* Editor.cs
 * V1.7 11/28/2017 Demo3
 * Pushed: 11/28/17
 * Description: Controls the Editor Button Functions for Won
 * TODO: Debugging and testing on Print
 * Implications: This controls the GUI button functionality and allows other features normally disabled from our GUI's controls to function
 * References: Uses reference from Visual Studio 2006 WPF Tutorial available at http://www.wpf-tutorial.com/rich-text-controls/how-to-creating-a-rich-text-editor/
 * 
 * TODO:
 * Known Issues: Fonts at the start of the document don't retain their state for font size and font style
 *                   Temporary Fix: set the default text to the rtbEditor TextBox's default text style
 *               Print_Executed causes the program to halt
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Xps;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using System.Printing;
using Microsoft.VisualBasic;

namespace Won
{

   /* Editor
    * Controls the state of the Won editor
    * 
    * Last Updated v1.7 11/28/17  Demo3
    * Core Features:
    * Stable:
    * Saving and Loading of RTF documents
    * Cut, Copy, and Paste on Selected Text
    * Undo, and Redo to undo and redo entered text
    * Font Size, Font Family Selection and Status
    * Bold, Italics, and Underline Selection and Status
    * Formatted text entry
    * Unstable:
    * Printing of Rich Text Box contents within the Won Editor
    *    Printing currently causes the program to halt and has inconsistent formatting with the document in the Editor
    * 
    * Unimplemented:
    * Highlighting
    * Text-Color
    * 
    * Planned Functional Features: Highlighting, Export to PDF
    * Planned Quality Features:
    */
   public partial class Editor : Window
   {

      //Below declaration written by Zachary Lowery
      //This is just an instance of arrays that allow certain string functions to search for the alphabet and do operations accordingly.
      char[] alphaAnyCase = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
      char[] numberArray = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };


      /* Written By Jordan Leibman
       * 11/6/17
       * The editor constructor sets up the editor and specifies defaults for the font size and font family.
       * TODO: have default settings actually enforced in the document. Currently the default settings control the UI element, but do not actually modify the document
       */
      public Editor()
      {
         //initializes the XAML code, do not delete this
         InitializeComponent();
         //Uses SystemFontFamilies class to retrieve different styles
         cmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
         cmbFontFamily.SelectedItem = Fonts.SystemFontFamilies.FirstOrDefault(family => family.Source == "Times New Roman");
         
         cmbFontFamily.SelectedItem = Fonts.SystemFontFamilies.FirstOrDefault(family => family.Source == "Segoe UI");
         //List of all possible sizes for font
         List<double> Size = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };

         cmbFontSize.ItemsSource = Size;
         cmbFontSize.SelectedItem = Size.FirstOrDefault(size => size == 12);

         //List of all possible colors for text and highlighting
         List<String> textColors = new List<String>() { "Black", "Red", "Yellow", "Blue" };
         List<String> highlightColors = new List<String>() { "White","Black", "Red", "Yellow", "Blue" };

         cmbTextColor.ItemsSource = textColors;
         cmbTextColor.SelectedItem = textColors.FirstOrDefault(color => color == "Black");

         cmbHighlight.ItemsSource = highlightColors;
         cmbHighlight.SelectedItem = highlightColors.FirstOrDefault(color => color == "White");
        }


      /* Written By Jordan Leibman
       * 11/6/17
       * Open_Executed causes a open dialog box to appear and allows someone to specify a file to load
       * After the load is complete the document should load into the current window
       * TODO: Integration testing: Currently the loaded file causes an exception in some cases
       * seems to occur when text is selected right after loading
       */
      private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
      {
         OpenFileDialog dlg = new OpenFileDialog();
         dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
         if (dlg.ShowDialog() == true)
         {
            FileStream filestream = new FileStream(dlg.FileName, FileMode.Open);
            TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
            range.Load(filestream, DataFormats.Rtf);
         }
      }

      /* Written By Jordan Leibman
       * 11/6/17
       * Save_Executed opens a dialog box to save the document. The current document is written to a rich text file(*.rtf)
       * TODO: Integration testing
       */
      private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
      {
         SaveFileDialog dlg = new SaveFileDialog();
         dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
         if (dlg.ShowDialog() == true)
         {
            FileStream filestream = new FileStream(dlg.FileName, FileMode.Create);
            TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
            range.Save(filestream, DataFormats.Rtf);
         }
      }

      /* Written By Jordan Leibman
       * 11/25/17
       * Print_Executed opens a dialog box to print the document
       * TODO: debugging on output document, print causes program to halt
       */
      private void Print_Executed(object sender, ExecutedRoutedEventArgs e)
      {
         PrintDialog dlg = new PrintDialog();
         if (dlg.ShowDialog() == true)
         {
            dlg.PrintDocument((((IDocumentPaginatorSource)rtbEditor.Document).DocumentPaginator), "WonPrint");
         }
      }

      /* Written By Jordan Leibman
       * 12/5/17
       * PrintRef_Executed is a refined Printing function that prevents the Editor from halting.
       * It functions by using the XPS class features and copying the printed document into a new document that is then printed
       * Reference for pseudocode was pulled from the MSDN forums.
       * TODO: Full Testing and integration with other features.
       */
      private void PrintRef_Executed(object sender, ExecutedRoutedEventArgs e)
      {
         //makes a new text range copy
         TextRange source = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
         MemoryStream stream = new MemoryStream();
         source.Save(stream, DataFormats.Xaml);

         //saves the copy into new FlowDocument
         FlowDocument print = new FlowDocument();
         TextRange copy = new TextRange(print.ContentStart, print.ContentEnd);
         copy.Load(stream, DataFormats.Xaml);

         //Makes a new PrintDocument based on the selected printing size
         PrintDocumentImageableArea area = null;
         XpsDocumentWriter writer = PrintQueue.CreateXpsDocumentWriter(ref area);
         if(writer != null && area != null)
         {
            DocumentPaginator paginator = ((IDocumentPaginatorSource)print).DocumentPaginator;

            paginator.PageSize = new Size(area.MediaSizeWidth, area.MediaSizeHeight);
            Thickness padding = print.PagePadding;

            //sets margins within the printed document
            print.PagePadding = new Thickness(
               Math.Max(area.OriginWidth, padding.Left),
               Math.Max(area.OriginHeight, padding.Top),
               Math.Max(area.MediaSizeWidth - (area.OriginWidth + area.ExtentWidth), padding.Right),
               Math.Max(area.MediaSizeWidth - (area.OriginHeight + area.ExtentHeight), padding.Bottom)
            );

            //removes columns from the document
            print.ColumnWidth = double.PositiveInfinity;

            //XPS write
            writer.Write(paginator);
         }
      }
      
      /* Written by Jordan Leibman
       * 11/25/17
       * Cut_Executed allows the use of the cut button to cut text
       * Cut button will only function if the selected text is not empty
       * TODO: integration testing
       */
      private void Cut_Executed(object sender, ExecutedRoutedEventArgs e)
      {
         if (rtbEditor.Selection.Text != "")
            rtbEditor.Cut();
      }

      /* Written by Jordan Leibman
       * 11/25/17
       * Copy_Executed allows the use of the copy button to copy text
       * Copy button will only function if the selected text is not empty
       * TODO: integration testing
       */
      private void Copy_Executed(object sender, ExecutedRoutedEventArgs e)
      {
         if (rtbEditor.Selection.Text != "")
            rtbEditor.Copy();
      }

      /* Written by Jordan Leibman
       * 11/25/17
       * Paste_Executed allows the use of the paste button to paste text
       * TODO: integration testing
       */
      private void Paste_Executed(object sender, ExecutedRoutedEventArgs e)
      {
         if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text) == true)
            rtbEditor.Paste();
      }

      /* Written by Jordan Leibman
       * 11/25/17
       * Undo_Executed allows the use of the undo button to undo the previously entered text
       * TODO: integration testing
       */
      private void Undo_Executed(object sender, ExecutedRoutedEventArgs e)
      {
         if (rtbEditor.CanUndo == true)
         {
            rtbEditor.Undo();
         }
      }

      /* Written by Jordan Leibman
       * 11/25/17
       * Undo_Executed allows the use of the redo button to redo entried previously removed by undo
       * TODO: integration testing
       */
      private void Redo_Executed(object sender, ExecutedRoutedEventArgs e)
      {
         if (rtbEditor.CanRedo == true)
         {
            rtbEditor.Redo();
         }
      }

      /* Written by Jordan Leibman and Michael Curtis
       * 12/5/17
       * Find_Executed is supposed to find and highlight all instances of the found string. Allows user input thorugh popup window.
       * TODO: implementation - original find function used alternate library System.Windows.Forms which was causing a compatibilty error.
       *                        Commented-out since unable to complete integration
       */
      /*
      private void Find_Executed(object sender, ExecutedRoutedEventArgs e)
      {
         rtbEditor.CaretPosition = rtbEditor.Document.ContentStart;
         string search = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd).Text;
         int index = 0;
         string findVal = Interaction.InputBox("Text to find:", "Find", "");
         while(index != search.Length - 1)
         {
            index = search.IndexOf(findVal);
            rtbEditor.CaretPosition = rtbEditor.Document.ContentStart.GetPositionAtOffset(index);

         }
         
      }
      */

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
       * Updated: 11/28/17 by Zachary Lowery-Exception Handling
       * Purpose of function is to be able to select a font size from the toolbar.
       * Pre-Conditions: Parameters are eventhandlers in which takes in a selected change 
       * of Font size.
       * Post-Conditions: Updates Font size selection in toolbar.
       */
      private void selectFontSize(object sender, TextChangedEventArgs e)
      {

         int index = 0;
         String newString = "";

         //Error-Exception handling cases written by Zachary Lowery

         //This loop checks for the presence of numerical characters in the font-size selection box. If there are any, it includes them.
         //The string is changed to exclude all non-numerical characters that were originally in the string.
         while ((index = cmbFontSize.Text.IndexOfAny(numberArray, index)) != -1)
         {

            newString += cmbFontSize.Text[index];

            if (newString[0] == '0')
            {
               newString = newString.Remove(0, 1);
            }
            index++;
         }
         cmbFontSize.Text = newString;

         //The numerical value selected is too high, truncate the rest of the string.
         if (cmbFontSize.Text.Length > 3)
         {
            cmbFontSize.Text = cmbFontSize.Text.Remove(3, cmbFontSize.Text.Length - 3);
         }


         //Makes sure there is a string left. If there isn't, returns without making changes.
         if (String.Compare(cmbFontSize.Text, "") == 0) return;

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

        /* Written By Troy McMillan
        * The purpose of this function is to change the color of the selected text to a color the user wishes
        * Pre-Conditions: Parameters are eventhandlers in which the user selects from a list of text colors
        * Post-Conditions: The selected text in the RTF is changed to the appropriate color
        */
        private void changeTextColor (object sender, SelectionChangedEventArgs e)
        {
            //a new brush is created based off the textcolor selection in the UI. it's converted to a string, then
            //typecasted into a color for a new brush
            Brush painter = new SolidColorBrush((Color)ColorConverter.ConvertFromString(cmbTextColor.SelectedItem.ToString()));

            //the new brush is then applied to the forground property of the selected text
            rtbEditor.Selection.ApplyPropertyValue(ForegroundProperty, painter);
         
        }

        /* Written By Troy McMillan
        * The purpose of this function is to highlight text in a user selected color
        * Pre-Conditions: Parameters are eventhandlers in which the user selects from a list of  
        * highlight colors
        * Post-Conditions: The selected text in the RTF is highlighted in the appropriate color
        */
        private void highlightText(object sender, SelectionChangedEventArgs e)
        {
            //a new brush is created based off the highlightColor selection in the UI. it's converted to a string, then
            //typecasted into a color for a new brush
            Brush painter = new SolidColorBrush((Color)ColorConverter.ConvertFromString(cmbHighlight.SelectedItem.ToString()));

            //the new brush is then applied to the background property of the selected text
            rtbEditor.Selection.ApplyPropertyValue(TextElement.BackgroundProperty, painter);

        }

        /* Written By Destoni Baldwin
         * 11/3/17
         * Purpose of function exportPDF is to save a file as a pdf.
         * Pre-Conditions: Function gets called when an event is triggered. 
         * Post-Conditions: Saves text in a pdf file. 
         */
        private void exportPDF(object sender, RoutedEventArgs e)
        {
            try
            {
                //open dialog box and set the the file type to pdf
                SaveFileDialog exportPDFFile = new SaveFileDialog();
                exportPDFFile.Filter = "PDF Files|*.pdf";
                if (exportPDFFile.ShowDialog() == true)
                {
                    //Create pdf document and add text and formatting.
                    using (FileStream stream = new FileStream(exportPDFFile.FileName, FileMode.Create))
                    {
                        Document document = new Document(PageSize.A1, 10f, 10f, 10f, 0f);
                        PdfWriter.GetInstance(document, stream);
                        document.Open();
                        TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                        document.Add(new iTextSharp.text.Paragraph(range.Text));
                        document.Close();
                        stream.Close();
                    }
                }
            }
            catch (DocumentException de)
            {
                Console.Error.WriteLine(de.Message);
            }
        }
    }
}