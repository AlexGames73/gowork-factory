namespace GoWorkFactoryViewAdministrator.Forms
{
    partial class MaterialsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonRemoveMaterial = new System.Windows.Forms.Button();
            this.buttonSettingMaterial = new System.Windows.Forms.Button();
            this.buttonCreateMaterial = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonRemoveMaterial
            // 
            this.buttonRemoveMaterial.Location = new System.Drawing.Point(588, 118);
            this.buttonRemoveMaterial.Name = "buttonRemoveMaterial";
            this.buttonRemoveMaterial.Size = new System.Drawing.Size(200, 38);
            this.buttonRemoveMaterial.TabIndex = 7;
            this.buttonRemoveMaterial.Text = "Удалить материал";
            this.buttonRemoveMaterial.UseVisualStyleBackColor = true;
            this.buttonRemoveMaterial.Click += new System.EventHandler(this.buttonRemoveMaterial_Click);
            // 
            // buttonSettingMaterial
            // 
            this.buttonSettingMaterial.Location = new System.Drawing.Point(588, 74);
            this.buttonSettingMaterial.Name = "buttonSettingMaterial";
            this.buttonSettingMaterial.Size = new System.Drawing.Size(200, 38);
            this.buttonSettingMaterial.TabIndex = 6;
            this.buttonSettingMaterial.Text = "Редактировать материал";
            this.buttonSettingMaterial.UseVisualStyleBackColor = true;
            this.buttonSettingMaterial.Click += new System.EventHandler(this.buttonSettingMaterial_Click);
            // 
            // buttonCreateMaterial
            // 
            this.buttonCreateMaterial.Location = new System.Drawing.Point(588, 30);
            this.buttonCreateMaterial.Name = "buttonCreateMaterial";
            this.buttonCreateMaterial.Size = new System.Drawing.Size(200, 38);
            this.buttonCreateMaterial.TabIndex = 5;
            this.buttonCreateMaterial.Text = "Создать материал";
            this.buttonCreateMaterial.UseVisualStyleBackColor = true;
            this.buttonCreateMaterial.Click += new System.EventHandler(this.buttonCreateMaterial_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(12, 30);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.Size = new System.Drawing.Size(566, 390);
            this.dataGridView.TabIndex = 4;
            // 
            // MaterialsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonRemoveMaterial);
            this.Controls.Add(this.buttonSettingMaterial);
            this.Controls.Add(this.buttonCreateMaterial);
            this.Controls.Add(this.dataGridView);
            this.Name = "MaterialsForm";
            this.Text = "MaterialsForm";
            this.Load += new System.EventHandler(this.MaterialsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonRemoveMaterial;
        private System.Windows.Forms.Button buttonSettingMaterial;
        private System.Windows.Forms.Button buttonCreateMaterial;
        private System.Windows.Forms.DataGridView dataGridView;
    }
}