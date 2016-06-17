using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace sqllite
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        SQLiteAsyncConnection conn = new SQLiteAsyncConnection("AptechDB");
        private async void create_Click(object sender, RoutedEventArgs e)
        {
            await conn.CreateTableAsync<Employee>();
            MessageDialog msg = new MessageDialog("Database created");
            await msg.ShowAsync();
        }

        private async void add_Click(object sender, RoutedEventArgs e)
        {
            Employee emp = new Employee {name = box2.Text,age = box3.Text  };
            await conn.InsertAsync(emp);
            MessageDialog msg = new MessageDialog("Record created");
            await msg.ShowAsync();
        }

        private async void modify_Click(object sender, RoutedEventArgs e)
        {
            Employee emp = new Employee { name = box2.Text, age = box3.Text };
            emp.id = Convert.ToInt16(box1.Text);
            int pk = emp.id;
            var query = conn.Table<Employee>().Where(x => x.id==pk);
            var res = await query.ToListAsync();
            if(res.Count>0)
            {
                await conn.UpdateAsync(emp);
                MessageDialog msg = new MessageDialog("REcord update");
                await msg.ShowAsync();

            }
            else
            {
                MessageDialog msg = new MessageDialog(" nnoREcord update");
                await msg.ShowAsync();
            }
        }

        private async void select_Click(object sender, RoutedEventArgs e)
        {
            var query = conn.Table<Employee>();
            var res = await query.ToListAsync();
            lstrecord.ItemsSource = res;
        }

        private async void remove_Click(object sender, RoutedEventArgs e)
        {
            Employee emp = new Employee {id= Convert.ToInt16(box1.Text)};
            await conn.DeleteAsync(emp);

            MessageDialog msg = new MessageDialog("REcord Delete");
            await msg.ShowAsync();
        }

        private async void find_Click(object sender, RoutedEventArgs e)
        {
            int pk = Convert.ToInt16(box1.Text);
            var query = conn.Table<Employee>().Where(x => x.id == pk);
            var res = await query.ToListAsync();
            foreach (var items in res)
            {
                box1.Text = items.id.ToString();
                box2.Text = items.name.ToString();
                box3.Text = items.age.ToString();

            }
        }
    }
}
