﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interactions.RequestModels
{
    public struct ListExpenses
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
