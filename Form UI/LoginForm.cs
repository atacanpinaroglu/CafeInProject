using Business.Abstract;
using Business.Constans;
using Entities.Concrete;
using System.DirectoryServices.ActiveDirectory;
using System.Windows.Forms;

namespace Form_UI
{
    public partial class LoginForm : Form
    {
        ICafeService _cafeManager;
        ICoffeeService _coffeeManager;
        IOrderService _orderManager;
        IOrderDetailService _orderDetailManager;
        IUserService _userManager;
        IUserDetailService _userDetailManager;
        public LoginForm(ICafeService cafeManager, ICoffeeService coffeeManager, IOrderService orderManager, IOrderDetailService orderDetailManager, IUserService userManager, IUserDetailService userDetailManager)
        {
            InitializeComponent();
            _cafeManager = cafeManager;
            _coffeeManager = coffeeManager;
            _orderManager = orderManager;
            _orderDetailManager = orderDetailManager;
            _userManager = userManager;
            _userDetailManager = userDetailManager;            
        }
        private void LoginForm_Load(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
        }       
       
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
            {
                errorProvider1.SetError(textBox1, Messages.TextBoxEmpty);
                textBox1.Focus();
            }
            else if (textBox2.Text == string.Empty)
            {
                errorProvider1.SetError(textBox2, Messages.TextBoxEmpty);
                textBox2.Focus();
            }
            else
            {
                var result = _userManager.GetByEmail(textBox1.Text);
                if (result.Success)
                {
                    var user = result.Data;
                    if (user.Password == textBox2.Text)
                    {
                        UserDetail userDetail = new UserDetail();
                        List<OrderDetail> orderDetails = new List<OrderDetail>();
                        var result2 = _userDetailManager.GetByUserId(user.UserId);
                        if (result2.Success)
                        {
                            userDetail = result2.Data;
                        }
                        else
                        {
                            userDetail = null;
                        }
                        HomePageForm homePageForm = new HomePageForm(_cafeManager, _coffeeManager, _orderManager, _orderDetailManager, _userManager, _userDetailManager, user, userDetail, orderDetails);
                        homePageForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show(Messages.WrongPassword);
                        textBox2.Clear();
                        textBox2.Focus();
                        checkBox1.Checked = false;
                    }
                }
                else
                {
                    MessageBox.Show(result.Message);
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox1.Focus();
                    checkBox1.Checked = false;
                }
            }          
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SignUpForm signUpForm = new SignUpForm(_cafeManager, _coffeeManager, _orderManager, _orderDetailManager, _userManager, _userDetailManager);
            signUpForm.Show();
            this.Hide();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}