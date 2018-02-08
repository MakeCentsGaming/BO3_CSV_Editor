using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO3_CSV_Editor
{
   class clsCsvToTable
   {

      public static DataTable GetDataTableFromCSV(string strFileName)
      {
         try
         {
            System.Data.OleDb.OleDbConnection conn = new OleDbConnection(@"Provider =Microsoft.Jet.OLEDB.4.0;Data Source=" + System.IO.Path.GetDirectoryName(strFileName) + ";Extended Properties=\"Text;HDR==YES;FMT=Delimited\"");
            conn.Open();
            string strQuery = "Select * from [" + System.IO.Path.GetFileName(strFileName) + "]";
            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter(strQuery, conn);
            System.Data.DataSet ds = new System.Data.DataSet();
            da.Fill(ds);
            return ds.Tables[0];
         }
         catch (Exception ex)
         { }
         
         return new DataTable();
      }

      
   }
}
