using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class LogMessage
    {
        public LogMessage(Exception exception, string type, DateTime created)
        {
            Text = exception.ToString();
            Type = type;
            Created = created;
        }

        public string Text { get; set; }

        public string Type { get; set; }

        public DateTime Created { get; set; }
    }
}
