using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constans
{
    public static class Messages
    {
        public static string UserAlreadyExist = "Bu e-posta adresi zaten mevcut!\nLütfen başka bir e-posta adresi giriniz.";
        public static string UserNotExist = "Kullanıcı bulunamadı!\nLütfen e-posta adresini doğru giriniz.";
        public static string WrongPassword = "Şifre hatalı!\nLütfen şifreyi doğru giriniz.";      
        public static string SignUpSuccesful = "Kayıt başarılı!\nGiriş ekranına yönlendiriliyorsunuz.";
        public static string TextBoxEmpty = "Bu alan boş bırakılamaz!";
        public static string WrongEmail = "Girdiğiniz e-posta adresi geçersiz!\nLütfen geçerli bir e-posta giriniz.";
        public static string WrongNumber = "Lütfen geçerli bir telefon numarası giriniz!";
        public static string JustEleven = "Telefon numarası 11 haneli olmalıdır!";
        public static string PasswordsAreNotMatch = "Girdiğiniz şifreler uyuşmamaktadır!";
        public static string UpdateAccountSettings = "Hesap ayarları güncellendi!";
        public static string AddAccountDetails = "Profil detayları kaydedildi!";
        public static string UpdateAccountDetails = "Profil detayları güncellendi!";
        public static string OrderSuccessful = "Siparişiniz başarılı bir şekilde alınmıştır!";
        public static string CardIsEmpty = "Şu anda sepetiniz boş!\nSipariş verebilmek için lütfen sepete ürün ekleyin.";
        public static string AccountDetailsEmpty = "Sipariş verebilmek için lütfen profil detaylarınızı güncelleyin.";
    }
}
