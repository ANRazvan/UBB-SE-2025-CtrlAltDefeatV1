using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialStuff.Model.MessageClasses
{
    interface Message
    {
        int getSenderID();
        int getChatID();
        DateTime getTimestamp();

        string toString();
    }
}