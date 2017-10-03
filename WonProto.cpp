// WonProto (Won Prototype)
// Written by Jordan Leibman
// Version 0.1 completed on 10/2/17
// Purpose: a  bare bones word processort prototype
// Features: Can Load a file, add new user input to file, and save a file
#include <stdlib.h>
#include <iostream>
#include <string>
#include <fstream>
#include <vector>

using namespace std;

int main(void){
    string document = ""; // the stored string for the document
    string readIn = ""; // used to readline on input
    char action; // used to determine user action queries
    
    
    cout << "Would you like to load a file?:(y/n): ";
    while(true){
        cin >> action;
        if(action == 'y' || action == 'Y' || action == 'n' || action == 'N') break;
        cout<<"\nEnter a proper character(y/n): ";
    }
    
    // Load
    // Reads a text file
    // Modifies the document string to match the inputted file text
    // If load fails will prompt user to try again
    if(action == 'y' || action == 'Y'){
       while(action != 'n' || action != 'N'){
            cout<< "\nFilename to load: ";
            cin>>readIn;
            ifstream inFile (readIn.c_str());
            if(inFile.is_open()){
                while(getline(inFile, readIn)){
                document  = document + readIn + '\n';
                }
                inFile.close();
                action = 'n';
            }else{
                cout<<"\nFile could not be opened";
                cout<<"\nWould you like to try again?(y/n): ";
                cin >> action;
                if(action == 'n' || action == 'N')
                    cout<<"\nMaking new Document";
            }
            if(action == 'n' || action == 'N') break;
        }
    }else{
        cout<<"\nMaking new Document";
    }
    
    // Text Entry
    // Takes user text and adds it to the document string
    // After user type EOI on its own line input will stop and prompt user to save and quit, or quit without saving
    cout<<"\nType freely, Type EOI on its own line to stop input\n";
    while(true){
        cin>>readIn;
        if(readIn == "EOI") break;
        document = document + readIn;
    }
    cout<<"Document is as follows\n";
    cout<<"\n" << document << "\n";
    cout<<"\nPress S to save and quit, Press Q to quit without saving: ";
    cin>>action;
        
    //Save
    //Saves document string to a file
    //User specifies the file name
    if(action == 'S' || action == 's'){
        cout<<"\nFilename to save: ";
        cin>>readIn;
        ofstream outFile (readIn.c_str());
        if(outFile.is_open()){
            outFile<< document;
            outFile.close();
        }else{
            cout<<"\nfailed to save";
        }
    }
    //Quit
    //Quits program
    if(action == 'q'|| action == 'Q') exit(1);
    
}

