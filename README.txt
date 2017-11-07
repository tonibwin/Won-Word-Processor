README ( Last Updated  11/6/17  v1.6  Demo2 )
Current Deployment is currently only present in a Windows environment.
Loading the visual studio solution Won.sln located in */GroupWon/bin/Won.sln will load the file in Visual Studio
After loading press the start button at the top of the page or press f5 to start building.

Jordan Note: I am currently looking into making the program functional on a linux environment as a backup.
I have seen some solutions that may work using emulation software but I will update the readme accordingly when I find a solution

Won Word Processor
Current Features
-Basic Text Entry: Allows for user controlled formatted text input and editting

-Font Controls(Unstable): Currently allows for Bold, Italics, Underline, Font Size, and Font Family to be changed
--Bold, Italics, and Underline supported through UI using hotkeys as well
--Font Controls are context sensitive, editor will reflect the current styles based on the selected text
**Using a non-numeric edit in font size will cause a crash**

-Save/Load(Unstable): Currently allows for the saving and opening of rtf(Rich Text Format) files
**Currently a new filename for saving is required each time**
**Some testing has revealed a crash when a save/load command is used when text is selected on the screen**

Planned Featues
-Additional Font Controls
--Highlighting in multiple colors

-Printing

-Additonal Save/Load Features
--Export to PDF
--SaveAs
--Save will save to stored filename

Non Functional Planned Features
-Proper Resizing and Scrolling behavior
-Changeable views allowing a dark view