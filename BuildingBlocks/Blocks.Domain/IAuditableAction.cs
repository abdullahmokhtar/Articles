namespace Blocks.Domain;

public interface IAuditableAction
{
    public DateTime CreatedOn => DateTime.UtcNow;
    public int CreatedBy { get; set; }
}

public interface IAuditableAction<TActionType> : IAuditableAction 
    where TActionType : Enum
{
    TActionType ActionType { get; }
}
