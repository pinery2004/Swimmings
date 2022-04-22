using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

using System.Data;
using System.IO;
using System.Configuration;
using SWF = System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Swimming
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		bool bFirstLoadDataTable = true;
		string strDogaFileDirectory = "";               // @"H:\USER\家族\啓子\水泳\水泳映像";
		DataTable dt = new DataTable();

		public MainWindow()
		{
			InitializeComponent();

			// 初期動画フォルダ名取得
			//GetConfigulation(out strDogaFileDirectory);
			strDogaFileDirectory = ConfigurationManager.AppSettings["FolderPath"];
			txtboxFolder.Text = strDogaFileDirectory;

			// 動画ファイル名一覧表作成
			uxDataGrid.DataContext = CreateData();
		}

		//private void GetConfigulation(out string o_strFilePath)
		//{
		//	//o_strFilePath = "";

		//	////すべてのキーとその値を取得
		//	//foreach (string key in ConfigurationManager.AppSettings.AllKeys)
		//	//{
		//	//	Console.WriteLine("{0} : {1}",
		//	//		key, ConfigurationManager.AppSettings[key]);
		//	//	o_strFilePath = ConfigurationManager.AppSettings[key];
		//	//}
		//	o_strFilePath = ConfigurationManager.AppSettings["FolderPath"];
		//}

		/// <summary>
		/// 動画ファイル名一覧表作成
		/// </summary>
		/// <returns>動画ファイル名一覧表 DataTable</returns>
		private DataTable CreateData()
		{
			DirectoryInfo di;

			if (Directory.Exists(strDogaFileDirectory))
			{
				di = new DirectoryInfo(strDogaFileDirectory);
				string[] columns = new string[] { "ID", "Date", "Style", "From", "File", "Made", "DispCd" };

				dt.Clear();

				if (bFirstLoadDataTable)
				{
					columns.Select(i => dt.Columns.Add(i)).ToArray();
					bFirstLoadDataTable = false;
				}

				foreach (var file in di.EnumerateFiles("*.MP4", SearchOption.TopDirectoryOnly))
				{
					DateTime timeLastWrite = file.LastWriteTime;
					string strID;
					string strDate;
					string strStyle;
					string strFrom;
					GetFilePropaty(file.Name, out strID, out strDate, out strStyle, out strFrom);
					string strMade = file.LastWriteTime.ToString("yyyy/MM/dd HH:mm:ss");
					DataRow dr = dt.NewRow();
					dr[0] = strID;
					dr[1] = strDate;
					dr[2] = strStyle;
					dr[3] = strFrom;
					dr[4] = file.Name;
					dr[5] = strMade;
					if (CheckDispFile(file.Name, strMade))
					{
						dt.Rows.Add(dr);
					}

					Console.WriteLine(strID + "/" + strDate + "/" + strStyle + "/" + strFrom);
					Console.WriteLine("========");
				}

				// IDでSort
				DataView dv = new DataView(dt);
				// 降順 "ID DESC", 昇順 "ID"
				dv.Sort = "ID DESC";
				dt = dv.ToTable();
			}
			return dt;

		}

		/// <summary>
		/// 動画ファイル名を調べる
		/// </summary>
		/// <param name="i_FileName">ファイル名(エラー表示用)</param>
		/// <param name="i_timeLastWrite">ファイル作成日時</param>
		/// <returns></returns>
		private bool CheckDispFile(string i_FileName, string i_strMade)
		{
			string strID;
			string strDate;
			string strStyle;
			string strFrom;
			GetFilePropaty(i_FileName, out strID, out strDate, out strStyle, out strFrom);

			bool bDisp = false;
			bool bCrawl = strStyle.Contains("クロール");
			bool bBreast = strStyle.Contains("平泳ぎ");
			bool bBack = strStyle.Contains("背泳ぎ");
			bool bButterfly = strStyle.Contains("バタフライ");
			bool bBatakick = strStyle.Contains("バタキック");
			bool bUnderWater = strFrom.Contains("水中");

			if (!(bCrawl || bBreast || bBack || bButterfly || bBatakick))
			{
				MessageBox.Show("ファイル名にエラーがありました\nファイル名=" + i_FileName +
					"\n泳法=" + strStyle + "\n撮影場所=" + strFrom + "\n作成日時=" + i_strMade,
					"画像ファイル名チェック",
					MessageBoxButton.OK,
					MessageBoxImage.Error);
			}

			if (chkSwimCrawl.IsChecked == true)
			{
				bDisp = bCrawl;
			}
			if (chkSwimBreast.IsChecked == true)
			{
				bDisp |= bBreast;
			}
			if (chkSwimBack.IsChecked == true)
			{
				bDisp |= bBack;
			}
			if (chkSwimButterfly.IsChecked == true)
			{
				bDisp |= bButterfly;
			}
			if (chkSwimBatakick.IsChecked == true)
			{
				bDisp |= bBatakick;
			}
			if (!(((chkFromPoolside.IsChecked == true) && !bUnderWater) || ((chkFromUnderwater.IsChecked == true) && bUnderWater)))
			{
				bDisp = false;
			}

			return bDisp;
		}

		private void GetFilePropaty(string strFile, out string o_strID, out string o_strDate, out string o_strStyle, out string o_strFrom)
		{
			var arrProperty = strFile.Split('_');
			foreach (string s in arrProperty)
			{
				Console.Write(s + "=");
			}
			Console.WriteLine("\n------");
			int nProperty = arrProperty.Length;
			o_strID = arrProperty[nProperty - 1].Substring(0, 3);
			o_strDate = strFile.Substring(0, 10);
			o_strStyle = arrProperty[3];
			if (nProperty < 6)
			{
				o_strFrom = "";
			}
			else
			{
				o_strFrom = arrProperty[4];
			}
		}

		private void btnDoga_Click(object sender, RoutedEventArgs e)
		{
			bool bDogaFileExist = true;
			//var dialog = new SWF.FolderBrowserDialog();
			//var result = dialog.ShowDialog();
			for (int i = 0; i < 100; i++)
			{
				using (var cofd = new CommonOpenFileDialog()
				{
					Title = "スイミングスクール動画が入っているフォルダを選択して下さい",
					InitialDirectory = $"{strDogaFileDirectory}",
					// フォルダ選択モードにする
					IsFolderPicker = true,
				})
				{
					if (cofd.ShowDialog() == CommonFileDialogResult.Ok)
					{
						// 選択されたフォルダを取得する
						strDogaFileDirectory = cofd.FileName + "\\";
						Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
						config.AppSettings.Settings["FolderPath"].Value = strDogaFileDirectory;
						txtboxFolder.Text = strDogaFileDirectory;
#if DEBUG
						MessageBox.Show($"{cofd.FileName}を選択しました");
#endif
						if (Directory.Exists(cofd.FileName))
						{
							if (checkDogaFolder(strDogaFileDirectory))
							{
								break;
							}
						}
					}
					else
					{
						// キャンセルの場合は入力終了
						bDogaFileExist = false;
						break;
					}
				}
			}

			if (bDogaFileExist)
			{
				uxDataGrid.DataContext = CreateData();
			}
			string strTest2 = ConfigurationManager.AppSettings["FolderPath"];
			Console.WriteLine(strTest2);

		}

		bool checkDogaFolder(string i_strDogaFileDirectory)
		{
			bool bDoga = false;
			DirectoryInfo di;

			//GetConfigulation(out strDogaFileDirectory);
			//txtboxFolder.Text = strDogaFileDirectory;

			if (Directory.Exists(i_strDogaFileDirectory))
			{
				di = new DirectoryInfo(i_strDogaFileDirectory);

				foreach (var file in di.EnumerateFiles("*.MP4", SearchOption.TopDirectoryOnly))
				{
					string strFileName = file.Name;
					bool bCrawl = strFileName.Contains("クロール");
					bool bBreast = strFileName.Contains("平泳ぎ");
					bool bBack = strFileName.Contains("背泳ぎ");
					bool bButterfly = strFileName.Contains("バタフライ");
					bool bBatakick = strFileName.Contains("バタキック");

					if (bCrawl || bBreast || bBack || bButterfly || bBatakick)
					{
						bDoga = true;
						break;
					}
				}
			}
			return bDoga;
		}

		/// <summary>
		/// クリックされたセルの行番号と列番号の取得
		/// </summary>
		/// <param name="dataGrid"></param>
		/// <param name="pos"></param>
		/// <returns></returns>
		public (int rowIndex, int columnIndex) ClickCellIndex(DataGrid dataGrid, Point pos)
		{
			DependencyObject dep = VisualTreeHelper.HitTest(dataGrid, pos)?.VisualHit;
			int rowIndex = -1;
			int columnIndex = -1;

			while (dep != null)
			{
				dep = VisualTreeHelper.GetParent(dep);

				while (dep != null)
				{
					if (dep is DataGridCell)
					{
						columnIndex = (dep == null) ? -1 : (dep as DataGridCell).Column.DisplayIndex;
					}
					if (dep is DataGridRow)
					{
						rowIndex = (dep == null) ? -1 : (int)(dep as DataGridRow).GetIndex();
					}

					if (rowIndex >= 0 && columnIndex >= 0)
					{
						break;
					}

					dep = VisualTreeHelper.GetParent(dep);
				}
			}
			return (rowIndex, columnIndex);
		}

	}
}
