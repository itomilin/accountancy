using AccountancyCRUD.Controller;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using iTextSharp;
using System.IO;
using iTextSharp.text.pdf;

namespace AccountancyCRUD.View
{
    public partial class MainForm : Form
    {
        public DataGridView GetGrid => grid;

        public ComboBox GetCombo => cbTableNames;

        public string NetWorthDate => dtpNetWorth.Value.Date.ToString("yyyy.MM.dd");

        public string SetNetWorth
        {
            set => tbNetWorthResult.Text = value;
        }

        public Button NetWorthButton => btnNetWorth;

        public Button GetDeleteButton => btnDelete;

        public Button GetInsertButton => btnInsert;

        public MainForm()
        {
            InitializeComponent();
            grid.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            grid.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.DisplayedCells;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveDialog.DefaultExt = "pdf";
            saveDialog.Filter =
            "Pdf files (*.pdf)|*.pdf|All files (*.*)|*.*";
            if (saveDialog.ShowDialog() == DialogResult.Cancel) return;

            var filename = saveDialog.FileName;

            iTextSharp.text.Document doc = new iTextSharp.text.Document();
            using (var writer = PdfWriter.GetInstance(doc, new FileStream(filename, FileMode.Create)))
            {
                string content = $"Obtained net worth by factory since " +
                    $"{dtpNetWorth.Value.Date.ToShortDateString()}, \n" +
                    $"till nowaday - {DateTime.Now.Date.ToShortDateString()}, consist " +
                    $"{tbNetWorthResult.Text}";
                doc.Open();
                var helvetica = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14);
                var helveticaBase = helvetica.GetCalculatedBaseFont(false);
                writer.DirectContent.BeginText();
                writer.DirectContent.SetFontAndSize(helveticaBase, 12f);
                writer.DirectContent.ShowTextAligned(iTextSharp.text.Element.ALIGN_LEFT,
                    content,
                    80, 766, 0);
                writer.DirectContent.EndText();
                doc.Close();
                writer.Close();
                //File.WriteAllText(filename, );
            }
        }
    }
}
