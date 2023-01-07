using Business.Abstract;
using Business.Concrete;
using Business.Constans;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Form_UI
{
    public partial class ProfileForm : Form
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
        public ProfileForm(ICafeService cafeManager, ICoffeeService coffeeManager, IOrderService orderManager, IOrderDetailService orderDetailManager, IUserService userManager, IUserDetailService userDetailManager, User user, UserDetail userDetail, List<OrderDetail> orderDetails)
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
        private void ProfileForm_Load(object sender, EventArgs e)
        {          
            textBox1.Text = _user.Email;
            textBox2.Text = _user.Password;

            if (_userDetail != null)
            {
                label11.Text = _userDetail.FirstName;
                textBox3.Text = _userDetail.FirstName;
                textBox4.Text = _userDetail.LastName;
                textBox5.Text = _userDetail.Phone;
                textBox6.Text = _userDetail.Address;
            }

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
            else if (!textBox1.Text.EndsWith("@hotmail.com") && !textBox1.Text.EndsWith("@gmail.com"))
            {
                errorProvider1.SetError(textBox1, Messages.WrongEmail);
                textBox1.Focus();
            }
            else
            {
                if (_user.Email == textBox1.Text)
                {
                    if (_user.Password != textBox2.Text)
                    {
                        _user.Password = textBox2.Text;
                        var result = _userManager.Update(_user);
                        MessageBox.Show(result.Message);
                    }                    
                }
                else
                {                    
                    var result = _userManager.GetByEmail(textBox1.Text);
                    if (!result.Success)
                    {
                        User user = new User
                        {
                            UserId = _user.UserId,
                            Email = textBox1.Text,
                            Password = textBox2.Text
                        };
                        var result2 = _userManager.Update(user);
                        if (result2.Success)
                        {
                            _user = user;
                            MessageBox.Show(result2.Message);
                        }
                        else
                        {
                            textBox1.Clear();
                            textBox2.Clear();
                            textBox1.Focus();
                            checkBox1.Checked = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show(Messages.UserAlreadyExist);
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox1.Focus();
                        checkBox1.Checked = false;
                    }                   
                }               
            }           
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == string.Empty)
            {
                errorProvider1.SetError(textBox3, Messages.TextBoxEmpty);
                textBox3.Focus();
            }
            else if (textBox4.Text == string.Empty)
            {
                errorProvider1.SetError(textBox4, Messages.TextBoxEmpty);
                textBox4.Focus();
            }
            else if (textBox5.Text == string.Empty)
            {
                errorProvider1.SetError(textBox5, Messages.TextBoxEmpty);
                textBox5.Focus();
            }
            else if (textBox5.Text.Length != 11)
            {
                try
                {
                    int number = Convert.ToInt32(textBox5.Text); 
                    errorProvider1.SetError(textBox5, Messages.JustEleven);
                    textBox5.Focus();
                }
                catch (Exception)
                {
                    errorProvider1.SetError(textBox5, Messages.WrongNumber);
                    textBox5.Focus();
                }              
            }
            else if (textBox6.Text == string.Empty)
            {
                errorProvider1.SetError(textBox6, Messages.TextBoxEmpty);
                textBox6.Focus();
            }            
            else
            {
                if (_userDetail == null)
                {
                    UserDetail userDetail = new UserDetail
                    {
                        UserId = _user.UserId,
                        FirstName = textBox3.Text,
                        LastName = textBox4.Text,
                        Phone = textBox5.Text,
                        Address = textBox6.Text
                    };
                    var result = _userDetailManager.Add(userDetail);
                    if (result.Success)
                    {
                        _userDetail = userDetail;
                        MessageBox.Show(result.Message);
                    }
                }
                else
                {
                    UserDetail userDetail = new UserDetail
                    {   
                        UserDetailId = _userDetail.UserDetailId,
                        UserId = _user.UserId,
                        FirstName = textBox3.Text,
                        LastName = textBox4.Text,
                        Phone = textBox5.Text,
                        Address = textBox6.Text
                    };
                    if (_userDetail.FirstName != userDetail.FirstName || _userDetail.LastName != userDetail.LastName || _userDetail.Phone != userDetail.Phone || _userDetail.Address != userDetail.Address)
                    {
                        var result = _userDetailManager.Update(userDetail);                       
                        if (result.Success)
                        {
                            _userDetail = userDetail;
                            MessageBox.Show(result.Message);
                            label11.Text = _userDetail.FirstName;
                        }
                    }
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
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }
        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
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
