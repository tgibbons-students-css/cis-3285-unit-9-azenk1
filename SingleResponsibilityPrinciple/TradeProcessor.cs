
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


        //Request 409
        private void StoreTrades(IEnumerable<TradeRecord> trades)
        {
      
            // using (var connection = new System.Data.SqlClient.SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\tradedatabase.mdf;Integrated Security=True;Connect Timeout=30;"))
            string connectSqlServer = "Data Source = athena.css.edu; Initial Catalog = CIS3285; Persist Security Info = True; User ID = tgibbons; Password = Data Source = athena.css.edu; Initial Catalog = CIS3285; Persist Security Info = True; User ID = tgibbons; Password = Saints4CSS";
            using (var connection = new System.Data.SqlClient.SqlConnection(connectSqlServer))
            // using (var connection = new System.Data.SqlClient.SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\tradedatabase.mdf;Integrated Security=True;Connect Timeout=30;"))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    foreach (var trade in trades)
                    {
                        var command = connection.CreateCommand();
                        command.Transaction = transaction;
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "dbo.insert_trade";
                        command.Parameters.AddWithValue("@sourceCurrency", trade.SourceCurrency);
                        command.Parameters.AddWithValue("@destinationCurrency", trade.DestinationCurrency);
                        command.Parameters.AddWithValue("@lots", trade.Lots);
                        command.Parameters.AddWithValue("@price", trade.Price);

                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                connection.Close();
            }
            
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
