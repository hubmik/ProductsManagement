using CustomerApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace ClientApp.Models
{
    public class InvoiceGenerator
    {
        private string GenerateInvoiceNumber()
        {
            Random rand = new Random();
            string invoiceNo = "FV";
            for (int i = 0; i < 4; i++)
            {
                invoiceNo += rand.Next(0, 9);
            }
            return invoiceNo;
        }

        private string CreateDataString()
        {
            string data = GenerateInvoiceNumber();
            data += $"";
            return data;
        }

        public void CreateInvoice(List<OrderedProducts> orderedProducts, int custId)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            Document doc = new Document(PageSize.LETTER, 10, 10, 25, 15);
            PdfWriter pdfWriter = PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
            doc.Open();
            iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph("Pierwszy paragrafik hihi.");            
            doc.Add(paragraph);
            doc.Close();
        }

        public void AddInvoiceToDatabase()
        {
            using (var context = new ApplicationDbContext())
            {

            }
        }
    }
}
