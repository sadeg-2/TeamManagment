﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManagment.Core.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException() : base("Item not Found")
        {

        }
    }
}
