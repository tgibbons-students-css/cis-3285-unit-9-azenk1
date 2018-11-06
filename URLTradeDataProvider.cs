using System;

public class URLTradeDataProvider
{
    public URLTradeDataProvider()
    {

    }

        public static List<string> readFromUrl()
        {

             //URL  to read from stored in string.
             string url = "http://faculty.css.edu/tgibbons/trades4.txt";

            var tradeData = new List<string>();
            // create a web client and use it to read the file stored at the given URL
            var client = new WebClient();
            using (var stream = client.OpenRead(url))
            using (var reader = new StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    tradeData.Add(line);
                }
            }
            return tradeData;
        }
	}
}
