﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cod3rsGrowth.Forms
{
    public class MessageService : IMessageService
    {
        public string GetSuccessMessage()
        {
            return "Successful Operation!!";
        }
    }
}
