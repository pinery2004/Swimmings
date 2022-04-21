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

			uxDataGrid.DataContext = CreateData();
		}

		private void GetConfigulation(out string o_strFilePath)
		{
			o_strFilePath = "";

			//すべてのキーとその値を取得
			foreach (string key in ConfigurationManager.AppSettings.AllKeys)
			{
				Console.WriteLine("{0} : {1}",
					key, ConfigurationManager.AppSettings[key]);
				o_strFilePath = ConfigurationManager.AppSettings[key];
			}
		}

		private DataTable CreateData()
		{
			DirectoryInfo di;

			GetConfigulation(out strDogaFileDirectory);
			txtboxFolder.Text = strDogaFileDirectory;

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

				//foreach (var file in di.EnumerateFiles("*クロール*", SearchOption.AllDirectories))
				foreach (var file in di.EnumerateFiles("*", SearchOption.AllDirectories))
				{
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
					if (CheckDispFile(strStyle, strFrom, file.Name, strMade))
					{
						dt.Rows.Add(dr);
					}
					//dt.Rows.Add(dr);
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
		/// <param name="i_strStyle">泳法</param>
		/// <param name="i_strFrom">撮影場所</param>
		/// <param name="i_FileName">ファイル名(エラー表示用)</param>
		/// <param name="i_strMade">ファイル作成日(エラー表示用)</param>
		/// <returns></returns>
		private bool CheckDispFile(string i_strStyle, string i_strFrom, string i_FileName, string i_strMade)
		{
			bool bDisp = false;
			bool bCrawl = i_strStyle.Contains("クロール");
			bool bBreast = i_strStyle.Contains("平泳ぎ");
			bool bBack = i_strStyle.Contains("背泳ぎ");
			bool bButterfly = i_strStyle.Contains("バタフライ");
			bool bBatakick = i_strStyle.Contains("バタキック");

			bool bUnderWater = i_strFrom.Contains("水中");

			if (!(bCrawl || bBreast || bBack || bButterfly || bBatakick))
			{
				MessageBox.Show("ファイル名にエラーがありました\nファイル名=" + i_FileName +
					"\n泳法=" + i_strStyle + "\n撮影場所=" + i_strFrom + "\n作成日時=" + i_strMade,
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

					//           using (SWF.FolderBrowserDialog swfdialog = new SWF.FolderBrowserDialog())
					//           {
					//// 初期選択フォルダーが設定できる①
					//swfdialog.SelectedPath = strDogaFileDirectory;

					//// ダイアログに表示する説明文を設定できる②
					//swfdialog.Description = "水泳動画が入っているフォルダを選択して下さい";

					//// 新しくフォルダを作成を許可する②
					//swfdialog.ShowNewFolderButton = false;

					//// ダイアログを表示する。
					//SWF.DialogResult swfresult = swfdialog.ShowDialog();
					//if (swfresult == SWF.DialogResult.OK)
					{
						// 選択されたフォルダを取得する
						string strDogaFileDirectory = cofd.FileName;

						txtboxFolder.Text = strDogaFileDirectory;

						//MessageBox.Show(string.Format("{0}が選択されました", strDogaFileDirectory));
						MessageBox.Show($"{cofd.FileName}を選択しました");
						if (Directory.Exists(cofd.FileName))
						{
							string[] files = SearchFile(strDogaFileDirectory, "*.mp4", "*.mp4", false);
							if (files.Length > 0) break;
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

		}

		/// <summary>
		/// ファイルの検索を行う
		/// </summary>
		/// <param name="dirPath">フォルダのパス。</param>
		/// <param name="pattern">検索する正規表現のパターン。</param>
		/// <param name="fileWildcards">対象とするファイル。</param>
		/// <param name="ignoreCase">大文字小文字を区別するか。</param>
		/// <returns>見つかったファイルパスの配列。</returns>
		public string[] SearchFile(
			string dirPath, string pattern, string fileWildcards, bool ignoreCase)
		{
			System.Collections.ArrayList fileCol =
				new System.Collections.ArrayList();

			//正規表現のオプションを設定
			System.Text.RegularExpressions.RegexOptions opts =
				System.Text.RegularExpressions.RegexOptions.None;
			if (ignoreCase)
				opts |= System.Text.RegularExpressions.RegexOptions.IgnoreCase;
			System.Text.RegularExpressions.Regex reg =
				new System.Text.RegularExpressions.Regex(pattern, opts);

			//フォルダ内にあるファイルを取得
			System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(dirPath);
			System.IO.FileInfo[] files = dir.GetFiles(fileWildcards);
			foreach (System.IO.FileInfo f in files)
			{
				//正規表現のパターンを使用して一つずつファイルを調べる
				if (reg.IsMatch(f.FullName))
				{
					fileCol.Add(f.FullName);
				}
			}

			return (string[])fileCol.ToArray(typeof(string));
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
