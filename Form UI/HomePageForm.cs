using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Form_UI
{
    public partial class HomePageForm : Form
    {
        User _user;
        UserDetail _userDetail;
        List<OrderDetail> _orderDetails;
        ICafeService _cafeManager;
        ICoffeeService _coffeeManager;
        IOrderService _orderManager;
        IOrderDetailService _orderDetailManager;
        IUserService _userManager;
        IUserDetailService _userDetailManager;
        public HomePageForm(ICafeService cafeManager, ICoffeeService coffeeManager, IOrderService orderManager, IOrderDetailService orderDetailManager, IUserService userManager, IUserDetailService userDetailManager, User user, UserDetail userDetail, List<OrderDetail> orderDetails)
        {
            InitializeComponent();
            _cafeManager = cafeManager;
            _coffeeManager = coffeeManager;
            _orderManager = orderManager;
            _orderDetailManager = orderDetailManager;
            _userManager = userManager;
            _userDetailManager = userDetailManager;
            _user = user;
            _userDetail = userDetail;
            _orderDetails = orderDetails;
        }
        Image viewIcon = Image.FromFile(Application.StartupPath + "\\Images\\viewicon.png");
        private void HomePageForm_Load(object sender, EventArgs e)
        {
            if (_userDetail != null)
            {
                label11.Text = _userDetail.FirstName;
            }

            var result = _orderManager.GetAllByUserId(_user.UserId);
            if (result.Success)
            {
                foreach (var item in result.Data)
                {                    
                    Button button = new Button();
                    button.Height = 40;
                    button.Width = 358;
                    button.BackColor = Color.FromArgb(37, 40, 43);
                    button.ForeColor = Color.White;
                    button.Text = "Sipariş tarihi: " + item.OrderDate.ToString() + " - Tutarı: " + Convert.ToInt32(item.OrderPrice).ToString() + "₺";
                    button.TextAlign = ContentAlignment.MiddleLeft;
                    flowLayoutPanel1.Controls.Add(button);

                    Button btn = new Button();
                    btn.Name = item.OrderId.ToString();
                    btn.Height = 40;
                    btn.Width = 40;
                    btn.Cursor = Cursors.Hand;
                    btn.BackColor = Color.FromArgb(128, 128, 255);
                    btn.BackgroundImage = viewIcon;
                    btn.BackgroundImageLayout = ImageLayout.Stretch;
                    btn.Click += ViewOrderDetails;
                    flowLayoutPanel1.Controls.Add(btn);
                }
            }
            else
            {
                Button button = new Button();
                button.Height = 120;
                button.Width = 398;
                button.ForeColor = Color.White;
                button.BackColor = Color.FromArgb(37, 40, 43);
                button.Text = "Sistemde kayıtlı bir siparişiniz bulunmamaktadır!";
                flowLayoutPanel1.Controls.Add(button);
            }
        }
        private void ViewOrderDetails(object sender, EventArgs e)
        {           
            Button btn = (sender as Button);
            var result = _orderDetailManager.GetAllByOrderId(Convert.ToInt32(btn.Name));
            if (result.Success)
            {
                OrderDetailsForm orderDetailsPage = new OrderDetailsForm(_coffeeManager, _cafeManager, result.Data);
                orderDetailsPage.ShowDialog();
            }           
        }

        private void starButton_Click(object sender, EventArgs e)
        {
            GoToStarbucksForm();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            GoToStarbucksForm();
        }
        private void label1_Click(object sender, EventArgs e)
        {
            GoToStarbucksForm();
        }
        private void label5_Click(object sender, EventArgs e)
        {
            GoToStarbucksForm();
        }
        private void label6_Click(object sender, EventArgs e)
        {
            GoToStarbucksForm();
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            GoToStarbucksForm();
        }

        private void pabloButton_Click(object sender, EventArgs e)
        {
            GoToPabloForm();
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            GoToPabloForm();
        }
        private void label2_Click(object sender, EventArgs e)
        {
            GoToPabloForm();
        }
        private void label7_Click(object sender, EventArgs e)
        {
            GoToPabloForm();
        }
        private void label8_Click(object sender, EventArgs e)
        {
            GoToPabloForm();
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            GoToPabloForm();
        }

        private void gloriaButton_Click(object sender, EventArgs e)
        {
            GoToGloriaForm();
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            GoToGloriaForm();
        }
        private void label4_Click(object sender, EventArgs e)
        {
            GoToGloriaForm();
        }
        private void label9_Click(object sender, EventArgs e)
        {
            GoToGloriaForm();
        }
        private void label10_Click(object sender, EventArgs e)
        {
            GoToGloriaForm();
        }
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            GoToGloriaForm();
        }

        private void GoToStarbucksForm()
        {
            StarbucksForm starbucksForm = new StarbucksForm(_cafeManager, _coffeeManager, _orderManager, _orderDetailManager, _userManager, _userDetailManager, _user, _userDetail, _orderDetails);
            starbucksForm.Show();
            this.Hide();
        }
        private void GoToPabloForm()
        {
            PabloForm pabloForm = new PabloForm(_cafeManager, _coffeeManager, _orderManager, _orderDetailManager, _userManager, _userDetailManager, _user, _userDetail, _orderDetails);
            pabloForm.Show();
            this.Hide();
        }
        private void GoToGloriaForm()
        {
            GloriaForm gloriaForm = new GloriaForm(_cafeManager, _coffeeManager, _orderManager, _orderDetailManager, _userManager, _userDetailManager, _user, _userDetail, _orderDetails);
            gloriaForm.Show();
            this.Hide();
        }

        private void profileButton_Click(object sender, EventArgs e)
        {
            ProfileForm profileForm = new ProfileForm(_cafeManager, _coffeeManager, _orderManager, _orderDetailManager, _userManager, _userDetailManager, _user, _userDetail, _orderDetails);
            profileForm.Show();
            this.Hide();
        }
        private void orderButton_Click(object sender, EventArgs e)
        {
            OrderForm orderForm = new OrderForm(_cafeManager, _coffeeManager, _orderManager, _orderDetailManager, _userManager, _userDetailManager, _user, _userDetail, _orderDetails);
            orderForm.Show();
            this.Hide();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm(_cafeManager, _coffeeManager, _orderManager, _orderDetailManager, _userManager, _userDetailManager);
            loginForm.Show();
            this.Hide();
        }
    }
}
