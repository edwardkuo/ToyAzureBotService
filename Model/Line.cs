using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model
{
    public class Line
    {

        public class LineChannel
        {
            public Message message { get; set; }
            public string replyToken { get; set; }
            public string type { get; set; }
            public Source source { get; set; }
            public long timestamp { get; set; }
        }

        public class Message
        {
            public Contentprovider contentProvider { get; set; }
            public string id { get; set; }
            public string type { get; set; }
        }

        public class Contentprovider
        {
            public string type { get; set; }
        }

        public class Source
        {
            public string type { get; set; }
            public string userId { get; set; }
        }




    }
}
