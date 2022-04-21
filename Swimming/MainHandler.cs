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
			Console.WriteLine("uxDataGrid_CurrentCellChanged");
		}

		private void uxDataGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Console.WriteLine("uxDataGrid_MouseLeftButtonDown");
		}

		private void uxDataGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
			Console.WriteLine("uxDataGrid_MouseRightButtonDown");
		}

		private void uxDataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			Console.WriteLine("uxDataGrid_PreviewKeyDown");
		}

		private void uxDataGrid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
		{
			(var rowIndex, var columnIndex) = ClickCellIndex(uxDataGrid, e.GetPosition(uxDataGrid));
			Console.WriteLine("uxDataGrid_PreviewMouseDown rowIndex=" + rowIndex + "    columnIndex=" + columnIndex);
			if (rowIndex > 0)
			{
				Console.WriteLine("ID=" + dt.Rows[rowIndex]["ID"].ToString());
				//System.Diagnostics.Process p = System.Diagnostics.Process.Start(@"H:\USER\家族\啓子\水泳\水泳映像\2020_02_05_バタフライ_264.MP4");
				string strUrl = dt.Rows[rowIndex]["File"].ToString();
				System.Diagnostics.Process p2 = System.Diagnostics.Process.Start(@"H:\USER\家族\啓子\水泳\水泳映像\" + strUrl);
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
			Console.WriteLine("chkSwimCrawl_Checked");
		}

		private void chkSwimCrawl_Unchecked(object sender, RoutedEventArgs e)
		{
			Console.WriteLine("chkSwimCrawl_Unchecked");
		}

		private void chkSwimBreast_Checked(object sender, RoutedEventArgs e)
		{
			Console.WriteLine("chkSwimBreast_Checked");
		}
		private void chkSwimBreast_Unchecked(object sender, RoutedEventArgs e)
		{
			Console.WriteLine("chkSwimBreast_Unchecked");
		}

		private void chkSwimBack_Checked(object sender, RoutedEventArgs e)
		{
			Console.WriteLine("chkSwimBack_Checked");
		}

		private void chkSwimBack_Unchecked(object sender, RoutedEventArgs e)
		{
			Console.WriteLine("chkSwimBack_Unchecked");
		}

		private void chkSwimButterfly_Checked(object sender, RoutedEventArgs e)
		{
			Console.WriteLine("chkSwimButterfly_Checked");
		}

		private void chkSwimButterfly_Unchecked(object sender, RoutedEventArgs e)
		{
			Console.WriteLine("chkSwimButterfly_Unchecked");
		}

		private void chkSwimBatakick_Checked(object sender, RoutedEventArgs e)
		{
			Console.WriteLine("chkSwimBatakick_Checked");
		}

		private void chkSwimBatakick_Unchecked(object sender, RoutedEventArgs e)
		{
			Console.WriteLine("chkSwimBatakick_Unchecked");
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
			Console.WriteLine("chkFromPoolside_Checked");
		}

		private void chkFromPoolside_Unchecked(object sender, RoutedEventArgs e)
		{
			Console.WriteLine("chkFromPoolside_Unchecked");
		}

		private void chkFromUnderwater_Checked(object sender, RoutedEventArgs e)
		{
			Console.WriteLine("chkFromUnderwater_Checked");
		}

		private void chkFromUnderwater_Unchecked(object sender, RoutedEventArgs e)
		{
			Console.WriteLine("chkFromUnderwater_Unchecked");
		}

		private void chkSwimCrawl_Click(object sender, RoutedEventArgs e)
		{
			Console.WriteLine("chkSwimCrawl_Click");
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
			Console.WriteLine("chkBreast_Click");
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
			Console.WriteLine("chkSwimBack_Click");
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
			Console.WriteLine("chkSwimButterfly_Click");
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
			Console.WriteLine("chkFromPoolside_Click");
			if (chkFromPoolside.IsChecked == true)
			{
				chkFromUnderwater.IsChecked = false;
			}
			uxDataGrid.DataContext = CreateData();
		}

		private void chkFromUnderwater_Click(object sender, RoutedEventArgs e)
		{
			Console.WriteLine("chkFromUnderwater_Click");
			if (chkFromUnderwater.IsChecked == true)
			{
				chkFromPoolside.IsChecked = false;
			}
			uxDataGrid.DataContext = CreateData();
		}
	}
}
