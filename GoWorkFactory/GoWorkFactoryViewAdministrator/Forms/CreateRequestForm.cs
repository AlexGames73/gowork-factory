using GoWorkFactoryBusinessLogic.BusinessLogics;
using GoWorkFactoryBusinessLogic.HelperModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace GoWorkFactoryViewAdministrator.Forms
{
    public partial class CreateRequestForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private Dictionary<int, (string, int)> materials;
        private readonly ReportLogic reportLogic;
        public CreateRequestForm(ReportLogic reportLogic)
        {
            InitializeComponent();
            this.reportLogic = reportLogic;
            materials = new Dictionary<int, (string, int)>();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<ProductMaterialForm>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (materials.ContainsKey(form.Id))
                {
                    materials[form.Id] = (form.ComponentName, materials[form.Id].Item2 + form.Count);
                }
                else
                {
                    materials.Add(form.Id, (form.ComponentName, form.Count));
                }
                LoadData();
            }
        }

        private void LoadData()
        {
            try
            {
                if (materials != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var pc in materials)
                    {
                        dataGridView.Rows.Add(new object[] { pc.Key, pc.Value.Item1, pc.Value.Item2 });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<ProductMaterialForm>();
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                form.Id = id;
                form.Count = materials[id].Item2;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    materials[form.Id] = (form.ComponentName, form.Count);
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {

                        materials.Remove(Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxEmail.Text))
            {
                if (!Regex.IsMatch(textBoxEmail.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                {
                    throw new Exception("Не правильный формат почты");
                }

                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (!(comboBoxTypeFile.SelectedIndex == 0 || comboBoxTypeFile.SelectedIndex == 1))
            {
                MessageBox.Show("Выберите тип файла", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (materials == null || materials.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (comboBoxTypeFile.SelectedIndex == 0)
                {
                    using (var dialog = new SaveFileDialog { Filter = "docx|*.docx" })
                    {
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            Stream file = reportLogic.SaveRequestMaterialsToDocFile(materials.Select(x => (x.Value.Item1, x.Value.Item2)).ToList());
                            MailLogic.MailSendAsync(new MailSendInfo
                            {
                                MailAddress = textBoxEmail.Text,
                                Subject = "Заявка на материалы",
                                Text = "Прикреплен файл, нужный материалов",
                                Attachments = new List<MailAttachment>
                                {
                                    new MailAttachment
                                    {
                                        ContentType = MimeTypes.Word,
                                        FileData = file,
                                        Name = "Завка на материалы"
                                    }
                                }
                            });
                        }
                    }
                }
                else if (comboBoxTypeFile.SelectedIndex == 1)
                {
                    Stream file = reportLogic.SaveRequestMaterialsToExcelFile(materials.Select(x => (x.Value.Item1, x.Value.Item2)).ToList());
                    MailLogic.MailSendAsync(new MailSendInfo
                    {
                        MailAddress = textBoxEmail.Text,
                        Subject = "Заявка на материалы",
                        Text = "Прикреплен файл, нужный материалов",
                        Attachments = new List<MailAttachment>
                        {
                            new MailAttachment
                            {
                                ContentType = MimeTypes.Excel,
                                FileData = file,
                                Name = "Завка на материалы"
                            }
                        }
                    });
                }


                MessageBox.Show("Заявка отправленна", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
