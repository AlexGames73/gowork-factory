using GoWorkFactoryBusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            var user = userLogic.Read(new GoWorkFactoryBusinessLogic.BindingModels.UserBindingModel
            {
                Username = textBoxLogin.Text,
                Email = textBoxPassword.Text
            }).ToList()?[0];

            if (user != null)
            {
                Container.Resolve<CreateMaterialForm>();
                Close();
            }
            else
            {
                MessageBox.Show("Не правильно введен пароль или логин", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
    }
}
