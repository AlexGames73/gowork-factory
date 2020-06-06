using GoWorkFactoryBusinessLogic.BindingModels;
using GoWorkFactoryBusinessLogic.BusinessLogics;
using GoWorkFactoryBusinessLogic.HelperModels;
using GoWorkFactoryBusinessLogic.Interfaces;
using Microsoft.Reporting.WinForms;
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
    public partial class ReportForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ReportLogic reportLogic;
        public ReportForm(ReportLogic reportLogic)
        {
            InitializeComponent();
            this.reportLogic = reportLogic;
        }

        private void buttonGenerateData_Click(object sender, EventArgs e)
        {
            try
            {

                var reportRequestOrders = reportLogic.GetRequestsOrders(dateTimePickerTo.Value, dateTimePickerFrom.Value); 
                if (reportRequestOrders != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var pc in reportRequestOrders)
                    {
                        dataGridView.Rows.Add(new object[] { pc.Date, pc.Type, pc.NameMaterial.ElementAtOrDefault(0), pc.Count.ElementAtOrDefault(0), pc.Price.ElementAtOrDefault(0) });
                        for (int i = 1; i < pc.NameMaterial.Count; i++)
                        {
                            dataGridView.Rows.Add(new object[] { "", "", pc.NameMaterial.ElementAtOrDefault(i), pc.Count.ElementAtOrDefault(i), pc.Type == "Заказ" ? "" : pc.Price.ElementAtOrDefault(i).ToString() });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        [Obsolete]
        private void buttonInExcel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxEmail.Text))
            {
                if (!Regex.IsMatch(textBoxEmail.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                {
                    MessageBox.Show("Не правильный формат почты", "Ошибка", MessageBoxButtons.OK, 
                        MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                return;
            }


            Stream file = reportLogic.SaveRequestOrderToPdfFile(dateTimePickerTo.Value, dateTimePickerFrom.Value);
            MailLogic.MailSendAsync(new MailSendInfo
            {
                MailAddress = textBoxEmail.Text,
                Subject = "Отчет по заявкам и материалам",
                Text = "Прикреплен файл по заявкам и материалам",
                Attachments = new List<MailAttachment>
                {
                    new MailAttachment
                    {
                        ContentType = MimeTypes.Pdf,
                        FileData = file,
                        Name = "Отчет по заявкам и материалам"
                    }
                }
            });

            MessageBox.Show("Заявка отправленна", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
        }
    }
}
