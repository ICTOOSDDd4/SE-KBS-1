﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPOS_APP.Factory.Database
{
    internal class Seeder
    {
        public static void Initialize()
        {
            RoleSeeder.Seed();
        }
    }
}
