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
    public partial class ProductMaterialForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id
        {
            get { return Convert.ToInt32(comboBoxMaterials.SelectedValue); }
            set { comboBoxMaterials.SelectedValue = value; }
        }
        public string ComponentName { get { return comboBoxMaterials.Text; } }
        public int Count
        {
            get { return Convert.ToInt32(textBoxCount.Text); }
            set
            {
                textBoxCount.Text = value.ToString();
            }
        }
        public ProductMaterialForm(IMaterialLogic materialLogic)
        {
            InitializeComponent();
            var materials = materialLogic.Read(null);
            if (materials != null)
            {
                comboBoxMaterials.DisplayMember = "NameMaterial";
                comboBoxMaterials.ValueMember = "Id";
                comboBoxMaterials.DataSource = materialLogic;
                comboBoxMaterials.SelectedItem = null;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxMaterials.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
