﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WonApplication
{
    class Program
    {
        static void Main(string[] args)
        {

            //Test test: Written by Jordan Leibman
            Console.WriteLine("Starting Test");
            Text t1 = new Text('a');
            Console.WriteLine(t1.ToString());
            t1.setBold(true);
            t1.setItalics(true);
            t1.setUnderline(true);
            t1.setText('t');
            Console.WriteLine(t1.ToString());
            Text t2 = new Text('s', true, false, false);
            Console.WriteLine(t2.getText());
            Console.WriteLine(t2.getBold());
            Console.WriteLine(t2.getItalics());
            Console.WriteLine(t2.getUnderline());
            Console.WriteLine("Terminating Test");


            /*---------------------------------------*/

            //Test for page, presumably written by Destoni Baldwin
            Console.WriteLine("Starting Page Test ");

            //Test Example 1 for Page
            Console.WriteLine("Example 1 for Page \n");
            Page p1 = new Page();
            p1.addPage(1);
            p1.text.Add(t1);
            p1.text.Add(t2);
            Console.WriteLine($"    p1 page number: {p1.pos}");
            Console.Write("    p1 text: ");
            for (int i = 0; i < p1.text.Count; i++) {
                Console.Write(p1.text[i].getText());
            }
            Console.WriteLine("\n\nEnd of Page Example 1 \n");

            //Test Example 2 for Page
            Console.WriteLine("Example 2 for Page\n");
            Page p2 = new Page();
            p2.addPage(2);
            Text t3= new Text('a');
            string textentry = "Hello, this is Won Word Processor!";

            for (int i = 0; i < textentry.Length; i++) {
                char convertedText = textentry[i];
                t3.setText(convertedText);
                t3.setBold(true);
                t3.setItalics(false);
                t3.setUnderline(true);
                p2.text.Add(t3);
                Console.Write(p2.text[i].getText());
            }

            Console.WriteLine("\n     Page 2 Text Entry");
            Console.WriteLine($"     Page Number: {p2.pos}");
            Console.Write("     Fonts of Text Entry: ");
            for (int j = 0; j < p2.text.Count; j++) {
                if (j == 0){
                    Console.WriteLine($"Bold: {p2.text[j].getBold()}; Italics: {p2.text[j].getItalics()}; Underline: {p2.text[j].getUnderline()}");
                    Console.Write("     ");
                }
                Console.Write(p2.text[j].getText());
            }
            //Console.WriteLine("\n\nEnd of Page Example 2 \n");

            Console.ReadKey();
        }
    }
}