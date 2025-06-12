using CleanAspire.Domain.Common;

namespace CleanAspire.Domain.Entities;
public class SalesOrder : BaseAuditableEntity, IAuditTrial
{
    /// <summary>
    /// Gets or sets the customer ID associated with the sales order.
    /// </summary>
    public string? CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the total amount of the sales order.
    /// </summary>
    public decimal TotalAmount { get; set; }
    /// <summary>
    /// Gets or sets the status of the sales order.
    /// </summary>
    public string Status { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the date when the sales order was placed.
    /// </summary>
    public DateTime OrderDate { get; set; }


    public int Quantity { get; set; }


    /// <summary>
    /// Gets or sets the customer associated with the sales order.
    /// </summary>
    public virtual Customer? Customer { get; set; }
}
