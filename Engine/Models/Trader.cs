﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Engine.Models
{

    public class Trader : LivingEntity
    {
        public Trader(string name)
        {
            Name = name;
        }
    }
}
