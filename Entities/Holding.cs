using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WealthSimple.Entities
{
    /// <summary>
    /// hold each investment information
    /// </summary>
    public class Holding
    {
        /// <summary>
        /// attributes of each investment
        /// </summary>
        public string Ticker { get; set; }
        public double TargetAlloc { get; set; }
        public double ActualAlloc { get; set; }
        public int ShareOwn { get; set; }
        public double SharePrice { get; set; }
        public double MarketValue { get; set; }
        public double TargetMarketValue { get; set; }
        public int TargetShare { get; set; }
        public string Instruction { get; set; }
        public int ShareDiff { get; set; }

        /// <summary>
        /// calculate the market value
        /// </summary>
        public void processMarketValue()
        {
            MarketValue = ShareOwn * SharePrice;
        }
    
        /// <summary>
        /// determine the instruction
        /// </summary>
        /// <param name="totalInvest">client total investment</param>
        /// <returns>instruction string</returns>
        public string processInstruction(double totalInvest)
        {
            TargetMarketValue = totalInvest * TargetAlloc;
            TargetShare = (int)Math.Floor(TargetMarketValue / SharePrice);
            ShareDiff = Math.Abs(TargetShare - ShareOwn);
            if (TargetShare == ShareOwn) //current investment already on target
                Instruction = "";
            else
            {
                Instruction = TargetShare > ShareOwn ? "Buy" : "Sell";
                Instruction = Instruction + " " + ShareDiff.ToString() + " shares of " + Ticker;
            }
            return Instruction;
        }
    }
}
