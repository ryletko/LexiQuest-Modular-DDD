namespace LexiQuest.Framework.Application.Errors;

public class PermissionAction
{
    private PermissionAction(string value) { Value = value; }

    public string Value { get; private set; }

    public static PermissionAction Read => new("read");
    public static PermissionAction Add => new("add");
    public static PermissionAction Edit => new("edit");
    public static PermissionAction Delete => new("delete");

    public override string ToString()
    {
        return Value;
    }
}
public class AccessDeniedException(string userId, string resource, PermissionAction permissionAction): Exception($"User {userId} is not allowed to {permissionAction} {resource}") 
{
    
}