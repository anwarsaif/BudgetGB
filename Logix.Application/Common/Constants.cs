namespace Logix.Application.Common
{
    public static class Languages
    {
        public const int Ar = 1;
        public const int Eng = 2;
    }

    public static class FilesPath
    {
        public const string TempPath = "TempFiles";
        public const string AllFiles = "AllFiles";
        public static readonly string ZacatQrPath = $"AllFiles{Path.DirectorySeparatorChar}QRCode{Path.DirectorySeparatorChar}ZATCA";
        public static readonly string InvoiceQrPath = $"AllFiles{Path.DirectorySeparatorChar}QRCode{Path.DirectorySeparatorChar}Invoice";
    }
    public class NonZeroIfEnabled
    {
        public bool IsEnable { get; set; } = false;
    }
    public static class SessionKeys
    {
        public const string AddTempFiles = "AddTempFiles";
        public const string EditTempFiles = "EditTempFiles";
        public const string TempFilesAdd = "TempFilesAdd";
        public const string AddTempCustomerFiles = "AddTempCustomerFiles";
        public const string EditTempCustomerFiles = "EditTempCustomerFiles";
        public const string AddSalTransactionItems = "AddSalTransactionItems";
        public const string EditSalTransactionItems = "EditSalTransactionItems";
        public const string AddSalTransactionLocation = "AddSalTransactionLocation";
        public const string EditSalTransactionLocation = "EditSalTransactionLocation";
        public const string CopiesSalTransactionLocation = " CopiesSalTransactionLocation";


        public const string EditCustomerContacts = "EditCustomerContacts";

        public const string AddOpmContractItem = "AddOpmContractItem";
        public const string AddOpmContractPurchaseItem = "AddOpmContractPurchaseItem";

        public const string AddOpmContractLocation = "AddOpmContractLocation";
        public const string AddOpmContractPurchaseLocation = "AddOpmContractPurchaseLocation";

        public const string AddContractEmployee = "AddContractEmployee";
        public const string OPMPayrollsearchResult = "OPMPayrollsearchResult";
        public const string AddOPMPayroll = "AddOPMPayroll";
        public const string EditOPMPayroll = "EditOPMPayroll";
        public const string AddOPmPurchasesInvoiceitems = "AddOPmPurchasesInvoiceitems";
        public const string EditOPmPurchasesInvoiceitems = "EditOPmPurchasesInvoiceitems";
        public const string CopiesSalTransactionItems = "CopiesSalTransactionItems";
        public const string InvestEmployee = "InvestEmployee";
        public const string SubitemsDto = "SubitemsDto";
        public const string InitialCreditsAdd = "InitialCreditsAdd";
        public const string InitialCreditsEdit = "InitialCreditsEdit";
        public const string InitialCreditsRepetitiont = "InitialCreditsRepetitiont";
        public const string ReinforcementsAdd = "ReinforcementsAdd";
        public const string ReinforcementsEdit = "ReinforcementsEdit";
        public const string CostsitemsAdd = "CostsitemsAdd";
        public const string CostsitemsEdit = "CostsitemsEdit";
        public const string EmpSupplierAssign = "EmpSupplierAssign";
        public const string EmpSupplierAssignBefore = "EmpSupplierAssignBefore";
        public const string BudgAccountExpensesAdd = "BudgAccountExpensesAdd";
        public const string BudgAccountExpensesEdit = "BudgAccountExpensesEdit";
        public const string AddMultipleLinksDetaile = "AddMultipleLinksDetaile";
        public const string EditMultipleLinksDetaile = "EditMultipleLinksDetaile";
        public const string AddMultiplelinkFinalDetaile = "AddMultiplelinkFinalDetaile";
        public const string EditMultiplelinkFinalDetaile = "EditMultiplelinkFinalDetaile";
        public const string Transfer_to_items = "Transfer_to_items";
        public const string Transfer_From_items = "Transfer_From_items";
        public const string AddSalesReturn = "AddSalesReturn";
        public const string EditSalesReturn = "EditSalesReturn";
        public const string AddSaleOrder = "AddSaleOrder";
        public const string EditSaleOrder = "EditSaleOrder";
    }

    public enum BranchAccountType
    {
        None,
        Sales,
        Purchases,
        ReSales,
        RePurchases,
        Customer,
        Supplier,
        Sales_Discount,
        Purchases_Discount,
        Cost_Goods_Sold,
        Inventory_Transfer_Account
    }



    public enum DataTypeIdEnum
    {
        String = 1,
        Boolean = 2,
        Numeric = 3,
        Date = 4,
        PickList = 5,
        Longstring = 6,
        Title = 7,
        Time = 8,
        File = 9,
        Table = 10,
        Label = 11,
        Link = 12
    }

}
