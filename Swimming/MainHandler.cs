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

namespace Swimming
{
	public partial class MainWindow : Window
	{
		private void uxDataGrid_CurrentCellChanged(object sender, EventArgs e)
		{
			WriteLine("uxDataGrid_CurrentCellChanged");
		}

		private void uxDataGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			WriteLine("uxDataGrid_MouseLeftButtonDown");
		}

		private void uxDataGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
			WriteLine("uxDataGrid_MouseRightButtonDown");
		}

		private void uxDataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			WriteLine("uxDataGrid_PreviewKeyDown");
		}

		private void uxDataGrid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
		{
			(var rowIndex, var columnIndex) = ClickCellIndex(uxDataGrid, e.GetPosition(uxDataGrid));
			WriteLine("uxDataGrid_PreviewMouseDown rowIndex=" + rowIndex + "    columnIndex=" + columnIndex);
			if (rowIndex > 0)
			{
				string strUrl = dt.Rows[rowIndex]["File"].ToString();
#if DEBUG
				WriteLine("ID=" + dt.Rows[rowIndex]["ID"].ToString());
				MessageBox.Show("ID=" + dt.Rows[rowIndex]["ID"].ToString(),
					"ID",
					MessageBoxButton.OK,
					MessageBoxImage.Asterisk);
				MessageBox.Show(strDogaFileDirectory + strUrl,
					"動画ファイル",
					MessageBoxButton.OK,
					MessageBoxImage.Asterisk);
#endif
				System.Diagnostics.Process p2 = System.Diagnostics.Process.Start(strDogaFileDirectory + strUrl);
			}
		}

		private void btnSwimSet_Click(object sender, RoutedEventArgs e)
		{
			chkSwimCrawl.IsChecked = true;
			chkSwimBreast.IsChecked = true;
			chkSwimBack.IsChecked = true;
			chkSwimButterfly.IsChecked = true;
			chkSwimBatakick.IsChecked = true;
			uxDataGrid.DataContext = CreateData();
		}

		private void btnSwimClr_Click(object sender, RoutedEventArgs e)
		{
			chkSwimCrawl.IsChecked = false;
			chkSwimBreast.IsChecked = false;
			chkSwimBack.IsChecked = false;
			chkSwimBreast.IsChecked = false;
			chkSwimButterfly.IsChecked = false;
			chkSwimBatakick.IsChecked = false;
			uxDataGrid.DataContext = CreateData();
		}

		private void chkSwimCrawl_Checked(object sender, RoutedEventArgs e)
		{
			WriteLine("chkSwimCrawl_Checked");
		}

		private void chkSwimCrawl_Unchecked(object sender, RoutedEventArgs e)
		{
			WriteLine("chkSwimCrawl_Unchecked");
		}

		private void chkSwimBreast_Checked(object sender, RoutedEventArgs e)
		{
			WriteLine("chkSwimBreast_Checked");
		}
		private void chkSwimBreast_Unchecked(object sender, RoutedEventArgs e)
		{
			WriteLine("chkSwimBreast_Unchecked");
		}

		private void chkSwimBack_Checked(object sender, RoutedEventArgs e)
		{
			WriteLine("chkSwimBack_Checked");
		}

		private void chkSwimBack_Unchecked(object sender, RoutedEventArgs e)
		{
			WriteLine("chkSwimBack_Unchecked");
		}

		private void chkSwimButterfly_Checked(object sender, RoutedEventArgs e)
		{
			WriteLine("chkSwimButterfly_Checked");
		}

		private void chkSwimButterfly_Unchecked(object sender, RoutedEventArgs e)
		{
			WriteLine("chkSwimButterfly_Unchecked");
		}

		private void chkSwimBatakick_Checked(object sender, RoutedEventArgs e)
		{
			WriteLine("chkSwimBatakick_Checked");
		}

		private void chkSwimBatakick_Unchecked(object sender, RoutedEventArgs e)
		{
			WriteLine("chkSwimBatakick_Unchecked");
		}

		private void btnTakeSet_Click(object sender, RoutedEventArgs e)
		{
			chkFromPoolside.IsChecked = true;
			chkFromUnderwater.IsChecked = true;
			uxDataGrid.DataContext = CreateData();
		}

		private void btnTakeClr_Click(object sender, RoutedEventArgs e)
		{
			chkFromPoolside.IsChecked = false;
			chkFromUnderwater.IsChecked = false;
			uxDataGrid.DataContext = CreateData();
		}

		private void chkFromPoolside_Checked(object sender, RoutedEventArgs e)
		{
			WriteLine("chkFromPoolside_Checked");
		}

		private void chkFromPoolside_Unchecked(object sender, RoutedEventArgs e)
		{
			WriteLine("chkFromPoolside_Unchecked");
		}

		private void chkFromUnderwater_Checked(object sender, RoutedEventArgs e)
		{
			WriteLine("chkFromUnderwater_Checked");
		}

		private void chkFromUnderwater_Unchecked(object sender, RoutedEventArgs e)
		{
			WriteLine("chkFromUnderwater_Unchecked");
		}

		private void chkSwimCrawl_Click(object sender, RoutedEventArgs e)
		{
			WriteLine("chkSwimCrawl_Click");
			if (chkSwimCrawl.IsChecked == true)
			{
				chkSwimBreast.IsChecked = false;
				chkSwimBack.IsChecked = false;
				chkSwimBreast.IsChecked = false;
				chkSwimButterfly.IsChecked = false;
				chkSwimBatakick.IsChecked = false;
			}
			uxDataGrid.DataContext = CreateData();
		}

		private void chkSwimBreast_Click(object sender, RoutedEventArgs e)
		{
			WriteLine("chkBreast_Click");
			if (chkSwimBreast.IsChecked == true)
			{
				chkSwimCrawl.IsChecked = false;
				chkSwimBack.IsChecked = false;
				chkSwimButterfly.IsChecked = false;
				chkSwimBatakick.IsChecked = false;
			}
			uxDataGrid.DataContext = CreateData();
		}

		private void chkSwimBack_Click(object sender, RoutedEventArgs e)
		{
			WriteLine("chkSwimBack_Click");
			if (chkSwimBack.IsChecked == true)
			{
				chkSwimCrawl.IsChecked = false;
				chkSwimBreast.IsChecked = false;
				chkSwimButterfly.IsChecked = false;
				chkSwimBatakick.IsChecked = false;
			}
			uxDataGrid.DataContext = CreateData();
		}

		private void chkSwimButterfly_Click(object sender, RoutedEventArgs e)
		{
			WriteLine("chkSwimButterfly_Click");
			if (chkSwimButterfly.IsChecked == true)
			{
				chkSwimCrawl.IsChecked = false;
				chkSwimBreast.IsChecked = false;
				chkSwimBack.IsChecked = false;
				chkSwimBreast.IsChecked = false;
				chkSwimBatakick.IsChecked = true;
			}
			uxDataGrid.DataContext = CreateData();
		}
		private void chkSwimBatakick_Click(object sender, RoutedEventArgs e)
		{
			if (chkSwimBatakick.IsChecked == true)
			{
				chkSwimCrawl.IsChecked = false;
				chkSwimBreast.IsChecked = false;
				chkSwimBack.IsChecked = false;
				chkSwimBreast.IsChecked = false;
			}
			uxDataGrid.DataContext = CreateData();
		}

		private void chkFromPoolside_Click(object sender, RoutedEventArgs e)
		{
			WriteLine("chkFromPoolside_Click");
			if (chkFromPoolside.IsChecked == true)
			{
				chkFromUnderwater.IsChecked = false;
			}
			uxDataGrid.DataContext = CreateData();
		}

		private void chkFromUnderwater_Click(object sender, RoutedEventArgs e)
		{
			WriteLine("chkFromUnderwater_Click");
			if (chkFromUnderwater.IsChecked == true)
			{
				chkFromPoolside.IsChecked = false;
			}
			uxDataGrid.DataContext = CreateData();
		}
	}
}
