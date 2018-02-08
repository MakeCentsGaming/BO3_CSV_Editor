using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Timers;
using System.Windows;
using System.Data;
using System.Windows.Controls;

namespace BO3_CSV_Editor
{
   /// <summary>
   /// 
   /// </summary>
   public partial class MainViewModel
   {      
      private Timer Clock { get; set; }            
      private static MainViewModel _instance;
      /// <summary>
      /// 
      /// </summary>
      public static MainViewModel Instance
      {
         get { return _instance; }
         set
         {
            _instance = value;
            if (_instance != null)
               _instance.OnPropertyChanged("Instance");
         }
      }
      /// <summary>
      /// 
      /// </summary>
      public Window MyParentWindow { get; set; }
      
      /// <summary>
      /// 
      /// </summary>
      public Action CloseAction { get; set; }

      /// <summary>
      /// 
      /// </summary>
      public Action SetWaitCursor { get; set; }

      /// <summary>
      /// 
      /// </summary>
      public Action SetNormalCursor { get; set; }

      private string _WelcomeMessage;
      /// <summary>
      /// 
      /// </summary>
      public string WelcomeMessage
      {
         get { return _WelcomeMessage; }
         set
         {
            _WelcomeMessage = value;
            OnPropertyChanged("WelcomeMessage");
         }
      }

      private string _CurrentDisplayDate;
      /// <summary>
      /// 
      /// </summary>
      public string CurrentDisplayDate
      {
         get { return _CurrentDisplayDate; }
         set
         {
            _CurrentDisplayDate = value;
            OnPropertyChanged("CurrentDisplayDate");
         }
      }
      

      private Visibility _OverlayVisibility;
      /// <summary>
      /// 
      /// </summary>
      public Visibility OverlayVisibility
      {
         get { return _OverlayVisibility; }
         set
         {
            _OverlayVisibility = value;
            OnPropertyChanged("OverlayVisibility");
         }
      }
      

      private string _OverlayMessage;
      /// <summary>
      /// 
      /// </summary>
      public string OverlayMessage
      {
         get { return _OverlayMessage; }
         set
         {
            _OverlayMessage = value;
            OnPropertyChanged("OverlayMessage");
         }
      }
      

      private bool _LayoutRootEnabled;
      /// <summary>
      /// 
      /// </summary>
      public bool LayoutRootEnabled
      {
         get { return _LayoutRootEnabled; }
         set
         {
            _LayoutRootEnabled = value;
            OnPropertyChanged("LayoutRootEnabled");
         }
      }


      private ObservableCollection<string> _DirList;
      /// <summary>
      /// 
      /// </summary>
      public ObservableCollection<string> DirList
      {
         get { return _DirList; }
         set
         {
            _DirList = value;
            OnPropertyChanged("DirList");
         }
      }

      private string _About;
      /// <summary>
      /// 
      /// </summary>
      public string About
      {
         get { return _About; }
         set
         {
            _About = value;
            OnPropertyChanged("About");
         }
      }
      
      
      private string _FileName;
      /// <summary>
      /// 
      /// </summary>
      public string FileName
      {
         get { return _FileName; }
         set
         {
            _FileName = value;
            OnPropertyChanged("FileName");
         }
      }

      private DataView _csvdata;
      /// <summary>
      /// 
      /// </summary>
      public DataView csvdata
      {
         get { return _csvdata; }
         set
         {
            _csvdata = value;
            OnPropertyChanged("csvdata");
         }
      }

      private ObservableCollection<Control> _CSVItems;
      /// <summary>
      /// 
      /// </summary>
      public ObservableCollection<Control> CSVItems
      {
         get { return _CSVItems; }
         set
         {
            _CSVItems = value;
            OnPropertyChanged("CSVItems");
         }
      }

      

      private ObservableCollection<Control> _CSVItems2;
      /// <summary>
      /// 
      /// </summary>
      public ObservableCollection<Control> CSVItems2
      {
         get { return _CSVItems2; }
         set
         {
            _CSVItems2 = value;
            OnPropertyChanged("CSVItems2");
         }
      }
          
      private ObservableCollection<string> _FirstColumn;
      /// <summary>
      /// 
      /// </summary>
      public ObservableCollection<string> FirstColumn
      {
         get { return _FirstColumn; }
         set
         {
            _FirstColumn = value;
            OnPropertyChanged("FirstColumn");
         }
      }

      private string _FirstColumnName;
      /// <summary>
      /// 
      /// </summary>
      public string FirstColumnName
      {
         get { return _FirstColumnName; }
         set
         {
            _FirstColumnName = value;
            OnPropertyChanged("FirstColumnName");
         }
      }

      private List<string> _CSVColumns;
      /// <summary>
      /// 
      /// </summary>
      public List<string> CSVColumns
      {
         get { return _CSVColumns; }
         set
         {
            _CSVColumns = value;
            OnPropertyChanged("CSVColumns");
         }
      }

      public string _KeyBox;
      /// <summary>
      /// 
      /// </summary>
      public string KeyBox
      {
         get { return _KeyBox; }
         set
         {
            _KeyBox = value;
            OnPropertyChanged("KeyBox");
         }
      }



   }/* End Class */
}/* End NameSpace */