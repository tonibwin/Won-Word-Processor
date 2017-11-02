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
         
      }

      //Save/Load Features

      //Styles(Bold, Italics, Underline)

      //Fonts(Style and Size)
   }
}
