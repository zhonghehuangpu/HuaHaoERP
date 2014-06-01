﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HuaHaoERP.Model.Warehouse;
using System.Data;

namespace HuaHaoERP.ViewModel.Warehouse
{
    class WarehouseProductConsole
    {
        internal bool Add(Guid ProductID, string StaffName, int Quantity)
        {
            string sql = " Insert into T_Warehouse_Product(Guid,ProductID,Date,Operator,Number,Remark) "
                       + " values('" + Guid.NewGuid() + "','" + ProductID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + StaffName + "','" + Quantity + "','入库')";
            return new Helper.SQLite.DBHelper().SingleExecution(sql);
        }
        internal bool Outbound(Guid ProductID, int Quantity)
        {
            string sql = " Insert into T_Warehouse_ProductPacking(Guid,ProductID,Date,Quantity,Remark) "
                       + " values('" + Guid.NewGuid() + "','" + ProductID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','-" + Quantity + "','出库')";
            return new Helper.SQLite.DBHelper().SingleExecution(sql);
        }
        internal bool ReadDetailsList(out List<WarehouseProductModel> data)
        {
            data = new List<WarehouseProductModel>();
            string sql = " SELECT                                                            "
                       + "	 a.*,                                                            "
                       + "   b.Name                                                          "
                       + " FROM                                                              "
                       + "	 T_Warehouse_Product a                                           "
                       + " LEFT JOIN T_ProductInfo_Product b ON a.ProductID=b.GUID           ";
            DataSet ds = new DataSet();
            if(new Helper.SQLite.DBHelper().QueryData(sql, out ds))
            {
                int id = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    WarehouseProductModel d = new WarehouseProductModel();
                    d.Guid = (Guid)dr["GUID"];
                    d.Id = id++;
                    d.ProductID = (Guid)dr["ProductID"];
                    d.ProductName = dr["Name"].ToString();
                    d.Date = dr["Date"].ToString();
                    d.Operator = dr["Operator"].ToString();
                    d.Number = int.Parse(dr["Number"].ToString());
                    d.Remark = dr["Remark"].ToString();
                    data.Add(d);
                }
                return true;
            }
            return false;
        }

        internal bool ReadNumList(out List<WarehouseProductNumModel> data)
        {
            data = new List<WarehouseProductNumModel>();
            string sql = "SELECT                                                  "
                       + "	a.ProductID,                                                  "
                       + "	total(a.Number) as Quantity,                            "
                       + "	b.Name as ProductName                                 "
                       + "FROM                                                    "
                       + "	T_Warehouse_Product a                                 "
                       + "LEFT JOIN T_ProductInfo_Product b on a.ProductID=b.GUID "
                       + " GROUP BY a.ProductID ";
            DataSet ds = new DataSet();
            if(new Helper.SQLite.DBHelper().QueryData(sql, out ds))
            {
                int id = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    WarehouseProductNumModel d = new WarehouseProductNumModel();
                    d.Id = id++;
                    d.ProductID = (Guid)dr["ProductID"];
                    d.ProductName = dr["ProductName"].ToString();
                    d.Quantity = int.Parse(dr["Quantity"].ToString());
                    data.Add(d);
                }
                return true;
            }
            return false;
        }

        internal bool ReadPackingNumList(out List<WarehouseProductNumModel> data)
        {
            data = new List<WarehouseProductNumModel>();
            string sql = "SELECT                                                  "
                       + "	a.ProductID,                                                  "
                       + "	total(a.Quantity) as Quantity,                            "
                       + "	b.Name as ProductName                                 "
                       + "FROM                                                    "
                       + "	T_Warehouse_ProductPacking a                                 "
                       + "LEFT JOIN T_ProductInfo_Product b on a.ProductID=b.GUID "
                       + " GROUP BY a.ProductID ";
            DataSet ds = new DataSet();
            if (new Helper.SQLite.DBHelper().QueryData(sql, out ds))
            {
                int id = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    WarehouseProductNumModel d = new WarehouseProductNumModel();
                    d.Id = id++;
                    d.ProductID = (Guid)dr["ProductID"];
                    d.ProductName = dr["ProductName"].ToString();
                    d.Quantity = int.Parse(dr["Quantity"].ToString());
                    data.Add(d);
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// 打包操作
        /// Quantity：散件总数
        /// PackedQuantity：打包的包数
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="Quantity"></param>
        /// <param name="PackedQuantity"></param>
        /// <returns></returns>
        internal bool Packing(Guid ProductID, int Quantity, int PackedQuantity)
        {
            List<string> sqls = new List<string>();
            string sql1 = " Insert into T_Warehouse_Product(Guid,ProductID,Date,Operator,Number,Remark) "
                        + " values('" + Guid.NewGuid() + "','" + ProductID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Helper.DataDefinition.CommonParameters.LoginUserName + "','-" + Quantity + "','包装*" + PackedQuantity + "')";
            string sql2 = " Insert into T_Warehouse_ProductPacking(Guid,ProductID,Date,Operator,Quantity) "
                        + " values('" + Guid.NewGuid() + "','" + ProductID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Helper.DataDefinition.CommonParameters.LoginUserName + "','" + PackedQuantity + "')";
            sqls.Add(sql1);
            sqls.Add(sql2);
            return new Helper.SQLite.DBHelper().Transaction(sqls);
        }
        /// <summary>
        /// 获取产品的默认设置的包装数
        /// </summary>
        internal int ReadProductPackingNum(Guid Guid)
        {
            string sql = "select PackageNumber FROM T_ProductInfo_Product where GUID='" + Guid + "'";
            object Num;
            new Helper.SQLite.DBHelper().QuerySingleResult(sql, out Num);
            return int.Parse(Num.ToString());
        }
    }
}
