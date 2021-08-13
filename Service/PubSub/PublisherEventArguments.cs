using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Event
{
    public class PublisherEventArguments : EventArgs
    {
        public string Command { get; set; }
        public string Date { get; set; }

        public PublisherEventArguments(AmazonPublisherMessage message)
        {
            Command = message.Command;
            Date = message.Date;
        }
    }
}
