using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using System.Security.Cryptography;

namespace OurGameName.DoMain.Data
{
    internal class SQLiteHelper
    {
        /// <summary>
        /// 数据库连接定义
        /// </summary>
        private SqliteConnection dbConnection;
        /// <summary>
        /// SQL命令定义
        /// </summary>
        private SqliteCommand dbCommand;
        /// <summary>
        /// 数据库读取定义
        /// </summary>
        private SqliteDataReader dataReader;

        /// <summary>
        /// SQLite构造函数
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        public SQLiteHelper(string connectionString)
        {
            try
            {
                dbConnection = new SqliteConnection(connectionString);
                dbConnection.Open();
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }

        /// <summary>
        /// 执行SQL命令
        /// </summary>
        /// <param name="queryString">SQl命令字符串</param>
        /// <returns></returns>
        public SqliteDataReader ExecuteQuery(string queryString)
        {
            dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = queryString;
            dataReader = dbCommand.ExecuteReader();
            return dataReader;
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void CloseConnection()
        {
            if (dbCommand != null)
            {
                dbCommand.Cancel();
            }
            dbCommand = null;

            if (dataReader != null)
            {
                dataReader.Close();
            }
            dataReader = null;

            if (dbConnection != null)
            {
                dbConnection.Close();
            }
            dbConnection = null;
        }

        /// <summary>
        /// 读取整张数据表
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public SqliteDataReader ReadFullTable(string TableName)
        {
            string querySyring = $"Select * From {TableName}";
            return ExecuteQuery(querySyring);
        }

        /// <summary>
        /// 向指定数据表中插入数据
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="Values">插入的数值</param>
        public SqliteDataReader InsertValues(string tableName, String[] Values)
        {
            int fieldCount = ReadFullTable(tableName).FieldCount;

            if (Values.Length != fieldCount)
            {
                throw new SqliteException("values.Length!=fieldCount");
            }

            string queryString = $"Inset Into {tableName} Values({Values}";
            for (int i = 1; i < Values.Length; i++)
            {
                queryString += $",{Values[i]}";
            }
            queryString += ")";
            return ExecuteQuery(queryString);
        }

        /// <summary>
        /// 更新指定数据表内的数据
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="colNames">字段名</param>
        /// <param name="colValues">字段名对应的数据</param>
        /// <param name="key">关键字</param>
        /// <param name="value">关键字对应的值</param>
        /// <param name="operation">操作符</param>
        public SqliteDataReader UpdateValues(string tableName, string[] colNames,
            string[] colValues, string key, string operation, string value)
        {
            //当字段名称和字段数值不对应时引发异常
            if (colNames.Length != colValues.Length)
            {
                throw new SqliteException("colNames.Length!=colValues.Length");
            }

            string queryString = $"Update {tableName} Set {colNames[0]} = {colValues[0]}";
            for (int i = 1; i < colValues.Length; i++)
            {
                queryString += $", {colNames[i]}={colValues[i]}";
            }
            queryString += $" Where {key}{operation}{value}";
            return ExecuteQuery(queryString);
        }
        /// <summary>
        /// 删除指定数据表内的数据
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="colNames">字段名</param>
        /// <param name="colValues">字段名对应的数据</param>
        public SqliteDataReader DelectValeueOR(string tableName, string[] colNames,
            string[] operations, string[] colValues)
        {
            if (colNames.Length != colValues.Length ||
                operations.Length != colNames.Length ||
                operations.Length != colNames.Length)
            {
                throw new SqliteException("参数个数无法对应");
            }

            string queryString = $"Delect from {tableName} Where {colNames[0]} + {operations[0]} + {colValues[0]}";
            for (int i = 1; i < colValues.Length; i++)
            {
                queryString += $"Or {colNames[i]} + {operations[i]} + {colValues[i]}";
            }
            return ExecuteQuery(queryString);
        }
        /// <summary>
        /// 删除指定数据表内的数据
        /// </summary>
        /// <returns>The values.</returns>
        /// <param name="tableName">数据表名称</param>
        /// <param name="colNames">字段名</param>
        /// <param name="colValues">字段名对应的数据</param>
        public SqliteDataReader DelectValeueAnd(string tableName, string[] colNames,
            string[] operations, string[] colValues)
        {
            if (colNames.Length != colValues.Length ||
                operations.Length != colNames.Length ||
                operations.Length != colNames.Length)
            {
                throw new SqliteException("参数个数无法对应");
            }

            string queryString = $"Delect from {tableName} Where {colNames[0]} + {operations[0]} + {colValues[0]}";
            for (int i = 1; i < colValues.Length; i++)
            {
                queryString += $"And {colNames[i]} + {operations[i]} + {colValues[i]}";
            }
            return ExecuteQuery(queryString);
        }
        /// <summary>
        /// 创建数据表
        /// </summary> +
        /// <returns>The table.</returns>
        /// <param name="tableName">数据表名</param>
        /// <param name="colNames">字段名</param>
        /// <param name="colTypes">字段名类型</param>
        public SqliteDataReader CreateTable(string tableName, string[] colNames, string[] colTypes)
        {
            string queryString = $"Create table {tableName}({colNames[0]} {colTypes[0]}";
            for (int i = 1; i < colNames.Length; i++)
            {
                queryString += $",{colNames[i]} {colTypes[i]}";
            }
            queryString += ")";
            return ExecuteQuery(queryString);
        }
    }
}
