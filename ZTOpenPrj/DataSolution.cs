using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;

namespace ZTOpenPrj
{
    class DataSolution
    {

        public static OleDbConnection getConnection()
        {
            string strPath = Application.StartupPath + "\\Database\\land.mdb";
            String conString = @"Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + strPath;
            OleDbConnection odbc = new OleDbConnection(conString);
            return odbc;
        }
       
        //�������ݿ��¼����Ӻ�ɾ����
        public bool UpdataUser(string sql)
        {
            OleDbConnection od = getConnection();
            OleDbCommand ocd = new OleDbCommand(sql, od);
            bool isOk = false; ;
            try
            {
                od.Open();

                if (ocd.ExecuteNonQuery() > 0)
                {
                    isOk = true;
                    Console.WriteLine("���ݳɹ�");

                } 

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                od.Close();

            }
            return isOk;

        }

        public DataRow[] GetNodeCollection(string clause)
        {

            OleDbConnection od = getConnection();
            DataSet ds = new DataSet();
            string sql = "select *from CityCode";
            try
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter(sql, od);
                adapter.Fill(ds, "cityCode");
            }catch(Exception ex){
                System.Console.WriteLine(ex.Message);
            }
            DataTable table = ds.Tables[0];
           // string a = table.Rows[2][2].ToString();
            DataRow []rows = table.Select(clause);
            return rows;
        }

        
        
        
    }
}
