﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HuaHaoERP.Model;
using System.Data;

namespace HuaHaoERP.ViewModel.Customer
{
    class ProcessorsConsole
    {
        internal bool Add(ProcessorsModel d)
        {
            bool flag = true;
            string sql = "Insert Into T_UserInfo_Processors(GUID,Number,Name,Address,Area,Phone,MobilePhone,Fax,Business,Clerk,OpeningBank,BankCardNo,BankCardName,Remark,AddTime) "
                        + " values('" + d.Guid + "','"+d.Number+"','"+d.Name+"','"+d.Address+"','"+d.Area+"','"+d.Phone+"','"+d.MobilePhone+"','"+d.Fax+"','"+d.Business+"','"+d.Clerk+"','"+d.OpeningBank+"','"+d.BankCardNo+"','"+d.BankCardName+"','"+d.Remark+"','" + d.AddTime.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            flag = new Helper.SQLite.DBHelper().SingleExecution(sql);
            return flag;
        }
        internal bool Update(ProcessorsModel d)
        {
            bool flag = true;
            List<string> sqls = new List<string>();
            string sql_Delete = "Delete From T_UserInfo_Processors Where GUID='" + d.Guid + "'";
            string sql_Insert = "Insert Into T_UserInfo_Processors(GUID,Number,Name,Address,Area,Phone,MobilePhone,Fax,Business,Clerk,OpeningBank,BankCardNo,BankCardName,Remark,AddTime) "
                        + " values('" + d.Guid + "','" + d.Number + "','" + d.Name + "','" + d.Address + "','" + d.Area + "','" + d.Phone + "','" + d.MobilePhone + "','" + d.Fax + "','" + d.Business + "','" + d.Clerk + "','" + d.OpeningBank + "','" + d.BankCardNo + "','" + d.BankCardName + "','" + d.Remark + "','" + d.AddTime.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            sqls.Add(sql_Delete);
            sqls.Add(sql_Insert);
            flag = new Helper.SQLite.DBHelper().Transaction(sqls);
            return flag;
        }
        internal bool MarkDelete(ProcessorsModel d)
        {
            bool flag = true;
            string sql = "Update T_UserInfo_Processors Set DeleteMark='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' Where GUID='" + d.Guid + "'";
            flag = new Helper.SQLite.DBHelper().SingleExecution(sql);
            return flag;
        }
        internal bool ReadList(out List<ProcessorsModel> data)
        {
            bool flag = true;
            data = new List<ProcessorsModel>();
            string sql = "select * from T_UserInfo_Processors Where DeleteMark is null order by AddTime";
            DataSet ds = new DataSet();
            flag = new Helper.SQLite.DBHelper().QueryData(sql, out ds);
            if (flag)
            {
                int id = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ProcessorsModel d = new ProcessorsModel();
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
                    d.OpeningBank = dr["OpeningBank"].ToString();
                    d.BankCardNo = dr["BankCardNo"].ToString();
                    d.BankCardName = dr["BankCardName"].ToString();
                    d.Remark = dr["Remark"].ToString();
                    d.AddTime = Convert.ToDateTime(dr["AddTime"]);
                    data.Add(d);
                }
            }
            return flag;
        }
        internal bool GetNameList(out DataSet ds)
        {
            bool flag = true;
            ds = new DataSet();
            string sql = "select Guid,Number,Name From T_UserInfo_Processors Where DeleteMark is null order by AddTime";
            flag = new Helper.SQLite.DBHelper().QueryData(sql, out ds);
            return flag;
        }
        internal bool GetNameList(string Parm, out DataSet ds)
        {
            bool flag = true;
            ds = new DataSet();
            string sql = "select Guid,Number,Name From T_UserInfo_Processors Where (Number LIKE '%" + Parm + "%' OR Name LIKE '%" + Parm + "%') AND DeleteMark is null order by AddTime";
            flag = new Helper.SQLite.DBHelper().QueryData(sql, out ds);
            return flag;
        }
    }
}
