# Won Word Processor
Won Word Processor is a text editor that allows basic text entry. Along with the ability to save and open RTF documents. It provides editing tools such as highlight, undo, redo, fonts, font colors, bold, italics, underline, cut, copy and paste. It allows printing and exporting to PDF. The development environment is Visual Studio and the programming language used was C#. 

Current Deployment is currently only possible in a Windows environment as some of the programs features are used to call the OS to do the feature. Implementation and compatibility on a non windows environment was not pursued or tested
Loading the visual studio solution Won.sln located in */GroupWon/bin/Won.sln will load the file in Visual Studio
After loading press the start button at the top of the page (or press f5 by default visual studio settings) to start building. Once the solution is built Visual Studio should start the program automatically

## Current Features
-Basic Text Entry: Allows for user controlled formatted text input and editting
--Editing commands are supported through hotkeys and buttons
--Allows for cut, copy, paste, undo, and redo

-Font Controls: Currently allows for Bold, Italics, Underline, Font Size, and Font Family to be changed
--Bold, Italics, and Underline supported through UI using hotkeys as well
--Font Controls are context sensitive, editor will reflect the current styles based on the selected text

-Save/Load(Unstable): Currently allows for the saving and opening of rtf(Rich Text Format) files
**Currently a new filename for saving is required each time**
**Some testing has revealed a crash when a save/load command is used when text is selected on the screen**

-Highlighting in multiple colors(Unstable): currently highlighting is only possible on text selection, also when moving to a different cursor position the highlighting selections are not updated to reflect the highlight states at the cursor position.

-Printing(Unstable): Printing through XPS class methods. Printing now prints a single column and keeps test mostly aligned. Requires fixes to print text as user sees it in the editor.

-Export to PDF(Unstable): Export to PDF still has some issues with formatting

Planned Featues
-Additional Font Controls
-Additonal Save/Load Features
--SaveAs
--Save will save to stored filename

Non Functional Planned Features
-Changeable views allowing a dark view
