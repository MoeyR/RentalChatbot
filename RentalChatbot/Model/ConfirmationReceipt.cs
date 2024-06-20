using System;

namespace RentalChatbot
{
    public partial class Program
    {
        
            /// <summary>
            /// Puchase Confirmation struct for displaying formated receipt after user make payment
            /// </summary>
            public struct ConfirmationReceipt
            {
               
                public string CustomerName { get; set; }
                public string PmtMethod { get; set; }
                public string[] ItemRented { get; set; }
                public double Price { get; set; }
                public DateTime DeliveryDate { get; set; }
                public int ReceiptNumber { get; set; }
                public DateTime PurchaseDate { get; set; }

                public ConfirmationReceipt(string customerName, string pmtMethod, string[] itemRented, int price, DateTime deliveryDate, int receiptNumber, DateTime purchaseDate)
                {
                    CustomerName = customerName;
                    PmtMethod = pmtMethod;
                    ItemRented = itemRented;
                    Price = price;
                    DeliveryDate = deliveryDate;
                    ReceiptNumber = receiptNumber;
                    PurchaseDate = purchaseDate;
                }


                public double calTotal()
                {
                    return Price * (1 - 0.15);
                }

                public override string ToString()
                {
                    string items = string.Join(", ", ItemRented);

                    return $" Customer name: {CustomerName} \n Payment method: {PmtMethod} \n Item rented: {items} \n\n Total Amount: ${calTotal()}\n (Discount of 15% Applied) \n\n Approx. Delivery Date: {DeliveryDate} \n Receipt No.: {ReceiptNumber} \n Purchase time: {PurchaseDate}";
                }
            }



        
    }
    
}
