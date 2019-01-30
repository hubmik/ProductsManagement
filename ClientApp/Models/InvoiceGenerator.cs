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
    public class InvoiceGenerator : InvoiceComponents
    {
        private string GenerateInvoiceNumber()
        {
            List<string> invoicesNo = null;
            using (var context = new ApplicationDbContext())
            {
                invoicesNo = context.Invoices
                    .Select(x => x.InvoiceNumber).ToList();
            }
            Random rand = new Random();
            string invoiceNo = "FV";
            for (int i = 0; i < 5; i++)
            {
                invoiceNo += rand.Next(0, 9);
            }
            if (invoicesNo.Contains(invoiceNo))
                GenerateInvoiceNumber();

            return invoiceNo;
        }

        private string CreateDataString(InvoiceComponents invoiceComponents, int id)
        {
            OrderedProductsStorage orderedProductsStorage = new OrderedProductsStorage();
            invoiceComponents.CompanyName = "MerchanApp";
            invoiceComponents.Country = "Poland";
            invoiceComponents.City = "Opole";
            invoiceComponents.Street = "Jana Bytnara 11e/12";
            //invoiceComponents.InvoiceDate = Convert.ToDateTime(invoiceComponents.InvoiceDate.ToString("yyyy-MM-dd"));
            string data = $"INVOICE NUMBER: {invoiceComponents.InvoiceNumber}";
            data += $"\nINVOICE DATE: {invoiceComponents.InvoiceDate.ToShortDateString()}\n";
            data += $"\nDEALER: {invoiceComponents.CompanyName}" +
                $"\n{invoiceComponents.Country}" +
                $"\n{invoiceComponents.City}" +
                $"\n{invoiceComponents.Street}\n" +
                $"\nCUSTOMER: {invoiceComponents.CustomerCompanyName}\n" +
                $"{invoiceComponents.CustomerCountry}\n" +
                $"{invoiceComponents.CustomerCity}\n" +
                $"{invoiceComponents.CustomerStreet}";

            data += "\n\n\n";
            data += $"TOTAL VALUE: {orderedProductsStorage.GetTotalValue(id).ToString()}zl";

            return data;
        }

        public void CreateInvoice(List<OrderedProductsStorage> orderedProducts, InvoiceComponents pdfInvoice, Invoices invoice, int orderId)
        {
            
            pdfInvoice.InvoiceNumber = GenerateInvoiceNumber();
            pdfInvoice.InvoiceDate = invoice.InvoiceDate;
            invoice.InvoiceNumber = pdfInvoice.InvoiceNumber;

            string StartPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string path = $"\\Invoice{pdfInvoice.InvoiceNumber}.pdf";

            Document doc = new Document(PageSize.LETTER, 10, 10, 25, 15);
            PdfWriter pdfWriter = PdfWriter.GetInstance(doc, new FileStream(StartPath + path, FileMode.Create));
            doc.Open();

            iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph(CreateDataString(
                pdfInvoice,
                orderId
                ));

            PdfPTable table = new PdfPTable(3)
            {
                TotalWidth = 260f,
                LockedWidth = true
            };

            float[] widths = new float[] { 1f, 1f, 1f };
            table.SetWidths(widths);
            table.HorizontalAlignment = 1;
            table.AddCell(new Phrase("Product Name"));
            table.AddCell(new Phrase("Quantity"));
            table.AddCell(new Phrase("Value"));

            PdfPCell cell = new PdfPCell();
            //cell = new PdfPCell(new Phrase("Products"));
            cell.HorizontalAlignment = 1;

            for (int i = 0; i < orderedProducts.Count; i++)
            {
                table.AddCell(orderedProducts[i].ProductName);
                table.AddCell(orderedProducts[i].Quantity.ToString());
                table.AddCell(orderedProducts[i].OrderedValue.ToString());
            }

            doc.Add(paragraph);
            doc.Add(table);
            doc.Close();
            AddInvoiceToDatabase(invoice, orderId);
        }

        private void AddInvoiceToDatabase(Invoices invoice, int orderId)
        {
            Invoices invoiceToAdd;
            using (var context = new ApplicationDbContext())
            {
                invoiceToAdd = new Invoices()
                {
                    OrderId = orderId,
                    InvoiceNumber = invoice.InvoiceNumber,
                    InvoiceDate = invoice.InvoiceDate
                };

                context.Invoices.Add(invoiceToAdd);
                context.SaveChanges();
            }
        }
    }
}
