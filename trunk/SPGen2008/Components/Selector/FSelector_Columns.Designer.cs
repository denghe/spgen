﻿namespace SPGen2008.Components.Selector
{
    partial class FSelector_Columns
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
            this._DataGridView = new System.Windows.Forms.DataGridView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this._Cancel_button = new System.Windows.Forms.Button();
            this._Submit_button = new System.Windows.Forms.Button();
            this.PrimaryKey = new System.Windows.Forms.DataGridViewImageColumn();
            this._Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._Caption = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._Memo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this._DataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // _DataGridView
            // 
            this._DataGridView.AllowUserToAddRows = false;
            this._DataGridView.AllowUserToDeleteRows = false;
            this._DataGridView.AllowUserToOrderColumns = true;
            this._DataGridView.AllowUserToResizeRows = false;
            this._DataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._DataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._DataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PrimaryKey,
            this._Name,
            this._Caption,
            this._Memo});
            this._DataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this._DataGridView.Location = new System.Drawing.Point(12, 12);
            this._DataGridView.Name = "_DataGridView";
            this._DataGridView.ReadOnly = true;
            this._DataGridView.RowHeadersWidth = 26;
            this._DataGridView.RowTemplate.Height = 23;
            this._DataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._DataGridView.Size = new System.Drawing.Size(819, 382);
            this._DataGridView.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.DimGray;
            this.pictureBox1.Location = new System.Drawing.Point(13, 407);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(818, 1);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // _Cancel_button
            // 
            this._Cancel_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._Cancel_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._Cancel_button.Location = new System.Drawing.Point(756, 421);
            this._Cancel_button.Name = "_Cancel_button";
            this._Cancel_button.Size = new System.Drawing.Size(75, 23);
            this._Cancel_button.TabIndex = 5;
            this._Cancel_button.Text = "&Cancel";
            this._Cancel_button.UseVisualStyleBackColor = true;
            // 
            // _Submit_button
            // 
            this._Submit_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._Submit_button.Location = new System.Drawing.Point(661, 421);
            this._Submit_button.Name = "_Submit_button";
            this._Submit_button.Size = new System.Drawing.Size(75, 23);
            this._Submit_button.TabIndex = 5;
            this._Submit_button.Text = "&Submit";
            this._Submit_button.UseVisualStyleBackColor = true;
            // 
            // PrimaryKey
            // 
            this.PrimaryKey.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.PrimaryKey.HeaderText = "";
            this.PrimaryKey.MinimumWidth = 27;
            this.PrimaryKey.Name = "PrimaryKey";
            this.PrimaryKey.ReadOnly = true;
            this.PrimaryKey.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PrimaryKey.Width = 27;
            // 
            // _Name
            // 
            this._Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this._Name.HeaderText = "FieldName";
            this._Name.MinimumWidth = 155;
            this._Name.Name = "_Name";
            this._Name.ReadOnly = true;
            this._Name.Width = 155;
            // 
            // _Caption
            // 
            this._Caption.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this._Caption.HeaderText = "DisplayName";
            this._Caption.MinimumWidth = 155;
            this._Caption.Name = "_Caption";
            this._Caption.ReadOnly = true;
            this._Caption.Width = 155;
            // 
            // _Memo
            // 
            this._Memo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this._Memo.HeaderText = "Discription";
            this._Memo.MinimumWidth = 255;
            this._Memo.Name = "_Memo";
            this._Memo.ReadOnly = true;
            // 
            // FSelector_Columns
            // 
            this.AcceptButton = this._Submit_button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._Cancel_button;
            this.ClientSize = new System.Drawing.Size(843, 456);
            this.Controls.Add(this._Submit_button);
            this.Controls.Add(this._Cancel_button);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this._DataGridView);
            this.Name = "FSelector_Columns";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "表--字段选择器";
            this.Load += new System.EventHandler(this.FSelector_Columns_Load);
            ((System.ComponentModel.ISupportInitialize)(this._DataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView _DataGridView;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button _Cancel_button;
        private System.Windows.Forms.Button _Submit_button;
        private System.Windows.Forms.DataGridViewImageColumn PrimaryKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Caption;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Memo;

    }
}