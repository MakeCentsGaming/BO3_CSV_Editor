using System.Windows.Input;

namespace BO3_CSV_Editor
{
   /// <summary>
   /// 
   /// </summary>
   public partial class MainViewModel
   {
      private ICommand pClose;
      /// <summary>
      /// 
      /// </summary>
      public ICommand CmdClose
      {
         get { return pClose = new DelegateCommand(fnClose); }
      }

      private ICommand pSaveCSV;
      /// <summary>
      /// 
      /// </summary>
      public ICommand CmdSaveCSV
      {
         get { return pSaveCSV = new DelegateCommand(fnSaveCSV); }
      }
      private ICommand pInsertAbove;
      /// <summary>
      /// 
      /// </summary>
      public ICommand CmdInsertAbove
      {
         get { return pInsertAbove = new DelegateCommand(fnInsertAbove); }
      }

      private ICommand pInsertBelow;
      /// <summary>
      /// 
      /// </summary>
      public ICommand CmdInsertBelow
      {
         get { return pInsertBelow = new DelegateCommand(fnInsertBelow); }
      }

      private ICommand pRefresh;
      /// <summary>
      /// 
      /// </summary>
      public ICommand CmdRefresh
      {
         get { return pRefresh = new DelegateCommand(fnRefresh); }
      }

      private ICommand pDelete;
      /// <summary>
      /// 
      /// </summary>
      public ICommand CmdDelete
      {
         get { return pDelete = new DelegateCommand(fnDelete); }
      }
      private ICommand pDuplicate;
      /// <summary>
      /// 
      /// </summary>
      public ICommand CmdDuplicate
      {
         get { return pDuplicate = new DelegateCommand(fnDuplicate); }
      }

   }/* End Class */
}/* End NameSpace */
