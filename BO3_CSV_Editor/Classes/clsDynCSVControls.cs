using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BO3_CSV_Editor
{
   public class clsDynCSVControls
   {
      
      public ComboBox box;
      public string boxvalue;
      public string header;
      public Label label;
      public string key;
      public List<string> boxsource;
      
      public clsDynCSVControls()
      {
         boxsource = new List<string>();
         box = new ComboBox();
         box.MouseLeave += Box_MouseLeave;
         box.Width = 280;
         box.IsEditable = true;
         label = new Label();        
      }

      private void Box_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
      {
         ComboBox t = (ComboBox)sender;
         //Console.WriteLine(t.Text);
         DataView dv = MainViewModel.Instance.csvdata;

         for (int i=0;i<dv.Table.Rows.Count;i++)
         {
            if(dv.Table.Rows[i][0].ToString()==MainViewModel.Instance.KeyBox)
            {
               for(int o=0;o<dv.Table.Columns.Count;o++)
               {
                  if(dv.Table.Columns[o].ColumnName==box.Name)
                  {
                     try
                     {
                        dv.Table.Rows[i][o] = t.Text;
                     }
                     catch
                     {
                        dv.Table.Rows[i][o] = DBNull.Value;
                     }
                  }
               }
            }
         }

      }


      public void SetLabel(string v)
      {
         v = v.Replace("__", "_");
         label.Content = v.Replace("_", "__");
         header = v;
      }

      public void SetBox(List<string> v)
      {
         boxsource = v;
         List<string> isource = boxsource.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
         isource = AddMoreToSource(isource, label);
         isource = SortedSource(isource);
         box.ItemsSource = isource;
      }

      private List<string> AddMoreToSource(List<string> isource, Label label)
      {
         //here is where we figure out how to add more to the source based on label

         return isource;
      }

      private List<string> SortedSource(List<string> isource)
      {
         try
         {
            List<int> nlist = new List<int>();
            foreach (string i in isource)
            {
               nlist.Add(Int32.Parse(i));
               
            }
            nlist.Sort();
            List<string> nnlist = new List<string>();
            foreach(int i in nlist)
            {
               nnlist.Add(i.ToString());
            }
            return nnlist;
         }
         catch
         {
            isource.Sort();
            return isource;
         }
      }
   }
}
