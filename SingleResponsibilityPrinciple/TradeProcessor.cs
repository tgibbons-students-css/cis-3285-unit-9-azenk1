
using SingleResponsibilityPrinciple.Contracts;
using System.Collections.Generic;

namespace SingleResponsibilityPrinciple
{
    public class TradeProcessor
    {
        public TradeProcessor(URLTradeDataProvider urlProvider, ITradeParser tradeParser, ITradeStorage tradeStorage)
        {
            this.urlProvider = urlProvider;
            this.tradeParser = tradeParser;
            this.tradeStorage = tradeStorage;
        }

        public void ProcessTrades()
        {
            List<string> lines = URLTradeDataProvider.readFromUrl();
            var trades = tradeParser.Parse(lines);
            tradeStorage.Persist(trades);
        }

        // private readonly ITradeDataProvider tradeDataProvider;
        private readonly URLTradeDataProvider urlProvider;
        private readonly ITradeParser tradeParser;
        private readonly ITradeStorage tradeStorage;
    }
}
