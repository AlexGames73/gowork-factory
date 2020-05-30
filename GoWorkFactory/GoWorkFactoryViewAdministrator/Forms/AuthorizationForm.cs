using GoWorkFactoryBusinessLogic.Interfaces;
using GoWorkFactoryDataBase.Models;
using System;
using System.Linq;
using System.Windows.Forms;
using Unity;

namespace GoWorkFactoryViewAdministrator.Forms
{
    public partial class AuthorizationForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly IUserLogic userLogic;
        public AuthorizationForm(IUserLogic userLogic)
        {
            InitializeComponent();
            this.userLogic = userLogic;
        }

        private void buttonAuthorization_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxLogin.Text))
            {
                MessageBox.Show("Введите логин", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPassword.Text))
            {
                MessageBox.Show("Введите пароль", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }

            var users = userLogic.Read(new GoWorkFactoryBusinessLogic.BindingModels.UserBindingModel
            {
                Username = textBoxLogin.Text,
                Password = textBoxPassword.Text
            }).ToList();

            if (users != null && users.Count > 0)
            {
                var curUser = users[0];
                if (curUser.Role == GoWorkFactoryBusinessLogic.Enums.UserRole.Admin)
                {
                    Program.Admin = curUser;
                    Close();
                }
                else
                {
                    MessageBox.Show("Пользователь не является админомs", "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Не правильно введен пароль или логин", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
    }
}
