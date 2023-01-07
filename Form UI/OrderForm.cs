using Business.Abstract;
using Business.Constans;
using Core.Utilities.Result;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Form_UI
{
    public partial class OrderForm : Form
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
        public OrderForm(ICafeService cafeManager, ICoffeeService coffeeManager, IOrderService orderManager, IOrderDetailService orderDetailManager, IUserService userManager, IUserDetailService userDetailManager, User user, UserDetail userDetail, List<OrderDetail> orderDetails)
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
        Image deleteIcon = Image.FromFile(Application.StartupPath + "\\Images\\exiticon.png");
        int orderPrice = 0;
        int index = 0;
        private void OrderForm_Load(object sender, EventArgs e)
        {
            if (_userDetail != null)
            {
                label11.Text = _userDetail.FirstName;
            }

            foreach (var item in _orderDetails)
            {
                item.OrderDetailId = index;
                var result = _coffeeManager.GetById(item.CoffeeId);
                var result2 = _cafeManager.GetById(result.Data.CafeId);
                Button button = new Button();
                button.Height = 40;
                button.Width = 398;
                if (_orderDetails.Count > 12)
                {
                    button.Width = 376;
                }
                button.ForeColor = Color.White;
                button.BackColor = Color.FromArgb(37, 40, 43);
                button.Text = result2.Data.CafeName + " - " + result.Data.Name + " - " + item.Amount + " adet - " + Convert.ToInt32(item.Price) + "₺";
                button.TextAlign = ContentAlignment.MiddleLeft;
                flowLayoutPanel1.Controls.Add(button);
                orderPrice += Convert.ToInt32(item.Price);

                Button btn = new Button();
                btn.Name = index.ToString();
                btn.Height = 40;
                btn.Width = 40;
                btn.Cursor = Cursors.Hand;
                btn.BackColor = Color.Firebrick;
                btn.BackgroundImage = deleteIcon;
                btn.BackgroundImageLayout = ImageLayout.Stretch;
                btn.Click += BtnClick;
                flowLayoutPanel1.Controls.Add(btn);
                index++;
            }
            label3.Text = orderPrice.ToString() + "₺";
            if (_orderDetails.Count == 0)
            {
                OrderIsEmpty();
            }
        }
        private void BtnClick(object sender, EventArgs e)
        {
            Button btn = (sender as Button);
            int index = flowLayoutPanel1.Controls.IndexOf(btn);
            flowLayoutPanel1.Controls.Remove(btn);
            flowLayoutPanel1.Controls.RemoveAt(index - 1);
            var orderDetail = _orderDetails.SingleOrDefault(o => o.OrderDetailId == Convert.ToInt32(btn.Name));
            orderPrice -= Convert.ToInt32(orderDetail.Price);
            _orderDetails.Remove(orderDetail);
            label3.Text = orderPrice.ToString() + "₺";
            if (_orderDetails.Count <= 12)
            {
                if (_orderDetails.Count == 0)
                {
                    OrderIsEmpty();
                }
                else
                {
                    for (int i = 0; i < flowLayoutPanel1.Controls.Count; i = i + 2)
                    {
                        flowLayoutPanel1.Controls[i].Width = 398;
                    }
                }               
            }          
        }

        private void OrderIsEmpty()
        {
            Button button = new Button();
            button.Height = 545;
            button.Width = 442;
            button.ForeColor = Color.White;
            button.BackColor = Color.FromArgb(37, 40, 43);
            button.Text = "Şu anda sepetinizde ürün bulunmamaktadır!";
            flowLayoutPanel1.Controls.Add(button);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            _orderDetails.Clear();
            orderPrice = 0;
            label3.Text = orderPrice.ToString() + "₺";
            OrderIsEmpty();
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (_orderDetails.Count > 0)
            {
                Order order = new Order
                {
                    UserId = _user.UserId,
                    OrderDate = DateTime.Now,
                    OrderPrice = orderPrice,
                };
                var result = _orderManager.Add(order);
                if (result.Success)
                {
                    var result2 = _orderManager.GetOrderId(order);
                    foreach (var item in _orderDetails)
                    {
                        OrderDetail orderDetail = new OrderDetail
                        {
                            OrderId = result2.Data,
                            CoffeeId = item.CoffeeId,
                            Amount = item.Amount,
                            Price = item.Price,
                        };
                        _orderDetailManager.Add(orderDetail);
                    }
                    MessageBox.Show(Messages.OrderSuccessful);
                    _orderDetails.Clear();
                    flowLayoutPanel1.Controls.Clear();
                    OrderIsEmpty();
                    orderPrice = 0;
                    label3.Text = orderPrice.ToString() + "₺";
                }                
            }
            else
            {
                MessageBox.Show(Messages.CardIsEmpty);
            }
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            HomePageForm homePageForm = new HomePageForm(_cafeManager, _coffeeManager, _orderManager, _orderDetailManager, _userManager, _userDetailManager, _user, _userDetail, _orderDetails);
            homePageForm.Show();
            this.Hide();
        }

        private void profileButton_Click(object sender, EventArgs e)
        {
            ProfileForm profileForm = new ProfileForm(_cafeManager, _coffeeManager, _orderManager, _orderDetailManager, _userManager, _userDetailManager, _user, _userDetail, _orderDetails);
            profileForm.Show();
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
