using System;
using System.Windows.Media.Imaging;

namespace beatsloves.Models
{
    public static class Session
    {
        private static int _userId;
        public static int UserID
        {
            get => _userId;
            set => _userId = value;
        }

        private static int _roleId;
        public static int RoleID
        {
            get => _roleId;
            set => _roleId = value;
        }

        private static decimal _balance;
        public static decimal Balance
        {
            get => _balance;
            set
            {
                if (_balance != value)
                {
                    _balance = value;
                    OnBalanceChanged?.Invoke();
                }
            }
        }

        private static BitmapImage _imageSource;
        public static BitmapImage imageSource
        {
            get => _imageSource;
            set => _imageSource = value;
        }

        public static event Action OnBalanceChanged;
    }
}
