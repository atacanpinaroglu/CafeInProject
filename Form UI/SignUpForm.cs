using Business.Abstract;
using Business.Concrete;
using Business.Constans;
using DataAccess.Abstract;
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
    public partial class SignUpForm : Form
    {
        ICafeService _cafeManager;
        ICoffeeService _coffeeManager;
        IOrderService _orderManager;
        IOrderDetailService _orderDetailManager;
        IUserService _userManager;
        IUserDetailService _userDetailManager;
        public SignUpForm(ICafeService cafeManager, ICoffeeService coffeeManager, IOrderService orderManager, IOrderDetailService orderDetailManager, IUserService userManager, IUserDetailService userDetailManager)
        {
            InitializeComponent();
            _cafeManager = cafeManager;
            _coffeeManager = coffeeManager;
            _orderManager = orderManager;
            _orderDetailManager = orderDetailManager;
            _userManager = userManager;
            _userDetailManager = userDetailManager;            
        }       
        private void SignUpForm_Load(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
            textBox3.UseSystemPasswordChar = true;
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
            else if (textBox3.Text == string.Empty)
            {
                errorProvider1.SetError(textBox3, Messages.TextBoxEmpty);
                textBox3.Focus();
            }
            else if (!textBox1.Text.EndsWith("@hotmail.com") && !textBox1.Text.EndsWith("@gmail.com"))
            {
                errorProvider1.SetError(textBox1, Messages.WrongEmail);
                textBox1.Focus();
            }
            else
            {
                if (textBox2.Text != textBox3.Text)
                {
                    MessageBox.Show(Messages.PasswordsAreNotMatch);
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox2.Focus();
                    checkBox1.Checked = false;
                }
                else
                {
                    User user = new User { Email = textBox1.Text, Password = textBox2.Text };
                    var result = _userManager.Add(user);
                    MessageBox.Show(result.Message);
                    if (result.Success)
                    {
                        GoToLoginForm();
                    }
                    else
                    {
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();
                        textBox1.Focus();
                        checkBox1.Checked = false;
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
            {
                textBox2.UseSystemPasswordChar = false;
                textBox3.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
                textBox3.UseSystemPasswordChar = true;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GoToLoginForm();
        }

        private void GoToLoginForm()
        {
            LoginForm loginForm = new LoginForm(_cafeManager, _coffeeManager, _orderManager, _orderDetailManager, _userManager, _userDetailManager);
            loginForm.Show();
            this.Hide();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
