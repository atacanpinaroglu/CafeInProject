using Business.Abstract;
using Business.Concrete;
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
    public partial class StarbucksForm : Form
    {
        User _user;
        UserDetail _userDetail;
        List<OrderDetail> _orderDetails;
        List<Coffee> _starbucksCoffees;
        ICafeService _cafeManager;
        ICoffeeService _coffeeManager;
        IOrderService _orderManager;
        IOrderDetailService _orderDetailManager;
        IUserService _userManager;
        IUserDetailService _userDetailManager;
        public StarbucksForm(ICafeService cafeManager, ICoffeeService coffeeManager, IOrderService orderManager, IOrderDetailService orderDetailManager, IUserService userManager, IUserDetailService userDetailManager, User user, UserDetail userDetail, List<OrderDetail> orderDetails)
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
        private void StarbucksForm_Load(object sender, EventArgs e)
        {
            if (_userDetail != null)
            {
                label11.Text = _userDetail.FirstName;
            }

            var result = _coffeeManager.GetAllByCafeId(3);
            if (result.Success)
            {
                _starbucksCoffees = result.Data;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var coffee = _starbucksCoffees[0];
            AddToCard(coffee, amount1);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var coffee = _starbucksCoffees[1];
            AddToCard(coffee, amount2);

        }
        private void button3_Click(object sender, EventArgs e)
        {
            var coffee = _starbucksCoffees[2];
            AddToCard(coffee, amount3);

        }
        private void button4_Click(object sender, EventArgs e)
        {
            var coffee = _starbucksCoffees[3];
            AddToCard(coffee, amount4);

        }
        private void button5_Click(object sender, EventArgs e)
        {
            var coffee = _starbucksCoffees[4];
            AddToCard(coffee, amount5);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            var coffee = _starbucksCoffees[5];
            AddToCard(coffee, amount6);

        }
        private void AddToCard(Coffee coffee, Label label)
        {
            if (label.Text == "0")
            {
                MessageBox.Show("Lütfen sepete eklemek istediğiniz miktarı seçin!");
            }
            else
            {
                OrderDetail orderDetail = new OrderDetail { CoffeeId = coffee.CoffeeId, Amount = Convert.ToInt16(label.Text), Price = coffee.UnitPrice * Convert.ToInt16(label.Text) };
                _orderDetails.Add(orderDetail);
                MessageBox.Show(Convert.ToInt16(label.Text) + " adet " + coffee.Name + " sepete eklendi.");              
            }
            label.Text = "0";
        }

        private void add1_Click(object sender, EventArgs e)
        {
            IncreaseAmount(amount1);
        }
        private void remove1_Click(object sender, EventArgs e)
        {
            ReduceAmount(amount1);
        }
        private void add2_Click(object sender, EventArgs e)
        {
            IncreaseAmount(amount2);
        }
        private void remove2_Click(object sender, EventArgs e)
        {
            ReduceAmount(amount2);
        }
        private void add3_Click(object sender, EventArgs e)
        {
            IncreaseAmount(amount3);
        }
        private void remove3_Click(object sender, EventArgs e)
        {
            ReduceAmount(amount3);
        }
        private void add4_Click(object sender, EventArgs e)
        {
            IncreaseAmount(amount4);
        }
        private void remove4_Click(object sender, EventArgs e)
        {
            ReduceAmount(amount4);
        }
        private void add5_Click(object sender, EventArgs e)
        {
            IncreaseAmount(amount5);
        }
        private void remove5_Click(object sender, EventArgs e)
        {
            ReduceAmount(amount5);
        }
        private void add6_Click(object sender, EventArgs e)
        {
            IncreaseAmount(amount6);
        }
        private void remove6_Click(object sender, EventArgs e)
        {
            ReduceAmount(amount6);
        }
        private void IncreaseAmount(Label label)
        {
            short amount = Convert.ToInt16(label.Text);
            if (amount < 99)
            {
                amount++;
                if (amount > 9)
                {
                    label.Location = new Point(405, label.Location.Y);
                }
                label.Text = amount.ToString();
            }
        }
        private void ReduceAmount(Label label)
        {
            short amount = Convert.ToInt16(label.Text);
            if (amount > 0)
            {
                amount--;
                if (amount < 10)
                {
                    label.Location = new Point(411, label.Location.Y);
                }
                label.Text = amount.ToString();
            }
        }

        private void profileButton_Click(object sender, EventArgs e)
        {
            ProfileForm profileForm = new ProfileForm(_cafeManager, _coffeeManager, _orderManager, _orderDetailManager, _userManager, _userDetailManager, _user, _userDetail, _orderDetails);
            profileForm.Show();
            this.Hide();
        }
        private void homeButton_Click(object sender, EventArgs e)
        {
            HomePageForm homePageForm = new HomePageForm(_cafeManager, _coffeeManager, _orderManager, _orderDetailManager, _userManager, _userDetailManager, _user, _userDetail, _orderDetails);
            homePageForm.Show();
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
