﻿using System;
using EmberCloudServices;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;

class Program
{
    static void Main()
    {
        SqlInstance db = new SqlInstance();
        
        //db.SqlInstanceCreate("testinstancia", "testpassword");
        
        db.SqlInstanceDrop("instanciapedorra");
    }
}
