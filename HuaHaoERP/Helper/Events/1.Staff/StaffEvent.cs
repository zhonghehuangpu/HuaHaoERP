﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HuaHaoERP.Model;

namespace HuaHaoERP.Helper.Events
{
    class StaffEvent
    {
        public static EventHandler<StaffEventArgs> EAdd;
        public static EventHandler<StaffEventArgs> EDelete;
        public static EventHandler<StaffEventArgs> EModify;
        public static EventHandler EUpdateDataGrid;

        internal static void OnAdd(object sender, StaffModel StaffData)
        {
            if (EAdd != null)
            {
                StaffEventArgs ee = new StaffEventArgs();
                ee.StaffData = StaffData;
                EAdd(sender, ee);
            }
        }
        internal static void OnDelete(object sender, StaffModel StaffData)
        {
            if (EDelete != null)
            {
                StaffEventArgs ee = new StaffEventArgs();
                ee.StaffData = StaffData;
                EDelete(sender, ee);
            }
        }
        internal static void OnModify(object sender, StaffModel StaffData)
        {
            if (EModify != null)
            {
                StaffEventArgs ee = new StaffEventArgs();
                ee.StaffData = StaffData;
                EModify(sender, ee);
            }
        }
        internal static void OnUpdateDataGrid(object sender, EventArgs e)
        {
            if (EUpdateDataGrid != null)
            {
                EUpdateDataGrid(sender, e);
            }
        }
    }
}