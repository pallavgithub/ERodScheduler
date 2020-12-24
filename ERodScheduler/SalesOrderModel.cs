using System.Collections.Generic;
using System.Xml.Serialization;

namespace ERodScheduler
{
    [XmlType("GetSOListRs")]
    public class SalesOrdersList
    {
        [XmlElement("SalesOrder")]
        public List<SalesOrder> Orders { get; set; }
    }

    public class SalesOrder
    {
        [XmlElement("ID")]
        public string Id { get; set; }

        [XmlElement("Note")]
        public string Note { get; set; }

        [XmlElement("TotalPrice")]
        public string TotalPrice { get; set; }

        [XmlElement("TotalTax")]

        public string TotalTax { get; set; }

        [XmlElement("EstimatedTax")]

        public string EstimatedTax { get; set; }

        [XmlElement("ItemTotal")]
        public string ItemTotal { get; set; }

        [XmlElement("Salesman")]
        public string Salesman { get; set; }

        [XmlElement("Number")]
        public string Number { get; set; }

        [XmlElement("Status")]
        public string Status { get; set; }

        [XmlElement("Carrier")]
        public string Carrier { get; set; }

        [XmlElement("FirstShipDate")]
        public string FirstShipDate { get; set; }

        [XmlElement("CreatedDate")]
        public string CreatedDate { get; set; }

        [XmlElement("IssuedDate")]
        public string IssuedDate { get; set; }

        [XmlElement("TaxRateName")]
        public string TaxRateName { get; set; }

        [XmlElement("ShippingTerms")]
        public string ShippingTerms { get; set; }

        [XmlElement("PaymentTerms")]
        public string PaymentTerms { get; set; }

        [XmlElement("CustomerContact")]
        public string CustomerContact { get; set; }

        [XmlElement("CustomerName")]
        public string CustomerName { get; set; }

        [XmlElement("CustomerID")]
        public string CustomerId { get; set; }

        [XmlElement("FOB")]
        public string Fob { get; set; }

        [XmlElement("QuickBooksClassName")]
        public string QuickBooksClassName { get; set; }

        [XmlElement("LocationGroup")]
        public string LocationGroup { get; set; }
       
        [XmlElement("PoNum")]
        public string PoNum { get; set; }
       
        [XmlElement("PriorityId")]
        public string PriorityId { get; set; }
        
        [XmlElement("PriceIsHomeCurrency")]
        public bool PriceIsHomeCurrency { get; set; }

        [XmlElement("BillTo")]
        public BillTo BillTo { get; set; }

        [XmlElement("Ship")]
        public BillTo Ship { get; set; }

        [XmlElement("VendorPO")]
        public string VendorPo { get; set; }

        [XmlElement("CustomerPO")]
        public string CustomerPo { get; set; }

        [XmlElement("TypeID")]
        public string TypeId { get; set; }

        [XmlElement("URL")]
        public string Url { get; set; }

        [XmlElement("Cost")]
        public string Cost { get; set; }

        [XmlElement("DateCompleted")]
        public string DateCompleted { get; set; }

        [XmlElement("DateLastModified")]
        public string DateLastModified { get; set; }

        [XmlElement("SalesmanInitials")]
        public string SalesmanInitials { get; set; }

        [XmlElement("CustomFields")]
        public CustomField[] CustomFields { get; set; }

        [XmlElement("Items")]
        public Items Items { get; set; }
    }

    public class BillTo
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("AddressField")]
        public string AddressField { get; set; }

        [XmlElement("City")]
        public string City { get; set; }

        [XmlElement("Zip")]
        public string Zip { get; set; }

        [XmlElement("Country")]
        public string Country { get; set; }

        [XmlElement("State")]
        public string State { get; set; }
    }

    public class CustomField
    {
        [XmlElement("ID")]
        public string Id { get; set; }

        [XmlElement("Type")]
        public string Type { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Description")]
        public string Description { get; set; }

        [XmlElement("SortOrder")]
        public string SortOrder { get; set; }
    }

    public class Items
    {
        [XmlElement("SalesOrderItem")]
        public SalesOrderItem SalesOrderItem { get; set; }
    }

    public class SalesOrderItem
    {
        [XmlElement("ID")]
        public string Id { get; set; }

        [XmlElement("ProductNumber")]
        public string ProductNumber { get; set; }

        [XmlElement("SOID")]
        public string Soid { get; set; }

        [XmlElement("Description")]
        public string Description { get; set; }

        [XmlElement("CustomerPartNum")]
        public string CustomerPartNum { get; set; }

        [XmlElement("Taxable")]
        public bool Taxable { get; set; }

        [XmlElement("Quantity")]
        public string Quantity { get; set; }

        [XmlElement("ProductPrice")]
        public string ProductPrice { get; set; }

        [XmlElement("TotalPrice")]
        public string TotalPrice { get; set; }

        [XmlElement("UOMCode")]
        public string UomCode { get; set; }

        [XmlElement("ItemType")]
        public string ItemType { get; set; }

        [XmlElement("Status")]
        public string Status { get; set; }

        [XmlElement("Note")]
        public string Note { get; set; }

        [XmlElement("QuickBooksClassName")]
        public string QuickBooksClassName { get; set; }

        [XmlElement("NewItemFlag")]
        public bool NewItemFlag { get; set; }
       
        [XmlElement("LineNumber")]
        public string LineNumber { get; set; }
        
        [XmlElement("ShowItemFlag")]
        public bool ShowItemFlag { get; set; }

        [XmlElement("AdjustmentAmount")]
        public string AdjustmentAmount { get; set; }

        [XmlElement("AdjustPercentage")]
        public string AdjustPercentage { get; set; }

        [XmlElement("DateLastFulfillment")]
        public string DateLastFulfillment { get; set; }

        [XmlElement("DateLastModified")]
        public string DateLastModified { get; set; }

        [XmlElement("DateScheduledFulfillment")]
        public string DateScheduledFulfillment { get; set; }

        [XmlElement("QtyFulfilled")]
        public string QtyFulfilled { get; set; }

        [XmlElement("QtyPicked")]
        public string QtyPicked { get; set; }

        [XmlElement("RevisionLevel")]
        public string RevisionLevel { get; set; }

        [XmlElement("TotalCost")]
        public string TotalCost { get; set; }
    }

}
