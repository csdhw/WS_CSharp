using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthSimple.Entities;
using System.IO;

namespace WealthSimple.Process
{
    public class Parser
    {
        /// <summary>
        /// read the input file line by line 
        /// </summary>
        /// <param name="sPath">file path of the input file</param>
        /// <param name="dTotalInvest">output parameter return the client total investment amount</param>
        /// <returns>List of Holding object</returns>
        public static List<Holding> inputParser(string sPath, out double dTotalInvest)
        {
            List<Holding> ret = new List<Holding>();
            List<string> err = new List<string>();
            string[] data;
            int iIndex = -1;
            string line;
            StreamReader reader = new StreamReader(sPath);
            Holding clientHolding;
            dTotalInvest = 0;

            //read input file
            while (reader.Peek() != -1)
            {
                line = reader.ReadLine();
                iIndex += 1;
                if (iIndex > 0) //skip the first header line
                {
                    if (line.Trim() != "") //if not empty line
                    {
                        try
                        {
                            data = line.Split(',');
                            clientHolding = new Holding();
                            clientHolding.Ticker = data[0];
                            clientHolding.TargetAlloc = double.Parse(data[1].ToString()) / 100;
                            clientHolding.ActualAlloc = double.Parse(data[2].ToString()) / 100;
                            clientHolding.ShareOwn = int.Parse(data[3].ToString());
                            clientHolding.SharePrice = double.Parse(data[4].ToString());
                            clientHolding.processMarketValue();
                            dTotalInvest = dTotalInvest + (clientHolding.MarketValue);
                            ret.Add(clientHolding);
                        }
                        catch
                        {
                            err.Add("Line:" + iIndex + "-Invalid input format.");
                        }
                    }
                }
            }
            reader.Close();

            //empty file
            if (err.Count == 0 && ret.Count == 0) 
                err.Add("Empty file");

            //if there is one line of error the whole input file is invalid
            if (err.Count > 0) 
            {
                ret.Clear();

                //write error to log file
                using (StreamWriter file = new StreamWriter(@"ErrLog.txt", false))
                {
                    file.WriteLine("Process datetime: " + DateTime.Now);
                    foreach (string log in err)
                        file.WriteLine(log);
                }
            }

            return ret;
        }
    }
}
