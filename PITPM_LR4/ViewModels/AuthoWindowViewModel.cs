using PITPM_LR4.Infrastructure.Commands;
using PITPM_LR4.Models;
using PITPM_LR4.ViewModels.Base;
using PITPM_LR4.Views.Windows;
using PITPM_LR4.Views.Windows.Admins;
using PITPM_LR4.Views.Windows.Users;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace PITPM_LR4.ViewModels
{
    internal class AuthoWindowViewModel : ViewModel
    {

        #region Данные авторизации

        private string _Login;
        private string _Password;

        public string Login
        {
            get => _Login;
            set => Set(ref _Login, value);
        }

        public string Password
        {
            get => _Password;
            set => Set(ref _Password, value);
        }

        #endregion

        #region Команды

        #region Команда для перехода на страницу Регистрации

        public ICommand RegistrWindowCommand { get; }

        public bool CanRegistrWindowCommandExecute(object p) => true;

        public void OnRegistrWindowCommandExecuted(object p)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();

            Application.Current.MainWindow.Close();
        }

        #endregion

        #region Команда для входа в систему

        public ICommand AuthoCommand { get; }

        private bool CanAuthoCommandExecute(object p) => true;
        private void OnAuthoCommandExectuted(object p)
        {
            User user1 = null;
            if (Login == "" || Password == "")
            {
                MessageBox.Show("Введены не все данные");
            }
            else
            {
                using (Context db = new Context())
                {
                    if (db.Users.Count() > 0)
                    {
                        foreach (var user in db.Users.ToList())
                        {
                            if (Login == user.Login && Password == user.Password)
                            {
                                user1 = user;

                                switch (user1.Role)
                                {
                                    case "Пользователь":
                                        MainUserWindow mainUserWindow = new MainUserWindow();
                                        mainUserWindow.Show();
                                        break;
                                    case "Администратор":
                                        MainAdminWindow mainAdminWindow = new MainAdminWindow();
                                        mainAdminWindow.Show();
                                        break;
                                }
                                break;
                            }
                            else
                                continue;
                        }
                        if (user1 == null)
                        {
                            MessageBox.Show("Неверный логин или пароль");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль");
                    }
                }
                Application.Current.MainWindow.Close();
            }
        }

        #endregion

        #endregion

        public AuthoWindowViewModel()
        {
            #region Команды

            RegistrWindowCommand = new LambdaCommand(OnRegistrWindowCommandExecuted, CanRegistrWindowCommandExecute);
            AuthoCommand = new LambdaCommand(OnAuthoCommandExectuted, CanAuthoCommandExecute);

            #endregion
        }

    }
}
