using System.Windows;
using System.Windows.Controls;

namespace BO3_CSV_Editor
{
   
    class clsDragNDrop
    {
      //private bool firstfile;//removed when gone to stat
      private TextBox tb;

      /// <summary>
      /// <para></para>
      /// clsDragNDrop.TextBoxDragNDrop(textbox);
      /// </summary>
      /// <param name="tb">The textbox</param>
      /// <param name="firstfile">Grab the first file in the array of args</param>
      public static void TextBoxDragNDrop(TextBox tb,bool firstfile = true)
      {
         //this.firstfile = firstfile;
         //this.tb = tb;
         tb.AllowDrop = true;
         tb.Drop += textBox_Drop;
         tb.PreviewDragOver += textBox_DragOver;
      }
      /// <summary>
      /// clsDragNDrop dnd = new clsDragNDrop;
      /// dnd.TextBoxDragNDrop(textbox, this);
      /// </summary>
      /// <param name="tb"></param>
      /// <param name="mainWindow"></param>
      /// <param name="firstfile"></param>
      public void TextBoxDragNDrop(TextBox tb, MainWindow mainWindow, bool firstfile = true)
      {
         this.tb = tb;
         mainWindow.AllowDrop = true;
         mainWindow.Drop += Window_Drop;
         mainWindow.PreviewDragOver += textBox_DragOver;
      }

      public static void ListDragNDrop(ListBox lb, bool firstfile = true)
      {
         //this.firstfile = firstfile;
         //this.tb = tb;
         lb.AllowDrop = true;
         lb.Drop += listBox_Drop;
         lb.PreviewDragOver += listBox_DragOver;
      }

      private static void listBox_DragOver(object sender, DragEventArgs e)
      {
         if (e.Data.GetDataPresent(DataFormats.FileDrop))
            e.Effects = DragDropEffects.Copy;
         else
            e.Effects = DragDropEffects.None;

         e.Handled = true;
      }

      private static void listBox_Drop(object sender, DragEventArgs e)
      {
         string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
         if (files != null && files.Length != 0)
         {
            foreach (string f in files)
            {
               if (!MainViewModel.Instance.DirList.Contains(f))
                  MainViewModel.Instance.DirList.Add(f);
            }

         }
      }
      private static void textBox_DragOver(object sender, DragEventArgs e)
      {
         if (e.Data.GetDataPresent(DataFormats.FileDrop))
            e.Effects = DragDropEffects.Copy;
         else
            e.Effects = DragDropEffects.None;

         e.Handled = true;
      }

      private static void textBox_Drop(object sender, DragEventArgs e)
      {
         string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
         if (files != null && files.Length != 0)
         {
            TextBox tb = (TextBox)sender;
            tb.Text = files[0];
         }
      }
      private void Window_Drop(object sender, DragEventArgs e)
      {
         string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
         if (files != null && files.Length != 0)
         {
            tb.Text = files[0];

         }
      }
   }
}
