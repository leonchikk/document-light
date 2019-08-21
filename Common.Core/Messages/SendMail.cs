using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Core.Messages
{
    public class SendMail
    {
        public string To { get; set; }
        public string Text { get; set; }
    }
}
