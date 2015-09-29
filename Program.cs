using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using WealthSimple.Entities;
using WealthSimple.Process;

namespace WealthSimple
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1) //no input file path provided
            {
                Console.WriteLine("Please provide input file path. Usage: WealthSimple.exe FilePath");
            }
            else
            {
                if (!File.Exists(args[0]))
                    Console.WriteLine("Provided file not exist."); //input file not exist
                else
                {
                    double dTotalInvest=0;
                    List<Holding> clientHolding = Parser.inputParser(args[0], out dTotalInvest); //parse the input file

                    if (clientHolding.Count == 0)  //the whole input file is invalid if there is one error
                        Console.WriteLine("Error in input file. Please see log.");
                    else
                    {
                        string sInstr = "";
                        string sTemp;
                        foreach (Holding hold in clientHolding) //concatenate the output instruction string
                        {
                            sTemp = hold.processInstruction(dTotalInvest);
                            if (sTemp != "")
                                sInstr = sInstr + sTemp + ";";
                        }
                        Console.WriteLine(sInstr);
                    }
                }
            }
        }
    }
}
