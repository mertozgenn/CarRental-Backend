using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public class Messages
    {
        public static string Added = "Eklendi";
        public static string Deleted = "Silindi";
        public static string CarNameInvalid = "Araba ismi geçersiz";
        public static string MaintenanceTime = "Sistem bakımda";
        public static string CarsListed = "Arabalar listelendi";
        public static string Updated = "Güncellendi";
        public static string PhotoLimitExceeded = "Bir araba için en fazla 5 fotoğraf eklenebilir";
        public static string RentalSucceeded = "Kiralama işlemi tamamlandı";
        public static string RentalFailed = "Araba teslim edilmemiş";
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Şifre hatalı";
        public static string SuccessfulLogin = "Sisteme giriş başarılı";
        public static string UserAlreadyExists = "Bu kullanıcı zaten mevcut";
        public static string UserRegistered = "Kullanıcı başarıyla kaydedildi";
        public static string AccessTokenCreated = "Access token başarıyla oluşturuldu";
        public static string AuthorizationDenied = "Yetkiniz yok";
    }
}
