﻿using System;
using System.Collections.Generic;

namespace HuaHaoERP.Model
{
    /// <summary>
    /// 流水线模块的Model
    /// 产品每个工序的半成品统计
    /// </summary>
    public class AssemblyLineModuleModel
    {
        private Guid guid;

        public Guid Guid
        {
            get { return guid; }
            set { guid = value; }
        }
        private string number;

        public string Number
        {
            get { return number; }
            set { number = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private List<AssemblyLineModuleProcessModel> processList;

        internal List<AssemblyLineModuleProcessModel> ProcessList
        {
            get { return processList; }
            set { processList = value; }
        }

    }

    public class AssemblyLineModuleProcessModel
    {
        private Guid guid;

        public Guid Guid
        {
            get { return guid; }
            set { guid = value; }
        }
        private Guid staffID;

        public Guid StaffID
        {
            get { return staffID; }
            set { staffID = value; }
        }
        private Guid productID;

        public Guid ProductID
        {
            get { return productID; }
            set { productID = value; }
        }
        private string lastProcess;
        /// <summary>
        /// 上一道工序
        /// </summary>
        public string LastProcess
        {
            get { return lastProcess; }
            set { lastProcess = value; }
        }
        private string process;

        public string Process
        {
            get { return process; }
            set { process = value; }
        }
        private int quantity;

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        private int breakNum;

        public int BreakNum
        {
            get { return breakNum; }
            set { breakNum = value; }
        }
    }
}
