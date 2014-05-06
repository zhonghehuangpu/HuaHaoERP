﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HuaHaoERP.Model;
using System.Data;

namespace HuaHaoERP.ViewModel.Customer
{
    class CustomerConsole
    {
        internal bool Add(CustomerModel d)
        {
            bool flag = true;
            string sql = "Insert Into T_Customer (GUID,Number,Name,Address,Area,Phone,MobilePhone,Fax,Business,Clerk,DebtCeiling,Remark) "
                       + " values('" + d.Guid + "','" + d.Number + "','" + d.Name + "','" + d.Address + "','" + d.Area + "','" + d.Phone + "','" + d.MobilePhone + "','" + d.Fax + "','" + d.Business + "','" + d.Clerk + "','" + d.DebtCeiling + "','" + d.Remark + "')";
            flag = new Helper.SQLite.DBHelper().SingleExecution(sql);
            return flag;
        }

        internal bool Delete(CustomerModel d)
        {
            bool flag = true;
            string sql = "Delete From T_Customer Where GUID='" + d.Guid + "'";
            flag = new Helper.SQLite.DBHelper().SingleExecution(sql);
            return flag;
        }

        internal bool MarkDelete(CustomerModel d)
        {
            bool flag = true;
            string sql = "Update T_Customer Set DeleteMark='" + DateTime.Now + "' Where GUID='" + d.Guid + "'";
            flag = new Helper.SQLite.DBHelper().SingleExecution(sql);
            return flag;
        }

        //internal bool Modify(CustomerModel d)
        //{
        //    bool flag = true;
        //    string sql = "Insert Into T_Customer (GUID,Number,Name,Address,Area,Phone,MobilePhone,Fax,Business,Clerk,DebtCeiling,Remark) "
        //               + " values('" + d.Guid + "','" + d.Number + "','" + d.Name + "','" + d.Address + "','" + d.Area + "','" + d.Phone + "','" + d.MobilePhone + "','" + d.Fax + "','" + d.Business + "','" + d.Clerk + "','" + d.DebtCeiling + "','" + d.Remark + "')";
        //    flag = new Helper.SQLite.DBHelper().SingleExecution(sql);
        //    return flag;
        //}

        internal bool ReadList(out List<CustomerModel> data)
        {
            bool flag = true;
            data = new List<CustomerModel>();
            string sql = "select * from T_Customer Where DeleteMark is null order by Number";
            DataSet ds = new DataSet();
            flag = new Helper.SQLite.DBHelper().QueryData(sql, out ds);
            if(flag)
            {
                int id = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    CustomerModel d = new CustomerModel();
                    d.Guid = (Guid)dr["GUID"];
                    d.Id = id++;
                    d.Number = dr["Number"].ToString();
                    d.Name = dr["Name"].ToString();
                    d.Address = dr["Address"].ToString();
                    d.Area = dr["Area"].ToString();
                    d.Phone = dr["Phone"].ToString();
                    d.MobilePhone = dr["MobilePhone"].ToString();
                    d.Fax = dr["Fax"].ToString();
                    d.Business = dr["Business"].ToString();
                    d.Clerk = dr["Clerk"].ToString();
                    d.DebtCeiling = decimal.Parse(dr["DebtCeiling"].ToString());
                    d.Remark = dr["Remark"].ToString();
                    data.Add(d);
                }
            }
            return flag;
        }
    }
}
