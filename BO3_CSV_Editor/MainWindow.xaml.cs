using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Drawing;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.ObjectModel;

namespace BO3_CSV_Editor
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      public MainViewModel MVM { get { return this.DataContext as MainViewModel; } }
      ObservableCollection<clsDynCSVControls> AllItems;
      BackgroundWorker bg;
      bool needsupdated = false;
      public MainWindow()
      {
         InitializeComponent();
         clsDragNDrop dnd = new clsDragNDrop();
         dnd.TextBoxDragNDrop(fileName, this);
         
         MVM.CSVItems = new ObservableCollection<Control>();
         MVM.CSVItems2 = new ObservableCollection<Control>();
         //C1DataGrid.ItemsSource = dt.DefaultView;
      }

      private Control ACBox()
      {
         ComboBox cb = new ComboBox();
         cb.Width = 100;
         cb.VerticalAlignment = VerticalAlignment.Top;
         cb.Margin = new Thickness(7, 4, 13, 0);

         cb.ItemsSource = new string[] { "test", "test2" };
         return cb;
      }

      private void FileNameChange(object sender, TextChangedEventArgs e)
      {
         
         InitTables();
         if (!File.Exists(fileName.Text)) return;
         if (!fileName.Text.EndsWith(".csv"))
         {
            MessageBox.Show("Not a valid csv file.", "Invalid File Type", MessageBoxButton.OK, MessageBoxImage.Error);
            fileName.Text = "";
            return;
         }
         this.Cursor = Cursors.Wait;
         bg = new BackgroundWorker();
         bg.DoWork += Bg_DoWork;
         bg.RunWorkerCompleted += Bg_RunWorkerCompleted;
         bg.RunWorkerAsync();
      }

      private void InitTables()
      {
         MVM.csvdata = new DataView();
         MVM.CSVItems = new ObservableCollection<Control>();
         MVM.CSVItems2 = MVM.CSVItems;
         MVM.FirstColumn = new ObservableCollection<string>();
         MVM.FirstColumnName = "";
      }

      private void Bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
      {
         this.Cursor = Cursors.Arrow;
         MVM.CSVItems2 = MVM.CSVItems;
         
      }

      private void Bg_DoWork(object sender, DoWorkEventArgs e)
      {

         Dispatcher.BeginInvoke(new Action(delegate
         {
            
            DataTable dt;


            if (!File.Exists(MVM.FileName)) return;
            dt = clsCsvToTable.GetDataTableFromCSV(MVM.FileName);
            foreach (DataColumn dc in dt.Columns)
            {
               dc.ColumnName = dc.ColumnName.Replace("_", "__");
            }
            SetupDropDowns(dt);

         }));

      }

      private void SetupDropDowns(DataTable dt)
      {
         MVM.FirstColumn = new ObservableCollection<string>();
         List<List<string>> sources;
         bool firsttime;
         MVM.csvdata = dt.DefaultView;

         AllItems = new ObservableCollection<clsDynCSVControls>();

         firsttime = true;


         MVM.FirstColumnName = MVM.csvdata.Table.Columns[0].ColumnName;
         //Console.WriteLine(" test " + MVM.FirstColumnName);
         MVM.CSVColumns = new List<string>();
         sources = new List<List<string>>();
         foreach (DataRow dr in MVM.csvdata.Table.Rows)
         {
            MVM.FirstColumn.Add(dr[0].ToString());
            for (int i = 0; i < MVM.csvdata.Table.Columns.Count; i++)
            {
               if (firsttime)
               {
                  sources.Add(new List<string>());
                  MVM.CSVColumns.Add(MVM.csvdata.Table.Columns[i].ColumnName);
               }
               sources[i].Add(dr[i].ToString());
            }
            firsttime = false;
         }
         MVM.FirstColumn = new ObservableCollection<string>(MVM.FirstColumn.OrderBy(i => i));
         //MVM.FirstColumn = sources[0];
         //Console.WriteLine(MVM.FirstColumn.Count);
         for (int i = 1; i < sources.Count; i++)
         {
            clsDynCSVControls cdcc = new clsDynCSVControls();
            cdcc.SetLabel(MVM.CSVColumns[i]);

            cdcc.SetBox(sources[i]);
            cdcc.box.Name = MVM.CSVColumns[i];
            MVM.CSVItems.Add(cdcc.label);
            MVM.CSVItems.Add(cdcc.box);
         }
      }

      private void ComboBox_TextChanged(object sender, TextChangedEventArgs e)
      {
         MVM.KeyBox = keybox.Text;
         MVM.UpdateComboboxes();
      }

      void myDG_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
      {
         if (e.EditAction == DataGridEditAction.Commit)
         {
            needsupdated = true;
         }
      }

      private void UpdateThisDropDown()
      {
         if(needsupdated)
         {
            needsupdated = false;
            MVM.KeyBox = keybox.Text;
            MVM.UpdateComboboxes();
            //Console.WriteLine(keybox.Text);
            UpdateBoxList();

         }
         
      }

      private void csvtable_CurrentCellChanged(object sender, EventArgs e)
      {
        
      }

      private void MenuItem_Loaded(object sender, RoutedEventArgs e)
      {
         MenuItem t =  (MenuItem)sender;
         if(!ShouldClick())
         {
            t.IsEnabled = false;
         }
         else
         {
            t.IsEnabled = true;
         }
      }

      private bool ShouldClick()
      {
         Console.WriteLine(MVM.FileName);
         if (!File.Exists(MVM.FileName)) return false;
         

         return true;
      }

     
      private void UpdateBoxList()
      {
         string curdata = keybox.Text;
         MVM.FirstColumn = new ObservableCollection<string>();
         foreach (DataRow dr in MVM.csvdata.Table.Rows)
         {
            MVM.FirstColumn.Add(dr[0].ToString());
         }
         MVM.FirstColumn = new ObservableCollection<string>(MVM.FirstColumn.OrderBy(i => i));
         keybox.Text = curdata;
         //Console.WriteLine(MVM.KeyBox);
      }

      private void csvtable_LostFocus(object sender, RoutedEventArgs e)
      {
         UpdateThisDropDown();

      }

      private void csvtable_KeyUp(object sender, KeyEventArgs e)
      {
         needsupdated = true;
      }

      private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
      {
         if (search.Text == "")
         {
            MVM.CSVItems2 = MVM.CSVItems;
            return;
         }
         ObservableCollection<Control> filter = new ObservableCollection<Control>();
         for(int i = 0;i<MVM.CSVItems.Count;i+=2)
         {
            Label l = (Label)MVM.CSVItems[i];
            //Console.WriteLine(l.Content);
            if (l.Content.ToString().Contains(search.Text))
            {
               filter.Add(MVM.CSVItems[i]);
               filter.Add(MVM.CSVItems[i+1]);
               continue;
            }
            ComboBox c = (ComboBox)MVM.CSVItems[i + 1];
            if (c.Text.ToString().Contains(search.Text))
            {
               filter.Add(MVM.CSVItems[i]);
               filter.Add(MVM.CSVItems[i+1]);
               continue;
            }
         }
         MVM.CSVItems2 = filter;
      }

      private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
      {
         if (search1.Text == "") return;
         csvtable.SelectedItems.Clear();
         foreach (DataRowView dr in csvtable.ItemsSource)
         {

            if(AnyCellContains(dr))
            {
               csvtable.SelectedItems.Add(dr);
            }
         }
      }

      private bool AnyCellContains(DataRowView dr)
      {
         for(int i=0;i<dr.Row.ItemArray.Count();i++)
         {
            //Console.WriteLine(dr.Row.ItemArray[i].ToString());
            if(dr.Row.ItemArray[i].ToString().Contains(search1.Text))
            {
               //Console.WriteLine(search.Text + " " + dr.Row.ItemArray[i].ToString());
               return true;
            }
         }
         
         return false;
      }

      private void search1_MouseEnter(object sender, MouseEventArgs e)
      {
         searchlabel.Content = "";
      }

      private void search1_MouseLeave(object sender, MouseEventArgs e)
      {
         if(search1.Text=="")
         {
            searchlabel.Content = "Search";
         }
      }
   }
}
