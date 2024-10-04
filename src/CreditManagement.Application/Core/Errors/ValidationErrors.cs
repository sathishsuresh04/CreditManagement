using CreditManagement.Domain.Primitives;

namespace CreditManagement.Application.Core.Errors;

public static class ValidationErrors
{
    public static class GetInvoiceTotalAmountById
    {
        public static Error InvoiceIdIsRequired => Error.Validation(
            "GetInvoiceTotalAmountById.InvoiceIdIsRequired",
            "Invoice ID is required.");
    }


    public static class CreateInvoice
    {
        public static Error NumberIsRequired => Error.Validation(
            "CreateInvoice.NumberIsRequired",
            "Invoice number is required.");

        public static Error NumberMaxLengthExceeded => Error.Validation(
            "CreateInvoice.NumberMaxLengthExceeded",
            "Invoice number must not exceed 50 characters.");

        public static Error SellerIsRequired => Error.Validation(
            "CreateInvoice.SellerIsRequired",
            "Seller is required.");

        public static Error SellerMaxLengthExceeded => Error.Validation(
            "CreateInvoice.SellerMaxLengthExceeded",
            "Seller name must not exceed 100 characters.");

        public static Error BuyerIsRequired => Error.Validation("CreateInvoice.BuyerIsRequired", "Buyer is required.");

        public static Error BuyerMaxLengthExceeded => Error.Validation(
            "CreateInvoice.BuyerMaxLengthExceeded",
            "Buyer name must not exceed 100 characters.");

        public static Error DescriptionMaxLengthExceeded => Error.Validation(
            "CreateInvoice.DescriptionMaxLengthExceeded",
            "Description must not exceed 500 characters.");

        public static Error IssuedDateCannotBeInFuture => Error.Validation(
            "CreateInvoice.IssuedDateCannotBeInFuture",
            "Issued date must not be in the future.");
    }
}