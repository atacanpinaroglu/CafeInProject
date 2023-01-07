using Business.Abstract;
using Business.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public partial class OrderDetailsForm : Form
    {
        List<OrderDetail> _orderDetails;
        ICoffeeService _coffeeManager;
        ICafeService _cafeManager;
        public OrderDetailsForm(ICoffeeService coffeeManager, ICafeService cafeManager, List<OrderDetail> orderDetails)
        {
            InitializeComponent();
            _orderDetails = orderDetails;
            _coffeeManager = coffeeManager;
            _cafeManager = cafeManager;
        }
        private void OrderDetailsPage_Load(object sender, EventArgs e)
        {
            foreach (var item in _orderDetails)
            {
                var result = _coffeeManager.GetById(item.CoffeeId);
                var result2 = _cafeManager.GetById(result.Data.CafeId);
                Button button = new Button();
                button.Height = 40;
                button.Width = 392;
                if (_orderDetails.Count > 5)
                {
                    button.Width = 366;
                }
                button.ForeColor = Color.White;
                button.BackColor = Color.FromArgb(37, 40, 43);
                button.Text = result2.Data.CafeName + " - " + result.Data.Name + " - " + item.Amount + " adet - " + Convert.ToInt32(item.Price) + "₺";
                button.TextAlign = ContentAlignment.MiddleLeft;
                flowLayoutPanel1.Controls.Add(button);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }       
    }
}
