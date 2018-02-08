using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BO3_CSV_Editor
{

   /// <summary>
   /// 
   /// </summary>
   public partial class MainViewModel
   {
      /// <summary>
      /// 
      /// </summary>
      /// <param name="obj"></param>
      private void fnClose(object obj = null)
      {
         CloseAction();
      }

      //MVM.MakeAbout();
      /// <summary>
      /// MVM.MakeAbout();
      /// <Label x:Name="About" Content="{ Binding About, Mode = TwoWay, UpdateSourceTrigger = PropertyChanged}" Margin="0,-3.283,3,0" VerticalAlignment="Top" HorizontalAlignment="Right" Width="101.967" Height="25.96"/>
      /// </summary>
      public void fnMakeAbout()
      {
         Version v = Assembly.GetExecutingAssembly().GetName().Version;
         About = string.Format(CultureInfo.InvariantCulture, @"Version {0}.{1}.{2} (r{3})", v.Major, v.Minor, v.Build, v.Revision);
      }

      internal void UpdateComboboxes()
      {
         
         try
         {
            
            foreach (DataRow dr in csvdata.Table.Rows)
            {
               //Console.WriteLine(dr[0] + " " + keybox.Text);
               if (dr[0].ToString() == KeyBox)
               {
                  //Console.WriteLine(dr[0]);
                  string find;
                  for (int i = 1; i < csvdata.Table.Columns.Count; i++)
                  {
                     //Console.WriteLine(i);
                     find = dr[i].ToString();
                     //Console.WriteLine(find);
                     string colname = csvdata.Table.Columns[i].ColumnName.ToString();
                     /*ComboBox t = (ComboBox)CSVItems.Where(k => k.Name == colname.ToString());
                     t.Text = dr[i].ToString();*/
                     foreach (Control k in CSVItems)
                     {
                        //Console.WriteLine(colname + " " + k.Name);
                        if (k.Name.ToString() == colname.ToString())
                        {
                           ComboBox t = (ComboBox)k;
                           t.Text = dr[i].ToString();
                        }
                     }
                  }
                  return;
               }
            }
         }
         catch
         {

         }
         foreach (Control k in CSVItems)
         {
            try
            {
               ComboBox t = (ComboBox)k;
               t.Text = "";
            }
            catch
            {

            }
         }
      }

      // <summary>
      /// 
      /// </summary>
      /// <param name="obj"></param>
      private void fnSaveCSV(object obj = null)
      {
         StringBuilder sb = new StringBuilder();

         IEnumerable<string> columnNames = csvdata.Table.Columns.Cast<DataColumn>().
                                           Select(column => column.ColumnName.Replace("__","_"));
         sb.AppendLine(string.Join(",", columnNames));


         foreach (DataRow row in csvdata.Table.Rows)
         {
            try
            {
               IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
               sb.AppendLine(string.Join(",", fields));
            }
            catch
            {

            }
            

            /*IEnumerable<string> fields = row.ItemArray.Select(field =>
              string.Concat("\"", field.ToString().Replace("\"", "\"\""), "\""));
            sb.AppendLine(string.Join(",", fields));*/
         }

         File.WriteAllText(FileName, sb.ToString());

      }

      // <summary>
      /// 
      /// </summary>
      /// <param name="obj"></param>
      private void fnInsertAbove(object obj = null)
      {
         
         for(int i=0;i<csvdata.Table.Rows.Count;i++)
         {
            if(i== ((MainWindow)System.Windows.Application.Current.MainWindow).csvtable.SelectedIndex)
            {
               DataView dvCategory = new DataView(csvdata.Table);
               DataRow newrow = dvCategory.Table.NewRow();
               
               dvCategory.Table.Rows.InsertAt(newrow, i);
               UpdateComboboxes();
               break;
            }
         }
      }

      // <summary>
      /// 
      /// </summary>
      /// <param name="obj"></param>
      private void fnInsertBelow(object obj = null)
      {
         for (int i = 0; i < csvdata.Table.Rows.Count; i++)
         {
            if (i-1 == ((MainWindow)System.Windows.Application.Current.MainWindow).csvtable.SelectedIndex)
            {
               DataView dvCategory = new DataView(csvdata.Table);
               DataRow newrow = dvCategory.Table.NewRow();

               dvCategory.Table.Rows.InsertAt(newrow, i);
               UpdateComboboxes();
               break;
            }
         }
      }

      // <summary>
      /// 
      /// </summary>
      /// <param name="obj"></param>
      private void fnRefresh(object obj = null)
      {
         if (MessageBox.Show("You will lose all progress if you continue. \n\nWould you like to reload?", "Reload", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
         {
            return;
         }
         

         csvdata.Table.Clear();
         
         DataTable dt;


         if (!File.Exists(FileName)) return;
         dt = clsCsvToTable.GetDataTableFromCSV(FileName);
         foreach (DataColumn dc in dt.Columns)
         {
            dc.ColumnName = dc.ColumnName.Replace("_", "__");
         }
         csvdata = dt.DefaultView;
         //SetupDropDowns(dt);
      }

      // <summary>
      /// 
      /// </summary>
      /// <param name="obj"></param>
      private void fnDelete(object obj = null)
      {
         for (int i = 0; i < csvdata.Table.Rows.Count; i++)
         {
            if (i == ((MainWindow)System.Windows.Application.Current.MainWindow).csvtable.SelectedIndex)
            {
               csvdata.Table.Rows[i].Delete();
               csvdata.Table.AcceptChanges();
               UpdateComboboxes();
               break;
            }
         }
      }

      // <summary>
      /// 
      /// </summary>
      /// <param name="obj"></param>
      private void fnDuplicate(object obj = null)
      {
         for (int i = 0; i < csvdata.Table.Rows.Count; i++)
         {
            if (i == ((MainWindow)System.Windows.Application.Current.MainWindow).csvtable.SelectedIndex)
            {
               DataView dvCategory = new DataView(csvdata.Table);
               DataRow newrow = dvCategory.Table.NewRow();
               newrow.ItemArray = csvdata.Table.Rows[i].ItemArray;
               dvCategory.Table.Rows.InsertAt(newrow, i);
               UpdateComboboxes();
               break;
            }
         }
      }

   }/* End Class */
}/* End NameSpace */
