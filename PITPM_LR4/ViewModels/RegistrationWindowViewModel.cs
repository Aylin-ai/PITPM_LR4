using PITPM_LR4.Infrastructure.Commands;
using PITPM_LR4.Models;
using PITPM_LR4.ViewModels.Base;
using PITPM_LR4.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PITPM_LR4.ViewModels
{
    internal class RegistrationWindowViewModel : ViewModel
    {

        #region Роли в системе

        public List<string> Roles { get; set; } = new List<string>() { "Пользователь", "Администратор" };

        #endregion

        #region Данные для Регистрации

        private string _Login;
        private string _Password1;
        private string _Password2;
        public string _Role = "Пользователь";

        public string Login { get => _Login; set => Set(ref _Login, value);}
        public string Password1 { get => _Password1; set => Set(ref _Password1, value);}
        public string Password2 { get => _Password2; set => Set(ref _Password2, value);}
        public string Role { get => _Role; set => Set(ref _Role, value);}

        #endregion

        #region Команды

        #region Команда регистрации пользователя

        public ICommand RegistrCommand { get; }

        private bool CanRegistrCommandExecute(object p) => true;
        public void OnRegistrCommandExectuted(object p)
        {
            if (!(Login == "" || Password1 == "" || Password2 == ""))
            {
                if (Password1 == Password2)
                {
                    using (Context db = new Context())
                    {
                        if (db.Users.Count() > 0)
                        {
                            foreach (var user in db.Users.ToList())
                            {
                                if (Login == user.Login)
                                {
                                    MessageBox.Show("Данный логин уже занят");
                                    Login = "";
                                    break;
                                }
                                else
                                {
                                    User user1 = new User() { Id = db.Users.Count() + 1, Login = Login, Password = Password1, Role = Role};

                                    db.Users.Add(user1);
                                    db.SaveChanges();

                                    MessageBox.Show($"Пользователь {user1.Login} успешно зарегистрирован");
                                    break;
                                }
                            }
                        }
                        else
                        {
                            User user1 = new User() { Id = db.Users.Count() + 1, Login = Login, Password = Password1, Role = Role };

                            db.Users.Add(user1);
                            db.SaveChanges();

                            MessageBox.Show($"Пользователь {user1.Login} успешно зарегистрирован");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Пароли не совпадают");
                    Password1 = "";
                    Password2 = "";
                }
            }
            else
                MessageBox.Show("Не введены все данные");

            AuthoWindow authoWindow = new AuthoWindow();
            authoWindow.Show();

            this.OnClosingRequest();
        }

        #endregion

        #endregion

        public RegistrationWindowViewModel()
        {
            #region Команды

            RegistrCommand = new LambdaCommand(OnRegistrCommandExectuted, CanRegistrCommandExecute);

            #endregion
        }

    }
}
