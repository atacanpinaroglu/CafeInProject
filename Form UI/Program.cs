using Business.Concrete;
using DataAccess.Concrete.EntityFramework;

namespace Form_UI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new LoginForm(new CafeManager(new EfCafeDal()), new CoffeeManager(new EfCoffeeDal()), new OrderManager(new EfOrderDal()), new OrderDetailManager(new EfOrderDetailDal()), new UserManager(new EfUserDal()), new UserDetailManager(new EfUserDetailDal())));
        }
    }
}